using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(14)]
    public class Toast : TestTemplate
    {

        [Test]
        public void ToastTest()
        {
            Driver.Click("text", 1000);
            Driver.GoBack();
            Driver.Click("longText", 1000);
            Driver.GoBack();
            Driver.Click("iconText", 1000);
            Driver.GoBack();
            Driver.Click("iconSize", 1000);
            Driver.GoBack();
            Driver.Click("iconJpg", 1000);
        }
    }
}
