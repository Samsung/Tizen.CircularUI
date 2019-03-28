using NUnit.Framework;

namespace Appium.UITests
{
    public class TestTemplate
    {
        public UITestDriver Driver;
#if EMUL_40
        public static int SpeedX = -45;
        public static int SpeedY = -35;
#elif EMUL_55
        public static int SpeedX = -100;
        public static int SpeedY = -120;
#elif WATCH_DEVICE
        //Galaxy Watch device
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
