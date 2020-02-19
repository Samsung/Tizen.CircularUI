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
            Thread.Sleep(1000);
            Driver.Click("icon1");
            Thread.Sleep(1000);
            Driver.Click("default");
            Thread.Sleep(1000);
        }

        [Test]
        public void SetIcon2()
        {
            Thread.Sleep(1000);
            Driver.Click("icon2");
            Thread.Sleep(1000);
            Driver.Click("default");
            Thread.Sleep(1000);
        }

        [Test]
        public void SetIcon3()
        {
            Thread.Sleep(1000);
            Driver.Click("icon3");
            Thread.Sleep(1000);
            Driver.Click("default");
            Thread.Sleep(1000);
        }
    }
}
