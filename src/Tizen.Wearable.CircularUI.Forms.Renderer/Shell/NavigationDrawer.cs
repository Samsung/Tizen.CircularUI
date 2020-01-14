using System;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using ElmSharp;
using EWidget = ElmSharp.Widget;
using ELayout = ElmSharp.Layout;
using EColor = ElmSharp.Color;
using ElmSharp.Wearable;

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class NavigationDrawer : ELayout
    {
        readonly int MinimumDrawerHeight = 50;

        Box _gestureBox;
        Box _outterBox;
        Box _contentBox;
        EvasObject _label;
        GestureLayer _gestureLayer;
        GestureLayer _dragGestureLayer;
        EvasObject _parent;
        IAnimatable _animatable;
        bool _isOpen;

        CancellationTokenSource _fadeInCancelTokenSource = null;

        public bool IsOpen
        {
            get
            {
                return _isOpen;
            }
            set
            {
                if(_isOpen != value)
                {
                    if(value)
                    {
                        Open();
                    }
                    else
                    {
                        Close();
                    }
                }
            }
        }

        public event EventHandler Toggled;

        public NavigationDrawer(EvasObject parent, IAnimatable animatable) : base(parent)
        {
            _parent = parent;
            _animatable = animatable;
            Initialize();
        }

        public new void SetContent(EvasObject content)
        {
            _contentBox.PackEnd(content);

            if(content is NavigationView nv)
            {
                nv.Dragged += (s, e) =>
                {
                    if(e.State == DraggedState.EdgeTop)
                    {
                        Close();
                    }
                };
            }
        }

        public async void Open(uint length = 300)
        {
            var toMove = _outterBox.Geometry;
            toMove.Y = 0;

            await RunMoveAnimation(_outterBox, toMove, length);

            if(!_isOpen)
            {
                _isOpen = true;
                Toggled?.Invoke(this, EventArgs.Empty);
            }
        }

        public async void Close(uint length = 300)
        {
            var toMove = _outterBox.Geometry;
            toMove.Y = Geometry.Height - MinimumDrawerHeight;

            await RunMoveAnimation(_outterBox, toMove, length);

            if(_isOpen)
            {
                _isOpen = false;
                Toggled?.Invoke(this, EventArgs.Empty);
            }
        }

        public async Task<bool> ShowAsync(EWidget target, Easing easing = null, uint length = 300, CancellationToken cancelltaionToken = default(CancellationToken))
        {
            var tcs = new TaskCompletionSource<bool>();

            await Task.Delay(1000);

            if (cancelltaionToken.IsCancellationRequested)
            {
                cancelltaionToken.ThrowIfCancellationRequested();
            }

            _outterBox.Show();
            var opacity = _outterBox.Opacity;

            new Animation((progress) =>
            {
                _outterBox.Opacity = opacity + (int)((255 - opacity) * progress);

            }).Commit(_animatable, "FadeIn", length: length, finished: (p, e) =>
            {
                _outterBox.Opacity = 255;
                tcs.SetResult(true);
            });

            return await tcs.Task;
        }

        public async Task<bool> HideAsync(EvasObject target, Easing easing = null, uint length = 300)
        {
            var tcs = new TaskCompletionSource<bool>();

            var opacity = _outterBox.Opacity;
            new Animation((progress) =>
            {
                _outterBox.Opacity = opacity - (int)(progress * opacity);

            }).Commit(_animatable, "FadeOut", length: length, finished: (p, e) =>
            {
                _outterBox.Opacity = 0;
                _outterBox.Hide();
                tcs.SetResult(true);
            });

            return await tcs.Task;
        }

        protected override IntPtr CreateHandle(EvasObject parent)
        {
            _gestureBox = new Box(parent);
            return _gestureBox.Handle;
        }

        void Initialize()
        {
            AlignmentX = -1;
            AlignmentY = -1;
            WeightX = 1;
            WeightY = 1;

            _gestureBox.RepeatEvents = true;

            _outterBox = new Box(this);
            _outterBox.Show();

            _contentBox = new Box(this);
            _contentBox.PropagateEvents = false;

            _label = new ElmSharp.Label(_outterBox)
            {
                BackgroundColor = EColor.Pink,
            };

            _contentBox.Show();
            _label.Show();

            _outterBox.PackEnd(_contentBox);
            _outterBox.PackEnd(_label);

            _label.StackAbove(_contentBox);

            _gestureLayer = new GestureLayer(_gestureBox);
            _gestureLayer.Attach(_gestureBox);

            _gestureLayer.SetMomentumCallback(GestureLayer.GestureState.Start, OnStart);
            _gestureLayer.SetMomentumCallback(GestureLayer.GestureState.End, OnEnd);
            _gestureLayer.SetMomentumCallback(GestureLayer.GestureState.Abort, OnEnd);

            _dragGestureLayer = new GestureLayer(_outterBox);
            _dragGestureLayer.Attach(_outterBox);

            _dragGestureLayer.SetMomentumCallback(GestureLayer.GestureState.Move, OnDragged);
            _dragGestureLayer.SetMomentumCallback(GestureLayer.GestureState.End, OnDragEnd);
            _dragGestureLayer.SetMomentumCallback(GestureLayer.GestureState.Abort, OnDragEnd);

            _gestureBox.SetLayoutCallback(OnLayout);
            _outterBox.SetLayoutCallback(OnContentLayout);

            RotaryEventManager.Rotated += OnRotate;
        }

        async void OnRotate(EventArgs args)
        {
            if(_fadeInCancelTokenSource != null)
            {
                _fadeInCancelTokenSource.Cancel();
            }

            _fadeInCancelTokenSource = new CancellationTokenSource();
            var token = _fadeInCancelTokenSource.Token;

            if (!_isOpen)
            {
                await HideAsync(_outterBox);

                _ = ShowAsync(_outterBox, cancelltaionToken:token);
            }
        }

        void OnLayout()
        {

            var geometry = _parent.Geometry;
            geometry.Y = (_isOpen) ? 0 : (geometry.Height - MinimumDrawerHeight);

            _gestureBox.Geometry = _parent.Geometry;
            _outterBox.Geometry = geometry;
        }

        void OnContentLayout()
        {
            var geometry = _outterBox.Geometry;
            geometry.Height = MinimumDrawerHeight;

            _contentBox.Geometry = _outterBox.Geometry;
            _label.Geometry = geometry;
        }

        void OnStart(GestureLayer.MomentumData moment)
        {
            if(_fadeInCancelTokenSource != null)
            {
                _fadeInCancelTokenSource.Cancel();
                _fadeInCancelTokenSource = null;
            }

            _ = HideAsync((EvasObject)_outterBox);
        }

        void OnEnd(GestureLayer.MomentumData moment)
        {
            if (_fadeInCancelTokenSource == null)
            {
                _fadeInCancelTokenSource = new CancellationTokenSource();
            }

            var token = _fadeInCancelTokenSource.Token;

            _ = ShowAsync(_outterBox, cancelltaionToken:token);
        }

        void OnDragged(GestureLayer.MomentumData moment)
        {
            var toMove = _outterBox.Geometry;
            toMove.Y = (moment.Y2 < 0) ? 0 : moment.Y2;

            _outterBox.Geometry = toMove;
        }

        void OnDragEnd(GestureLayer.MomentumData moment)
        {
            if (_outterBox.Geometry.Y < (_gestureBox.Geometry.Height / 2))
            {
                Open();
            }
            else
            {
                Close();
            }
        }

        Task RunMoveAnimation(EvasObject target, Rect dest, uint length, Easing easing = null)
        {
            var tcs = new TaskCompletionSource<bool>();

            var dx = target.Geometry.X - dest.X;
            var dy = target.Geometry.Y - dest.Y;

            new Animation((progress) =>
            {
                var toMove = dest;
                toMove.X += (int)(dx * (1 - progress));
                toMove.Y += (int)(dy * (1 - progress));

                target.Geometry = toMove;
            }).Commit(_animatable, "Move", length: length, finished: (s, e) =>
            {
                target.Geometry = dest;
                tcs.SetResult(true);
            });
            return tcs.Task;
        }
    }
}