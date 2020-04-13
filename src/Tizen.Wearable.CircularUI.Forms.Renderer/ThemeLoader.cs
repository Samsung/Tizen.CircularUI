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
using System.IO;

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    internal static class ThemeLoader
    {
        const string CircularUITheme = "tizen-circular-ui-theme.edj";

        public static string AppResourcePath { get; private set; }

        public static bool IsInitialized { get; private set; }

        public static void Initialize(string resourcePath)
        {
            if (!IsInitialized)
            {
                AppResourcePath = resourcePath;
                IsInitialized = true;
                AddThemeOverlay(CircularUITheme);
            }
        }

        public static void AddThemeOverlay(string themeFilePath)
        {
            if (!IsInitialized)
            {
                Log.Error(FormsCircularUI.Tag, $"ThemeLoader is not initialized properly");
                return;
            }
            Elementary.AddThemeOverlay(Path.Combine(AppResourcePath, themeFilePath));
        }
    }
}
