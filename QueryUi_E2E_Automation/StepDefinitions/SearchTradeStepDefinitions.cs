using QueryUi_E2E_Automation.StepDefinitions;
using OilHub_Core.Configuration;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.UIExtensions;
using static OilHub_Core.CommonSteps.StepArgUtils;
using QueryUi_E2E_Maps.Pages;
using QueryUi_E2E_Automation.StepDataTypes;
using TechTalk.SpecFlow.Assist;
using OilHub_Core.ControlAbstractions.Grids;
using OilHub_Core.Utility;
using OilHub_Core.ControlAbstractions.Search;

namespace QueryUi_E2E_Automation.StepDefinitions
{
    [Binding]
    public class SearchTradeStepDefintions
    {

        private readonly ITestDataContext _testDataContext;
        private readonly IWebDriverProvider _driverProvider;
        private readonly IApp _app;

        private readonly OpenFin _openFin;
        private readonly Helpers _helpers;
        private readonly AppConfiguration _configuration;

        public SearchTradeStepDefintions(ITestDataContext testDataContext, IWebDriverProvider driverProvider, IApp app,
            Helpers helpers, AppConfiguration configuration, OpenFin openFin)
        {
            this._testDataContext = testDataContext;
            this._driverProvider = driverProvider;
            this._app = app;
            this._helpers = helpers;
            this._configuration = configuration;
            this._openFin = openFin;
        }
        public IPillSearchBar GetSearchBar(string? name = null)
        {
            return name is null ? _app.GetCurrentPage().GetSpecialControl<IPillSearchBar>() : _app.GetCurrentPage().GetSpecialControl<IPillSearchBar>();
        }

        [Then(@"I wait for 5 seconds")]
        public void WhenIWaitForFiveSeconds()
        {
            Thread.Sleep(5000);
        }

        private static readonly IEnumerable<char> PILL_PARAM_SPLIT_CHARS = GetDefaultSplitChars().Concat(new[] { '|' });

        [When("I add the search pill in search bar: ([^\\|]*) \\| ([^\\|]*) \\| ([^\\|]*)")]
        public void WhenIAddTheSearchPill(string fieldName, string comparison, string? parameters)
        {
            Thread.Sleep(500);
            GetSearchBar().ClearSearchPills();
            GetSearchBar().AddPill(fieldName, comparison, SplitListString(parameters, PILL_PARAM_SPLIT_CHARS).ToArray());
        }

        private IPage GetCurrentPage()
        {
            return this._app.GetCurrentPage();
        }

    }
}