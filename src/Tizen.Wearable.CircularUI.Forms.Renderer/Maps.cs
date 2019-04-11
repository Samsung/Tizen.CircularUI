using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Tizen.Wearable.CircularUI.Forms.Renderer.Map;
using Xamarin.Forms.Platform.Tizen;

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public static class Maps
    {
        static MapService _mapService = null;

        internal static bool IsInitialized { get; private set; }

        internal static MapService MapService
        {
            get
            {
                Debug.Assert(_mapService != null, "MapSerivce is not initialized!");
                return _mapService;
            }
        }

        /// <summary>
        /// Initialize Map service with authentication key value.
        /// </summary>
        public static void Init(string apiKey, GoogleSignedType signType = GoogleSignedType.ApiKey, string channel = null, string privateKey = null)
        {
            if (IsInitialized)
                return;

            _mapService = new MapService(apiKey, signType, channel, privateKey);
            GeocodingBackend.Register();
            IsInitialized = true;
        }
    }
}
