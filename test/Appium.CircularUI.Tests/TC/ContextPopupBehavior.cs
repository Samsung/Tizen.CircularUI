using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(8)]
    public class ContextPopupBehavior : TestTemplate
    {
        static string PositionTestName = "Position";
        static string CommandParameterTestName = "CommandParameter";
        static string AttachDettachTestName = "Attach/Detach/Visibility";

        [Test]
        public void PositionTest()
        {
            var labelPostionElementId = "labelPositionOption";
            var labelOffsetYElementId = "labelOffsetY";
            var changeOptionBtnId = "changePositionOption";
            var changeOffsetBtnId = "changeOffset";

            Driver.FindTC(PositionTestName);

            var option1 = Driver.GetText(labelPostionElementId);

            Driver.Click(changeOptionBtnId);
            Driver.Click(320, 180);  //click outside of ctx pop-up

            var option2 = Driver.GetText(labelPostionElementId);
            Assert.AreNotEqual(option1, option2);

            Driver.Click(changeOptionBtnId);
            Driver.Click(320, 180);
            var expacted = "Relative";
            var result = Driver.GetText(labelPostionElementId);
            Assert.AreEqual(expacted, result);

            var offsetY1 = Driver.GetText(labelOffsetYElementId);
            Driver.Click(changeOffsetBtnId);
            Driver.Click(320, 180);
            var offsetY2 = Driver.GetText(labelOffsetYElementId);
            Assert.AreNotEqual(offsetY1, offsetY2);
        }

        [Test]
        public void CommandParameterTest()
        {
            var labelElementId = "label";
            var checkBtnId = "check";

            Driver.FindTC(CommandParameterTestName);

            var result1 = Driver.GetText(labelElementId);
            Driver.Click(checkBtnId, 3000);
            Driver.Click(checkBtnId);

            var result2 = Driver.GetText(labelElementId);
            Assert.AreNotEqual(result1, result2);
        }

        [Test]
        public void AttachDetachTest()
        {
            var attachBtnId = "attach";
            var detachBtnId = "detach";
            var visibleBtnId = "visible";

            Driver.FindTC(AttachDettachTestName);

            Driver.Click(attachBtnId);
            Driver.Click(visibleBtnId);

            var image = "CtxPopup_AttachVisible.png";
            Driver.CheckScreenshot(image);
            Driver.Click(180, 200);

            Driver.Click(detachBtnId);
            Driver.Click(visibleBtnId);

            var image2 = "CtxPopup_DetachVisible.png";
            Driver.CheckScreenshot(image2);
            Driver.Click(180, 200);
        }
    }
}
