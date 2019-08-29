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
using System.ComponentModel;
using ElmSharp;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using XForms = Xamarin.Forms.Forms;

[assembly: ResolutionGroupName("CircleUI")]
[assembly: ExportEffect(typeof(Tizen.Wearable.CircularUI.Forms.Renderer.TizenConfirmPopupEffect), "ContextPopupEffectBehavior")]
namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class TizenConfirmPopupEffect : PlatformEffect
    {
        SelectContextPopup _popup;
        ContextPopupEffectBehavior _behavior;

        protected override void OnAttached()
        {
            _behavior = Element.GetValue(ContextPopupEffectBehavior.ContextPopupEffectBehaviorProperty) as ContextPopupEffectBehavior;
            _behavior.PropertyChanged += OnBehaviorPropertyChanged;

            if (_behavior != null)
                UpdatePopupVisibility();

        }

        protected override void OnDetached()
        {
            if (_popup != null)
            {
                _popup.Dismiss();
                _popup.Unrealize();
            }
            if (_behavior != null)
                _behavior.PropertyChanged -= OnBehaviorPropertyChanged;
        }

        void OnBehaviorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ContextPopupEffectBehavior.VisibilityProperty.PropertyName)
            {
                UpdatePopupVisibility();
            }
        }

        void UpdatePopupVisibility()
        {
            if (_behavior == null) return;
            if (_behavior.Visibility)
            {
                CreatePopup();
                _popup.Show();
            }
            else
            {
                if (_popup == null) return;
                _popup.Dismiss();
                _popup.Unrealize();
                _popup = null;
            }
        }

        void CreatePopup()
        {
            if (_popup != null) return;

            ElmSharp.EvasObject parent = null;
            if (Control?.Parent != null)
            {
                parent = Control.Parent;
            }
            if (parent == null)
            {
                parent = XForms.NativeParent;
            }

            _popup = new SelectContextPopup(parent);
            _popup.Dismissed += (s, e) => _behavior.Visibility = false;

            _popup.Accepted += (s, e) => _behavior?.AcceptCommand?.Execute(_behavior?.AcceptCommandParameter);
            _popup.Canceled += (s, e) => _behavior?.CancelCommand?.Execute(_behavior?.CancelCommandParameter);

            string accept = _behavior.AcceptText ?? "";

            var acceptItem = _popup.Append(accept);

            string cancel = _behavior.CancelText;
            if (!string.IsNullOrEmpty(cancel))
            {
                var cancelItem = _popup.Append(cancel);

                acceptItem.Style = "select_mode/top";
                cancelItem.Style = "select_mode/bottom";
                cancelItem.Selected += (s, e) => _popup.Cancel();
            }
            acceptItem.Selected += (s, e) => _popup.Accept();

            var option = _behavior.PositionOption;
            var offset = _behavior.Offset;
            int x, y;
            Rect rect;
            Rect ctxRect = _popup.Geometry;
            switch (option)
            {
                case PositionOption.Absolute:
                    x = XForms.ConvertToPixel(offset.X);
                    y = XForms.ConvertToPixel(offset.Y);
                    break;
                case PositionOption.BottomOfView:
                    rect = Control.Geometry;
                    x = rect.X + rect.Width / 2 + XForms.ConvertToPixel(offset.X);
                    y = rect.Y + rect.Height + XForms.ConvertToPixel(offset.Y);
                    break;
                case PositionOption.CenterOfParent:
                    rect = XForms.NativeParent.Geometry;

                    x = rect.Width / 2 + XForms.ConvertToPixel(offset.X);
                    y = rect.Height / 2 - ctxRect.Height / 2 + XForms.ConvertToPixel(offset.Y);
                    break;
                case PositionOption.Relative:
                    rect = XForms.NativeParent.Geometry;
                    x = (int)(rect.Width * offset.X);
                    y = (int)(rect.Height * offset.Y);
                    break;
                default:
                    x = 0;
                    y = 0;
                    break;
            }

            var window = XForms.NativeParent.Geometry;
            if (y + ctxRect.Height > window.Height)
            {
                y = window.Height - ctxRect.Height;
            }

            _popup.Move(x, y);
        }

        class SelectContextPopup : ElmSharp.ContextPopup
        {
            public SelectContextPopup(EvasObject parent) : base(parent)
            {
                Style = "select_mode";
                Dismissed += OnDismissed;
                IsAccepted = false;

                SetDirectionPriorty(ContextPopupDirection.Down, ContextPopupDirection.Down, ContextPopupDirection.Down, ContextPopupDirection.Down);
            }

            public bool IsAccepted { get; set; }
            public void Accept()
            {
                IsAccepted = true;
                Accepted?.Invoke(this, EventArgs.Empty);
                Dismiss();
            }

            public void Cancel()
            {
                IsAccepted = false;
                Dismiss();
            }

            public event EventHandler Accepted;
            public event EventHandler Canceled;

            void OnDismissed(object sender, EventArgs e)
            {
                if (!IsAccepted)
                {
                    Canceled?.Invoke(this, EventArgs.Empty);
                }
                Unrealize();
            }
        }
    }
}
