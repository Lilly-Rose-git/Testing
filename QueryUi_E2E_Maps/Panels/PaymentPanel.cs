using OpenQA.Selenium;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.UIExtensions;
using static OilHub_Core.ControlAbstractions.ControlBase;

namespace QueryUi_E2E_Maps.Panels
{
    public class PaymentPanel : PanelBase
    {
        private static readonly string PANEL_NAME = "payment";
        private static readonly string PAYMENT_PANEL_XPATH = "//app-payment";
        private static readonly By PANEL_LOCATOR = By.XPath(PAYMENT_PANEL_XPATH);

        public PaymentPanel(IWebDriverProvider webDriverProvider, By ancestorLocator) : base(PANEL_NAME, PANEL_LOCATOR, ancestorLocator, webDriverProvider)
        {
            AncestorLocator = ancestorLocator;
        }

        public DateInputBox PaymentDueDate;

        public By AncestorLocator { get; }

        protected override void InitialiseControls()
        {
            PaymentDueDate = new DateInputBox("payment-due-date", ControlBase.WithFormControlName("paymentDueDate"), PANEL_LOCATOR, WebDriverProvider);

            Controls.Add(PaymentDueDate);

        }


    }
}
