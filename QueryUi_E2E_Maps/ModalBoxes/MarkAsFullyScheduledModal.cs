using OpenQA.Selenium;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.UIExtensions;

namespace QueryUi_E2E_Maps.ModalBoxes
{
    public class MarkAsFullyScheduledModal : ModalBoxBase
    {
        private static readonly By FULLY_SCHEDULED_MODAL_LOCATOR = By.XPath("//app-update-fully-scheduled-modal");
        public MarkAsFullyScheduledModal(IWebDriverProvider webDriverProvider) : base("fully scheduled", webDriverProvider, FULLY_SCHEDULED_MODAL_LOCATOR)
        {

        }

        public Button Continue { get; set; }
            
        protected override void InitialiseOwnControls()
        {
            Continue = RegisterControl(new Button("continue", By.XPath("//button[normalize-space(text()) = 'Continue']"), FULLY_SCHEDULED_MODAL_LOCATOR, WebDriverProvider));

        }
    }
}
