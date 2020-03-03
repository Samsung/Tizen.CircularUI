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
using Tizen.Wearable.CircularUI.Forms.Renderer;
using Xamarin.Forms;

namespace WearableUIGallery.Tizen.Wearable
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        private static string APIKEY = "Invalid_Google_API_Key"; //Insert Your Google API key

        protected override void OnCreate()
        {
            base.OnCreate();
            LoadApplication(new App());
        }

        protected override void OnTerminate()
        {
            base.OnTerminate();
#if UITEST
            global::Tizen.Appium.TizenAppium.StopService();
#endif
        }

        static void Main(string[] args)
        {
            var app = new Program();
            Forms.Init(app);
            FormsCircularUI.Init(APIKEY);
#if UITEST
            global::Tizen.Appium.TizenAppium.StartService();
#endif
            app.Run(args);
        }
    }
}
