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
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(Tizen.Wearable.CircularUI.Forms.PopupEntry), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.PopupEntryRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class PopupEntryRenderer : EntryRenderer
    {
        ElmSharp.Background _editorPopup;
        ElmSharp.Color _popupBackgroundColor;

        Xamarin.Forms.Platform.Tizen.Native.Entry _editor;

        Interop.EFL.InputPanelEventCallback _editorStateChanged;

        Interop.EFL.InputPanelState _IMEState;

        ElmSharp.Color DefaultColor { get; set; }

        bool _IsPopupOpened = false;

        public PopupEntryRenderer()
        {
            DefaultColor = new ElmSharp.Color(40, 40, 40, 255); //editfield bg default color
            RegisterPropertyHandler(PopupEntry.PopupBackgroundColorProperty, UpdatePopupBackgroundColor);
            RegisterPropertyHandler(PopupEntry.IsPopupOpenedProperty, UpdateIsPopupOpened);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                Control.IsEditable = false;
                Control.SetInputPanelEnabled(false);
                Control.Clicked += OnClicked;

                // Set Text again for apply text style because IsEditable reset the style of text
                Control.Text = string.Empty;
                Control.Text = Element.Text;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_editor != null)
                {
                    IntPtr entryHandle = Interop.EFL.elm_entry_imf_context_get(_editor.RealHandle);
                    Interop.EFL.ecore_imf_context_input_panel_event_callback_del(entryHandle, Interop.EFL.InputPanelEventType.State, _editorStateChanged);
                }
            }
            base.Dispose(disposing);
        }

        void OnClicked(object sender, EventArgs e) => ShowPopup();

        Window FindWindow(EvasObject obj)
        {
            EvasObject parent = obj.Parent;
            while (!(parent is Window) && parent != null)
            {
                parent = parent.Parent;
            }
            return parent as Window;
        }

        void CreatePopup()
        {
            var rect = Xamarin.Forms.Platform.Tizen.Forms.NativeParent.Geometry;

            var root = FindWindow(Xamarin.Forms.Platform.Tizen.Forms.NativeParent);

            _editorPopup = new ElmSharp.Background(root)
            {
                Geometry = rect
            };

            var layout = new ElmSharp.Layout(_editorPopup);
            layout.SetTheme("layout", "entry", "default");
            layout.Show();

            _editorPopup.SetPartContent("overlay", layout);

            _editor = new Xamarin.Forms.Platform.Tizen.Native.Entry(layout);
            _editor.IsSingleLine = true;
            _editor.Scrollable = true;
            _editor.SetInputPanelEnabled(false);
            _editor.AllowFocus(true);
            _editor.Show();

            _editor.SetInputPanelReturnKeyType(ElmSharp.InputPanelReturnKeyType.Done);

            _editor.UpdateKeyboard(Element.Keyboard, Element.IsSpellCheckEnabled, Element.IsTextPredictionEnabled);

            _editorPopup.BackButtonPressed += (s, e) => HidePopup();

            _editor.Activated += (s, e) => HidePopup();

            IntPtr entryHandle = Interop.EFL.elm_entry_imf_context_get(_editor.RealHandle);

            _editorStateChanged = EditorStateChanged;
            Interop.EFL.ecore_imf_context_input_panel_event_callback_add(entryHandle, Interop.EFL.InputPanelEventType.State, _editorStateChanged, IntPtr.Zero);

            layout.SetPartContent("elm.swallow.content", _editor);
        }

        void PopupEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Control.Text = e.NewTextValue;
        }

        void PopupEntryActivated(object sender, EventArgs e)
        {
            ((IEntryController)Element).SendCompleted();
        }

        void HidePopup()
        {
            Control.TextChanged -= OnTextChanged;

            if (_IMEState != Interop.EFL.InputPanelState.Hide)
            {
                _editor.HideInputPanel();
            }
            else if (_IMEState == Interop.EFL.InputPanelState.Hide)
            {
                _editorPopup.Hide();
                _editor.TextChanged -= PopupEntryTextChanged;
                _editor.Activated -= PopupEntryActivated;
            }
            _IsPopupOpened = false;
            ((PopupEntry)Element).IsPopupOpened = false;
        }

        void ShowPopup()
        {
            if (_editorPopup == null)
            {
                CreatePopup();
            }
            _editor.IsPassword = Control.IsPassword;
            _editor.HorizontalTextAlignment = Control.HorizontalTextAlignment;

            _editor.Text = Control.Text;
            _editor.TextChanged += PopupEntryTextChanged;
            _editor.Activated += PopupEntryActivated;

            _editor.TextStyle = Control.TextStyle;
            _editorPopup.Color = _popupBackgroundColor;

            _editor.MoveCursorEnd();
            _editor.ShowInputPanel();

            Control.TextChanged += OnTextChanged;
            _IsPopupOpened = true;
            ((PopupEntry)Element).IsPopupOpened = true;
        }

        void EditorStateChanged(IntPtr data, IntPtr ctx, int value)
        {
            _IMEState = (Interop.EFL.InputPanelState)value;
            if (_IMEState == Interop.EFL.InputPanelState.Hide)
            {
                HidePopup();
            }
            else if (_IMEState == Interop.EFL.InputPanelState.Show)
            {
                _editor.SetFocus(true);
                _editorPopup.RaiseTop();
                _editorPopup.Show();
            }
        }

        void UpdatePopupBackgroundColor()
        {
            Xamarin.Forms.Color bgColor = ((PopupEntry)Element).PopupBackgroundColor;
            if (bgColor == Xamarin.Forms.Color.Default)
            {
                _popupBackgroundColor = DefaultColor;
            }
            else
            {
                _popupBackgroundColor = bgColor.ToNative();
            }
        }

        void OnTextChanged(object sender, EventArgs e)
        {
            if (_editor != null)
            {
                if (_editor.Text != Control.Text)
                {
                    _editor.Text = Control.Text;
                    if (!_editor.IsFocused)
                    {
                        _editor.MoveCursorEnd();
                    }
                }
            }
        }

        void UpdateIsPopupOpened()
        {
            bool isOpended = ((PopupEntry)Element).IsPopupOpened;
            if (isOpended == _IsPopupOpened)
            {
                return;
            }

            _IsPopupOpened = isOpended;
            if (isOpended)
            {
                ShowPopup();
            }
            else
            {
                HidePopup();
            }
        }
    }
}
