

using OpenQA.Selenium;
using OilHub_Core.ControlAbstractions.Grids;
using OilHub_Core.ControlAbstractions.Search;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.UIExtensions;
using QueryUi_E2E_Maps.Grids;
using QueryUi_E2E_Maps.Panels;

namespace QueryUi_E2E_Maps.Pages
{
    public class HomePage : PageBase
    {
        public Button PhysicalOperationsButton { get; private set; }
        public Button Costs { get; private set; }
        public Button QueryCosts { get; private set; }
        public Button Trades { get; private set; }
        public Button TradesQuerySwaps { get; private set; }


        public HomePage(IWebDriverProvider driverProvider) : base("home-page", "Home", driverProvider, By.TagName("app-root"))
        {

        }

        protected override void InitialiseOwnControls()
        {
            //@TODO come up with more generic xPath for PhysicalOperationsButton
            this.PhysicalOperationsButton = new Button("physical-operations", By.XPath("//div[@id = 'menuItem2']"), By.XPath("//gl-side-menu"), WebDriverProvider);
            this.Controls.Add(PhysicalOperationsButton);

            this.Costs = new Button("costs", By.XPath("//div[@id = 'menuItem1']"), By.XPath("//gl-side-menu"), WebDriverProvider);
            this.Controls.Add(Costs);

            this.Trades = new Button("trades", By.XPath("//div[@id = 'menuItem0']"), By.XPath("//gl-side-menu"), WebDriverProvider);
            this.Controls.Add(Trades);


            //this.QueryCosts = new Button("query-costs", By.XPath("//div/div/div[2]/a"), By.XPath("gl-slide-out-menu"), WebDriverProvider);
            this.QueryCosts = new Button("query-costs", By.XPath("//span[text()='Costs']"), By.XPath("//gl-slide-out-menu"), WebDriverProvider);
            this.Controls.Add(QueryCosts);

            this.TradesQuerySwaps = new Button("query-swaps", By.XPath("//span[text()='Swaps']"), By.XPath("//gl-slide-out-menu"), WebDriverProvider);
            this.Controls.Add(TradesQuerySwaps);

        }

        public PhysicalOperationsPanel PhysicalOperationsPanel { get; private set; }

        protected override void InitialisePanels()
        {
            this.PhysicalOperationsPanel = this.InitializeAndRegister(new PhysicalOperationsPanel(WebDriverProvider, this.RelativeLocator));

        }

        protected override void InitialiseSpecialControls()
        {

        }

        protected override void InitialiseModals()
        {

        }
    }
}
