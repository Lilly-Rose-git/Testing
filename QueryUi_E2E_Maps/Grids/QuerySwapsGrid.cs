using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OilHub_Core.ControlAbstractions.Grids;
using OilHub_Core.Utility.UIExtensions;
using OilHub_Core.ControlAbstractions.ContextMenus;
using OilHub_Core.ControlAbstractions;

namespace QueryUi_E2E_Maps.Grids
{
    public class QuerySwapsGrid : Grid
    {
        public QuerySwapsGrid(
            By ancestorLocator,
            IWebDriverProvider webDriverProvider)
            : base("swaps list search results", By.XPath("//gl-ag-grid"), ancestorLocator, webDriverProvider, GridConfig.DEFAULT)
        {

        }

        ContextMenuItem? copyTrade;
        EditableTextColumn? qnty;
        EditableTextColumn? price;


        public override void InitializeColumns()
        {
            qnty = new EditableTextColumn("Quantity", this, WebDriverProvider);
            this.RegisterExplicitColumn(qnty);
            price = new EditableTextColumn("Fixed Price", this, WebDriverProvider);
            this.RegisterExplicitColumn(price);

        }

        public override void InitializeContextMenuItems()
        {
            copyTrade = new ContextMenuItem("Copy Trade", By.XPath("//*[@id='mainAgGrid']/div/div/div/div/div/span[text()='Copy Trade']"), By.XPath("//gl-ag-grid"), this.WebDriverProvider);
            this.RegisterExplicitContextMenuItem(copyTrade);
        }
    }
}
