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
using System.Diagnostics;
using ElmSharp;

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public static class FormsCircularUI
    {
        public static readonly string Tag = "CircularUI";

        public static bool IsInitialized { get; private set; }

        public static void Init()
        {
            if (IsInitialized) return;
            IsInitialized = true;
        }

        public static void Init(string apiKey)
        {
            if (!string.IsNullOrEmpty(apiKey))
            {
                GoogleMaps.Init(apiKey);
            }
            else
            {
                Debug.Assert(!string.IsNullOrEmpty(apiKey), "apiKey is null or empty!");
            }

            Init();
        }
    }
}
