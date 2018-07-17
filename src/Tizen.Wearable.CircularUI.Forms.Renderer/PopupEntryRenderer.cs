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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(Tizen.Wearable.CircularUI.Forms.PopupEntry), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.PopupEntryRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class PopupEntryRenderer : EntryRenderer
    {
        ElmSharp.Background _editorPopup;
        Xamarin.Forms.Platform.Tizen.Native.Entry _editor;

        Interop.EFL.InputPanelEventCallback _editorStateChanged;

        ElmSharp.Color _defaultColor;

        Interop.EFL.InputPanelState _IMEState;

        public PopupEntryRenderer() : base()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                Control.IsEditable = false;
                Control.Clicked += OnClicked;
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

        void CreatePopup()
        {
            var rect = Xamarin.Forms.Platform.Tizen.Forms.NativeParent.Geometry;

            _editorPopup = new ElmSharp.Background(Xamarin.Forms.Platform.Tizen.Forms.NativeParent)
            {
                Geometry = rect
            };

            var layout = new ElmSharp.Layout(_editorPopup);
            layout.SetTheme("layout", "entry", "default");
            layout.Show();

            _editorPopup.SetPartContent("overlay", layout);
            _editorPopup.Color = Control.BackgroundColor;

            _editor = new Xamarin.Forms.Platform.Tizen.Native.Entry(layout);
            _editor.IsSingleLine = true;
            _editor.Scrollable = true;
            _editor.PredictionAllowed = false;
            _editor.SetInputPanelEnabled(false);
            _editor.ShowInputPanel();
            _editor.AllowFocus(true);
            _editor.Show();

            _editor.SetInputPanelReturnKeyType(ElmSharp.InputPanelReturnKeyType.Done);

            _editor.SetInputPanelLayout(ElmSharp.InputPanelLayout.Normal);

            _editorPopup.BackButtonPressed += (s, e) => HidePopup();

            _editor.Activated += (s, e) => HidePopup();

            IntPtr entryHandle = Interop.EFL.elm_entry_imf_context_get(_editor.RealHandle);

            _editorStateChanged = EditorStateChanged;
            Interop.EFL.ecore_imf_context_input_panel_event_callback_add(entryHandle, Interop.EFL.InputPanelEventType.State, _editorStateChanged, IntPtr.Zero);

            layout.SetPartContent("elm.swallow.content", _editor);
        }

        void HidePopup()
        {
            Control.Text = _editor.Text;
            if (_IMEState != Interop.EFL.InputPanelState.Hide)
            {
                _editor.HideInputPanel();
            }
            else if (_IMEState == Interop.EFL.InputPanelState.Hide)
            {
                _editorPopup.Hide();
            }
        }

        void ShowPopup()
        {
            if (_editorPopup == null)
            {
                CreatePopup();
            }
            if (_editor.IsPassword != Control.IsPassword)
                _editor.IsPassword = Control.IsPassword;

            _editor.Text = Control.Text;

            _defaultColor = new ElmSharp.Color(40, 40, 40, 255); //editfield bg default color
            _editor.TextColor = Control.TextColor;
            _editorPopup.Color = Control.BackgroundColor == default(ElmSharp.Color) ? _defaultColor : Control.BackgroundColor;

            _editor.MoveCursorEnd();
            _editor.ShowInputPanel();
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
    }
}
