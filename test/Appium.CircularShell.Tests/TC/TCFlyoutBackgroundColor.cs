using NUnit.Framework;
using System.Threading;


namespace Appium.CircularShell.Tests
{
    [TestFixture]
    public class TCFlyoutBackgroundColor : TestCaseBase
    {
        public TCFlyoutBackgroundColor() : base("FlyoutBackgroundColor")
        {
        }

        [Test]
        public void SetBlueViolet()
        {
            Thread.Sleep(1000);
            Driver.Click("BlueViolet");
            Driver.Click("Open");
            Driver.Navigate().Back();
            Driver.Click("default");
            Thread.Sleep(1000);
        }

        [Test]
        public void SetBrown()
        {
            Thread.Sleep(1000);
            Driver.Click("Brown");
            Driver.Click("Open");
            Driver.Navigate().Back();
            Driver.Click("default");
        }

        [Test]
        public void SetFuchsia()
        {
            Driver.Flick(0, -100);
            Driver.Click("Fuchsia");
            Driver.Click("Open");
            Driver.Navigate().Back();
            Driver.Click("default");
        }

    }
}
