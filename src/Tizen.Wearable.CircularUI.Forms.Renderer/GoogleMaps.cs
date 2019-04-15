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

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    internal static class GoogleMaps
    {
        private static string _apiKey;

        internal static bool IsInitialized { get; private set; }


        /// <summary>
        /// Initialize Map service with authentication key value.
        /// </summary>
        public static void Init(string apiKey)
        {
            if (IsInitialized)
                return;

            _apiKey = apiKey;
            IsInitialized = true;
        }

        internal static string GetKey()
        {
            return _apiKey;
        }
    }
}
