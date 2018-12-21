using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(7)]
    public class CircleStackLayout : TestTemplate
    {
        [Test]
        public void CircleStackLayoutTest()
        {
            var image1 = "CircleStackLayout_VerticalLabel.png";
            Driver.CheckScreenshot(image1);

            Driver.Flick(-120, 0);
            Driver.Flick(-120, 0);

            var image2 = "CircleStackLayout_Vertical.png";
            Driver.CheckScreenshot(image2);

            Driver.Flick(-120, 0);
            Driver.Flick(-120, 0);
            Driver.Flick(-120, 0);
            Driver.Flick(-120, 0);
            Driver.Flick(-120, 0);
            Driver.Flick(-120, 0);
            Driver.Flick(-120, 0);

            var image3 = "CircleStackLayout_Horizontal.png";
            Driver.CheckScreenshot(image3);

        }
    }
}
