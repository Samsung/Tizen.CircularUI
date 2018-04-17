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

using Tizen.Applications;
using ElmSharp;
using Tizen.Wearable.CircularUI.Forms.Renderer.Watchface;
using System;

namespace SimpleTextWatchface
{
    class App : FormsWatchface
    {
        ClockViewModel _viewModel;
        protected override void OnCreate()
        {
            base.OnCreate();
            var app = new WatchfaceApplication();
            _viewModel = new ClockViewModel();
            app.BindingContext = _viewModel;
            LoadWatchface(app);
        }

        protected override void OnTick(TimeEventArgs time)
        {
            base.OnTick(time);
            if (_viewModel != null)
            {
                _viewModel.Time = time.Time.UtcTimestamp;
            }
        }

        protected override void OnAmbientChanged(AmbientEventArgs mode)
        {
            base.OnAmbientChanged(mode);
            if (_viewModel != null)
            {
                if (mode.Enabled)
                {
                    _viewModel.Mode = "Ambient";
                    _viewModel.IsNormalMode = false;
                }
                else
                {
                    _viewModel.Mode = "Watch";
                    _viewModel.IsNormalMode = true;
                }
            }
        }


        protected override void OnAmbientTick(TimeEventArgs time)
        {
            base.OnAmbientTick(time);
            if (_viewModel != null)
            {
                _viewModel.Time = time.Time.UtcTimestamp;
            }
        }

        static void Main(string[] args)
        {
            App app = new App();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            Tizen.Wearable.CircularUI.Forms.Renderer.CircularUI.Init();
            app.Run(args);
        }
    }
}
