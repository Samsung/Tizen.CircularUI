using NUnit.Framework;
using System.Threading;

namespace Appium.UITests.Shell
{
    [TestFixture]
    public class TCFlyoutForeBackgroundColor : ShellTestCaseBase
    {
        public TCFlyoutForeBackgroundColor() : base("ShellTest/FlyoutForegroundColorTest") { }

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
