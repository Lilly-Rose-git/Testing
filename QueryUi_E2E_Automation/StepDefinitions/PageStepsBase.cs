using PhysOps_E2E_Automation.StepDefinitions;
using PhysOps_E2E_Automation.StepDataTypes;
using Glencore.TradeCapture.API.Common.DTO;
using OpenQA.Selenium;
using System.Diagnostics.CodeAnalysis;
using TC_E2E_ControlAbstractions;
using TC_E2E_Core.Utility;
using TC_E2E_Core.Utility.UIExtensions;
using Xunit;
using Xunit.Sdk;
using static System.Net.Mime.MediaTypeNames;

namespace PhysOps_E2E_Automation.StepDefinitions
{
    [Binding]
    public class PageStepsBase
    {
        protected IApp App { get; }
        protected OpenFin OpenFin { get; }
        protected ITestDataContext TestDataContext { get; }
        protected Helpers Helpers { get; }
        protected IWebDriverProvider WebDriverProvider { get; }

        public PageStepsBase(
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

        [Given(@"I am using Trade Capture with account type (.*)")]
        public void GivenIAmUsingTradeCaptureWithAccountTypeX(string accountType)
        {
            EnsureTradeCaptureIsOpenForAccountType(accountType);
        }

        [When(@"I am using Trade Capture with account type (.*)")]
        public void WhenIAmUsingTradeCaptureWithAccountTypeX(string accountType)
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

            Helpers.SwitchToHomePageWindowHandle();
            IJavaScriptExecutor js = (IJavaScriptExecutor)GetWebDriver();
            js.ExecuteScript($"fin.desktop.Window.getCurrent().navigate('{url}');");

            SetCurrentPageModel(page);
        }

        [When(@"I go to the (.*) page")]
        public void WhenIGoToXPage(string page)
        {
            var trade = TestDataContext.TryGet<TradeContent>("Trade");
            var tradeId = trade?.HeaderNumber;
            var url = Helpers.BuildUrl(page, null, tradeId?.ToString());

            Helpers.SwitchToHomePageWindowHandle();
            IJavaScriptExecutor js = (IJavaScriptExecutor)GetWebDriver();
            js.ExecuteScript($"fin.desktop.Window.getCurrent().navigate('{url}');");
            //js.ExecuteScript($"window.open('{url}','_blank');");

            SetCurrentPageModel(page);
        }

        [When(@"I go directly to the (.*) page")]
        public void WhenIGoToInvoicesPage(string page)
        {
            Helpers.NavigateToPageUrl(page);
            //Helpers.SwitchToPageUsingHandleUrl(url);
            Helpers.SwitchToInvoicingPageWindowHandle();
            SetCurrentPageModel(page);
        }

        [When(@"I go directly to the (.*) page with the url path (.*)")]
        public void WhenIGoToTheYPageWithTheXUrl(string page, string path)
        {
            Helpers.NavigateToPageUrl(path);
            //Helpers.SwitchToPageUsingHandleUrl(url);
            Helpers.SwitchToInvoicingPageWindowHandle();
            SetCurrentPageModel(page);
        }

        [Given(@"I am on the home page")]
        public void IAmOnTheHomePage()
        {
            Helpers.NavigateToHomePageUrl();
            //Helpers.SwitchToHomePageWindowHandle();
        }

        [Then(@"I close the current window")]
        public void ICloseTheCurrentWindow()
        {
            Helpers.CloseCurrentWindow();
            //Helpers.SwitchToHomePageWindowHandle();
        }

        [Then(@"I close the home page window")]
        public void ICloseTheHomePageWindow()
        {
            Helpers.SwitchToHomePageWindowHandle();
            Helpers.CloseCurrentWindow();
        }

        private void SetCurrentPageModel(string pageName)
        {
            this.App.SetCurrentPage(pageName);
        }

        private IPage GetCurrentPage()
        {
            return this.App.GetCurrentPage();
        }

