using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.UIExtensions;
using System.Collections.Generic;
using OilHub_Core.ControlAbstractions.ContextMenus;
using QueryUi_E2E_Maps.Grids;
using OilHub_Core.ControlAbstractions.Grids;
using OilHub_Core.CommonSteps;
namespace TC_E2E_Automation.StepDefinitions
{
    [Binding]
    public class TradeCaptureUiSteps
    {
        protected IApp App { get; }
        protected OpenFin OpenFin { get; }
        protected ITestDataContext TestDataContext { get; }
        protected Helpers Helpers { get; }
        protected IWebDriverProvider WebDriverProvider { get; }

        public TradeCaptureUiSteps(
            IApp app,
            OpenFin openFin,
            ITestDataContext testDataContext,
            Helpers helpers,
            IWebDriverProvider webDriverProvider)
        {
            App = app;
            OpenFin = openFin;
            TestDataContext = testDataContext;
            Helpers = helpers;
            WebDriverProvider = webDriverProvider;            
        }

        protected IWebDriver GetWebDriver()
        {
            return WebDriverProvider.GetWebDriver();
        }

        private IPage GetCurrentPage()
        {
            return this.App.GetCurrentPage();
        }

        [Given(@"I am using Query UI with account type (.*)")]
        public void GivenIAmUsingOilHubWithAccountTypeX(string accountType)
        {
            EnsureTradeCaptureIsOpenForAccountType(accountType);
        }

        [When(@"I am using Query UI with account type (.*)")]
        public void WhenIAmUsingOilHubWithAccountTypeX(string accountType)
        {
            EnsureTradeCaptureIsOpenForAccountType(accountType);
        }

        private void EnsureTradeCaptureIsOpenForAccountType(string accountType)
        {
            OpenFin.EnsureOpenForAccountType(accountType);

            if (!string.IsNullOrEmpty(WebDriverProvider.CurrentAccountType) && WebDriverProvider.CurrentAccountType != accountType)
            {
                WebDriverProvider.ExitQuitAndDispose();
            }            

            if (WebDriverProvider.CurrentAccountType != accountType)
            {
                WebDriverProvider.EnsureDriverForAccountType(accountType);
            }
        }


        [When(@"I go to the (.*) page with the (.*) role")]
        public void WhenIGoToXPageWithTheYRole(string page, string role)
        {
            var trade = TestDataContext.TryGet<TradeContent>("Trade");
            var tradeId = trade.HeaderNumber;
            var url = Helpers.BuildUrl(page, role, tradeId.ToString());

            IJavaScriptExecutor js = (IJavaScriptExecutor)GetWebDriver();
            js.ExecuteScript($"fin.desktop.Window.getCurrent().navigate('{url}');");

            SetCurrentPageModel(page);
            Thread.Sleep(5000);
        }

        [When(@"I go to the (.*) page")]
        public void WhenIGoToXPage(string page)
        {
            var trade = TestDataContext.TryGet<TradeContent>("Trade");
            var tradeId = trade?.HeaderNumber;
            var url = Helpers.BuildUrl(page, null, tradeId?.ToString());
            
            IJavaScriptExecutor js = (IJavaScriptExecutor)GetWebDriver();
            js.ExecuteScript($"fin.desktop.Window.getCurrent().navigate('{url}');");

            SetCurrentPageModel(page);
        }

        [Given(@"I am on the (.*) page")]
        public void IAmOnTheXPage(string page)
        {
            Thread.Sleep(5000);
            SetCurrentPageModel(page);
            //WebDriverProvider.SwitchToPageHandleUsingPageTitle(this.App.GetCurrentPage().GetPageTitle());
        }

        [Then(@"I am on the (.*) page")]
        public void IAmOnTheXPage1(string page)
        {
            Thread.Sleep(4000);
            SetCurrentPageModel(page);
            //NavigateToPageUrl();
            WebDriverProvider.SwitchToPageHandleUsingPageTitle(this.App.GetCurrentPage().GetPageTitle());
        }
        private void SetCurrentPageModel(string pageName)
        {
            this.App.SetCurrentPage(pageName);
        }
          
        public void NavigateToPageUrl()
        {

            WebDriverWait wait = new WebDriverWait(WebDriverProvider.GetWebDriver(), TimeSpan.FromSeconds(60));
            wait.Until(d => (string)((IJavaScriptExecutor)d).ExecuteScript("return document.readyState") == "complete");

            IJavaScriptExecutor js = (IJavaScriptExecutor)WebDriverProvider.GetWebDriver();
            js.ExecuteScript(@"fin.Window.create({name: 'NewWindow',
        url: 'https://gbldncommoniistest1.anyaccess.net/oilhubhome/#/',
        defaultWidth: 1000,
        defaultHeight: 700,
        autoShow: true
    });
");

        }

        [Then(@"I click (.*) context menu item on (.*) grid")]
        public void ThenIClickCopyContextMenuItemOnSwapsListSearchResultsGrid(string contextMenuItem, string gridName)
        {
            GetCurrentPage().GetSpecialControl<IGrid>(gridName).GetContextMenuItem(contextMenuItem).Click();
            //GetGrid(gridName).ContextClick(contextMenuItemIndex);
        }

        [Then (@"I click the ""(.+)"" content in the ""(.*)"" row of the ""(.*)"" grid")]
        public void WhenIClickTheXButtonInTheYRow(string columnName, string row, string gridName)
        {
            GetCurrentPage().GetSpecialControl<IGrid>(gridName). GetColumn(columnName).  Click(int.Parse(row));
        }

        [When(@"I set the value in the ""(.*)"" column in the ""(.*)"" row to be ""(.*)"" within the ""(.*)"" grid")]
        public void WhenISetTheValueInTheXColumnToBeY(string columnName, string rowIndex, string valueToSet, string gridName)
        {
            var grid = GetCurrentPage().GetSpecialControl<IGrid>(gridName);
            grid.GetColumn<IColumnWithSettableValue>(columnName).SetValue(int.Parse(rowIndex), valueToSet);
        }

        [When(@"I Wait for 60 seconds")]
        public void WhenIWait60Sec()
        {
            Thread.Sleep(60000);
        }
    }
}
