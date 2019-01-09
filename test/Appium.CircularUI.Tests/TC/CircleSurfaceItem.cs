using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(4)]
    public class CircleSurfaceItem : TestTemplate
    {
        static string CircleProgressBarTestName = "CircleProgressBar2";
        static string CircleSlider1TestName = "CircleSlider1";
        static string CircleSlider2TestName = "CircleSlider2";

        [Test]
        public void CircleProgressBarTest()
        {
            Driver.FindTC(CircleProgressBarTestName);

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
            Driver.FindTC(CircleSlider1TestName);

            var enableBtnId = "enable";

#if WATCH_DEVICE
            var image = "CircleSurfaceItem_CircleSliderDisable.png";
            Driver.CheckScreenshot(image);
#endif

            Driver.Click(enableBtnId);

#if WATCH_DEVICE
            var image2 = "CircleSurfaceItem_CircleSliderEnable.png";
            Driver.CheckScreenshot(image2);
#endif
        }

        [Test]
        public void CircleSliderTest2()
        {
            Driver.FindTC(CircleSlider2TestName);

            var changeBtnId = "change";
            Driver.Click(changeBtnId);
#if WATCH_DEVICE
            var image = "CircleSurfaceItem_CircleSliderChangeFocus1.png";
            Driver.CheckScreenshot(image);
#endif
            Driver.Click(changeBtnId);
#if WATCH_DEVICE
            var image2 = "CircleSurfaceItem_CircleSliderChangeFocus2.png";
            Driver.CheckScreenshot(image2);
#endif
        }
    }
}