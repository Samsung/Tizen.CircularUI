using System;
using ElmSharp;
using ELayout = ElmSharp.Layout;
using EColor = ElmSharp.Color;

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class NavigationDrawer : ELayout
    {
        readonly int _minimumHeight = 50;

        Box _outterBox;
        Box _contentBox;
        EvasObject _label;
        bool _isLabelShow = true;
        GestureLayer _gestureLayer;

        public int MinimumDrawerHeight => _minimumHeight;

        public int NavigationDrawOpenThredhold => (int)(Parent.Geometry.Height / 2);

        public bool IsLabelVisible
        {
            get
            {
                return _isLabelShow;
            }
            set
            {
                _isLabelShow = value;

                if (_isLabelShow)
                {
                    CreateLabel();
                }
                else
                {
                    RemoveLabel();
                }
            }
        }

        public event EventHandler<NavigationDrawerDragEventArgs> NavigationDrawerDragged;

        public NavigationDrawer(EvasObject parent) : base(parent)
        {
            Initialize();
        }

        public new void SetContent(EvasObject content)
        {
            _contentBox.PackEnd(content);
        }

        protected override IntPtr CreateHandle(EvasObject parent)
        {
            _outterBox = new Box(parent);
            return _outterBox.Handle;
        }

        void Initialize()
        {
            AlignmentX = -1;
            AlignmentY = -1;
            WeightX = 1;
            WeightY = 1;

            _contentBox = new Box(this)
            {
                AlignmentX = -1,
                AlignmentY = -1,
                WeightX = 1,
                WeightY = 1,
                PropagateEvents = false
            };
            _contentBox.Show();

            _outterBox.PackEnd(_contentBox);

            IsLabelVisible = true;

            _gestureLayer = new GestureLayer(_outterBox);
            _gestureLayer.Attach(_outterBox);

            _gestureLayer.SetMomentumCallback(GestureLayer.GestureState.Start, OnStart);
            _gestureLayer.SetMomentumCallback(GestureLayer.GestureState.Move, OnMove);
            _gestureLayer.SetMomentumCallback(GestureLayer.GestureState.End, OnEnd);
            _gestureLayer.SetMomentumCallback(GestureLayer.GestureState.Abort, OnAbort);
        }

        void CreateLabel()
        {
            // TODO : This label will be replaced with an image for NavigationDrawer later.
            if (_label == null)
            {
                _label = new ElmSharp.Label(_outterBox)
                {
                    WeightX = 1,
                    AlignmentX = -1,
                    AlignmentY = 0,
                    MinimumHeight = 50,
                    BackgroundColor = EColor.Pink,
                };
                _label.Show();
                _outterBox.PackStart(_label);
            }
        }

        void RemoveLabel()
        {
            if(_label != null)
            {
                _label.Unrealize();
                _label = null;
            }
        }

        void OnStart(GestureLayer.MomentumData moment)
        {
            NavigationDrawerDragged?.Invoke(this, new NavigationDrawerDragEventArgs(GestureLayer.GestureState.Start, moment));
        }

        void OnMove(GestureLayer.MomentumData moment)
        {
            NavigationDrawerDragged?.Invoke(this, new NavigationDrawerDragEventArgs(GestureLayer.GestureState.Move, moment));
        }

        void OnEnd(GestureLayer.MomentumData moment)
        {
            NavigationDrawerDragged?.Invoke(this, new NavigationDrawerDragEventArgs(GestureLayer.GestureState.End, moment));
        }

        void OnAbort(GestureLayer.MomentumData moment)
        {
            NavigationDrawerDragged?.Invoke(this, new NavigationDrawerDragEventArgs(GestureLayer.GestureState.Abort, moment));
        }


    }
}