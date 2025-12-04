using OpenQA.Selenium;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.UIExtensions;
using QueryUi_E2E_Maps.Panels;

namespace QueryUi_E2E_Maps.Pages
{
    public class TradeFormPage : PageBase
    {     
        public TradeFormPage(IWebDriverProvider webDriverProvider) : base("trade-form", webDriverProvider, By.TagName("app-trade-form"))
        {
            
        }

        public bool AreSummaryFieldsLocked()
        {
            return this.SummaryPanel.GetAllControls().AllDisabled();
        }

        public Button SaveButton { get; set; }
        public Button CloseButton { get; set; }


        protected override void InitialiseOwnControls()
        {
            this.SaveButton = new Button("save button", By.XPath("save-button"), By.XPath("footer"), WebDriverProvider);
            this.CloseButton = new Button("close button", By.XPath("//div[contains(@class, 'icon-btn icon-Close')]"), By.XPath("//div[contains(@class, 'control-pannel')]"), WebDriverProvider);
            
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.CloseButton);
        }

        public SummaryPanel SummaryPanel { get; private set; }
        public UnsavedChangesPanel UnsavedChangesPanel { get; private set; }
        public TradeAmendmentPanel TradeAmendmentPanel { get; private set; }
        public DeliveryPanel DeliveryPanel { get; private set; }
        public PricingPanel PricingPanel { get; private set; }
        public PaymentPanel PaymentPanel { get; private set; }
        public CommodityDetailsPanel CommodityDetailsPanel { get; private set; }
        public SupplementaryDataPanel SupplementaryDataPanel { get; private set; }

        protected override void InitialisePanels()
        {
            this.SummaryPanel = this.InitializeAndRegister(new SummaryPanel(WebDriverProvider, this.RelativeLocator));
            
            this.UnsavedChangesPanel = this.InitializeAndRegister(new UnsavedChangesPanel(WebDriverProvider, this.RelativeLocator));            

            this.TradeAmendmentPanel = this.InitializeAndRegister(new TradeAmendmentPanel(WebDriverProvider, this.RelativeLocator));            

            this.DeliveryPanel = this.InitializeAndRegister(new DeliveryPanel(WebDriverProvider, this.RelativeLocator));            

            this.PricingPanel = this.InitializeAndRegister(new PricingPanel(WebDriverProvider, this.RelativeLocator));            

            this.PaymentPanel = this.InitializeAndRegister(new PaymentPanel(WebDriverProvider, this.RelativeLocator));            

            this.CommodityDetailsPanel = this.InitializeAndRegister(new CommodityDetailsPanel(WebDriverProvider, this.RelativeLocator));            

            this.SupplementaryDataPanel = this.InitializeAndRegister(new SupplementaryDataPanel(WebDriverProvider, this.RelativeLocator));             
        }

        protected override void InitialiseModals()
        {

        }
    }
}
