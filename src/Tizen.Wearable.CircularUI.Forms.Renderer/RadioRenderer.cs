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
using System.Linq;
using Tizen.Wearable.CircularUI.Forms;
using Tizen.Wearable.CircularUI.Forms.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using ERadio = ElmSharp.Radio;
using XForms = Xamarin.Forms.Forms;

[assembly: ExportRenderer(typeof(Radio), typeof(RadioRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{

    public class RadioRenderer : ViewRenderer<Radio, ERadio>
    {
        static Lazy<RadioGroupManager> s_GroupManager = new Lazy<RadioGroupManager>();
        int _changedCallbackDepth;

        public RadioRenderer()
        {
            RegisterPropertyHandler(Radio.GroupNameProperty, UpdateGroupName);
            RegisterPropertyHandler(Radio.IsSelectedProperty, UpdateIsSelected);
            RegisterPropertyHandler(Radio.ColorProperty, UpdateColor);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Radio> e)
        {
            if (Control == null)
            {
                var radio = new ERadio(XForms.NativeParent) { StateValue = 1 };
                SetNativeControl(radio);
                Control.ValueChanged += ChangedEventHandler;
            }
            base.OnElementChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Control != null)
                {
                    Control.ValueChanged -= ChangedEventHandler;
                }
            }
            base.Dispose(disposing);
        }

        void UpdateIsSelected()
        {
            if (_changedCallbackDepth == 0)
            {
                Control.GroupValue = Element.IsSelected ? 1 : 0;
            }
            s_GroupManager.Value.UpdateChecked(Element.GroupName, Element);
        }

        void UpdateGroupName()
        {
            s_GroupManager.Value.PartGroup(Element);
            s_GroupManager.Value.JoinGroup(Element.GroupName, Element);
        }

        void ChangedEventHandler(object sender, EventArgs e)
        {
            _changedCallbackDepth++;
            Element.IsSelected = Control.GroupValue == 1 ? true : false;
            _changedCallbackDepth--;
        }

        void UpdateColor()
        {
            var color = Element.Color;
            if (color != Xamarin.Forms.Color.Default)
            {
                Control.Color = Element.Color.ToNative();
            }
        }
    }

    internal class RadioGroupManager
    {
        Dictionary<string, List<Radio>> _groupMap = new Dictionary<string, List<Radio>>();

        public void JoinGroup(string groupName, Radio radio)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return;
            }

            if (!_groupMap.ContainsKey(groupName))
            {
                _groupMap.Add(groupName, new List<Radio>());
            }
            _groupMap[groupName].Add(radio);
            UpdateChecked(groupName, radio);
        }

        public void PartGroup(Radio radio)
        {
            string groupName = null;
            foreach (var list in _groupMap)
            {
                if (list.Value.Contains(radio))
                {
                    groupName = list.Key;
                }
            }
            PartGroup(groupName, radio);
        }

        public void PartGroup(string groupName, Radio radio)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return;
            }

            if (_groupMap.ContainsKey(groupName))
            {
                _groupMap[groupName].Remove(radio);
            }
        }

        public void UpdateChecked(string groupName, Radio radio)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return;
            }

            if (radio.IsSelected)
            {
                foreach (var btn in _groupMap[groupName].Where(b => b.IsSelected && b != radio))
                {
                    btn.IsSelected = false;
                }
            }
        }
    }
}
