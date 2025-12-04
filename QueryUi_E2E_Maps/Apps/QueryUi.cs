using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.UIExtensions;
using QueryUi_E2E_Maps.Pages;

namespace QueryUi_E2E_Maps.Apps
{
    public class QueryUi : AppBase
    {
        public QueryUi(IWebDriverProvider webDriverProvider) : base("Query UI", webDriverProvider)
        {
        }

        public TradeFormPage TradeOverviewPage { get; private set; }

        public InvoicesPage TradeListPage { get; private set; }

        public HomePage HomePage { get; private set; }

        public MovementPlannerPage PhysOpsManagerPage { get; private set; }

        public QueryCostsPage QueryCostsPage { get; private set; }

        public QuerySwapsPage QuerySwapsPage { get; private set; }

        public override void InitializePages()
        {
            TradeOverviewPage = InitializeAndRegister(new TradeFormPage(WebDriverProvider));

            TradeListPage = InitializeAndRegister(new InvoicesPage(WebDriverProvider));

            HomePage = InitializeAndRegister(new HomePage(WebDriverProvider));

            PhysOpsManagerPage = InitializeAndRegister(new MovementPlannerPage(WebDriverProvider));

            QueryCostsPage = InitializeAndRegister(new QueryCostsPage(WebDriverProvider));

            QuerySwapsPage = InitializeAndRegister(new QuerySwapsPage(WebDriverProvider));
        }
    }
}
