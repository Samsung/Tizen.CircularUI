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
            Driver.FindTC(AddMoveTestName);

            Driver.Flick(-120, 0);
            Driver.Flick(-120, 0);
            Driver.Flick(-120, 0);
            Driver.Click("moveButton");

            var image = "IndexPopup_Move.png";
            Driver.CheckScreenshot(image);

            Driver.Flick(-120, 0);
            Driver.Flick(-120, 0);
            Driver.Flick(-120, 0);
            Driver.Flick(-120, 0);
            Driver.Click("addButton");

            Driver.Flick(-120, 0);
            var result = Driver.GetText("label0");
            var expect = "Added Page(0)";
            Assert.AreEqual(expect, result);
        }

        [Test]
        public void TemplateTest()
        {
            Driver.FindTC(TemplateTestName);

            Driver.Flick(-120, 0);
            Driver.Flick(-120, 0);
            var image = "IndexPopup_Template.png";
            Driver.CheckScreenshot(image);
        }
    }
}
