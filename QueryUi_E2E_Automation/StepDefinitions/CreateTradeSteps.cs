
using OilHub_Core.Utility.APIExtensions.Transforms;
using System.Net;
using OilHub_Core;
using static OilHub_Core.CommonSteps.StepArgUtils;
using System.Diagnostics;
using OilHub_Core.Configuration;
using Glencore.OilHub.Common.Core.Logic;
using OilHub_Core.Utility.APIExtensions.Model;
using OilHub_Core.MessagePublisher;

namespace QueryUi_E2E_Automation.StepDefinitions
{
    [Binding]
    public class CreateTradeSteps
    {
        private readonly IApiClient _apiClient;
        private readonly ITestDataContext _testDataContext;        
        private readonly ITransforms _transforms;
        private readonly TestDataLocator _testDataLocator;
        private readonly IMessagePublisher _messagePublisher;
        public static string TRADE_TYPE_COLUMN_NAME => TableExtensions.TRADE_TYPE_COLUMN_NAME;
        private List<TradeContent> _response;

        private readonly AppConfiguration _configuration;

        public CreateTradeSteps( ITestDataContext testDataContext, IApiClient apiClient, ITransforms transforms, TestDataLocator testDataLocator, IMessagePublisher messagePublisher)
        {
            this._testDataContext = testDataContext;
            this._apiClient = apiClient;            
            this._transforms = transforms;
            this._testDataLocator = testDataLocator;
            this._messagePublisher = messagePublisher;
        }


        [Given(@"I have a Trade")]
        public async Task GivenIHaveAExternalTrade(Table tradeContent)
        //{
        //    foreach (var row in tradeContent.Rows)
        //    {
        //        var trade = _transforms.CreatePostTradeDto(row.GetTradeType(), row.ToDictionary(excludeColumns: new[] { TRADE_TYPE_COLUMN_NAME }));
        //        _testDataContext.AddOrReplace("Trade", trade);
        //        _response = await  _apiClient.PostApiAsync<List<TradeContent>>
        //            (EndPoints.PostPhysicalTrade, trade, HttpStatusCode.OK, "Trader");
        //        _testDataContext.AddOrReplace("Trade", _response[0]);
        //    }
        //}
        {
            foreach (var row in tradeContent.Rows)
            {
                var trade = _transforms.CreatePostTradeDto(row.GetTradeType(), row.ToDictionary(excludeColumns: new[] { TRADE_TYPE_COLUMN_NAME }));
                _testDataContext.AddOrReplace("Trade", trade);
            }
            var response = await _apiClient.PostApiAsync<List<TradeContent>>
                    (EndPoints.PostPhysicalTrade, _testDataContext.Get<PostTradeDto>("Trade"), HttpStatusCode.OK, "Trader");
            _testDataContext.AddOrReplace("Trade", response);

            Console.WriteLine("Trade ID is " + response[0].HeaderNumber);
        }


        [Given(@"I Enrich Trade")]
        public async Task GivenIEnrichTrade(Table tradeContent)
        {
            var trade = _testDataContext.Get<TradeContent>("Trade");

            var workflowId = await GetWorkflowId(trade, "RiskManager", "risk-manager");
            trade.ContractFields.WorkflowStatus = "Trader Amendment: W/ Risk";
            trade.TradeFields.TradeStatus = "New Trade";


            foreach (var row in tradeContent.Rows)
            {
                trade =
                    _transforms.UpdatePutTradeDto
                    (row.GetTradeType(), row.ToDictionary(excludeColumns: new[] { TRADE_TYPE_COLUMN_NAME }),trade);
               
                var putTradeDto = 
                    ConvertToPutTradeDTOwithWorkflowId(trade, "Test Automation", "WorkflowProcess", "RiskManager", workflowId);
                var url = $"{string.Format(EndPoints.UpdateWorkflowAllocation, workflowId)}";
                var response = 
                    await _apiClient.PutApiAsync<List<TradeContent>>(url, putTradeDto, HttpStatusCode.OK, "risk-manager"); 
            }

        }
      
        [Given(@"Publish ActiveMQ Message")]
        public void GivenPublishMessage()
        {
            _messagePublisher.SendMessage(new PublisherMessage
            {
                ActiveMqQueue = "Consumer.TCTest.VirtualTopic.TradeCapture",
                Messages = new[] { "{\"test\":123}" }
            });

        }

       

        [Given(@"Trade has a (.*) Lock")]
        public async Task GivenTradeHasAPlannedLock(string lockType)
        {
            var trade = _testDataContext.Get<TradeContent>("Trade");
            var tradeLockDto = CreateTradeLockDTO(trade, lockType);
            var response = await _apiClient.PutApiAsync<BusinessLogicResultDto>(EndPoints.PutTradeLock, tradeLockDto, HttpStatusCode.OK, "Trader");
            response.Errors.Should().BeNull();
        }

        [Given(@"Trade has a (.*) Lock on ((?:\d+)(?:st|nd|rd|th)) leg")]
        public async Task GivenTradeHasAPlannedLockOnLeg(string lockType, string legNo)
        {
            _testDataContext.AddOrReplace("Trade", _response[RowIndexFrom(legNo)]);
            var trade = _testDataContext.Get<TradeContent>("Trade");
            var tradeLockDto = CreateTradeLockDTO(trade, lockType);
            var response = await _apiClient.PutApiAsync<BusinessLogicResultDto>(EndPoints.PutTradeLock, tradeLockDto, HttpStatusCode.OK, "Trader");
            response.Errors.Should().BeNull();
        }

