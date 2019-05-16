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

using ElmSharp;
using System;
using Tizen.Applications;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using XApplication = Xamarin.Forms.Application;

namespace Tizen.Wearable.CircularUI.Forms.Renderer.Widget
{
    public class FormsWidgetBase : WidgetBase
    {
        XApplication _application;

        public FormsWidgetBase()
        {
            Log.Debug(FormsCircularUI.Tag, "Constructor called");
        }

        public override void OnCreate(Bundle content, int w, int h)
        {
            Log.Debug(FormsCircularUI.Tag, $"OnCreate() w:{w} h:{h}");
            base.OnCreate(content, w, h);
            XApplication.ClearCurrent();
        }

        public override void OnPause()
        {
            Log.Debug(FormsCircularUI.Tag, "OnPause()");
            base.OnPause();
            if (_application != null)
            {
                Log.Debug(FormsCircularUI.Tag, "_application.SendSleepAsync()");
                _application.SendSleepAsync();
            }
        }

        public override void OnResume()
        {
            Log.Debug(FormsCircularUI.Tag, "OnResume()");
            base.OnResume();
            if (_application != null)
            {
                Log.Debug(FormsCircularUI.Tag, " _application.SendResume()");
                _application.SendResume();
            }
        }

        public override void OnDestroy(WidgetDestroyType reason, Bundle content)
        {
            Log.Debug(FormsCircularUI.Tag, $"OnDestroy() {reason}");
            base.OnDestroy(reason, content);
            Platform.GetRenderer(_application?.MainPage)?.Dispose();
        }

        public override void OnResize(int w, int h)
        {
            Log.Debug(FormsCircularUI.Tag, $"OnResize()  w:{w} h:{h}");
            base.OnResize(w, h);
        }

        public override void OnUpdate(Bundle content, bool isForce)
        {
            Log.Debug(FormsCircularUI.Tag, $"OnResize()  content:{content} isForce:{isForce}");
            base.OnUpdate(content, isForce);
        }

        public void LoadApplication(XApplication application)
        {
            if (Window == null)
            {
                throw new NullReferenceException("MainWindow is not prepared, This method should be called in OnCreated().");
            }

            if (application == null)
            {
                throw new ArgumentException("application");
            }
            _application = application;
            XApplication.Current = application;
            application.SendStart();

            var rootView = application.MainPage.CreateEvasObject(Window);
            OnRootViewUpdated(rootView);

            application.PropertyChanging += (s, e) =>
            {
                Log.Debug(FormsCircularUI.Tag, "application.PropertyChanging");
                if (e.PropertyName == nameof(XApplication.MainPage))
                {
                    Platform.GetRenderer(application?.MainPage)?.Dispose();
                }
            };
            application.PropertyChanged += (s, e) =>
            {
                Log.Debug(FormsCircularUI.Tag, "application.PropertyChanged");
                if (e.PropertyName == nameof(XApplication.MainPage))
                {
                    OnRootViewUpdated(application.MainPage.CreateEvasObject(Window));
                }
            };
        }

        protected virtual void OnRootViewUpdated(EvasObject rootView)
        {
            Log.Debug(FormsCircularUI.Tag, "OnRootViewUpdated()");
            rootView.Geometry = new Rect(0, 0, Window.ScreenSize.Width, Window.ScreenSize.Height);
        }
    }
}
