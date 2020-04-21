using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(4)]
    public class CircleSurfaceItem : TestTemplate
    {
        static string CircleProgressBarTestName = "CircleSurfaceItem/CircleProgressBar2";
        static string CircleSlider1TestName = "CircleSurfaceItem/CircleSlider1";
        static string CircleSlider2TestName = "CircleSurfaceItem/CircleSlider2";

        [Test]
        public void CircleProgressBarTest()
        {
            Driver.RunTC(CircleProgressBarTestName);

            var startBtnId = "start";

            Driver.Click(startBtnId, 15000);

#if WATCH_DEVICE
            var image = "CircleSurfaceItem_CircleProgressBar.png";
            Driver.CheckScreenshot(image);
#endif
        }

        [Test]
        public void CircleSliderTest1()
        {
            Driver.RunTC(CircleSlider1TestName);

            var enableBtnId = "enable";

#if WATCH_DEVICE
            var image = "CircleSurfaceItem_CircleSliderDisable.png";
            Driver.CheckScreenshot(image);
#endif

            FindAndClick(enableBtnId);

#if WATCH_DEVICE
            var image2 = "CircleSurfaceItem_CircleSliderEnable.png";
            Driver.CheckScreenshot(image2);
#endif
        }

        [Test]
        public void CircleSliderTest2()
        {
            Driver.RunTC(CircleSlider2TestName);

            var changeBtnId = "change";
            FindAndClick(changeBtnId);
#if WATCH_DEVICE
            var image = "CircleSurfaceItem_CircleSliderChangeFocus1.png";
            Driver.CheckScreenshot(image);
#endif
            FindAndClick(changeBtnId);
#if WATCH_DEVICE
            var image2 = "CircleSurfaceItem_CircleSliderChangeFocus2.png";
            Driver.CheckScreenshot(image2);
#endif
        }
    }
}