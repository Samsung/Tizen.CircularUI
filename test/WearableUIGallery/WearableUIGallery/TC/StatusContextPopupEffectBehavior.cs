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
using Tizen.Wearable.CircularUI.Forms;

namespace WearableUIGallery.TC
{
    public class StatusContextPopupEffectBehavior : ContextPopupEffectBehavior
    {
        public static BindableProperty StatusProperty = BindableProperty.Create(nameof(Status), typeof(string), typeof(StatusContextPopupEffectBehavior), null);

        public string Status
        {
            get => (string)GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }

        public void OnAttachedTo(Button ctxButton)
        {
            base.OnAttachedTo(ctxButton);
        }

        public void OnDetachingFrom(Button ctxButton)
        {
            base.OnDetachingFrom(ctxButton);
        }
    }

}