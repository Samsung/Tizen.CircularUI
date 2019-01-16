using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(2)]
    public class CirclePage : TestTemplate
    {
        static string CirclePageTestName = "CirclePageBehavior";
        static string RotaryFocusTestName = "RotaryFocus";
        static string ActionButtonTestName = "ActionButton";


        [Test]
        public void CirclePageBehaviorTest()
        {
            Driver.FindTC(CirclePageTestName);
            Driver.Click(350, 180, 3000);
#if WATCH_DEVICE
            var image = "CirclePage_ToolbarItems.png";
            Driver.CheckScreenshot(image);
#endif
        }

        [Test]
        public void RotaryFocusTest()
        {
            Driver.FindTC(RotaryFocusTestName);

#if WATCH_DEVICE
            var image = "CirclePage_RotaryFocusEnable.png";
            Driver.CheckScreenshot(image);
#endif
        }

        [Test]
        public void ActionButtonTest()
        {
            Driver.FindTC(ActionButtonTestName);

            var setBtnId = "set";
            var enableBtnId = "enable";
            var changeColorBtnId = "changeColor";
            Driver.Click(setBtnId);
#if WATCH_DEVICE
            var image = "CirclePage_SetActionButton.png";
            Driver.CheckScreenshot(image);
#endif
            Driver.Click(enableBtnId);
#if WATCH_DEVICE
            var image2 = "CirclePage_DisableActionButton.png";
            Driver.CheckScreenshot(image2);
#endif
            Driver.Click(enableBtnId);
            Driver.Click(changeColorBtnId);
#if WATCH_DEVICE
            var image3 = "CirclePage_ChangeColorActionButton.png";
            Driver.CheckScreenshot(image3);
#endif
        }
    }
}