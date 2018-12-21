using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(10)]
    public class CircleStepper : TestTemplate
    {
        static string CircleStepperTestName = "Grid";
        static string LabelFormatTestName = "LabelFormat";

        [Test]
        public void CircleStepperTest()
        {
            Driver.FindTC(CircleStepperTestName);

            var elementId = "stepperM";
            var expect = "[Color: A=1, R=0, G=0, B=1, Hue=0.666666686534882, Saturation=1, Luminosity=0.5]";
            var result = Driver.GetAttribute<string>(elementId, "MarkerColor");
            Assert.AreEqual(expect, result);
        }

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
