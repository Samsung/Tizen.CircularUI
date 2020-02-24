using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Appium.CircularShell.Tests
{
    [TestFixture]
    public class TCFlyoutIcon : TestCaseBase
    {
        public TCFlyoutIcon() : base("FlyoutIcon") { }

        [Test]
        public void SetIcon1()
        {
            FindAndClick("icon1");
            FindAndClick("default");
        }

        [Test]
        public void SetIcon2()
        {
            FindAndClick("icon2");
            FindAndClick("default");
        }

        [Test]
        public void SetIcon3()
        {
            FindAndClick("icon3");
            FindAndClick("default");
        }
    }
}
