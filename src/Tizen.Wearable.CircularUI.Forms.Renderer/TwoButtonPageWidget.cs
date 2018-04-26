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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class TwoButtonPageWidget : Background, IContainable<EvasObject>
    {
        ObservableBox _outbox;

        ElmSharp.Layout _frame;
        ElmSharp.Layout _buttonLayer;
        ElmSharp.Button[] _buttons;
        Canvas _canvas;

        bool _overlap;

        public TwoButtonPageWidget(EvasObject parent) : base(parent)
        {
            _frame = new ElmSharp.Layout(parent);
            _frame.SetTheme("popup", "base", "circle");
            SetPartContent("overlay", _frame);
            _frame.Show();

            _buttonLayer = new ElmSharp.Layout(_frame);
            _buttonLayer.SetTheme("popup", "buttons2", "popup/circle");
            _frame.SetPartContent("elm.swallow.action_area", _buttonLayer);
            _buttonLayer.Show();

            _outbox = new ObservableBox(parent);
            _outbox.SetAlignment(NamedHint.Fill, NamedHint.Fill);
            _outbox.SetWeight(NamedHint.Expand, NamedHint.Expand);
            _outbox.SetLayoutCallback(() => { });

            _frame.SetPartContent("elm.swallow.content", _outbox);
            _outbox.Show();

            _canvas = new Canvas(_outbox);
            EcoreMainloop.Post(() => _canvas.Geometry = _outbox.Geometry);
            _outbox.PackEnd(_canvas);
            _canvas.Show();

            _buttons = new ElmSharp.Button[2];
            _overlap = false;
        }

        public bool Overlap
        {
            get => _overlap;
            set
            {
                if (_overlap != value)
                {
                    _overlap = value;
                    if (_overlap)
                    {
                        _canvas.Geometry = _outbox.Geometry;
                    }
                    else
                    {
                        OnLayout();
                    }
                }
            }
        }

        public Canvas Canvas => _canvas;

        public new IList<EvasObject> Children => _canvas.Children;

        public event EventHandler<LayoutEventArgs> LayoutUpdated
        {
            add => _canvas.LayoutUpdated += value;
            remove => _canvas.LayoutUpdated -= value;
        }

        protected override void OnUnrealize()
        {
            _canvas.Unrealize();
            HideButton(0);
            HideButton(1);
            _buttonLayer.Unrealize();
            _frame.Unrealize();
            base.OnUnrealize();
        }

        public void ShowButton1(string text, string image = null, Action action = null) => ShowButton(0, "popup/circle/left", "actionbtn1", text, image, action);
        public void HideButton1() => HideButton(0);
        public void ShowButton2(string text, string image = null, Action action = null) => ShowButton(1, "popup/circle/right", "actionbtn2", text, image, action);
        public void HideButton2() => HideButton(1);

        void OnLayout()
        {
            var rect = _outbox.Geometry;
            if (_buttons[0] != null)
            {
                var rect1 = _buttons[0].Geometry;
                rect.X = rect1.Right;
                rect.Width -= rect1.Width;
            }
            if (_buttons[1] != null)
            {
                var rect2 = _buttons[1].Geometry;
                rect.Width -= rect2.Width;
            }
            Canvas.Geometry = rect;
        }

        void ShowButton(int id, string style, string part, string text, string image = null, Action action = null)
        {
            HideButton(id);

            _buttons[id] = new ElmSharp.Button(_buttonLayer)
            {
                Text = text,
                Style = style
            };
            if (!string.IsNullOrEmpty(image))
            {
                var path = ResourcePath.GetPath(image);
                var buttonImage = new ElmSharp.Image(_buttons[id]);
                buttonImage.LoadAsync(path);
                buttonImage.Show();
                _buttons[id].SetPartContent("elm.swallow.content", buttonImage);
            }
            if (action != null)
            {
                _buttons[id].Clicked += (s, e) => action();
            }
            _buttonLayer.SetPartContent(part, _buttons[id]);

            if (_overlap) EcoreMainloop.Post(OnLayout);
        }
        void HideButton(int id)
        {
            if (_buttons[id] != null)
            {
                _buttons[id].Unrealize();
                _buttons[id].SetPartContent("elm.swallow.button", null);
                _buttons[id] = null;
                if (_overlap) EcoreMainloop.Post(OnLayout);
            }
        }
    }
}
