using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(11)]
    public class IndexPage : TestTemplate
    {
        static string AddMoveTestName = "IndexPage/Add/Move";
        static string TemplateTestName = "IndexPage/Template";

        [Test]
        public void AddMoveTest()
        {
            var speedX = SpeedX * 2;
            Driver.RunTC(AddMoveTestName);

            Driver.Flick(speedX, 0);
            Driver.Flick(speedX, 0);
            Driver.Flick(speedX, 0);
            Driver.Flick(speedX, 0);
            FindAndClick("addButton");
            Driver.Flick(speedX, 0);
            var result = Driver.GetText("label0");
            var expect = "Added Page(0)";
            Assert.AreEqual(expect, result);
        }

        [Test]
        public void TemplateTest()
        {
            var speedX = SpeedX * 2;
            Driver.RunTC(TemplateTestName);

            Driver.Flick(speedX, 0);
            Driver.Flick(speedX, 0);
#if WATCH_DEVICE
            var image = "IndexPopup_Template.png";
            Driver.CheckScreenshot(image);
#endif
        }
    }
}
