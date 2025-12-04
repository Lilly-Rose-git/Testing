using BoDi;
using System.Web;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.APIExtensions;
using OilHub_Core.Utility;
using OilHub_Core.Utility.APIExtensions.Transforms;
using OilHub_Core.Utility.Authorization;
using OilHub_Core.Configuration;
using OilHub_Core.Utility.UIExtensions;
using QueryUi_E2E_Maps.Apps;
using QueryUi_E2E_Maps.Pages;
using OilHub_Core.MessagePublisher;

namespace OilHub_Core.Hooks
{
    [Binding]
    public class DependencyResolutionHooks
    {
        private static IObjectContainer _globalContainer;
        private readonly IObjectContainer _objectContainer;

        public DependencyResolutionHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static void BeforeTestRun(IObjectContainer globalContainer)
        {
            RegisterGlobalDependencies(globalContainer);

            _globalContainer = globalContainer;
        }

        private const string CONFIG_ROOT_DIR = "Configs";

        private static void RegisterGlobalDependencies(IObjectContainer globalContainer)
        {
            var environmentNameSource = new EnvironmentNameSource();
            globalContainer.RegisterInstanceAs(environmentNameSource);

            var appConfigurationProvider = new AppConfigurationProvider(environmentNameSource, CONFIG_ROOT_DIR);

            var appConfiguration = appConfigurationProvider.GetAppConfiguration();
            globalContainer.RegisterInstanceAs(appConfiguration);

            Credentials credentials = new Credentials(new PowerShellHandler(), appConfiguration, environmentNameSource);

            globalContainer.RegisterInstanceAs(credentials);

            var chromeDriverProvider = new ChromeDriverProvider(appConfiguration, credentials);

            globalContainer.RegisterInstanceAs<IWebDriverProvider>(chromeDriverProvider);
            globalContainer.RegisterInstanceAs(
                new OpenFin(
                    appConfiguration,
                    Path.Combine(Directory.GetCurrentDirectory(), appConfigurationProvider.GetOpenFinConfigurationFilePath()),
                    chromeDriverProvider,
                    credentials));
        }

        [BeforeScenario("@ui")]
        public void BeforeScenarioWithTag()
        {

            var credentials = _globalContainer.Resolve<Credentials>();
            _objectContainer.RegisterInstanceAs(credentials);

            _objectContainer.RegisterTypeAs<ApiClient, IApiClient>();
            _objectContainer.RegisterTypeAs<TestDataContext, ITestDataContext>();

            var environmentNameSource = _globalContainer.Resolve<EnvironmentNameSource>();
            _objectContainer.RegisterInstanceAs(environmentNameSource);

            var configuration = _globalContainer.Resolve<AppConfiguration>();
            _objectContainer.RegisterInstanceAs(configuration);

            var openFin = _globalContainer.Resolve<OpenFin>();
            _objectContainer.RegisterInstanceAs(openFin);

            var driverProvider = _globalContainer.Resolve<IWebDriverProvider>();
            _objectContainer.RegisterInstanceAs(driverProvider);

            _objectContainer.RegisterTypeAs<Transforms, ITransforms>();

            _objectContainer.RegisterInstanceAs(new Helpers(configuration));

            var app = new QueryUi(driverProvider);
            app.Initialize();

            _objectContainer.RegisterInstanceAs<IApp>(app);
        }

        [BeforeScenario("@api")]
        public void BeforeScenarioWithAPI()
        {
            _objectContainer.RegisterTypeAs<ApiClient, IApiClient>();
            _objectContainer.RegisterTypeAs<TestDataContext, ITestDataContext>();

            var environmentNameSource = _globalContainer.Resolve<EnvironmentNameSource>();
            _objectContainer.RegisterInstanceAs(environmentNameSource);

            var configuration = _globalContainer.Resolve<AppConfiguration>();
            _objectContainer.RegisterInstanceAs(configuration);
            _objectContainer.RegisterInstanceAs(configuration.ActiveMqConfig);

            _objectContainer.RegisterInstanceAs(new TestDataLocator(environmentNameSource));

            _objectContainer.RegisterTypeAs<Transforms, ITransforms>();
            _objectContainer.RegisterInstanceAs(configuration);
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            var openFin = _globalContainer.Resolve<OpenFin>();

            if (openFin != null)
            {
                openFin.KillAndDisposeOpenFin();
            }

            var driverProvider = _globalContainer.Resolve<IWebDriverProvider>();

            if (driverProvider != null)
            {
                driverProvider.ExitQuitAndDispose();
            }
        }

        [AfterStep]
        public void HaveABreakHaveAKitKat()
        {
            Thread.Sleep(2000);
        }
    }
}