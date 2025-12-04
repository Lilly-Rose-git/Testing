using OpenQA.Selenium;
using QueryUi_E2E_Maps.Grids;
using QueryUi_E2E_Maps.Panels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.ControlAbstractions.Grids;
using OilHub_Core.ControlAbstractions.Search;
using OilHub_Core.Utility.UIExtensions;

namespace QueryUi_E2E_Maps.Pages
{
    public class QueryCostsPage : PageBase
    {
        public IGrid SearchResultsGrid { get; private set; }
        public IPillSearchBar SearchBar { get; private set; }

        public QueryCostsPage(IWebDriverProvider driverProvider) : base("query-costs", "Costs: TEST", driverProvider, By.TagName("gl-root"))
        {

        }

        protected override void InitialiseOwnControls()
        {
            
        }

        public PhysicalOperationsPanel PhysicalOperationsPanel { get; private set; }

        protected override void InitialisePanels()
        {
            
        }

        protected override void InitialiseSpecialControls()
        {
            var queryCostsGrid = new QueryCostsGrid(this.RelativeLocator, WebDriverProvider);
            queryCostsGrid.Initialize();
            this.SearchResultsGrid = RegisterSpecialControl(queryCostsGrid);
            this.SearchBar = RegisterSpecialControl(PillSearchBar.CreateDefault(this.RelativeLocator, WebDriverProvider));
        }

        protected override void InitialiseModals()
        {

        }
    }
}
