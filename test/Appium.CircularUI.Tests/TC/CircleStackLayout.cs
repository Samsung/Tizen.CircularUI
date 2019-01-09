using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(7)]
    public class CircleStackLayout : TestTemplate
    {
        [Test]
        public void CircleStackLayoutTest()
        {
            var speedX = SpeedX * 2;
            for (int i = 0; i < 18; i++)
            {
                Driver.Flick(speedX, 0);
            }
        }
    }
}
