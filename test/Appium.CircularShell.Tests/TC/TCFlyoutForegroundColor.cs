using NUnit.Framework;
using System.Threading;

namespace Appium.CircularShell.Tests
{
    [TestFixture]
    public class TCFlyoutForeBackgroundColor : TestCaseBase
    {
        public TCFlyoutForeBackgroundColor() : base("FlyoutForegroundColor") { }

        [Test]
        public void SetBlueViolet()
        {
            FindAndClick("BlueViolet");
            FindAndClick("default");
        }

        [Test]
        public void SetBrown()
        {
            FindAndClick("Brown");
            FindAndClick("default");
        }

        [Test]
        public void SetFuchsia()
        {
            FindAndClick("Fuchsia");
            FindAndClick("default");
        }
    }
}
