using ElmSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Tizen.Applications;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Tizen;

namespace Xamarin.Forms.CircularUI.Tizen.Watchface
{
    public class FormsWatchface : WatchApplication
    {
        Application _application;
        bool _isInitialStart;

        public FormsWatchface()
        {
            Platform.Tizen.Forms.SetFlags("LightweightPlatform_Experimental");
            _isInitialStart = true;
        }

        public void LoadWatchface(Application application)
        {
            if (application == null) throw new ArgumentException("application");
            _application = application;
            Application.Current = application;
            application.SendStart();

            var rootView = application.MainPage.CreateEvasObject(Window);
            OnRootViewUpdated(rootView);

            application.PropertyChanging += (s, e) =>
            {
                if (e.PropertyName == nameof(Application.MainPage))
                {
                    IDisposable obj = application.MainPage.Platform as IDisposable;
                    obj?.Dispose();
                }
            };
            application.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Application.MainPage))
                {
                    OnRootViewUpdated(application.MainPage.CreateEvasObject(Window));
                }
            };
        }

        public void Run()
        {
            Run(System.Environment.GetCommandLineArgs());
        }

        protected virtual void OnRootViewUpdated(EvasObject rootView)
        {
            rootView.Geometry = new Rect(0, 0, Window.ScreenSize.Width, Window.ScreenSize.Height);
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            Application.ClearCurrent();
            Device.Info.CurrentOrientation = GetDeviceOrientation();
            Window.RotationChanged += (sender, e) => Device.Info.CurrentOrientation = GetDeviceOrientation();
            Window.BackButtonPressed += (sender, e) => _application?.MainPage?.SendBackButtonPressed();
        }

        protected override void OnTerminate()
        {
            base.OnTerminate();
            IDisposable obj = _application.MainPage.Platform as IDisposable;
            obj?.Dispose();
        }

        protected override void OnAppControlReceived(AppControlReceivedEventArgs e)
        {
            base.OnAppControlReceived(e);
            if (!_isInitialStart && _application != null)
            {
                _application.SendResume();
            }
            _isInitialStart = false;
        }

        protected override void OnPause()
        {
            base.OnPause();
            if (_application != null)
            {
                _application.SendSleepAsync();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (_application != null)
            {
                _application.SendResume();
            }
        }

        Xamarin.Forms.Internals.DeviceOrientation GetDeviceOrientation()
        {
            Xamarin.Forms.Internals.DeviceOrientation orientation = Xamarin.Forms.Internals.DeviceOrientation.Other;
            var isPortraitDevice = Xamarin.Forms.Platform.Tizen.Forms.NaturalOrientation.IsPortrait();
            switch (Window.Rotation)
            {
                case 0:
                case 180:
                    orientation = isPortraitDevice ? Xamarin.Forms.Internals.DeviceOrientation.Portrait : Xamarin.Forms.Internals.DeviceOrientation.Landscape;
                    break;

                case 90:
                case 270:
                    orientation = isPortraitDevice ? Xamarin.Forms.Internals.DeviceOrientation.Landscape : Xamarin.Forms.Internals.DeviceOrientation.Portrait;
                    break;
            }
            return orientation;
        }
    }
}
