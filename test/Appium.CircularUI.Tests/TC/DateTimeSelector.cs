using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(3)]
    public class DateTimeSelector : TestTemplate
    {

        [Test]
        public void DateTimeSelectorTest()
        {
            Driver.RunTC(this.GetType().Name);
        }
    }
}