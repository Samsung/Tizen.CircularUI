/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using ElmSharp;
using Tizen.Applications;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Tizen;
using NSystem = System;
using XApplication = Xamarin.Forms.Application;
using XForms = Xamarin.Forms.Forms;
using ERect = ElmSharp.Rect;

namespace Tizen.Wearable.CircularUI.Forms.Renderer.Watchface
{
    public class FormsWatchface : WatchApplication
    {
        XApplication _application;
        bool _isInitialStart;

        public FormsWatchface()
        {
            XForms.SetFlags("LightweightPlatform_Experimental");
            _isInitialStart = true;
        }

        public void LoadWatchface(XApplication application)
        {
            if (application == null) throw new ArgumentException("application");
            _application = application;
            XApplication.Current = application;
            application.SendStart();

            var rootView = application.MainPage.CreateEvasObject(Window);
            OnRootViewUpdated(rootView);

            application.PropertyChanging += (s, e) =>
            {
                if (e.PropertyName == nameof(XApplication.MainPage))
                {
                    Platform.GetRenderer(application?.MainPage)?.Dispose();
                }
            };
            application.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(XApplication.MainPage))
                {
                    OnRootViewUpdated(application.MainPage.CreateEvasObject(Window));
                }
            };
        }

        public void Run()
        {
            Run(NSystem.Environment.GetCommandLineArgs());
        }

        protected virtual void OnRootViewUpdated(EvasObject rootView)
        {
            rootView.Geometry = new ERect(0, 0, Window.ScreenSize.Width, Window.ScreenSize.Height);
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            XApplication.ClearCurrent();
            Device.Info.CurrentOrientation = GetDeviceOrientation();
            Window.RotationChanged += (sender, e) => Device.Info.CurrentOrientation = GetDeviceOrientation();
            Window.BackButtonPressed += (sender, e) => _application?.MainPage?.SendBackButtonPressed();
        }

        protected override void OnTerminate()
        {
            base.OnTerminate();
            Platform.GetRenderer(_application?.MainPage)?.Dispose();
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
            Xamarin.Forms.Internals.DeviceOrientation orientation;
            var isPortraitDevice = XForms.NaturalOrientation.IsPortrait();
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
                default:
                    orientation = Xamarin.Forms.Internals.DeviceOrientation.Other;
                    break;
            }
            return orientation;
        }
    }
}
