using OpenQA.Selenium;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.UIExtensions;
using static OilHub_Core.ControlAbstractions.ControlBase;

namespace QueryUi_E2E_Maps.Panels
{
    public class SupplementaryDataPanel : PanelBase
    {
        private static readonly string PANEL_NAME = "supplementary-panel";
        private static readonly string SUPPLEMENTARY_DATA_PANEL_XPATH = "//app-supplementary-one";
        private static readonly By PANEL_LOCATOR = By.XPath(SUPPLEMENTARY_DATA_PANEL_XPATH);

        public SupplementaryDataPanel(IWebDriverProvider webDriverProvider, By ancestorLocator) : base(PANEL_NAME, PANEL_LOCATOR, ancestorLocator, webDriverProvider)
        {
            AncestorLocator = ancestorLocator;
        }

        public Checkbox TimebarOverride;

        public By AncestorLocator { get; }

        protected override void InitialiseControls()
        {
            TimebarOverride = new Checkbox("timebar-override", WithFormControlName("timebarOverride"), AncestorLocator, WebDriverProvider);

            Controls.Add(TimebarOverride);
        }


    }
}
