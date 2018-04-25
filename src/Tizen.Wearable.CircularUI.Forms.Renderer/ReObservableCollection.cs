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

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(CirclePage), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.CirclePageRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    class ReObservableCollection<T> : ObservableCollection<T>
    {
        protected override void ClearItems()
        {
            var oldItems = Items.ToList();
            Items.Clear();
            using (BlockReentrancy())
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldItems));
            }
            base.ClearItems();
        }
    }
}
