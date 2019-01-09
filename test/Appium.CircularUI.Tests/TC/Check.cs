using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(15)]
    public class Check : TestTemplate
    {

        [Test]
        public void CheckTest()
        {
            Driver.Click("default");
            Driver.Click("onOff");
            Driver.Click("small");

            var isToggled = Driver.GetAttribute<bool>("default", "IsToggled");
            Assert.True(isToggled, "Check(default).IsToggled should be true, but got " + isToggled);

            var isToggled2 = Driver.GetAttribute<bool>("onOff", "IsToggled");
            Assert.False(isToggled2, "Check(onOff).IsToggled should be false, but got " + isToggled2);

            var isToggled3 = Driver.GetAttribute<bool>("small", "IsToggled");
            Assert.True(isToggled3, "Check(small).IsToggled should be true, but got " + isToggled3);


            Driver.Flick(0, SpeedY);
            Driver.Flick(0, SpeedY);


            Driver.Click("defaultColorBlue");
            Driver.Click("defaultColorRed");
            Driver.Click("defaultColorGreen");

            var result = Driver.GetAttribute<string>("defaultColorBlue", "Color");
            var expect = "[Color: A=1, R=0, G=0, B=1, Hue=0.666666686534882, Saturation=1, Luminosity=0.5]";
            Assert.AreEqual(expect, result);

            var result2 = Driver.GetAttribute<string>("defaultColorRed", "Color");
            var expect2 = "[Color: A=1, R=1, G=0, B=0, Hue=1, Saturation=1, Luminosity=0.5]";
            Assert.AreEqual(expect2, result2);

            Driver.Flick(0, SpeedY);
            Driver.Flick(0, SpeedY);

            Driver.Click("onOffColorBlue");
            Driver.Click("onOffColorGreen");
            var result4 = Driver.GetAttribute<string>("onOffColorBlue", "Color");
            var expect4 = "[Color: A=1, R=0, G=0, B=1, Hue=0.666666686534882, Saturation=1, Luminosity=0.5]";
            Assert.AreEqual(expect4, result4);
        }
    }
}
