using OpenQA.Selenium;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.UIExtensions;
using static OilHub_Core.ControlAbstractions.ControlBase;

namespace QueryUi_E2E_Maps.Panels
{
    public class SummaryPanel : PanelBase
    {        
        private static readonly string PANEL_NAME = "summary";
        private static readonly string SUMMARY_PANEL_XPATH = "//app-summary";
        private static readonly By PANEL_LOCATOR = By.XPath(SUMMARY_PANEL_XPATH);

        public SummaryPanel(IWebDriverProvider webDriverProvider, By ancestorLocator) : base(PANEL_NAME, PANEL_LOCATOR, ancestorLocator, webDriverProvider)
        {
            AncestorLocator = ancestorLocator;
        }

        public TextInputBox QuantityInput { get; private set; }

        public TextInputBox FooInput { get; private set; }
        public RadioField PerDay { get; private set; }
        public By AncestorLocator { get; }

        protected override void InitialiseControls()
        {
            QuantityInput = new TextInputBox("quantity", WithFormControlName("quantity"), PANEL_LOCATOR, WebDriverProvider);
            PerDay = new RadioField("per-day", WithFormControlName("dayOrDelivery"), PANEL_LOCATOR, WebDriverProvider);

            this.Controls.Add(QuantityInput);
            this.Controls.Add(PerDay);
        }

       
    }
}
