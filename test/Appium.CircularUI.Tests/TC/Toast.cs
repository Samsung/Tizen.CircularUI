using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(14)]
    public class Toast : TestTemplate
    {

        [Test]
        public void ToastTest()
        {
            Driver.Click("text", 5000);
            Driver.Click("longText", 5000);
            Driver.Click("iconText", 5000);
            Driver.Click("iconSize", 5000);
            Driver.Click("iconJpg", 5000);
        }
    }
}
