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
using System.Collections.Generic;
using Tizen.Applications;

namespace Tizen.Wearable.CircularUI.Forms.Renderer.Widget
{
    public class FormsWidgetApplication : WidgetApplication
    {
        protected FormsWidgetApplication(Type type) : base(type)
        {
            Xamarin.Forms.Platform.Tizen.Forms.SetFlags("LightweightPlatform_Experimental");
        }

        protected FormsWidgetApplication(IDictionary<Type, string> typeInfo) : base(typeInfo)
        {
            Xamarin.Forms.Platform.Tizen.Forms.SetFlags("LightweightPlatform_Experimental");
        }

        public override void Exit()
        {
            // It is intended, disable to terminate.
            Log.Debug(FormsCircularUI.Tag, "Exit() called!");
        }
    }
}
