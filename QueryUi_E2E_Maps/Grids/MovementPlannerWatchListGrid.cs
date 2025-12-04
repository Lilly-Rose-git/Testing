using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OilHub_Core.ControlAbstractions.ContextMenus;
using OilHub_Core.ControlAbstractions.Grids;
using OilHub_Core.Utility.UIExtensions;

namespace QueryUi_E2E_Maps.Grids
{
    public class MovementPlannerWatchListGrid : Grid
    {
        public MovementPlannerWatchListGrid(
            By ancestorLocator,
            IWebDriverProvider webDriverProvider)
            : base("watch list results", By.XPath("(//gl-ag-grid)[2]"), ancestorLocator, webDriverProvider, GridConfig.DEFAULT)
        {
        }

        public ReadonlyTextColumn TradeNumber { get; private set; }
        public override void InitializeColumns()
        {
            TradeNumber = RegisterExplicitColumn(new ReadonlyTextColumn("Trade No.", this, WebDriverProvider));
        }

        public override void InitializeContextMenuItems()
        {
        }
    }
}
