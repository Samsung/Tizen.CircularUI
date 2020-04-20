using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(5)]
    public class PopupEntry : TestTemplate
    {
        [Test]
        public void PopupEntryTextColorTest()
        {
            Driver.RunTC(this.GetType().Name);

            var elementId = "popupEntry1";
            var result = Driver.GetAttribute<string>(elementId, "TextColor");
#if EMUL_50
            var expect = "[Color: A=1, R=0, G=0, B=1, Hue=0.6666666865348816, Saturation=1, Luminosity=0.5]";
#else
            var expect = "[Color: A=1, R=0, G=0, B=1, Hue=0.666666686534882, Saturation=1, Luminosity=0.5]";
#endif
            Assert.AreEqual(expect, result);
        }

        [Test]
        public void PopupEntryTextTest()
        {
            Driver.RunTC(this.GetType().Name);

            var entryId = "popupEntry2";
            var addstring = "TEST";
            string before = Driver.GetText(entryId);

            Driver.InputText(entryId, addstring);

            string after = Driver.GetText(entryId);
            Assert.AreEqual(before + addstring, after);
        }

        [Test]
        public void PopupEntryPasswordTest()
        {
            Driver.RunTC(this.GetType().Name);

            var entryId = "popupEntry3";
            var addstring = "1234";

            string before = Driver.GetText(entryId);

            Driver.InputText(entryId, addstring);

            string after = Driver.GetText(entryId);
            Assert.AreEqual(before + addstring, after);
        }

        [Test]
        public void PopupEntryBackgroundColorTest()
        {
            Driver.RunTC(this.GetType().Name);

            var elementId = "popupEntry4";

            var result = Driver.GetAttribute<string>(elementId, "BackgroundColor");
            var expect = "[Color: A=1, R=0, G=0.545098066329956, B=0.545098066329956, Hue=0.5, Saturation=1, Luminosity=0.272549033164978]";
            Assert.AreEqual(expect, result);

            var result2 = Driver.GetAttribute<string>(elementId, "PopupBackgroundColor");
            var expect2 = "[Color: A=1, R=0, G=0.545098066329956, B=0.545098066329956, Hue=0.5, Saturation=1, Luminosity=0.272549033164978]";

            Assert.AreEqual(expect2, result2);
        }
    }
}
