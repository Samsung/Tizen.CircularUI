using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(13)]
    public class InformationPopup : TestTemplate
    {

        [Test]
        public void InformationPopupTest()
        {
            Driver.Click("text");
            Driver.GoBack();
            Driver.Click("longText");
            Driver.GoBack();
            Driver.Click("titleTextButton");
#if WATCH_DEVICE
            var image = "InformationPopup_TextButton.png";
            Driver.CheckScreenshot(image);
#endif
            Driver.GoBack();
            Driver.Flick(0, SpeedY);
            Driver.Flick(0, SpeedY);
            Driver.Click("longTextButton");
            Driver.GoBack();
            Driver.Click("titleTextButton2");
            Driver.GoBack();
            Driver.Click("process");
            Driver.GoBack();
            Driver.Click("process2");
            Driver.GoBack();
            Driver.Click("changeButtonColor");
#if WATCH_DEVICE
            var image2 = "InformationPopup_ChangeButtonColor.png";
            Driver.CheckScreenshot(image2);
#endif
            Driver.GoBack();

        }
    }
}
