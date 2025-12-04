using OpenQA.Selenium;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.UIExtensions;
using static OilHub_Core.ControlAbstractions.ControlBase;

namespace QueryUi_E2E_Maps.Panels
{
    public class DeliveryPanel : PanelBase
    {
        private static readonly string PANEL_NAME = "delivery";
        private static readonly string DELIVERY_PANEL_XPATH = "//app-delivery";
        private static readonly By PANEL_LOCATOR = By.XPath(DELIVERY_PANEL_XPATH);

        public DeliveryPanel(IWebDriverProvider webDriverProvider, By ancestorLocator) : base(PANEL_NAME, PANEL_LOCATOR, ancestorLocator, webDriverProvider)
        {
            AncestorLocator = ancestorLocator;
        }

        public Dropdown TradeSubType { get; private set; }
        public SegmentedButton MethodOfTransport { get; private set; }  
        public Checkbox MoTOptionality { get; private set; }

        public By AncestorLocator { get; }

        protected override void InitialiseControls()
        {
            TradeSubType = new Dropdown("trade-sub-type", WithFormControlName("tradeSubtype"), PANEL_LOCATOR, WebDriverProvider);            

            MethodOfTransport = new SegmentedButton("method-of-transport", WithFormControlName("methodOfTransport"), PANEL_LOCATOR, WebDriverProvider);

            MoTOptionality = new Checkbox("mot-optionality", WithFormControlName("motOptionality"), AncestorLocator, WebDriverProvider);
            
            
            
            Controls.Add(TradeSubType);
            Controls.Add(MethodOfTransport);
            Controls.Add(MoTOptionality);
        }


    }
}
