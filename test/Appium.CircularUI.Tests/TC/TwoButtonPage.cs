using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(12)]
    public class TwoButtonPage : TestTemplate
    {
        static string TwoButtonPageBehaviorTestName = "TwoButtonPageBehavior";
        static string TwoButtonPageListTestName = "TwoButtonPageListView";
        static string TwoButtonPopupTestName = "TwoButtonPopup";

        [Test]
        public void TwoButtonPageBehaviorTest()
        {
            Driver.FindTC(TwoButtonPageBehaviorTestName);

            for (int i = 0; i < 5; i++)
                Driver.Flick(0, SpeedY);
            Driver.Click("change2");
            Driver.Flick(0, SpeedY);
            Driver.Click("remove1");
            Driver.Click("changeColor2");
#if WATCH_DEVICE
            var image = "TwoButtonPage_Behavior.png";
            Driver.CheckScreenshot(image);
#endif
        }

        [Test]
        public void TwoButtonPageListTest()
        {
            Driver.FindTC(TwoButtonPageListTestName);

            for (int i = 0; i < 4; i++)
                Driver.Flick(0, SpeedY);
#if WATCH_DEVICE
            var image = "TwoButtonPage_ListView.png";
            Driver.CheckScreenshot(image);
#endif
        }

        [Test]
        public void TwoButtonPopupTest()
        {
            Driver.FindTC(TwoButtonPopupTestName);

            Driver.Click("text");
#if WATCH_DEVICE
            var image = "TwoButtonPopup_Text.png";
            Driver.CheckScreenshot(image);
#endif
            Driver.GoBack();

            for (int i = 0; i < 4; i++)
                Driver.Flick(0, SpeedY);

            Driver.Click("changeRightButtonColor");
#if WATCH_DEVICE
            var image2 = "TwoButtonPopup_ChangeIconColor.png";
            Driver.CheckScreenshot(image2);
#endif
        }
    }
}
