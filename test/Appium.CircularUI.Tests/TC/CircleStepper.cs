using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(10)]
    public class CircleStepper : TestTemplate
    {
        static string LabelFormatTestName = "LabelFormat";

        [Test]
        public void LabelFormatTest()
        {
            Driver.FindTC(LabelFormatTestName);

            var elementId = "stepper";
            var expect = "%1.1f";
            var result = Driver.GetAttribute<string>(elementId, "LabelFormat");
            Assert.AreEqual(expect, result);
        }
    }
}
