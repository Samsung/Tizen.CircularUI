using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Threading;

namespace Appium.UITests
{
    public class TestTemplate
    {
        readonly int _screenHeight = 360;
        readonly int _delayTime = 1000;

        readonly string MainPageUri = "//Main/TestRun/MainPage";

        public int SpeedX;
        public int SpeedY;

        public UITestDriver Driver;

        [OneTimeSetUp]
        public void Init()
        {
            Driver = UITestDriver.Instance;
            SpeedX = Driver.FlickSpeedX;
            SpeedY = Driver.FlickSpeedY;
        }

        [SetUp]
        protected virtual void TestSetUp()
        {
            var IsFlyoutEnabled = Driver.GetAttribute<string>("shell", "FlyoutBehavior");
            while (IsFlyoutEnabled != "Disabled")
            {
                Driver.Click("FlyoutSwitch");
                IsFlyoutEnabled = Driver.GetAttribute<string>("shell", "FlyoutBehavior");
            }
        }

        [TearDown]
        protected virtual void TestCleanUp()
        {
            GoBackToMainPage();
        }

        protected virtual void GoBackToMainPage()
        {
            var uri = Driver.GetAttribute<string>("shell", "CurrentUri");
            while (uri != MainPageUri)
            {
                Driver.GoBack();
                Thread.Sleep(_delayTime);
                uri = Driver.GetAttribute<string>("shell", "CurrentUri");
            }
        }

        public void FindAndClick(string automationId)
        {
            var element = Driver.GetElement(automationId);
            var y = element.Location.Y;
            var height = element.Size.Height;
            var retry = 10;

            if ((y < 0))
            {
                while ((y < 0) && (retry > 0))
                {
                    Driver.Flick(0, SpeedY * (-1));
                    Thread.Sleep(_delayTime);
                    y = element.Location.Y;
                    retry--;
                }
            }
            else if ((y + height) > _screenHeight)
            {
                while (((y + height) > _screenHeight) && (retry > 0))
                {
                    Driver.Flick(0, SpeedY);
                    Thread.Sleep(_delayTime);
                    y = element.Location.Y;
                    height = element.Size.Height;
                    retry--;
                }
            }
            element.Click();
            Thread.Sleep(_delayTime);
        }
    }
}
