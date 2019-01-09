using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(16)]
    public class Radio : TestTemplate
    {
        static string StackLayoutTestName = "StackLayout";
        static string ListViewTestName = "ListView";

        [Test]
        public void StackLayoutTest()
        {
            Driver.FindTC(StackLayoutTestName);

            Driver.Click("radioValueVibrator", 3000);
            Driver.Click("radioValueMute");

            var result = Driver.GetText("labelMode");
            var expect = "SoundMode:Mute";
            Assert.AreEqual(expect, result);

            Driver.Flick(0, SpeedY);
            Driver.Flick(0, SpeedY);
            Driver.Click("radioValueStrong");

            var result2 = Driver.GetText("labelStrength");
            var expect2 = "Vib strength:Strong";
            Assert.AreEqual(expect2, result2);

            var resultColor = Driver.GetAttribute<string>("radioValueStrong", "Color");
            var expectColor = "[Color: A=1, R=0, G=0, B=1, Hue=0.666666686534882, Saturation=1, Luminosity=0.5]";
            Assert.AreEqual(expectColor, resultColor);
        }

        [Test]
        public void ListViewTest()
        {
            Driver.FindTC(ListViewTestName);

            Driver.Click("NoOff");
            Driver.Flick(0, SpeedY);
            Driver.Click("15s");
            Driver.Flick(0, SpeedY);
            Driver.Click("30s");
            Driver.Flick(0, SpeedY);
            Driver.Click("1m");
        }
    }
}
