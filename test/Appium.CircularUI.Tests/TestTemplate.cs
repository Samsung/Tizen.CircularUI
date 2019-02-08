using NUnit.Framework;

namespace Appium.UITests
{
    public class TestTemplate
    {
        public UITestDriver Driver;
#if EMUL
        public static int SpeedX = -45;
        public static int SpeedY = -35;
#elif WATCH_DEVICE
        //Watch device
        public static int SpeedX = -80;
        public static int SpeedY = -60;
#else
        public static int SpeedX = -40;
        public static int SpeedY = -40;
#endif

        [OneTimeSetUp]
        public void Init()
        {
            Driver = UITestDriver.Instance;
        }

        [SetUp]
        public void TestSetUp()
        {
            Driver.FindTC(this.GetType().Name);
        }

        [TearDown]
        public void TestCleanUp()
        {
            Driver.GoHomePage();
        }
    }
}
