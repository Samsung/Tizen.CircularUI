using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Tizen;
using OpenQA.Selenium.Remote;
using System.Drawing;

namespace Appium.CircularShell.Tests
{
    public class TizenDriverApp : TizenDriver<TizenElement>
    {
        RemoteTouchScreen _touchScreen;

        public static TizenDriverApp CreateDriver(string server, string emulatorID, string appID)
        {
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("platformName", "Tizen");
            appiumOptions.AddAdditionalCapability("deviceName", emulatorID);
            appiumOptions.AddAdditionalCapability("appPackage", appID);
            return new TizenDriverApp(new Uri(server), appiumOptions);
        }

        public TizenDriverApp(Uri uri, AppiumOptions appiumOptions) : base(uri, appiumOptions)
        {
            _touchScreen = new RemoteTouchScreen(this);
        }


        public string GetText(string automationId)
        {
            return FindElementByAccessibilityId(automationId).Text;
        }

        public void SetText(string automationId, string text)
        {
            FindElementByAccessibilityId(automationId).SetAttribute("Text", text);
        }

        public void ClearText(string automationId)
        {
            FindElementByAccessibilityId(automationId).Clear();
        }

        public void ReplaceText(string automationId, string text)
        {
            FindElementByAccessibilityId(automationId).ReplaceValue(text);
        }

        public string GetAttribute(string automationId, string attribute)
        {
            return FindElementByAccessibilityId(automationId).GetAttribute(attribute);
        }

        public void SetAttribute(string automationId, string attribute, string value)
        {
            FindElementByAccessibilityId(automationId).SetAttribute(attribute, value);
        }

        public Size GetSize(string automationId)
        {
            return FindElementByAccessibilityId(automationId).Size;
        }

        public Point GetLocation(string automationId)
        {
            return FindElementByAccessibilityId(automationId).Location;
        }

        public bool GetDisplayed(string automationId)
        {
            return FindElementByAccessibilityId(automationId).Displayed;
        }

        public bool GetEnabled(string automationId)
        {
            return FindElementByAccessibilityId(automationId).Enabled;
        }

        public void Click(string automationId)
        {
            FindElementByAccessibilityId(automationId).Click();
        }

        public void Down(int x, int y)
        {
            _touchScreen.Down(x, y);
        }

        public void Up(int x, int y)
        {
            _touchScreen.Up(x, y);
        }

        public void Move(int x, int y)
        {
            _touchScreen.Move(x, y);
        }

        public void Flick(int speedX, int speedY)
        {
            _touchScreen.Flick(speedX, speedY);
        }

        public void ExecuteScript(string script)
        {
            ExecuteScript(script);
        }
    }
}
