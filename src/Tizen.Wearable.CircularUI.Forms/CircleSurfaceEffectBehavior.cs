﻿/*
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
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The CircleSurfaceEffectBehavior is a behavior which allows you to add views that require CircleSurface.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    /// [Obsolete("CircleSurfaceEffectBehavior is obsolete as of version 1.5.0. Please use BezelInteracationPage instead.")]
    public class CircleSurfaceEffectBehavior : Behavior<Page>
    {
        internal static readonly BindableProperty SurfaceProperty = BindableProperty.CreateAttached("CircleSurface", typeof(object), typeof(CircleSurfaceEffectBehavior), null);

        public static readonly BindableProperty RotaryFocusObjectProperty = BindableProperty.CreateAttached("RotaryFocusObject", typeof(IRotaryFocusable), typeof(CircleSurfaceEffectBehavior), null);

        internal static object GetSurface(BindableObject obj) => obj.GetValue(SurfaceProperty);
        internal static void SetSurface(BindableObject obj, object surface) => obj.SetValue(SurfaceProperty, surface);

        public static IRotaryFocusable GetRotaryFocusObject(BindableObject obj) => (IRotaryFocusable)obj.GetValue(RotaryFocusObjectProperty);
        public static void SetRotaryFocusObject(BindableObject obj, IRotaryFocusable focusable) => obj.SetValue(RotaryFocusObjectProperty, focusable);

        protected override void OnAttachedTo(Page page)
        {
            base.OnAttachedTo(page);

            var effect = Effect.Resolve("CircleUI.CircleSurfaceEffect");
            if (effect != null)
            {
                page.Effects.Add(effect);
            }
        }

        protected override void OnDetachingFrom(Page page)
        {
            var effect = Effect.Resolve("CircleUI.CircleSurfaceEffect");
            if (effect != null)
            {
                page.Effects.Remove(effect);
            }
            base.OnDetachingFrom(page);
            page.RemoveBinding(RotaryFocusObjectProperty);
        }
    }
}
