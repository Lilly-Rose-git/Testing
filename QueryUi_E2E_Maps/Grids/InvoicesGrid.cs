using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OilHub_Core.ControlAbstractions.Grids;
using OilHub_Core.Utility.UIExtensions;

namespace QueryUi_E2E_Maps.Grids
{
    public class InvoicesGrid : Grid
    {
        public InvoicesGrid(
            By ancestorLocator,
            IWebDriverProvider webDriverProvider)
            : base("invoices results", By.CssSelector("gl-ag-grid"), ancestorLocator, webDriverProvider, GridConfig.DEFAULT)
        {

        }

        //EditableTextColumn amount = new EditableTextColumn("amount",null, web )

        public override void InitializeColumns()
        {
            //this.RegisterExplicitColumn();
        }

        public override void InitializeContextMenuItems()
        {

        }
    }
}
