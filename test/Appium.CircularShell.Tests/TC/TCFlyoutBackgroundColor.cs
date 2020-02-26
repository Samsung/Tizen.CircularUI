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
