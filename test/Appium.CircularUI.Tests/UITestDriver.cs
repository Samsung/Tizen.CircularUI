using System;
using System.IO;
using System.Drawing;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Tizen;
using OpenQA.Selenium;

using OpenQA.Selenium.Appium.Android;
using NUnit.Framework;
using OpenQA.Selenium.Appium.MultiTouch;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;

namespace Appium.UITests
{
    public sealed class UITestDriver // to communicate appium
    {
        const int DelayTime = 1000;
        const string Platform = "Tizen";

        static bool createFolder = false;
        static UITestDriver _instance;
        AppiumDriver<AppiumWebElement> _driver;
        RemoteTouchScreen _touchScreen;

        public static UITestDriver Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UITestDriver(Platform, "");
                }
                return _instance;
            }
        }

        public static string Profile { get; private set; }

        public RemoteTouchScreen TouchScreen
        {
            get
            {
                return _touchScreen;
            }
        }

        UITestDriver(string platform = "", string profile = "")
        {
            if (platform == "Tizen")
            {
                InitTizen(platform, profile);
            }
            else
            {
                Console.WriteLine($"Platform is not Tizen!!");
            }
        }

        void InitTizen(string platform, string profile)
        {
            AppiumOptions option = new AppiumOptions();

            option.AddAdditionalCapability("platformName", platform);
            option.AddAdditionalCapability("deviceName", "tizen-wearable");
            option.AddAdditionalCapability("appPackage", "org.tizen.example.WearableUIGallery.Tizen.Wearable");
            //option.AddAdditionalCapability("app", "org.tizen.example.WearableUIGallery.Tizen.Wearable-1.0.0.tpk");
            //option.AddAdditionalCapability("reboot", "true");
            //_driver = new TizenDriver<AppiumWebElement>(new Uri("http://127.0.0.1:4723/wd/hub"), option);
            _driver = new TizenDriver<AppiumWebElement>(new Uri("http://10.113.111.149:4723/wd/hub"), option);
            _touchScreen = new RemoteTouchScreen(_driver);
        }

        public void Quit()
        {
            _driver.Quit();
        }

        public void GoBack()
        {
            _driver.Navigate().Back();
            System.Threading.Thread.Sleep(1000);
        }

        public void GoHomePage()
        {
            var currentPage = GetAttribute<string>("MainPage", "CurrentPage");
            while (currentPage.IndexOf("TCListPage") == -1)
            {
                _driver.Navigate().Back();
                System.Threading.Thread.Sleep(1000);
                currentPage = GetAttribute<string>("MainPage", "CurrentPage");
            }

            System.Threading.Thread.Sleep(1000);
        }

        public void FindTC(string testName)
        {
            while (true) {
                ReadOnlyCollection<AppiumWebElement> resultByAccessibilityId = _driver.FindElementsByAccessibilityId(testName);
                if (resultByAccessibilityId.Count == 0)
                {
                    Flick(0, -80);
                }
                else
                {
                    var element = _driver.FindElementByAccessibilityId(testName) as TizenElement;
                    if (element != null)
                    {
                        element.Click();
                        System.Threading.Thread.Sleep(1000);
                        break;
                    }
                }
            }
        }

        public void Flick(int speedX, int speedY, int delay = DelayTime)
        {
            _touchScreen.Flick(speedX, speedY);
            System.Threading.Thread.Sleep(delay);
        }

        public void Click(int x, int y, int delay = DelayTime)
        {
            var touchAction = new TouchAction(_driver);
            touchAction.Press(x, y);
            touchAction.Wait(500);
            touchAction.Release();
            touchAction.Perform();
            System.Threading.Thread.Sleep(delay);
        }

        public void Click(string automationId, int delay = DelayTime)
        {
            _driver.FindElementByAccessibilityId(automationId).Click();
            System.Threading.Thread.Sleep(delay);
        }

        public void Drag(int startX, int startY, int endX, int endY, int delayTime = DelayTime)
        {
            _touchScreen.Down(startX, startY);
            System.Threading.Thread.Sleep(delayTime);
            _touchScreen.Move(endX, endX);
            System.Threading.Thread.Sleep(delayTime);
            _touchScreen.Up(endX, endX);
        }

        public void SetText(string automationId, string text, int delayTime = DelayTime)
        {
            _driver.FindElementByAccessibilityId(automationId).SetImmediateValue(text);
            System.Threading.Thread.Sleep(delayTime);
        }

        public T GetAttribute<T>(string automationId, string attribute, int delayTime = DelayTime)
        {
            System.Threading.Thread.Sleep(delayTime);
            var element = _driver.FindElementByAccessibilityId(automationId);
            if (element.Id == "-1")
            {
                return default(T);
            }

            var stringValue = element.GetAttribute(attribute);

            if (!String.IsNullOrEmpty(stringValue))
            {
                T value = (T)Convert.ChangeType(stringValue, typeof(T));
                return value;
            }

            return default(T);
        }

        public Point GetLocation(string automationId, int delayTime = DelayTime)
        {
            System.Threading.Thread.Sleep(delayTime);
            var element = _driver.FindElementByAccessibilityId(automationId);
            return element.Location;
        }

        public string GetText(string automationId, int delayTime = DelayTime)
        {
            System.Threading.Thread.Sleep(delayTime);
            AppiumWebElement element;
            try
            {
                element = _driver.FindElementByAccessibilityId(automationId);
            }
            catch(Exception ee)
            {
                return string.Format("Exception {0} ", ee);
            }

            return element.Text;
        }

        public Size GetSize(string automationId)
        {
            var element = _driver.FindElementByAccessibilityId(automationId);
            return element.Size;
        }

        public void SetAttribute(string automationId, string attribute, object value, int delayTime = DelayTime)
        {
            Console.WriteLine($"#### SetAttribute  automationId:{automationId},  attribute:{attribute}, value:{value}");
            var element = _driver.FindElementByAccessibilityId(automationId) as TizenElement;
            if (element != null)
            {
                element.SetAttribute(attribute, value.ToString());
                System.Threading.Thread.Sleep(delayTime);
            }
        }

        public void GetScreenshotAndSave(string imageName)
        {
            TizenDriver<AppiumWebElement> tizenDriver = _driver as TizenDriver<AppiumWebElement>;
            if (tizenDriver == null)
                return;

            var path = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            var imagePath = path + "/images/" + imageName;

            Screenshot screenshot = tizenDriver.GetScreenshot();

            Image image = TransformScreenshot(screenshot, new Rectangle(0, 0, 360, 360));

            image.Save(imagePath);
        }

        public bool CompareScreenshot(string imagePath)
        {
            TizenDriver<AppiumWebElement> tizenDriver = _driver as TizenDriver<AppiumWebElement>;
            if (tizenDriver == null)
                return false;

            //var path = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            var path = AppDomain.CurrentDomain.BaseDirectory;
            if (path.Contains("bin\\Debug\\"))
            {
                path = path.TrimEnd("bin\\Debug".ToCharArray());
            }

            if (!File.Exists(path + "\\temp"))
            {
                Directory.CreateDirectory(path + "\\temp");
            }
            var tempImagePath = path + "\\temp\\temp.png";

            Screenshot screenshot = tizenDriver.GetScreenshot();

            Image image = TransformScreenshot(screenshot, new Rectangle(0, 0, 360, 360));
            image.Save(tempImagePath);

            var orgImage = Image.FromFile(path + "\\images\\" + imagePath);
            var compareImage = Image.FromFile(tempImagePath);
            Image resultImage;

            var result = CompareImages(orgImage, compareImage, out resultImage);
            if (!createFolder)
            {
                if (!Directory.Exists(path + "\\images\\result"))
                {
                    Directory.CreateDirectory(path + "\\images\\result");
                }
                else
                {
                    Directory.Delete(path + "\\images\\result", true);
                    Directory.CreateDirectory(path + "\\images\\result");
                }
                createFolder = true;
            }

            if (!result)
            {
                var imageName = imagePath.Replace(".png", "");
                orgImage.Save(path + "\\images\\result\\" + imageName + "_expect.png");
                compareImage.Save(path + "\\images\\result\\" + imageName + "_actual.png");
                if (resultImage != null)
                {
                    resultImage.Save(path + "\\images\\result\\" + imageName + "_diff.png");
                }
            }

            image.Dispose();
            compareImage.Dispose();

            File.Delete(tempImagePath);

            return result;
        }

        public bool CompareImages(Image firstImage, Image secondImage, out Image resultImage)
        {
            resultImage = null;
            if (firstImage == null || secondImage == null)
            {
                throw new NullReferenceException("Images should not be null");
            }

            if (firstImage.Width != secondImage.Width || firstImage.Height != secondImage.Height)
            {
                return false;
            }

            bool result = true;
            Bitmap firstBitmap = new Bitmap(firstImage);
            Bitmap secondBitmap = new Bitmap(secondImage);
            Bitmap resultBitmap = new Bitmap(secondImage);

            BitmapData firstBitmapData = firstBitmap.LockBits(new Rectangle(0, 0, firstImage.Width, firstImage.Height), ImageLockMode.ReadOnly, firstImage.PixelFormat);
            BitmapData secondBitmapData = secondBitmap.LockBits(new Rectangle(0, 0, secondImage.Width, secondImage.Height), ImageLockMode.ReadOnly, secondImage.PixelFormat);
            BitmapData resultBitmapData = resultBitmap.LockBits(new Rectangle(0, 0, secondImage.Width, secondImage.Height), ImageLockMode.ReadOnly, secondImage.PixelFormat);

            int Depth = Image.GetPixelFormatSize(firstBitmap.PixelFormat);
            int size = firstBitmapData.Stride * firstBitmapData.Height;

            byte[] firstPixels = new byte[size];
            byte[] secondPixels = new byte[size];
            byte[] resultPixels = new byte[size];

            Marshal.Copy(firstBitmapData.Scan0, firstPixels, 0, size);
            Marshal.Copy(secondBitmapData.Scan0, secondPixels, 0, size);
            Marshal.Copy(resultBitmapData.Scan0, resultPixels, 0, size);

            int firstGrayScale = 0;
            int secondGrayScale = 0;
            int diff = 0;

            for (int i = 0; i < size; i += Depth / 8)
            {
                firstGrayScale = (int)((firstPixels[i] * 0.11) + (firstPixels[i + 1] * 0.59) + (firstPixels[i + 2] * 0.3));
                secondGrayScale = (int)((secondPixels[i] * 0.11) + (secondPixels[i + 1] * 0.59) + (secondPixels[i + 2] * 0.3));

                diff = firstGrayScale - secondGrayScale;

                if (Math.Abs(diff) > 3)
                {
                    if (resultPixels[i + 2] > 200)
                    {
                        resultPixels[i] = 255;
                        resultPixels[i + 1] = 0;
                        resultPixels[i + 2] = 0;
                    }
                    else
                    {
                        resultPixels[i] = 0;
                        resultPixels[i + 1] = 0;
                        resultPixels[i + 2] = 255;
                    }

                    result = false;
                }
            }

            Marshal.Copy(firstPixels, 0, firstBitmapData.Scan0, firstPixels.Length);
            Marshal.Copy(secondPixels, 0, secondBitmapData.Scan0, secondPixels.Length);
            Marshal.Copy(resultPixels, 0, resultBitmapData.Scan0, secondPixels.Length);

            firstBitmap.UnlockBits(firstBitmapData);
            secondBitmap.UnlockBits(secondBitmapData);
            resultBitmap.UnlockBits(resultBitmapData);

            if (!result)
            {
                resultImage = Image.FromHbitmap(resultBitmap.GetHbitmap());
            }

            return result;
        }

        public Image TransformScreenshot(Screenshot screenshot, Rectangle rectangle)
        {
            if (screenshot == null)
            {
                throw new NullReferenceException("Screenshot should not be null");
            }
            MemoryStream imageStream = new MemoryStream(screenshot.AsByteArray);
            Image screenshotImage = Image.FromStream(imageStream);

            Bitmap bmpImage = new Bitmap(screenshotImage);
            return bmpImage.Clone(rectangle, bmpImage.PixelFormat);
        }

        public void CheckScreenshot(string image)
        {
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(true, CompareScreenshot(image), $"CompareToScreenshot result should be true. {image}");
        }
    }
}