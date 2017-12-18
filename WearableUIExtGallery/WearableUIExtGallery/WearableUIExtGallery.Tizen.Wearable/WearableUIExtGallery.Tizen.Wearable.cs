using System;

namespace WearableUIExtGallery.Tizen.Wearable
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            Xamarin.Forms.CircularUI.Tizen.CircularUI.Init();
            app.Run(args);
        }
    }
}
