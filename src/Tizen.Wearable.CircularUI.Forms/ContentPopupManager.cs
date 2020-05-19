/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
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

using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    public static class ContentPopupManager
    {
        public static async Task ShowPopup(this INavigation navigation, ContentPopup popup)
        {
            await ShowPopup(popup);
        }

        public static async Task ShowPopup(ContentPopup popup)
        {
            if (popup == null)
                return;

            using (var renderer = DependencyService.Get<IContentPopupRenderer>(DependencyFetchTarget.NewInstance))
            {
                if (renderer == null)
                    return;

                renderer.SetElement(popup);

                await renderer.Open();
            }
        }
    }
}
