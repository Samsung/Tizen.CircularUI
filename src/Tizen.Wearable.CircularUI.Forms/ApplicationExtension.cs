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

using Xamarin.Forms;
using Application = Xamarin.Forms.Application;
using T = Xamarin.Forms.PlatformConfiguration.Tizen;

namespace Tizen.Wearable.CircularUI.Forms
{
    public static class ApplicationExtension
    {
        public static readonly BindableProperty OverlayContentProperty
            = BindableProperty.CreateAttached("OverlayContent", typeof(View), typeof(Application), default(View));

        public static View GetOverlayContent(BindableObject application)
        {
            return (View)application.GetValue(OverlayContentProperty);
        }

        public static void SetOverlayContent(BindableObject application, View value)
        {
            application.SetValue(OverlayContentProperty, value);
        }

        public static View GetOverlayContent(this IPlatformElementConfiguration<T, Application> config)
        {
            return GetOverlayContent(config.Element);
        }

        public static IPlatformElementConfiguration<T, Application> SetOverlayContent(this IPlatformElementConfiguration<T, Application> config, View value)
        {
            SetOverlayContent(config.Element, value);
            return config;
        }
    }
}
