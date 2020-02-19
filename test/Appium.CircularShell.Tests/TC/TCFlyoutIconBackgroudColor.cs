using NUnit.Framework;
using System.Threading;

namespace Appium.CircularShell.Tests
{
    [TestFixture]
    public class TCFlyoutIconBackgroundColor : TestCaseBase
    {
        public TCFlyoutIconBackgroundColor() : base("FlyoutIconBackgroundColor") { }

        [Test]
        public void SetBlueViolet()
        {
            Thread.Sleep(1000);
            Driver.Click("BlueViolet");
            Thread.Sleep(1000);
            Driver.Click("default");
            Thread.Sleep(1000);
        }

        [Test]
        public void SetBrown()
        {
            Thread.Sleep(1000);
            Driver.Click("Brown");
            Thread.Sleep(1000);
            Driver.Click("default");
            Thread.Sleep(1000);
        }

        [Test]
        public void SetFuchsia()
        {
            Thread.Sleep(1000);
            Driver.Click("Fuchsia");
            Thread.Sleep(1000);
            Driver.Click("default");
            Thread.Sleep(1000);
        }
    }
}
