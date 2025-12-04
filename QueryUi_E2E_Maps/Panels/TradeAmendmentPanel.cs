using OpenQA.Selenium;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.UIExtensions;

namespace QueryUi_E2E_Maps.Panels
{
    public class TradeAmendmentPanel : PanelBase
    {
        private static readonly string PANEL_NAME = "trade amendment";
        private static readonly string TRADE_AMENDMENT_PANEL_XPATH = "//div[contains(text(), 'Trade Amendment')]//ancestor::div[contains(@class, 'dx-overlay-wrapper')]";
        private static readonly By PANEL_LOCATOR = By.XPath(TRADE_AMENDMENT_PANEL_XPATH);

        public TradeAmendmentPanel(IWebDriverProvider webDriverProvider, By ancestorLocator) : base(PANEL_NAME, PANEL_LOCATOR, ancestorLocator, webDriverProvider)
        {
            AncestorLocator = ancestorLocator;
        }

        public By AncestorLocator { get; }

        public Button TradeAmendmentSave { get; set; }

        protected override void InitialiseControls()
        {
            this.TradeAmendmentSave = new Button("trade amendment save", By.XPath("//button[contains(text(), 'Save')]"), By.XPath(TRADE_AMENDMENT_PANEL_XPATH), WebDriverProvider);

            this.Controls.Add(TradeAmendmentSave);
        }


    }
}
