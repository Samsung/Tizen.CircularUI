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
