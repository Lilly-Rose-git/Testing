using OpenQA.Selenium;
using QueryUi_E2E_Maps.Grids;
using QueryUi_E2E_Maps.Panels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.ControlAbstractions.ContextMenus;
using OilHub_Core.ControlAbstractions.Grids;
using OilHub_Core.ControlAbstractions.Search;
using OilHub_Core.Utility.UIExtensions;
using QueryUi_E2E_Maps.ModalBoxes;

namespace QueryUi_E2E_Maps.Pages
{
    public class QuerySwapsPage : PageBase
    {
        public IGrid SearchResultsGrid { get; private set; }
        public IPillSearchBar SearchBar { get; private set; }
        public Button SaveButton { get; private set; }
        public Button CloneButton { get; private set; }

        public IContextMenuItem SelectContextMenuItem { get; private set; }


        public QuerySwapsPage(IWebDriverProvider driverProvider) : base("query-swaps", "Swaps: TEST", driverProvider, By.TagName("gl-root"))
        {

        }
      
        protected override void InitialiseOwnControls()
        {
            SaveButton = new Button("save", By.XPath("//..//gl-tool-tip[@type='button' and @btnvalue='Save']/div/div"), this.RelativeLocator, WebDriverProvider);
            this.Controls.Add(SaveButton);
        }

        public PhysicalOperationsPanel PhysicalOperationsPanel { get; private set; }
        public UnsavedChangesModalBox UnsavedChangesModalBox { get; private set; }

        protected override void InitialisePanels()
        {

        }

        protected override void InitialiseSpecialControls()
        {
            var querySwapsGrid = new QuerySwapsGrid(this.RelativeLocator, WebDriverProvider);
            querySwapsGrid.Initialize();
            this.SearchResultsGrid = RegisterSpecialControl(querySwapsGrid);
            this.SearchBar = RegisterSpecialControl(PillSearchBar.CreateDefault(this.RelativeLocator, WebDriverProvider));
        }

        protected override void InitialiseModals()
        {
            UnsavedChangesModalBox = RegisterModal(new UnsavedChangesModalBox(WebDriverProvider));

        }
    }
}
