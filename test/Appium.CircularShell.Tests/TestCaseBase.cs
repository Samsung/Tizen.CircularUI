using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace Appium.CircularShell.Tests
{
    public abstract class TestCaseBase
    {
        readonly int _screenHeight = 360;

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
            FindAndClick("Go");
        }

        [TearDown]
        public void TestCleanUp()
        {
            FindAndClick("Back");
        }

        public void GoBack()
        {
            Driver.Navigate().Back();
            Thread.Sleep(1000);
        }

        public void FindAndClick(string automationId)
        {
            var element = Driver.FindElementByAccessibilityId(automationId);
            var y = element.Location.Y;
            var height = element.Size.Height;
            var retry = 10;

            if ((y < 0))
            {
                while ((y < 0) && (retry > 0))
                {
                    Driver.Flick(0, 100);
                    Thread.Sleep(1000);
                    y = element.Location.Y;
                    retry--;
                }
            }
            else if ((y + height) > _screenHeight)
            {
                while (((y + height) > _screenHeight) && (retry > 0))
                {
                    Driver.Flick(0, -100);
                    Thread.Sleep(1000);
                    y = element.Location.Y;
                    height = element.Size.Height;
                    retry--;
                }
            }
            element.Click();
            Thread.Sleep(1000);
        }
    }
}