        [Given(@"I have a (.*) Trade entered with (.*) account")]
        public void GivenIHaveXTradeEnteredWithYAccount(string tradeType, string apiTradeAccount)
        {
            if (tradeType == "External")
            {
                var jObject = JObject.Parse(File.ReadAllText(_testDataLocator.GetTestDataFilePath("ExternalTrade.json")));
                var trade = jObject.ToObject<PostTradeDto>();
                _testDataContext.AddOrReplace("Trade", trade);
                var response = _apiClient.PostApiAsync<List<TradeContent>>
                    (EndPoints.PostPhysicalTrade, trade, HttpStatusCode.OK, apiTradeAccount);
                _testDataContext.AddOrReplace("Trade", response.Result[0]);
            }
        }

        [When(@"I retrieve Trade")]
        public async Task WhenIRetrieveTrade()
        {
            var trade = _testDataContext.Get<TradeContent>("Trade");
            var url = $"{string.Format(EndPoints.GetPhysicalTrade,trade.HeaderNumber)}";
            var response = await _apiClient.GetApiAsync<List<TradeContent>>(url, "Trader");
            _testDataContext.AddOrReplace("Trade", response);
        }

        [When(@"I retrieve Trade with (.*) account")]
        public void WhenIRetrieveTradeWithXAccount(string apiTradeAccount)
        {
            var trade = _testDataContext.Get<TradeContent>("Trade");
            var url = $"{string.Format(EndPoints.GetPhysicalTrade, trade.HeaderNumber)}";
            var response = _apiClient.GetApiAsync<List<TradeContent>>(url, apiTradeAccount);
        }


        [When(@"I update (.*) Trade")]
        public void WhenIUpdateExternalTrade(string tradeType)
        {
            var trade = _testDataContext.Get<TradeContent>("Trade");
            trade.ContractFields.TraderInitials = "AKP";
            var url = EndPoints.PutPhysicalTrade;
            var putTradeDto = ConvertToPutTradeDTO(trade, "Test Automation", "Amendment", "Trader");
            var response = _apiClient.PutApiAsync<List<TradeContent>>(url, putTradeDto, HttpStatusCode.OK,"Trader");
            response.Result[0].ContractFields.TraderInitials.Should().Be("AKP");
        }

        [When(@"I update (.*) Trade with (.*) account")]
        public void WhenIUpdateXTradeWithYAccount(string tradeType, string apiTradeAccount)
        {
            var trade = _testDataContext.Get<TradeContent>("Trade");
            trade.ContractFields.TraderInitials = "AKP";
            var url = EndPoints.PutPhysicalTrade;
            var putTradeDto = ConvertToPutTradeDTO(trade, "Test Automation", "Amendment", "Trader");
            var response = _apiClient.PutApiAsync<List<TradeContent>>(url, putTradeDto, HttpStatusCode.OK, apiTradeAccount);
            response.Result[0].ContractFields.TraderInitials.Should().Be("AKP");
        }

       
        private async Task<int> GetWorkflowId(TradeContent trade, string role, string profile)
        {
            var index = new WorkflowAllocationDto();

            var response = new List<WorkflowAllocationDto>();
            RetrieveWorkflowAllocationsDto retrieveWorkflowAllocationsDto = new RetrieveWorkflowAllocationsDto()
            {
                Types = new List<string> { role },
                OperatingDesks = new List<string> {trade.Header.OperatingDesk}
            };
            for (int i = 0; i < 100; i++)
            {
                 response = await _apiClient.PostApiAsync<List<WorkflowAllocationDto>>(EndPoints.GetWorkFlowAllocationsForType,
                    retrieveWorkflowAllocationsDto, HttpStatusCode.OK, profile);
                index = response.Find(f => f.EntityId == trade.Header.HeaderNumber);
                if (index != null) { break; }
                else { Thread.Sleep(3000); }
            }
            return index.WorkflowAllocationId;
        }
        private PutTradeDto ConvertToPutTradeDTO(TradeContent trade, string comment, string operation, string role)
        {
            PutTradeDto putTradeDto = new PutTradeDto()
            {
                Trades = new TradeDto()
                {
                    Content = new List<TradeContent> { trade },
                },
                UpdateComment = comment,
                UpdateOperation = operation,
                UpdateRole = role

            };
            return putTradeDto;
        }
		private PutTradeDto ConvertToPutTradeDTOwithWorkflowId(TradeContent trade, string comment, string operation, string role, int workFlowallocationId)
        {
            PutTradeDto putTradeDto = new PutTradeDto()
            {
                Trades = new TradeDto()
                {
                    Content = new List<TradeContent> { trade },
                    WorkflowAllocationId = workFlowallocationId
                },
                UpdateComment = comment,
                UpdateOperation = operation,
                UpdateRole = role

            };
            return putTradeDto;
        }

        private TradeLockDto CreateTradeLockDTO(TradeContent trade, string lockType)
        {
            TradeLockDto tradeLockDto = new TradeLockDto();
            tradeLockDto.TradeNumber = (int)trade.HeaderNumber;
            tradeLockDto.ParcelState = lockType;
            return tradeLockDto;
        }

    }
}
