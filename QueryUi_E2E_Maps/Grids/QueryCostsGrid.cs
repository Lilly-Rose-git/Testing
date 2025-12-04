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
    public class QueryCostsGrid : Grid
    {
        public QueryCostsGrid(
            By ancestorLocator,
            IWebDriverProvider webDriverProvider)
            : base("query costs search results", By.XPath("//gl-ag-grid"), ancestorLocator, webDriverProvider, GridConfig.DEFAULT)
        {

        }

        

        public override void InitializeColumns()
        {
            
        }

        public override void InitializeContextMenuItems()
        {
        }
    }
}