        //[When(@"I set the (.*) field value to be (.*)")]
        //public void WhenISetTheXFieldValueToY(string fieldName, string valueToSet)
        //{
        //    GetCurrentPage().GetControlByName<IHasSettableValue>(fieldName).SetValue(valueToSet);
        //}

        //[When(@"I set the field values to be as follows")]
        //public void WhenISetTheFieldValuesToBe(Table table)
        //{
        //    var fieldNamesAndValues = table.CreateSet<FieldNameAndValue>();

        //    foreach (var fieldNameAndValue in fieldNamesAndValues)
        //    {
        //        GetCurrentPage().GetControlByName<IHasSettableValue>(fieldNameAndValue.FieldName).SetValue(fieldNameAndValue.Value);
        //    }
        //}

        //[When(@"I click the (.*) field")]
        //public void WhenIClickTheXField(string fieldName)
        //{
        //    GetCurrentPage().GetControlByName<IClickable>(fieldName).Click();
        //}

        //[Then(@"The (.*) field value should be (.*)")]
        //public void ThenTheXFieldValueShouldBeY(string fieldName, string expectedValue)
        //{
        //    AssertFieldHasValue(fieldName, expectedValue);
        //}

        //[Then(@"The field values should be as follows")]
        //public void TheFieldValuesShouldBeAsFollows(Table table)
        //{
        //    var fieldNamesAndValues = table.CreateSet<FieldNameAndValue>();

        //    AssertFieldsHaveValues(fieldNamesAndValues);
        //}

        private void AssertFieldsHaveValues(IEnumerable<FieldNameAndValue> fieldsAndValues)
        {
            Action[] fieldValueAssertions =
                fieldsAndValues.Select<FieldNameAndValue, Action>(
                    fieldAndValue => () => AssertFieldHasValue(fieldAndValue.FieldName, fieldAndValue.Value)).ToArray();

            Assert.Multiple(fieldValueAssertions);
        }

        private void AssertFieldsHaveValues(IEnumerable<(IHasValue Field, string ExpectedValue)> fieldsAndValues)
        {
            Action[] fieldValueAssertions =
                fieldsAndValues.Select<(IHasValue Field, string ExpectedValue), Action>(
                    fieldAndValue => () => AssertFieldHasValue(fieldAndValue.Field, fieldAndValue.ExpectedValue)).ToArray();

            Assert.Multiple(fieldValueAssertions);
        }

        private void AssertFieldHasValue(string fieldName, string expectedValue)
        {
            AssertFieldHasValue(GetCurrentPage().GetControlByName<IHasValue>(fieldName), expectedValue);
        }

        private void AssertFieldHasValue(IHasValue field, string expectedValue)
        {
            var actualValue = field.GetValue();

            Assert.True(actualValue?.Equals(expectedValue), CreateValueAssertionFailureMessage($"{field.HumanName} field", expectedValue, actualValue?.ToString() ?? "[NULL]"));
        }

        private string CreateValueAssertionFailureMessage(string name, string expectedValue, string actualValue)
        {
            return $"Expected the {name} value be {expectedValue} but it was {actualValue}";
        }

        //[Then(@"I click on (.*)")]
        //public void ThenIClickOnXControl(string fieldName)
        //{
        //    GetCurrentPage().GetControlByName<IClickable>(fieldName).Click();
        //}

        [Then(@"The (.*) field should be (.*)")]
        public void ThenTheXFieldShouldBeY(string fieldName, string expectedState)
        {
            var control = GetCurrentPage().GetControlByName(fieldName);

            AssertControlState(control, expectedState);
        }

        //[Then(@"The following fields should be (.*)")]
        //public void ThenTheFollowingFieldsShouldBeX(string expectedState, string fields)
        //{
        //    var fieldNames = SplitFollowingDocString(fields);

        //    var currentPage = GetCurrentPage();

