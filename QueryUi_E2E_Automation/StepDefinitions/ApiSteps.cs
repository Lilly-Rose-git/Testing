
using OilHub_Core.Utility.APIExtensions.Transforms;
using OilHub_Core.CommonSteps;

namespace TC_E2E_Automation.StepDefinitions
{
    [Binding]
    public class ApiSteps
    {
        private readonly IApiClient _apiClient;
        private readonly ITestDataContext _testDataContext;        
        private readonly ITransforms _transforms;
        private readonly TestDataLocator _testDataLocator;
        
        private TraderOfficeDTO _traderOfficeResponse;
        private string _endpoint;

        public ApiSteps( ITestDataContext testDataContext, IApiClient apiClient, ITransforms transforms, TestDataLocator testDataLocator)
        {
            this._testDataContext = testDataContext;
            this._apiClient = apiClient;            
            this._transforms = transforms;
            this._testDataLocator = testDataLocator;
        }

        [When(@"user retrieves ""(.*)""")]
        public async Task WhenUserExecutesGETEndpointWithTraderIntials(string resource,Table parameterTable)
        {
            List<string> paramValues = new List<string>();
            foreach (var row in parameterTable.Rows)
            {
                paramValues.Add(row["ParameterValue"]);
            }
            var url = $"{string.Format(EndPoints.GetOfficeForTrader, paramValues[0])}";
            var traderOfficeResponse = await _apiClient.GetApiAsync<TraderOfficeDTO>(url, "Trader");
            _testDataContext.AddOrReplace("Trader", traderOfficeResponse);
        }

        [Then(@"user should see following results in ""(.*)"" response")]
        public void ThenUserShouldSeeFollowingResults(string resource, string fields)
        {
            List<string> fieldList = StepArgUtils.SplitListString(fields).ToList<string>();
            if (resource.Equals("TraderOfficeDetails"))
            {
                var traderOfficeResponse = _testDataContext.Get<TraderOfficeDTO>("Trader");
                traderOfficeResponse.OfficeCode.Should().Be(fieldList[0]);
                traderOfficeResponse.OperatingDesk.Should().Be(fieldList[1]);

            }
            else if (resource.Equals("Trade"))
            {
                var trade = _testDataContext.Get<List<TradeContent>>("Trade");
                trade[0].TransferPricing.MarginComment.Should().Be(fieldList[0]);
                trade[0].TransferPricing.MarginPct.Should().Be(Decimal.Parse(fieldList[1]));
            }
            else {
                throw new Exception("$\"{resource} not found..! ");
            }



        }

      
}
}
