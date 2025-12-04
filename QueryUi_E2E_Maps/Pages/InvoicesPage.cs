using Microsoft.Win32;
using OpenQA.Selenium;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.ControlAbstractions.Grids;
using OilHub_Core.ControlAbstractions.Search;
using OilHub_Core.Utility.UIExtensions;
using QueryUi_E2E_Maps.Grids;

namespace QueryUi_E2E_Maps.Pages
{
    public class InvoicesPage : PageBase
    {

        public IGrid SearchResultsGrid { get; private set; }
        public IPillSearchBar SearchBar { get; private set; }
        public Button ReviewQueueButton { get; private set; }
        public Button ReadyForInvoicingButton { get; private set; }
        public Button WatchListButton { get; private set; } 
        public Button ConvertToPrepaymentButton { get; private set; }

        public InvoicesPage(IWebDriverProvider driverProvider) : base("invoices", driverProvider, By.TagName("app-root-container"))
        {
            
        }

        protected override void InitialiseOwnControls()
        {
            this.ReviewQueueButton = new Button("review queue button", By.XPath("//button/span[text()='Review Queue ']/.."), By.XPath("//gl-tabs"), WebDriverProvider);
            this.ReadyForInvoicingButton = new Button("ready for invoicing button", By.XPath("//button/span[text()='Ready for Invoicing']/.."), By.XPath("//gl-tabs"), WebDriverProvider);
            this.WatchListButton = new Button("watch list button", By.XPath("//button/span[text()='Watch List']/.."), By.XPath("//gl-tabs"), WebDriverProvider);
            this.ConvertToPrepaymentButton = new Button("convert pp button", By.XPath("//div/button[normalize-space(text())='Convert to Prepayment & Continue']"), By.XPath("//gl-tabs"), WebDriverProvider);

            this.Controls.Add(this.ReviewQueueButton);
            this.Controls.Add(this.ReadyForInvoicingButton);
            this.Controls.Add(this.WatchListButton);
            this.Controls.Add(ConvertToPrepaymentButton);
        }

        protected override void InitialisePanels()
        {
            
        }

        protected override void InitialiseSpecialControls()
        {
            var invoicesGrid = new TradeListGrid(this.RelativeLocator, WebDriverProvider);
            invoicesGrid.Initialize();
            this.SearchResultsGrid = RegisterSpecialControl(invoicesGrid);
            this.SearchBar = RegisterSpecialControl(PillSearchBar.CreateDefault(this.RelativeLocator, WebDriverProvider));
        }

        protected override void InitialiseModals()
        {

        }
    }
}
