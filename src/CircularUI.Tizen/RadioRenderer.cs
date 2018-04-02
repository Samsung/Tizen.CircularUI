using System;
using System.Collections.Generic;
using System.Linq;
using CircularUI;
using CircularUI.Tizen;
using Xamarin.Forms.Platform.Tizen;
using ERadio = ElmSharp.Radio;
using TForms = Xamarin.Forms.Platform.Tizen.Forms;

[assembly: ExportRenderer(typeof(Radio), typeof(RadioRenderer))]

namespace CircularUI.Tizen
{

    public class RadioRenderer : ViewRenderer<Radio, ERadio>
    {
        static Lazy<RadioGroupManager> s_GroupManager = new Lazy<RadioGroupManager>();
        int _changedCallbackDepth = 0;

        public RadioRenderer()
        {
            RegisterPropertyHandler(Radio.GroupNameProperty, UpdateGroupName);
            RegisterPropertyHandler(Radio.IsSelectedProperty, UpdateIsSelected);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Radio> e)
        {
            if (Control == null)
            {
                var radio = new ERadio(TForms.NativeParent) { StateValue = 1 };
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
