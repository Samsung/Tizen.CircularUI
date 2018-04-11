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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace CircularUI
{
    /// <summary>
    /// The CirclePage is a ContentPage, which allows you to insert views that require CircleSurface, and you can show ToolbarItems with MoreOption.
    /// It has an ActionButton, and can use the MenuItem type as text, icon, command, and so on.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class CirclePage : ContentPage
    {
        /// <summary>
        /// BindableProperty. Identifies the ActionButton bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty ActionButtonProperty = BindableProperty.Create(nameof(ActionButton), typeof(ActionButtonItem), typeof(CirclePage), null,
            propertyChanged: (b, o, n) =>
            {
                if (n != null)
                    ((Element)n).Parent = (Element)b;
            });
        /// <summary>
        /// BindableProperty. Identifies the RotaryFocusTargetName bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty RotaryFocusTargetNameProperty = BindableProperty.Create(nameof(RotaryFocusTargetName), typeof(string), typeof(CirclePage), null,
            propertyChanged: RotaryFocusTargetNameChanged);
        /// <summary>
        /// BindablePropertyKey. Identifies the RotaryFocusObject bindable property Key.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        static readonly BindablePropertyKey RotaryFocusObjectPropertyKey = BindableProperty.CreateReadOnly(nameof(RotaryFocusObject), typeof(IRotaryFocusable), typeof(CirclePage), null);
        /// <summary>
        /// BindableProperty. Identifies the RotaryFocusObject bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty RotaryFocusObjectProperty = RotaryFocusObjectPropertyKey.BindableProperty;

        static void RotaryFocusTargetNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            CirclePage page = bindable as CirclePage;
            if (page != null && newValue is string)
            {
                string name = newValue as string;
                var obj = page.FindByName<IRotaryFocusable>(name);
                if (obj != null)
                {
                    page.RotaryFocusObject = obj;
                }
            }
        }
        /// <summary>
        /// Creates and initializes a new instance of the CirclePage class.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public CirclePage()
        {
            var surfaceItems = new ObservableCollection<ICircleSurfaceItem>();
            surfaceItems.CollectionChanged += OnSurfaceItemsChanged;
            CircleSurfaceItems = surfaceItems;
        }
        /// <summary>
        /// Gets a list of CircleSurfaceItems represented through CircleSurface.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public IList<ICircleSurfaceItem> CircleSurfaceItems { get; }

        /// <summary>
        /// Gets or sets ActionButton that presents a menu item and associates it with a command
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public ActionButtonItem ActionButton
        {
            get => (ActionButtonItem)GetValue(ActionButtonProperty);
            set => SetValue(ActionButtonProperty, value);
        }
        /// <summary>
        /// Gets object of RotaryFocusObject to receive bezel action(take a rotary event) from the current page.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public IRotaryFocusable RotaryFocusObject
        {
            get => (IRotaryFocusable)GetValue(RotaryFocusObjectProperty);
            private set => SetValue(RotaryFocusObjectPropertyKey, value);
        }

        /// <summary>
        /// Gets or sets target name of RotaryFocusObject.
        /// If RotaryFocusTargetName is set, it registers only a consumer in the RotaryFocusObject property to receive bezel action (take a rotary event) from the current page
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public string RotaryFocusTargetName
        {
            get => (string)GetValue(RotaryFocusTargetNameProperty);
            set => SetValue(RotaryFocusTargetNameProperty, value);
        }

        void OnSurfaceItemsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action != NotifyCollectionChangedAction.Add) return;

            foreach (Element item in args.NewItems)
            {
                item.Parent = this;
            }
        }
    }
}
