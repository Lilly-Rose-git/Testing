using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OilHub_Core.ControlAbstractions;
using OilHub_Core.Utility.UIExtensions;

namespace QueryUi_E2E_Maps.ModalBoxes
{
    public class TradeMovementModal : ModalBoxBase
    {
        private static readonly By TRADE_MOVEMENT_MODAL_LOCATOR = By.XPath("//app-move-modal");
        public TradeMovementModal(IWebDriverProvider webDriverProvider) : base("trade movement", webDriverProvider, TRADE_MOVEMENT_MODAL_LOCATOR)
        {

        }

        public Button Move { get; set; }
        public RadioField NewMovement { get; set; }

        protected override void InitialiseOwnControls()
        {
            NewMovement = RegisterControl(new RadioField("new movement", By.XPath("//div[contains(@class, 'new-movement') and text() = ' New Movement ']"), TRADE_MOVEMENT_MODAL_LOCATOR, WebDriverProvider));

            Move = RegisterControl(new Button("move", By.XPath("//button[normalize-space(text()) = 'Move']"), TRADE_MOVEMENT_MODAL_LOCATOR, WebDriverProvider));

        }
    }
}
