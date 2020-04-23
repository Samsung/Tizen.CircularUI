using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(13)]
    public class InformationPopup : TestTemplate
    {

        [Test]
        public void InformationPopupTest()
        {
            Driver.RunTC(this.GetType().Name);

            FindAndClick("text");
            Driver.GoBack();
            FindAndClick("longText");
            Driver.GoBack();
            FindAndClick("titleTextButton");
#if WATCH_DEVICE
            var image = "InformationPopup_TextButton.png";
            Driver.CheckScreenshot(image);
#endif
            Driver.GoBack();
            FindAndClick("longTextButton");
            Driver.GoBack();
            FindAndClick("titleTextButton2");
            Driver.GoBack();
            FindAndClick("process");
            Driver.GoBack();
            FindAndClick("process2");
            Driver.GoBack();
            FindAndClick("changeButtonColor");
#if WATCH_DEVICE
            var image2 = "InformationPopup_ChangeButtonColor.png";
            Driver.CheckScreenshot(image2);
#endif
            Driver.GoBack();

        }
    }
}
