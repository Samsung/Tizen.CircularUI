using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appium
{
    public static class Config
    {
        #region fields
        public const string PLATFORM = "Tizen";
        //public const string APPIUM_SERVER_URI = "http://192.168.0.49:4723/wd/hub"; //Please insert your appium server address
        public const string APPIUM_SERVER_URI = "http://10.113.165.69:4723/wd/hub"; 

        public const string APP_PACKAGE_NAME = "org.tizen.example.WearableUIGallery.Tizen.Wearable";
        public const string APP_NAME = "org.tizen.example.WearableUIGallery.Tizen.Wearable-1.0.0.tpk";
        public const string DEVICE_NAME = "emulator-26111";

        public const int SPEEDX_EMUL_40 = -45;
        public const int SPEEDY_EMUL_40 = -35;
        public const int SPEEDX_EMUL_50 = -100;
        public const int SPEEDY_EMUL_50 = -100;
        public const int SPEEDX_GALAXY_WATCH = -100;
        public const int SPEEDY_GALAXY_WATCH = -100;
        #endregion
    }
}
