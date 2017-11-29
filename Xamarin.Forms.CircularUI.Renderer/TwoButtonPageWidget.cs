using ElmSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;

namespace Xamarin.Forms.CircularUI.Renderer
{
    public class TwoButtonPageWidget : Background, IContainable<EvasObject>
    {
        ElmSharp.Layout _frame;
        ElmSharp.Layout _buttonLayer;
        ElmSharp.Layout _contentLayer;
        ElmSharp.Button[] _buttons;
        Canvas _canvas;
        string _title;

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

            _contentLayer = new ElmSharp.Layout(_frame);
            _contentLayer.SetTheme("layout", "popup", "content/circle/buttons2");
            _frame.SetPartContent("elm.swallow.content", _contentLayer);
            _contentLayer.Show();

            _canvas = new Platform.Tizen.Native.Canvas(_contentLayer);
            _contentLayer.SetPartContent("elm.swallow.content", _canvas);
            _canvas.Show();

            _buttons = new ElmSharp.Button[2];
        }

        public Canvas Canvas => _canvas;

        public new IList<EvasObject> Children => _canvas.Children;

        public string Title
        {
            get => _title;
            set
            {
                if (_title == value) return;
                _title = value;
                _contentLayer.SetPartText("elm.text.title", _title);
                _contentLayer.SignalEmit("", "load");
            }
        }

        public event EventHandler<LayoutEventArgs> LayoutUpdated
        {
            add => _canvas.LayoutUpdated += value;
            remove => _canvas.LayoutUpdated -= value;
        }

        protected override void OnUnrealize()
        {
            _canvas.Unrealize();
            _contentLayer.Unrealize();
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
                Console.WriteLine($"button {id} image = {path}");
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
        }
        void HideButton(int id)
        {
            if (_buttons[id] != null)
            {
                _buttons[id].Unrealize();
                _buttons[id].SetPartContent("elm.swallow.button", null);
                _buttons[id] = null;
            }
        }
    }
}
