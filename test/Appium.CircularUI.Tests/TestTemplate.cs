using NUnit.Framework;

namespace Appium.UITests
{
    public class TestTemplate
    {
        public UITestDriver Driver;

        public int SpeedX;
        public int SpeedY;

        [OneTimeSetUp]
        public void Init()
        {
            Driver = UITestDriver.Instance;
        }

        [SetUp]
        public void TestSetUp()
        {
            Driver.FindTC(this.GetType().Name);
            SpeedX = Driver.FlickSpeedX;
            SpeedY = Driver.FlickSpeedY;
        }

        [TearDown]
        public void TestCleanUp()
        {
            Driver.GoHomePage();
        }
    }
}
