using NUnit.Framework;

namespace Appium.UITests
{
    public class TestTemplate
    {
        public UITestDriver Driver;

        [SetUp]
        public void TestFixtureSetUp()
        {
            Driver = UITestDriver.Instance;
            Driver.FindTC(this.GetType().Name);
        }

        [TearDown]
        public void TestFixtureTearDown()
        {
            Driver.GoHomePage();
        }

        [TearDown]
        public void TearDown()
        {
            System.Threading.Thread.Sleep(1000);
            //Driver.GoHomePage();
        }
    }
}
