using NUnit.Framework;

namespace Appium.UITests
{
    [TestFixture, Order(1)]
    public class ListView : TestTemplate
    {
        static string NoFishEyeHeaderTestName = "ListView/NoFishEyeHeaderList";
        static string CircleListBehaviorTestName = "ListView/CircleListBehavior";
        static string GroupListTestName = "ListView/GroupList";
        static string CircleListViewTestName = "ListView/CircleListView";
        static string ViewCellTestName = "ListView/ViewCell";
        static string HeaderWithGroupTestName = "ListView/HeaderWithGroup";
        static string HeaderWithoutGroupTestName = "ListView/HeaderWithoutGroup";
        static string ChangeBarColorTestName = "ListView/ChangeBarColor";

        [Test]
        public void NoFishEyeHeaderTest()
        {
            Driver.RunTC(NoFishEyeHeaderTestName);

#if WATCH_DEVICE
            System.Threading.Thread.Sleep(3000);
            var image = "CircleListView_NoFishEyeHeaderTest.png";
            Driver.CheckScreenshot(image);
#else
            Driver.Flick(0, SpeedY * 2);
            Driver.Flick(0, SpeedY * 2);
#endif
        }

        [Test]
        public void CircleListBehaviorTest()
        {
            Driver.RunTC(CircleListBehaviorTestName);

            Driver.Flick(0, SpeedY * 2, 3000);
            Driver.Click(30, 180);

            Driver.Flick(0, SpeedY * 2, 3000);
            Driver.Flick(0, SpeedY * 2, 3000);

            var text = Driver.GetText("TestItem4");
            Assert.AreEqual(text, "TestItem4");

            Driver.Click(350, 180);
            Driver.Click(350, 180);

            var text2 = Driver.GetText("TestItem3");
            Assert.AreNotEqual(text2, "TestItem3");
        }

        [Test]
        public void GroupListTest()
        {
            Driver.RunTC(GroupListTestName);

#if WATCH_DEVICE
            System.Threading.Thread.Sleep(3000);
            var image = "CircleListView_GroupListTest.png";
            Driver.CheckScreenshot(image);
#else
            Driver.Flick(0, SpeedY * 2);
            Driver.Flick(0, SpeedY * 2);
#endif
        }

        [Test]
        public void CircleListViewTest()
        {
            Driver.RunTC(CircleListViewTestName);

#if WATCH_DEVICE
            System.Threading.Thread.Sleep(3000);
            var image = "CircleListView.png";
            Driver.CheckScreenshot(image);
#else
            Driver.Flick(0, SpeedY * 2);
            Driver.Flick(0, SpeedY * 2);
#endif
        }


        [Test]
        public void ViewCellTest()
        {
            Driver.RunTC(ViewCellTestName);

#if WATCH_DEVICE
            System.Threading.Thread.Sleep(3000);
            var image = "CircleListView_ViewCell.png";
            Driver.CheckScreenshot(image);
#else
            Driver.Flick(0, SpeedY * 2);
            Driver.Flick(0, SpeedY * 2);
#endif
        }

        [Test]
        public void HeaderWithGroupTest()
        {
            Driver.RunTC(HeaderWithGroupTestName);

#if WATCH_DEVICE
            System.Threading.Thread.Sleep(3000);
            var image = "CircleListView_HeaderWithGroup.png";
            Driver.CheckScreenshot(image);
#else
            Driver.Flick(0, SpeedY * 2);
            Driver.Flick(0, SpeedY * 2);
#endif
        }

        [Test]
        public void HeaderWithoutGroupTest()
        {
            Driver.RunTC(HeaderWithoutGroupTestName);

#if WATCH_DEVICE
            System.Threading.Thread.Sleep(3000);
            var image = "CircleListView_HeaderWithoutGroup.png";
            Driver.CheckScreenshot(image);
#else
            Driver.Flick(0, SpeedY * 2);
            Driver.Flick(0, SpeedY * 2);
#endif
        }

        [Test]
        public void ChangeBarColorTest()
        {
            Driver.RunTC(ChangeBarColorTestName);
            var listId = "list";

            var result = Driver.GetAttribute<string>(listId, "BarColor");
            var expect = "[Color: A=1, R=1, G=0, B=0, Hue=1, Saturation=1, Luminosity=0.5]";
            Assert.AreEqual(expect, result);

        }
    }
}
