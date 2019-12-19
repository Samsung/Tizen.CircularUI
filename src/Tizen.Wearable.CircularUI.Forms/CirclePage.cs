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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
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
        /// BindableProperty. Identifies the RotaryFocusObject bindable property Key.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty RotaryFocusObjectProperty = BindableProperty.Create(nameof(RotaryFocusObject), typeof(IRotaryFocusable), typeof(CirclePage), null);

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

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool Appeared { get; set; }

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
        /// Gets or sets object of RotaryFocusObject to receive bezel action(take a rotary event) from the current page.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public IRotaryFocusable RotaryFocusObject
        {
            get => (IRotaryFocusable)GetValue(RotaryFocusObjectProperty);
            set => SetValue(RotaryFocusObjectProperty, value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            foreach (var item in CircleSurfaceItems)
            {
                var element = item as Element;
                if (element != null)
                    SetInheritedBindingContext(element, BindingContext);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Appeared = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Appeared = false;
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
