using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(9)]
    public class CircleScroller : TestTemplate
    {
        static string VerticalTestName = "CircleScroller/Vertical";
        static string HorizontalTestName = "CircleScroller/Horizontal";
        static string RemoveAddTestName = "CircleScroller/Remove/Add";
        static string ChangeBarColorTestName = "CircleScroller/ChangeBarColor";

        [Test]
        public void VerticalTest()
        {
            Driver.RunTC(VerticalTestName);
            Driver.Flick(0, SpeedY * 2);
            Driver.Flick(0, SpeedY * 2);
            Driver.Flick(0, SpeedY * 2);
        }

        [Test]
        public void HorizontalTest()
        {
            Driver.RunTC(HorizontalTestName);
            Driver.Flick(SpeedX, 0);
            Driver.Flick(SpeedX, 0);
        }

        [Test]
        public void RemoveAddTest()
        {
            Driver.RunTC(RemoveAddTestName);

            var elementId = "hideableLabel";
            var isVisible = Driver.GetAttribute<bool>(elementId, "IsVisible");
            Assert.True(isVisible, elementId + ".IsVisible should be true, but got " + isVisible);

            Driver.Flick(0, SpeedY * 2);
            FindAndClick("button");

#if WATCH_DEVICE
            var image = "CircleScrollView_Remove.png";
            Driver.CheckScreenshot(image);
#endif
        }

        [Test]
        public void ChangeBarColorTest()
        {
            Driver.RunTC(ChangeBarColorTestName);

            var elementId = "myScrollView";
            var before = Driver.GetAttribute<string>(elementId, "BarColor");

            FindAndClick("button");

            var after = Driver.GetAttribute<string>(elementId, "BarColor");
            Assert.AreNotEqual(before, after);

        }
    }
}
