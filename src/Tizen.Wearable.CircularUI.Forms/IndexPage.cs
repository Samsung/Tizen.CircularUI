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
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The IndexPage is extension of Xamarin.Forms.MultiPage.
    /// When Page is added/removed at Multipage. circular index is added/removed automatically at the top of window.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    [Obsolete("IndexPage is obsolete as of version 1.5.0. Please use Xamarin.Forms.CarouselView and Xamarin.Forms.IndicatorView instead.")]
    public class IndexPage : MultiPage<ContentPage>
    {
        protected override ContentPage CreateDefault(object item)
        {
            var page = new ContentPage();
            return page;
        }
    }
}
