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
    public class CircleSurfaceView : View, ICircleSurfaceProvider
    {
        public IList<ICircleSurfaceItem> CircleSurfaceItems { get; internal set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public object CircleSurface { get; set; }

        public CircleSurfaceView()
        {
            var circleSurfaceItems = new ObservableCollection<ICircleSurfaceItem>();
            circleSurfaceItems.CollectionChanged += OnCircleObjectItemsCollectionChanged;
            CircleSurfaceItems = circleSurfaceItems;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            foreach (CircleSurfaceItem item in CircleSurfaceItems)
            {
                SetInheritedBindingContext(item, BindingContext);
            }
        }

        void OnCircleObjectItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Element item in args.NewItems)
                    item.Parent = this;
            }
            else if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Element item in args.OldItems)
                    item.Parent = null;
            }
        }
    }
}
