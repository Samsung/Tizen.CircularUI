namespace Appium.CircularShell.Tests
{
    public class AppInitializer
    {
        static TizenDriverApp s_instance;
        public static TizenDriverApp GetDriver()
        {
            if (s_instance == null)
            {
                //string address = "http://10.113.165.80:4723/wd/hub";
                string address = "http://127.0.0.1:4723/wd/hub";
                s_instance = TizenDriverApp.CreateDriver(address, "emulator-26101", "org.tizen.example.CircularShellGallery");
            }
            return s_instance;
        }
    }
}
