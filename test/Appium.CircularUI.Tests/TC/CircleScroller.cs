using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(9)]
    public class CircleScroller : TestTemplate
    {
        static string VerticalTestName = "Vertical";
        static string HorizontalTestName = "Horizontal";
        static string RemoveAddTestName = "Remove/Add";
        static string ChangeBarColorTestName = "ChangeBarColor";

        [Test]
        public void VerticalTest()
        {
            Driver.FindTC(VerticalTestName);
            Driver.Flick(0, -150);
            Driver.Flick(0, -150);
            Driver.Flick(0, -150);
        }

        [Test]
        public void HorizontalTest()
        {
            Driver.FindTC(HorizontalTestName);
            Driver.Flick(-150, 0);
            Driver.Flick(-150, 0);
            Driver.Flick(-150, 0);
        }

        [Test]
        public void RemoveAddTest()
        {
            Driver.FindTC(RemoveAddTestName);

            var elementId = "hideableLabel";
            var isVisible = Driver.GetAttribute<bool>(elementId, "IsVisible");
            Assert.True(isVisible, elementId + ".IsVisible should be true, but got " + isVisible);

            Driver.Flick(0, -150);
            Driver.Click("button");

            var image = "CircleScrollView_Remove.png";
            Driver.CheckScreenshot(image);
        }

        [Test]
        public void ChangeBarColorTest()
        {
            Driver.FindTC(ChangeBarColorTestName);

            var elementId = "myScrollView";
            var before = Driver.GetAttribute<string>(elementId, "BarColor");

            Driver.Click("button");

            var after = Driver.GetAttribute<string>(elementId, "BarColor");
            Assert.AreNotEqual(before, after);

        }
    }
}
