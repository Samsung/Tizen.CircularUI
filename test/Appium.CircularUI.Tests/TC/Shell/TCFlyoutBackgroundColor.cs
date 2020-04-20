using NUnit.Framework;
using System.Threading;


namespace Appium.UITests.Shell
{
    [TestFixture]
    public class TCFlyoutBackgroundColor : ShellTestCaseBase
    {
        public TCFlyoutBackgroundColor() : base("ShellTest/FlyoutBackgroundColorTest")
        {
        }

        [Test]
        public void SetBlueViolet()
        {
            FindAndClick("BlueViolet");
            FindAndClick("Open");
            GoBack();
            FindAndClick("default");
        }

        [Test]
        public void SetBrown()
        {
            FindAndClick("Brown");
            FindAndClick("Open");
            GoBack();
            FindAndClick("default");
        }

        [Test]
        public void SetFuchsia()
        {
            FindAndClick("Fuchsia");
            FindAndClick("Open");
            GoBack();
            FindAndClick("default");
        }
    }
}
