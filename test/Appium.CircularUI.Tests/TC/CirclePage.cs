using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(2)]
    public class CirclePage : TestTemplate
    {
        static string CirclePageTestName = "CirclePage/CirclePageBehavior";
        static string RotaryFocusTestName = "CirclePage/RotaryFocus";
        static string ActionButtonTestName = "CirclePage/ActionButton";

        [Test]
        public void CirclePageBehaviorTest()
        {
            Driver.RunTC(CirclePageTestName);
            Driver.Click(350, 180, 3000);
#if WATCH_DEVICE
            var image = "CirclePage_ToolbarItems.png";
            Driver.CheckScreenshot(image);
#endif
        }

        [Test]
        public void RotaryFocusTest()
        {
            Driver.RunTC(RotaryFocusTestName);

#if WATCH_DEVICE
            var image = "CirclePage_RotaryFocusEnable.png";
            Driver.CheckScreenshot(image);
#endif
        }

        [Test]
        public void ActionButtonTest()
        {
            Driver.RunTC(ActionButtonTestName);

            var setBtnId = "set";
            var enableBtnId = "enable";
            var changeColorBtnId = "changeColor";
            FindAndClick(setBtnId);
#if WATCH_DEVICE
            var image = "CirclePage_SetActionButton.png";
            Driver.CheckScreenshot(image);
#endif
            FindAndClick(enableBtnId);
#if WATCH_DEVICE
            var image2 = "CirclePage_DisableActionButton.png";
            Driver.CheckScreenshot(image2);
#endif
            FindAndClick(enableBtnId);
            FindAndClick(changeColorBtnId);
#if WATCH_DEVICE
            var image3 = "CirclePage_ChangeColorActionButton.png";
            Driver.CheckScreenshot(image3);
#endif
        }
    }
}