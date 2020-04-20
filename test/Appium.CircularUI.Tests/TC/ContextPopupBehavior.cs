using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(8)]
    public class ContextPopupBehavior : TestTemplate
    {
        static string PositionTestName = "ContextPopupBehavior/Position";
        static string CommandParameterTestName = "ContextPopupBehavior/CommandParameter";
        static string AttachDettachTestName = "ContextPopupBehavior/Attach/Detach/Visibility";

        [Test]
        public void PositionTest()
        {
            var labelPostionElementId = "labelPositionOption";
            var labelOffsetYElementId = "labelOffsetY";
            var changeOptionBtnId = "changePositionOption";
            var changeOffsetBtnId = "changeOffset";

            Driver.RunTC(PositionTestName);

            var option1 = Driver.GetText(labelPostionElementId);

            FindAndClick(changeOptionBtnId);
            Driver.Click(320, 180);  //click outside of ctx pop-up

            var option2 = Driver.GetText(labelPostionElementId);
            Assert.AreNotEqual(option1, option2);

            FindAndClick(changeOptionBtnId);
            Driver.Click(320, 180);
            var expacted = "Relative";
            var result = Driver.GetText(labelPostionElementId);
            Assert.AreEqual(expacted, result);

            var offsetY1 = Driver.GetText(labelOffsetYElementId);
            FindAndClick(changeOffsetBtnId);
            Driver.Click(320, 180);
            var offsetY2 = Driver.GetText(labelOffsetYElementId);
            Assert.AreNotEqual(offsetY1, offsetY2);
        }

        [Test]
        public void CommandParameterTest()
        {
            var labelElementId = "label";
            var checkBtnId = "check";

            Driver.RunTC(CommandParameterTestName);

            var result1 = Driver.GetText(labelElementId);
            Driver.Click(checkBtnId, 3000);
            FindAndClick(checkBtnId);

            var result2 = Driver.GetText(labelElementId);
            Assert.AreNotEqual(result1, result2);
        }

        [Test]
        public void AttachDetachTest()
        {
            var attachBtnId = "attach";
            var detachBtnId = "detach";
            var visibleBtnId = "visible";

            Driver.RunTC(AttachDettachTestName);

            FindAndClick(attachBtnId);
            FindAndClick(visibleBtnId);
#if WATCH_DEVICE
            var image = "CtxPopup_AttachVisible.png";
            Driver.CheckScreenshot(image);
#endif
            Driver.Click(180, 200);
            FindAndClick(detachBtnId);
            FindAndClick(visibleBtnId);
#if WATCH_DEVICE
            var image2 = "CtxPopup_DetachVisible.png";
            Driver.CheckScreenshot(image2);
#endif
            Driver.Click(180, 200);
        }
    }
}
