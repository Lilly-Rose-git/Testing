using OpenQA.Selenium;
using OilHub_Core.ControlAbstractions.Grids;
using OilHub_Core.ControlAbstractions.Search;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.UIExtensions;
using QueryUi_E2E_Maps.Grids;
using QueryUi_E2E_Maps.ModalBoxes;

namespace QueryUi_E2E_Maps.Pages
{
    public class MovementPlannerPage : PageBase
    {

        public IGrid TradeListGrid { get; private set; }
        public IGrid WatchListGrid { get; private set; }    
        public IPillSearchBar SearchBar { get; private set; }
        public TradeMovementModal TradeMovementModal { get; private set; }
        public MarkAsFullyScheduledModal MarkAsFullyScheduledModal { get; private set; }
        

        public MovementPlannerPage(IWebDriverProvider driverProvider) : base("movement-planner", "Movement Planner", driverProvider, By.TagName("app-root"))
        {
        }

        protected override void InitialiseOwnControls()
        {
        }

        protected override void InitialisePanels()
        {
        }

        protected override void InitialiseSpecialControls()
        {
            var tradeListGrid = new MovementPlannerTradeListGrid(this.RelativeLocator, WebDriverProvider);
            tradeListGrid.Initialize();
            this.TradeListGrid = RegisterSpecialControl(tradeListGrid);

            var watchListGrid = new MovementPlannerWatchListGrid(this.RelativeLocator, WebDriverProvider);
            watchListGrid.Initialize();
            this.WatchListGrid = RegisterSpecialControl(watchListGrid); 

            this.SearchBar = RegisterSpecialControl(PillSearchBar.CreateDefault(this.RelativeLocator, WebDriverProvider));
        }

        protected override void InitialiseModals()
        {
            TradeMovementModal = RegisterModal(new TradeMovementModal(WebDriverProvider));
            MarkAsFullyScheduledModal = RegisterModal(new MarkAsFullyScheduledModal(WebDriverProvider));
        }
    }
}
