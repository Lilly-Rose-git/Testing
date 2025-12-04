using OpenQA.Selenium;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.UIExtensions;

namespace QueryUi_E2E_Maps.Panels
{
    public class UnsavedChangesPanel : PanelBase
    {
        private static readonly string PANEL_NAME = "save changes";
        private static readonly string UNSAVED_CHANGES_PANEL_XPATH = "//div/app-modal-window-content";
        private static readonly By PANEL_LOCATOR = By.XPath(UNSAVED_CHANGES_PANEL_XPATH);

        public UnsavedChangesPanel(IWebDriverProvider webDriverProvider, By ancestorLocator) : base(PANEL_NAME, PANEL_LOCATOR, ancestorLocator, webDriverProvider)
        {
            AncestorLocator = ancestorLocator;
        }

        public By AncestorLocator { get; }

        public Button SaveChanges { get; set; }
        public Button DiscardAndContinue { get; set; }


        protected override void InitialiseControls()
        {
            SaveChanges = new Button("save changes", By.XPath("//button[contains(text(), 'Save Changes')]"), By.XPath("//div[contains(@class, 'form-footer')]"), WebDriverProvider);
            DiscardAndContinue = new Button("discard changes", By.XPath("//button[contains(text(), 'Discard & Continue')]"), By.XPath("//div[contains(@class, 'form-footer')]"), WebDriverProvider);

            Controls.Add(SaveChanges);
            Controls.Add(DiscardAndContinue);
        }


    }
}
