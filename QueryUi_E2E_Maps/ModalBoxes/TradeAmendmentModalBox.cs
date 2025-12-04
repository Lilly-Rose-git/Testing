using OpenQA.Selenium;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.UIExtensions;

namespace QueryUi_E2E_Maps.ModalBoxes
{
    public class TradeAmendmentModalBox : ModalBoxBase
    {
        private static readonly By TRADE_AMENDMENT_MODAL_LOCATOR = By.XPath("//div[contains(@class, 'amend-modal')]");
        public TradeAmendmentModalBox(IWebDriverProvider webDriverProvider) : base("trade-amendment-modal", webDriverProvider, TRADE_AMENDMENT_MODAL_LOCATOR)
        {

        }

        public Button TradeAmendmentSave { get; set; }
        public TextInputArea TradeAmendmentComment { get; set; }

        protected override void InitialiseOwnControls()
        {            
            TradeAmendmentSave = RegisterControl(new Button("trade-amendment-save", By.XPath(".//button[contains(@class, 'btn primary')]"), TRADE_AMENDMENT_MODAL_LOCATOR, WebDriverProvider));

            TradeAmendmentComment = RegisterControl(new TextInputArea("trade-amendment-comment", By.XPath(".//label[.//textarea[@id='reason']]"), TRADE_AMENDMENT_MODAL_LOCATOR, WebDriverProvider));            
        }
    }
}