        //    var fieldControls = fieldNames.Select(fieldName => currentPage.GetControlByName(fieldName));

        //    AssertControlStates(fieldControls, expectedState);
        //}

        [Then(@"The (.*) panel should be (.*)")]
        public void ThenTheXPanelShouldBeY(string panelName, string state)
        {
            var panel = GetCurrentPage().GetPanelByName(panelName);

            AssertPanelState(panel, state);
        }

        [Then(@"The (.*) panel controls should be (.*)")]
        public void ThenTheXPanelControlsShouldBeY(string panelName, string state)
        {
            var panel = GetCurrentPage().GetPanelByName(panelName);

            AssertControlStates(panel.GetAllControls(), state);
        }


        private IEnumerable<string> SplitFollowingDocString(string followingDocString)
        {
            return followingDocString.Split(',', ';', '\n', '\r').Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s));
        }

        private void AssertControlStates(IEnumerable<IControl> controls, string state)
        {
            Assert.Multiple(controls.Select<IControl, Action>(control => () => AssertControlState(control, state)).ToArray());
        }

        private bool AssertControlState(IControl control, string state, string? stateImpliedBy = null)
        {
            var failureMessage = CreateStateAssertionFailureMessage(control, state, stateImpliedBy);

            bool stateIsCorrect;

            switch (state)
            {
                case "present":
                case "absent":
                case "visible":
                case "hidden":
                    {
                        stateIsCorrect = IsControlInState(control, state);
                        break;
                    }
                case "locked":
                case "unlocked":
                case "mandatory":
                case "optional":
                    {
                        stateIsCorrect =
                            AssertControlState(control, "present", stateImpliedBy: state) &&
                            AssertControlState(control, "visible", stateImpliedBy: state) &&
                            IsControlInState(control, state);

                        break;
                    }
                default:
                    throw new XunitException($"Unknown state '{state}' in assertion");
            }

            Assert.True(stateIsCorrect, failureMessage);

            return stateIsCorrect;
        }

        private bool IsControlInState(IControl control, string state)
        {
            switch (state)
            {
                case "present":
                    return control.IsPresent();
                case "absent":
                    return !control.IsPresent();
                case "visible":
                    return control.IsVisible();
                case "hidden":
                    return !control.IsVisible();
                case "locked":
                    return !control.IsEnabled();
                case "unlocked":
                    return control.IsEnabled();
                case "mandatory":
                    {
                        return TryCast<IHasSettableValue>(control, out var castControl) && castControl.IsMandatory();
                    }
                case "optional":
                    {
                        return TryCast<IHasSettableValue>(control, out var castControl) && !castControl.IsMandatory();
                    }
                default:
                    throw new XunitException($"Unknown state '{state}'");
            }
        }

        private string CreateStateAssertionFailureMessage(IHasHumanName namedThing, string state, string? stateImpliedBy = null)
        {
            string notText = stateImpliedBy == null ? "not" : $"not {state}";

            return string.Concat($"Expected {namedThing.HumanName} to be {stateImpliedBy ?? state} but it was ", notText);
        }

        private bool TryCast<T>(IControl control, [NotNullWhen(returnValue: true)] out T? castControl) where T : class
        {
            castControl = control as T;

            return castControl != null;
        }

        private void AssertPanelState(IPanel panel, string state, string? stateImpliedBy = null)
        {
            var failureMessage = CreateStateAssertionFailureMessage(panel, state, stateImpliedBy);

            switch (state)
            {
                case "present":
                    Assert.True(panel.IsPresent(), failureMessage);
                    break;
                case "absent":
                    Assert.False(panel.IsPresent(), failureMessage);
                    break;
                case "visible":
                    Assert.True(panel.IsVisible(), failureMessage);
                    break;
                case "hidden":
                    Assert.False(panel.IsVisible(), failureMessage);
                    break;
                default:
                    throw new XunitException($"Unknown panel state '{state}' in assertion");
            }
        }
    }
}
