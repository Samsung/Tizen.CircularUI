using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace Appium.CircularShell.Tests
{
    public abstract class TestCaseBase
    {
        protected TestCaseBase(string tcName)
        {
            TCName = tcName;
        }

        public string TCName { get; }

        public TizenDriverApp Driver { get; set; }


        [OneTimeSetUp]
        public void Init()
        {
            Driver = AppInitializer.GetDriver();
        }

        [SetUp]
        public void TestSetUp()
        {
            Driver.SetText("TCName", TCName);
            Driver.Click("Go");
        }

        [TearDown]
        public void TestCleanUp()
        {
            Driver.Click("Back");
        }
    }
}
