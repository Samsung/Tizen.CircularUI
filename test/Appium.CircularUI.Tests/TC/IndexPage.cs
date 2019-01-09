using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(11)]
    public class IndexPage : TestTemplate
    {
        static string AddMoveTestName = "Add/Move";
        static string TemplateTestName = "Template";

        [Test]
        public void AddMoveTest()
        {
            var speedX = SpeedX * 2;
            Driver.FindTC(AddMoveTestName);

            Driver.Flick(speedX, 0);
            Driver.Flick(speedX, 0);
            Driver.Flick(speedX, 0);
            Driver.Flick(speedX, 0);
            Driver.Click("addButton");
            Driver.Flick(speedX, 0);
            var result = Driver.GetText("label0");
            var expect = "Added Page(0)";
            Assert.AreEqual(expect, result);
        }

        [Test]
        public void TemplateTest()
        {
            var speedX = SpeedX * 2;
            Driver.FindTC(TemplateTestName);

            Driver.Flick(speedX, 0);
            Driver.Flick(speedX, 0);
#if WATCH_DEVICE
            var image = "IndexPopup_Template.png";
            Driver.CheckScreenshot(image);
#endif
        }
    }
}
