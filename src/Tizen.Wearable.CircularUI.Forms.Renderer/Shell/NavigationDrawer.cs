using System;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using ElmSharp;
using ElmSharp.Wearable;
using EWidget = ElmSharp.Widget;
using ELayout = ElmSharp.Layout;
using EColor = ElmSharp.Color;

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class NavigationDrawer : ELayout, IAnimatable
    {
        int _iconHeight = 50;

        Box _mainLayout;
        Box _drawer;
        Box _contentBox;
        Box _iconBox;
        GestureLayer _mainLayoutGesture;
        GestureLayer _drawerGesture;
        bool _isOpen;

        CancellationTokenSource _fadeInCancelTokenSource = null;

        public int IconHeight
        {
            get
            {
                return _iconHeight;
            }
            set
            {
                _iconHeight = value;
            }
        }

        public bool IsOpen
        {
            get
            {
                return _isOpen;
            }
            set
            {
                if (_isOpen != value)
                {
                    if (value)
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

        public NavigationDrawer(EvasObject parent) : base(parent)
        {
            Initialize();
        }

        public void BatchBegin()
        {
        }

        public void BatchCommit()
        {
        }

        public new void SetContent(EvasObject content)
        {
            _contentBox.PackEnd(content);

            if (content is NavigationView nv)
            {
                nv.Dragged += (s, e) =>
                {
                    if (e.State == DraggedState.EdgeTop)
                    {
                        Close();
                    }
                };
            }
        }

        public async void Open(uint length = 300)
        {
            var toMove = _drawer.Geometry;
            toMove.Y = 0;

            await RunMoveAnimation(_drawer, toMove, length);

            if (!_isOpen)
            {
                _isOpen = true;
                Toggled?.Invoke(this, EventArgs.Empty);
            }
        }

        public async void Close(uint length = 300)
        {
            var toMove = _drawer.Geometry;
            toMove.Y = Geometry.Height - _iconHeight;

            await RunMoveAnimation(_drawer, toMove, length);

            if (_isOpen)
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

            target.Show();
            var opacity = target.Opacity;

            new Animation((progress) =>
            {
                target.Opacity = opacity + (int)((255 - opacity) * progress);

            }).Commit(this, "FadeIn", length: length, finished: (p, e) =>
            {
                target.Opacity = 255;
                tcs.SetResult(true);
            });

            return await tcs.Task;
        }

        public async Task<bool> HideAsync(EWidget target, Easing easing = null, uint length = 300)
        {
            var tcs = new TaskCompletionSource<bool>();

            var opacity = target.Opacity;
            new Animation((progress) =>
            {
                target.Opacity = opacity - (int)(progress * opacity);

            }).Commit(this, "FadeOut", length: length, finished: (p, e) =>
            {
                target.Opacity = 0;
                target.Hide();
                tcs.SetResult(true);
            });

            return await tcs.Task;
        }

        protected override IntPtr CreateHandle(EvasObject parent)
        {
            _mainLayout = new Box(parent);
            return _mainLayout.Handle;
        }

        void Initialize()
        {
            AlignmentX = -1;
            AlignmentY = -1;
            WeightX = 1;
            WeightY = 1;

            _mainLayout.RepeatEvents = true;

            _drawer = new Box(this);
            _drawer.Show();

            _contentBox = new Box(this);
            _contentBox.PropagateEvents = false;
            _contentBox.Show();

            _iconBox = new Box(this);
            //TODO remove it
            _iconBox.BackgroundColor = EColor.Pink;
            _iconBox.Show();

            _drawer.PackEnd(_contentBox);
            _drawer.PackEnd(_iconBox);

            _mainLayoutGesture = new GestureLayer(_mainLayout);
            _mainLayoutGesture.Attach(_mainLayout);

            _mainLayoutGesture.SetMomentumCallback(GestureLayer.GestureState.Start, OnContentDragStarted);
            _mainLayoutGesture.SetMomentumCallback(GestureLayer.GestureState.End, OnContentDragEnded);
            _mainLayoutGesture.SetMomentumCallback(GestureLayer.GestureState.Abort, OnContentDragEnded);

            _drawerGesture = new GestureLayer(_drawer);
            _drawerGesture.Attach(_drawer);

            _drawerGesture.SetMomentumCallback(GestureLayer.GestureState.Move, OnDrawerDragged);
            _drawerGesture.SetMomentumCallback(GestureLayer.GestureState.End, OnDrawerDragEnded);
            _drawerGesture.SetMomentumCallback(GestureLayer.GestureState.Abort, OnDrawerDragEnded);

            _mainLayout.SetLayoutCallback(OnLayout);
            _drawer.SetLayoutCallback(OnContentLayout);

            RotaryEventManager.Rotated += OnRotateEventReceived;
        }

        async void OnRotateEventReceived(EventArgs args)
        {
            if (_fadeInCancelTokenSource != null)
            {
                _fadeInCancelTokenSource.Cancel();
            }

            _fadeInCancelTokenSource = new CancellationTokenSource();
            var token = _fadeInCancelTokenSource.Token;

            if (!_isOpen)
            {
                await HideAsync(_drawer);

                _ = ShowAsync(_drawer, cancelltaionToken:token);
            }
        }

        void OnLayout()
        {
            var geometry = Geometry;
            geometry.Y = (_isOpen) ? 0 : (geometry.Height - _iconHeight);

            _mainLayout.Geometry = Geometry;
            _drawer.Geometry = geometry;
        }

        void OnContentLayout()
        {
            var geometry = _drawer.Geometry;
            geometry.Height = _iconHeight;

            _contentBox.Geometry = _drawer.Geometry;
            _iconBox.Geometry = geometry;

            _iconBox.StackAbove(_contentBox);
        }

        void OnContentDragStarted(GestureLayer.MomentumData moment)
        {
            if (_fadeInCancelTokenSource != null)
            {
                _fadeInCancelTokenSource.Cancel();
                _fadeInCancelTokenSource = null;
            }

            if (!_isOpen)
            {
                _ = HideAsync(_drawer);
            }
        }

        void OnContentDragEnded(GestureLayer.MomentumData moment)
        {
            if (_fadeInCancelTokenSource == null)
            {
                _fadeInCancelTokenSource = new CancellationTokenSource();
            }

            var token = _fadeInCancelTokenSource.Token;

            _ = ShowAsync(_drawer, cancelltaionToken:token);
        }

        void OnDrawerDragged(GestureLayer.MomentumData moment)
        {
            var toMove = _drawer.Geometry;
            toMove.Y = (moment.Y2 < 0) ? 0 : moment.Y2;

            _drawer.Geometry = toMove;
        }

        void OnDrawerDragEnded(GestureLayer.MomentumData moment)
        {
            if (_drawer.Geometry.Y < (_mainLayout.Geometry.Height / 2))
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
            }).Commit(this, "Move", length: length, finished: (s, e) =>
            {
                target.Geometry = dest;
                tcs.SetResult(true);
            });
            return tcs.Task;
        }
    }
}