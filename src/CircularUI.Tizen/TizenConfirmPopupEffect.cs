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
using System.ComponentModel;
using System.Linq;
using System.Text;
using ElmSharp;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ResolutionGroupName("CircleUI")]
[assembly: ExportEffect(typeof(CircularUI.Tizen.TizenConfirmPopupEffect), "ConfirmPopupEffect")]
namespace CircularUI.Tizen
{
    public class TizenConfirmPopupEffect : PlatformEffect
    {
        SelectContextPopup _popup;

        protected override void OnAttached()
        {
            Show();
        }

        protected override void OnDetached()
        {
            if (_popup != null)
            {
                _popup.Dismiss();
            }
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
            if (args.PropertyName == ConfirmPopupEffect.ConfirmVisibilityProperty.PropertyName)
            {
                Show();
            }
        }

        void Show()
        {
            bool visibility = ConfirmPopupEffect.GetConfirmVisibility(Element);
            Console.WriteLine("Show called. - " + visibility);
            if (visibility)
            {
                _popup = ShowPopup();
            }
            else
            {
                _popup?.Dismiss();
            }
        }

        SelectContextPopup ShowPopup()
        {
            ElmSharp.EvasObject parent = null;
            if (Control?.Parent != null)
            {
                parent = Control.Parent;
            }
            if (parent == null)
            {
                parent = Forms.NativeParent;
            }

            var ctx = new SelectContextPopup(parent);
            ctx.Dismissed += (s, e) => ConfirmPopupEffect.SetConfirmVisibility(Element, false);

            var acceptCommand = ConfirmPopupEffect.GetAcceptCommand(Element);
            if (acceptCommand != null)
            {
                ctx.Accepted += (s, e) => acceptCommand.Execute(ConfirmPopupEffect.GetAcceptCommandParameter(Element));
            }

            var cancelCommand = ConfirmPopupEffect.GetCancelCommand(Element);
            if (cancelCommand != null)
            {
                ctx.Canceled += (s, e) => cancelCommand.Execute(ConfirmPopupEffect.GetCancelCommandParameter(Element));
            }

            string accept = ConfirmPopupEffect.GetAcceptText(Element);

            if (string.IsNullOrEmpty(accept))
            {
                accept = "Ok";
            }
            var acceptItem = ctx.Append(accept);

            ctx.Show();

            string cancel = ConfirmPopupEffect.GetCancelText(Element);
            if (!string.IsNullOrEmpty(cancel))
            {
                var cancelItem = ctx.Append(cancel);

                acceptItem.Style = "select_mode/top";
                cancelItem.Style = "select_mode/bottom";
                cancelItem.Selected += (s, e) => ctx.Cancel();

                Console.WriteLine($"Handle.Style = [{acceptItem.Style}]");
                Console.WriteLine($"Handle.Style = [{cancelItem.Style}]");
            }
            acceptItem.Selected += (s, e) => ctx.Accept();

            var option = ConfirmPopupEffect.GetPositionOption(Element);
            var offset = ConfirmPopupEffect.GetOffset(Element);
            int x = 0, y = 0;
            Rect rect;
            switch (option)
            {
                case PositionOption.Absolute:
                    x = (int)offset.X;
                    y = (int)offset.Y;
                    break;
                case PositionOption.BottomOfView:
                    rect = Control.Geometry;
                    x = rect.X + rect.Width / 2 + (int)offset.X;
                    y = rect.Y + rect.Height / 2 + (int)offset.Y;
                    break;
                case PositionOption.CenterOfParent:
                    rect = Xamarin.Forms.Platform.Tizen.Forms.NativeParent.Geometry;
                    var ctxRect = ctx.Geometry;
                    x = rect.Width / 2 + (int)offset.X;
                    y = rect.Height / 2 - ctxRect.Height / 2 + (int)offset.Y;
                    break;
                case PositionOption.Relative:
                    rect = Xamarin.Forms.Platform.Tizen.Forms.NativeParent.Geometry;
                    x = (int)(rect.Width * offset.X);
                    y = (int)(rect.Height * offset.Y);
                    break;
            }
            ctx.Move(x, y);

            return ctx;
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
