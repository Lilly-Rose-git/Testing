using OpenQA.Selenium;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.UIExtensions;
using static OilHub_Core.ControlAbstractions.ControlBase;

namespace QueryUi_E2E_Maps.Panels
{
    public class PricingPanel : PanelBase
    {
        private static readonly string PANEL_NAME = "pricing";
        private static readonly string PRICING_PANEL_XPATH = "//app-pricing";
        private static readonly By PANEL_LOCATOR = By.XPath(PRICING_PANEL_XPATH);

        public PricingPanel(IWebDriverProvider webDriverProvider, By ancestorLocator) : base(PANEL_NAME, PANEL_LOCATOR, ancestorLocator, webDriverProvider)
        {
            AncestorLocator = ancestorLocator;
        }

        public RadioField Formula;
        public Dropdown PricingTerms;

        public By AncestorLocator { get; }

        protected override void InitialiseControls()
        {
            Formula = new RadioField("formula", WithFormControlName("automaticPricingType"), PANEL_LOCATOR, WebDriverProvider);
            PricingTerms = new Dropdown("pricing-terms", WithFormControlName("pricingTerms"), PANEL_LOCATOR, WebDriverProvider);
            
            Controls.Add(Formula);
            Controls.Add(PricingTerms);
           
        }


    }
}
