using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(7)]
    public class CircleStackLayout : TestTemplate
    {
        [Test]
        public void CircleStackLayoutTest()
        {
            Driver.RunTC(this.GetType().Name);
#if EMUL_40
            var speedX = SpeedX * 3;
#else
            var speedX = SpeedX * 2;
#endif
            for (int i = 0; i < 18; i++)
            {
                Driver.Flick(speedX, 0);
            }
        }
    }
}
