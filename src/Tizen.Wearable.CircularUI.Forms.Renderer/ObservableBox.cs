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

using ElmSharp;
using System.Collections.Generic;
using System.Collections.Specialized;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;

[assembly: ExportRenderer(typeof(CirclePage), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.CirclePageRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    class ObservableBox : ElmSharp.Box, IContainable<EvasObject>
    {
        ReObservableCollection<EvasObject> _children;
        public ObservableBox(EvasObject parent) : base(parent)
        {
            _children = new ReObservableCollection<EvasObject>();
            _children.CollectionChanged += OnChildrenChanged;
        }
        IList<EvasObject> IContainable<EvasObject>.Children => _children;

        void OnChildrenChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var child in e.NewItems)
                {
                    if (child is EvasObject)
                    {
                        PackEnd(child as EvasObject);
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var child in e.OldItems)
                {
                    if (child is EvasObject)
                    {
                        UnPack(child as EvasObject);
                    }
                }
            }
        }
    }
}
