using NUnit.Framework;

namespace Appium.UITests
{
    public class TestTemplate
    {
        public UITestDriver Driver;
#if EMUL
        public static int SpeedX = -40;
        public static int SpeedY = -40;
#else
        //Watch device
        public static int SpeedX = -80;
        public static int SpeedY = -80;
#endif

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
        }
    }
}
