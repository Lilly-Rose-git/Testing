using OpenQA.Selenium;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.UIExtensions;

namespace QueryUi_E2E_Maps.ModalBoxes
{
    public class UnsavedChangesModalBox : ModalBoxBase
    {
        private static readonly By UNSAVED_CAHNGES_MODAL_LOCATOR = By.XPath("//div[@class='dx-popup-content']");

        public UnsavedChangesModalBox(IWebDriverProvider webDriverProvider) : base("unsaved-changes-modal", webDriverProvider, UNSAVED_CAHNGES_MODAL_LOCATOR)
        {

        }

        public By AncestorLocator { get; }

        public Button SaveChanges { get; set; }
        public Button DiscardAndContinue { get; set; }


        protected override void InitialiseOwnControls()
        {
            SaveChanges = RegisterControl(new Button("save changes", By.XPath("//button[contains(text(), 'Save Changes')]"), Location.AncestorLocator, WebDriverProvider));
           
            DiscardAndContinue = RegisterControl(new Button("discard changes", By.XPath("//button[contains(text(), 'Discard & Continue')]"), Location.AncestorLocator, WebDriverProvider));

        }


    }
}
