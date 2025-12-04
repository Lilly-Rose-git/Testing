using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.UIExtensions;

namespace QueryUi_E2E_Maps.Panels
{
    public class PhysicalOperationsPanel : PanelBase
    {
        private static readonly string PANEL_NAME = "phyisical-operations";
        private static readonly string PHYSICAL_OPERATIONS_PANEL = "//gl-slide-out-menu[1]";
        private static readonly By PANEL_LOCATOR = By.XPath(PHYSICAL_OPERATIONS_PANEL);

        public PhysicalOperationsPanel(IWebDriverProvider webDriverProvider, By ancestorLocator) : base(PANEL_NAME, PANEL_LOCATOR, ancestorLocator, webDriverProvider)
        {
            AncestorLocator = ancestorLocator;
        }

        public Dropdown TradeSubType { get; private set; }
        public SegmentedButton MethodOfTransport { get; private set; }
        public Checkbox MoTOptionality { get; private set; }

        public By AncestorLocator { get; }

        public Button MovementPlannerLive { get; private set; }

        protected override void InitialiseControls()
        {
            // @TODO come up with more generic xPath for MovementPlannerLive button
            MovementPlannerLive = new Button("movement-planner-live", By.XPath("//span[text()='Movement Planner (Live)']"), PANEL_LOCATOR, WebDriverProvider);

            Controls.Add(MovementPlannerLive);
        }


    }
}
