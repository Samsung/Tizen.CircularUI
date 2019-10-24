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
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Tizen.Applications;
using ElmSharp;
using XApplication = Xamarin.Forms.Application;
using XPropertyChangingEventArgs = Xamarin.Forms.PropertyChangingEventArgs;
using XPropertyChangingEventHandler = Xamarin.Forms.PropertyChangingEventHandler;

namespace Tizen.Wearable.CircularUI.Forms.Renderer.Widget
{
    public abstract class FormsWidgetBase : WidgetBase
    {
        XApplication _application;

        public override void OnCreate(Bundle content, int w, int h)
        {
            base.OnCreate(content, w, h);
            XApplication.ClearCurrent();
        }

        public override void OnPause()
        {
            base.OnPause();
            if (_application != null)
            {
                _application.SendSleepAsync();
            }
        }

        public override void OnResume()
        {
            base.OnResume();
            if (_application != null)
            {
                _application.SendResume();
            }
        }

        public override void OnDestroy(WidgetDestroyType reason, Bundle content)
        {
            base.OnDestroy(reason, content);
            Platform.GetRenderer(_application?.MainPage)?.Dispose();
        }

        public void LoadApplication(XApplication application)
        {
            if (Window == null)
            {
                throw new NullReferenceException("MainWindow is not prepared, This method should be called in OnCreated().");
            }

            if (application == null)
            {
                throw new ArgumentException("application cannot be null.");
            }
            _application = application;
            XApplication.Current = application;
            application.SendStart();

            var rootView = application.MainPage.CreateEvasObject(Window);
            OnRootViewUpdated(rootView);

            application.PropertyChanging += new XPropertyChangingEventHandler(AppOnPropertyChanging);
            application.PropertyChanged += new PropertyChangedEventHandler(AppOnPropertyChanged);
        }

        protected virtual void OnRootViewUpdated(EvasObject rootView)
        {
            rootView.Geometry = new Rect(0, 0, Window.ScreenSize.Width, Window.ScreenSize.Height);
        }

        void AppOnPropertyChanging(object sender, XPropertyChangingEventArgs args)
        {
            if (args.PropertyName == nameof(XApplication.MainPage))
            {
                Platform.GetRenderer(_application?.MainPage)?.Dispose();
            }
        }

        void AppOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(XApplication.MainPage))
            {
                OnRootViewUpdated(_application.MainPage.CreateEvasObject(Window));
            }
        }
    }
}
