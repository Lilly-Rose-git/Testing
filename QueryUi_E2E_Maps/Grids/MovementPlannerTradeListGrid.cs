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
    public class MovementPlannerTradeListGrid : Grid
    {
        public MovementPlannerTradeListGrid(
            By ancestorLocator,
            IWebDriverProvider webDriverProvider)
            : base("trade list search results", By.XPath("(//gl-ag-grid)[1]"), ancestorLocator, webDriverProvider, GridConfig.WITH_CONTEXT_MENU)
        {
        }

        EditableTextColumn? amount;

        public override void InitializeColumns()
        {
            amount = new EditableTextColumn("Amount", this, WebDriverProvider);
            this.RegisterExplicitColumn(amount);
        }

        public ContextMenuItem MoveFullQtyToMovement { get; set; }

        public override void InitializeContextMenuItems()
        {
            MoveFullQtyToMovement = RegisterExplicitContextMenuItem(new ContextMenuItem("move full qty to movement", By.XPath("//span[text()='Move Full Qty to Movement']"), By.XPath("//*[@id='mainAgGrid']/div/div[5]/div"), WebDriverProvider));
        }
    }
}
