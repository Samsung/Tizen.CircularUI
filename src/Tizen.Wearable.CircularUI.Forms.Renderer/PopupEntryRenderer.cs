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

            _editor.ChangedByUser += (s, e) => HidePopup();

            _editor.SetInputPanelReturnKeyType(ElmSharp.InputPanelReturnKeyType.Done);

            _editor.SetInputPanelLayout(ElmSharp.InputPanelLayout.Normal);

            _editorStateChanged = EditorStateChanged;
            IntPtr entryHandle = Interop.EFL.elm_entry_imf_context_get(_editor.RealHandle);
            Interop.EFL.ecore_imf_context_input_panel_event_callback_add(entryHandle, Interop.EFL.InputPanelEventType.State, _editorStateChanged, IntPtr.Zero);

            layout.SetPartContent("elm.swallow.content", _editor);

            layout.BackButtonPressed += (s, e) => HidePopup();
        }

        void HidePopup()
        {
            Control.Text = _editor.Text;
            _editor.HideInputPanel();
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
            _editor.ShowInputPanel();
            _editor.MoveCursorEnd();
            _editor.SetFocus(true);

            var edjeObj = Control.EdjeObject;
            edjeObj.GetColorClass("layout/editfield/bg",
                out int r, out int g, out int b, out int a,
                out _, out _, out _, out _,
                out _, out _, out _, out _);

            _defaultColor = new ElmSharp.Color(r, g, b, a);

            _editor.TextColor = Control.TextColor;
            _editorPopup.Color = Control.BackgroundColor == default(ElmSharp.Color) ? _defaultColor : Control.BackgroundColor;

            _editorPopup.RaiseTop();
            _editorPopup.Show();
        }

        void EditorStateChanged(IntPtr data, IntPtr ctx, int value)
        {
            if (value == (int)Interop.EFL.InputPanelState.Hide)
            {
                Control.Text = _editor.Text;
                _editorPopup.Hide();
            }
        }
    }
}
