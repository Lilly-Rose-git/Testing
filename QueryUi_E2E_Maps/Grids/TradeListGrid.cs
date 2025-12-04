using OpenQA.Selenium;
using OilHub_Core.ControlAbstractions.Grids;
using OilHub_Core.Utility.UIExtensions;

namespace QueryUi_E2E_Maps.Grids
{
    public class TradeListGrid : Grid
    {
        public TradeListGrid(            
            By ancestorLocator,
            IWebDriverProvider webDriverProvider)
            : base("trade list search results", By.CssSelector("gl-ag-grid"), ancestorLocator, webDriverProvider, GridConfig.DEFAULT)
        {

        }

        EditableTextColumn? amount;

        public override void InitializeColumns()
        {
            amount = new EditableTextColumn("Amount", this, WebDriverProvider );
            this.RegisterExplicitColumn(amount);    
        }

        public override void InitializeContextMenuItems()
        {
        }
    }
}
