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
using System;

namespace WearableUIGallery.TC
{
    public class TapContextPopupEffectBehavior : ContextPopupEffectBehavior
    {
        TapGestureRecognizer tapRecognizer;

        public static readonly BindableProperty IsTappedProperty =
            BindableProperty.Create(
                "IsTapped", typeof(bool), typeof(TapContextPopupEffectBehavior), false);

        public bool IsTapped
        {
            set { SetValue(IsTappedProperty, value); }
            get { return (bool)GetValue(IsTappedProperty); }
        }

        void OnTapped(object sender, EventArgs e)
        {
            IsTapped = !IsTapped;
        }

        protected override void OnAttachedTo(View view)
        {
            base.OnAttachedTo(view);
            Console.WriteLine($"OnAttachedTo() of TapContextPopupEffectBehavior is called.");

            tapRecognizer = new TapGestureRecognizer();
            tapRecognizer.Tapped += OnTapped;
            view.GestureRecognizers.Add(tapRecognizer);
        }

        protected override void OnDetachingFrom(View view)
        {
            base.OnDetachingFrom(view);
            Console.WriteLine($"OnDetachingFrom() of TapContextPopupEffectBehavior is called.");

            view.GestureRecognizers.Remove(tapRecognizer);
            tapRecognizer.Tapped -= OnTapped;
        }
    }

}