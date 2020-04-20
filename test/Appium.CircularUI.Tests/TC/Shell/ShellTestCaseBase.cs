
namespace Appium.UITests.Shell
{
    public abstract class ShellTestCaseBase : TestTemplate
    {
        public string TCUri { get; }

        protected ShellTestCaseBase(string uri)
        {
            TCUri = uri;
        }

        protected override void TestSetUp()
        {
            var IsFlyoutEnabled = Driver.GetAttribute<string>("shell", "FlyoutBehavior");
            while (IsFlyoutEnabled != "Flyout")
            {
                Driver.Click("FlyoutSwitch");
                IsFlyoutEnabled = Driver.GetAttribute<string>("shell", "FlyoutBehavior");
            }

            Driver.RunTC(TCUri);
        }

        public void GoBack()
        {
            Driver.GoBack();
        }
    }
}
