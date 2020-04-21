using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(16)]
    public class Radio : TestTemplate
    {
        static string StackLayoutTestName = "Radio/StackLayout";
        static string ListViewTestName = "Radio/ListView";

        [Test]
        public void StackLayoutTest()
        {
            Driver.RunTC(StackLayoutTestName);

            Driver.Click("radioValueVibrator", 3000);
            FindAndClick("radioValueMute");

            var result = Driver.GetText("labelMode");
            var expect = "SoundMode:Mute";
            Assert.AreEqual(expect, result);

            Driver.Flick(0, SpeedY);
            Driver.Flick(0, SpeedY);
            FindAndClick("radioValueStrong");

            var result2 = Driver.GetText("labelStrength");
            var expect2 = "Vib strength:Strong";
            Assert.AreEqual(expect2, result2);

            var resultColor = Driver.GetAttribute<string>("radioValueStrong", "Color");
#if EMUL_50
            var expectColor = "[Color: A=1, R=0, G=0, B=1, Hue=0.6666666865348816, Saturation=1, Luminosity=0.5]";
#else
            var expectColor = "[Color: A=1, R=0, G=0, B=1, Hue=0.666666686534882, Saturation=1, Luminosity=0.5]";
#endif
            Assert.AreEqual(expectColor, resultColor);

        }

        [Test]
        public void ListViewTest()
        {
            Driver.RunTC(ListViewTestName);

            FindAndClick("NoOff");
            Driver.Flick(0, SpeedY);
#if EMUL_50
            FindAndClick("30s");
            Driver.Flick(0, SpeedY);
            FindAndClick("5m");
            Driver.Flick(0, SpeedY);
            FindAndClick("10m");
#else
            FindAndClick("15s");
            Driver.Flick(0, SpeedY);
            FindAndClick("30s");
            Driver.Flick(0, SpeedY);
            FindAndClick("1m");
#endif
        }
    }
}
