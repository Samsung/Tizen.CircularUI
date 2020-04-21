using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(12)]
    public class TwoButtonPage : TestTemplate
    {
        static string TwoButtonPageBehaviorTestName = "TwoButtonPage/TwoButtonPageBehavior";
        static string TwoButtonPageListTestName = "TwoButtonPage/TwoButtonPageListView";
        static string TwoButtonPopupTestName = "TwoButtonPage/TwoButtonPopup";

        [Test]
        public void TwoButtonPageBehaviorTest()
        {
            Driver.RunTC(TwoButtonPageBehaviorTestName);

            //for (int i = 0; i < 5; i++)
            //    Driver.Flick(0, SpeedY);
            FindAndClick("change2");
            Driver.Flick(0, SpeedY);
            FindAndClick("remove1");
            FindAndClick("changeColor2");
#if WATCH_DEVICE
            var image = "TwoButtonPage_Behavior.png";
            Driver.CheckScreenshot(image);
#endif
        }

        [Test]
        public void TwoButtonPageListTest()
        {
            Driver.RunTC(TwoButtonPageListTestName);

            for (int i = 0; i < 6; i++)
                Driver.Flick(0, SpeedY);
#if WATCH_DEVICE
            var image = "TwoButtonPage_ListView.png";
            Driver.CheckScreenshot(image);
#endif
        }

        [Test]
        public void TwoButtonPopupTest()
        {
            Driver.RunTC(TwoButtonPopupTestName);

            FindAndClick("text");
#if WATCH_DEVICE
            var image = "TwoButtonPopup_Text.png";
            Driver.CheckScreenshot(image);
#endif
            Driver.GoBack();

            for (int i = 0; i < 4; i++)
                Driver.Flick(0, SpeedY);

            FindAndClick("changeRightButtonColor");
#if WATCH_DEVICE
            var image2 = "TwoButtonPopup_ChangeIconColor.png";
            Driver.CheckScreenshot(image2);
#endif
        }
    }
}
