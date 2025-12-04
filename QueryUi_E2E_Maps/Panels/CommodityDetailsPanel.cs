using OpenQA.Selenium;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.UIExtensions;
using static OilHub_Core.ControlAbstractions.ControlBase;

namespace QueryUi_E2E_Maps.Panels
{
    public class CommodityDetailsPanel : PanelBase
    {
        private static readonly string PANEL_NAME = "commodity-details";
        private static readonly string COMMODITY_DETAILS_PANEL_XPATH = "//app-commodity-details";
        private static readonly By PANEL_LOCATOR = By.XPath(COMMODITY_DETAILS_PANEL_XPATH);

        public CommodityDetailsPanel(IWebDriverProvider webDriverProvider, By ancestorLocator) : base(PANEL_NAME, PANEL_LOCATOR, ancestorLocator, webDriverProvider)
        {
            AncestorLocator = ancestorLocator;
        }

        public Dropdown Tolerance { get; private set; }
        public TextInputBox SpecificGravity { get; private set; }
        public TextInputBox MinTolerance { get; private set; }
        public TextInputBox MaxTolerance { get; private set; }

        public By AncestorLocator { get; }

        protected override void InitialiseControls()
        {

            Tolerance = new Dropdown("tolerance", WithFormControlName("toleranceType"), PANEL_LOCATOR, WebDriverProvider);
            SpecificGravity = new TextInputBox("specific-gravity", WithFormControlName("specificGravity"), PANEL_LOCATOR, WebDriverProvider);
            MinTolerance = new TextInputBox("minTolerence", WithFormControlName("minTolerance"), PANEL_LOCATOR, WebDriverProvider);
            MaxTolerance = new TextInputBox("maxTolerence", WithFormControlName("maxTolerance"), PANEL_LOCATOR, WebDriverProvider);



            Controls.Add(Tolerance);
            Controls.Add(SpecificGravity);
            Controls.Add(MinTolerance);
            Controls.Add(MaxTolerance);
        }


    }
}
