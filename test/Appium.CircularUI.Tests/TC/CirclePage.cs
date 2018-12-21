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

            Driver.Click(350, 180, 2000);
            var image = "CirclePage_ToolbarItems.png";
            Driver.CheckScreenshot(image);
        }

        [Test]
        public void RotaryFocusTest()
        {
            Driver.FindTC(RotaryFocusTestName);

            var image = "CirclePage_RotaryFocusEnable.png";
            Driver.CheckScreenshot(image);
        }

        [Test]
        public void ActionButtonTest()
        {
            Driver.FindTC(ActionButtonTestName);

            var setBtnId = "set";
            var enableBtnId = "enable";
            var changeColorBtnId = "changeColor";

            Driver.Click(setBtnId);
            var image = "CirclePage_SetActionButton.png";
            Driver.CheckScreenshot(image);

            Driver.Click(enableBtnId);
            var image2 = "CirclePage_DisableActionButton.png";
            Driver.CheckScreenshot(image2);

            Driver.Click(enableBtnId);
            Driver.Click(changeColorBtnId);
            var image3 = "CirclePage_ChangeColorActionButton.png";
            Driver.CheckScreenshot(image3);
        }
    }
}