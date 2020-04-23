using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(14)]
    public class Toast : TestTemplate
    {

        [Test]
        public void ToastTest()
        {
            Driver.RunTC(this.GetType().Name);

            FindAndClick("text");
            Driver.GoBack();
            FindAndClick("longText");
            Driver.GoBack();
            FindAndClick("iconText");
            Driver.GoBack();
            FindAndClick("iconSize");
            Driver.GoBack();
            FindAndClick("iconJpg");
        }
    }
}
