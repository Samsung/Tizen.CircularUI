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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The MapView class is used to display a map on the screen.
    /// </summary>
    public class GoogleMapView : View, IGoogleMapViewController
    {
        /// <summary>
        /// BindableProperty. Identifies the ItemsSource bindable property.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(IEnumerable), typeof(IEnumerable), typeof(GoogleMapView),
            default(IEnumerable), propertyChanged: (b, o, n) => ((GoogleMapView)b).OnItemsSourcePropertyChanged((IEnumerable)o, (IEnumerable)n));

        /// <summary>
        /// BindableProperty. Identifies the ItemTemplate bindable property.
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(GoogleMapView),
            default(DataTemplate), propertyChanged: (b, o, n) => ((GoogleMapView)b).OnItemTemplatePropertyChanged((DataTemplate)o, (DataTemplate)n));

        readonly ObservableCollection<Marker> _markers = new ObservableCollection<Marker>();

        private GoogleMapOption _option;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler LoadMapRequested;

        public GoogleMapView()
        {
            var option = new GoogleMapOption(new LatLng(41.890202, 12.492049));
            MapOption = option;
        }

        internal GoogleMapOption MapOption
        {
            get
            {
                return _option;
            }
            set
            {
                if (_option.Equals(value))
                    return;
                _option = value;
            }
        }

        /// <summary>
        /// An IList of the Pins on this MapView.
        /// </summary>
        public IList<Marker> Markers
        {
            get { return _markers; }
        }

        /// <summary>
        /// Gets or sets the source of items to template and display.
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the DataTemplate to apply to the ItemsSource.
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        void OnItemsSourcePropertyChanged(IEnumerable oldItemsSource, IEnumerable newItemsSource)
        {
            if (oldItemsSource is INotifyCollectionChanged ncc)
            {
                ncc.CollectionChanged -= OnItemsSourceCollectionChanged;
            }

            if (newItemsSource is INotifyCollectionChanged ncc1)
            {
                ncc1.CollectionChanged += OnItemsSourceCollectionChanged;
            }

            _markers.Clear();
            CreateMarkerItems();
        }

        void OnItemTemplatePropertyChanged(DataTemplate oldItemTemplate, DataTemplate newItemTemplate)
        {
            if (newItemTemplate is DataTemplateSelector)
            {
                throw new NotSupportedException($"You are using an instance of {nameof(DataTemplateSelector)} to set the {nameof(GoogleMapView)}.{ItemTemplateProperty.PropertyName} property. Use an instance of a {nameof(DataTemplate)} property instead to set an item template.");
            }

            _markers.Clear();
            CreateMarkerItems();
        }

        void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex == -1)
                        goto case NotifyCollectionChangedAction.Reset;
                    foreach (object item in e.NewItems)
                        CreateMarker(item);
                    break;
                case NotifyCollectionChangedAction.Move:
                    if (e.OldStartingIndex == -1 || e.NewStartingIndex == -1)
                        goto case NotifyCollectionChangedAction.Reset;
                    // Not tracking order
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex == -1)
                        goto case NotifyCollectionChangedAction.Reset;
                    foreach (object item in e.OldItems)
                        RemoveMarker(item);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldStartingIndex == -1)
                        goto case NotifyCollectionChangedAction.Reset;
                    foreach (object item in e.OldItems)
                        RemoveMarker(item);
                    foreach (object item in e.NewItems)
                        CreateMarker(item);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    _markers.Clear();
                    break;
            }
        }

        void CreateMarkerItems()
        {
            if (ItemsSource == null || ItemTemplate == null)
            {
                return;
            }

            foreach (object item in ItemsSource)
            {
                CreateMarker(item);
            }
        }

        void CreateMarker(object newItem)
        {
            if (ItemTemplate == null)
            {
                return;
            }

            var marker = (Marker)ItemTemplate.CreateContent();
            marker.BindingContext = newItem;
            _markers.Add(marker);
        }

        void RemoveMarker(object itemToRemove)
        {
            Marker pinToRemove = _markers.FirstOrDefault(marker => marker.BindingContext?.Equals(itemToRemove) == true);
            if (pinToRemove != null)
            {
                _markers.Remove(pinToRemove);
            }
        }

        /// <summary>
        /// Set GoogleMapOption value to MapView.
        /// </summary>
        public void Update(GoogleMapOption value)
        {
            MapOption = value;
            LoadMapRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
