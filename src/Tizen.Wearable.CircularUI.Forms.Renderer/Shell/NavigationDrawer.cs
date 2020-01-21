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
        Box _contentBox;
        EvasObject _content;
        Box _drawerBox;
        Box _drawerContentBox;
        EvasObject _drawerContent;
        Box _drawerIconBox;
        GestureLayer _contentGesture;
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

        public void SetMainContent(EvasObject content)
        {
            if (content == null)
            {
                UnsetMainContent();
                return;
            }

            _content = content;
            _content.Show();
            _contentBox.PackEnd(_content);
            _content.Geometry = _contentBox.Geometry;
        }

        public void SetDrawerContent(EvasObject content)
        {
            if (content == null)
            {
                UnsetDrawerContent();
                return;
            }

            _drawerContent = content;
            _drawerContent.Show();
            _drawerContentBox.PackEnd(_drawerContent);

            _drawerContentBox.Show();
            _drawerIconBox.Show();

            if (_drawerContent is NavigationView nv)
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
            var toMove = _drawerBox.Geometry;
            toMove.Y = 0;

            await RunMoveAnimation(_drawerBox, toMove, length);

            if (!_isOpen)
            {
                _isOpen = true;
                Toggled?.Invoke(this, EventArgs.Empty);
            }
        }

        public async void Close(uint length = 300)
        {
            var toMove = _drawerBox.Geometry;
            toMove.Y = Geometry.Height - _iconHeight;

            await RunMoveAnimation(_drawerBox, toMove, length);

            if (_isOpen)
            {
                _isOpen = false;
                Toggled?.Invoke(this, EventArgs.Empty);
            }
        }

        protected override IntPtr CreateHandle(EvasObject parent)
        {
            _mainLayout = new Box(parent);
            return _mainLayout.Handle;
        }

        void IAnimatable.BatchBegin()
        {
        }

        void IAnimatable.BatchCommit()
        {
        }

        void Initialize()
        {
            AlignmentX = -1;
            AlignmentY = -1;
            WeightX = 1;
            WeightY = 1;

            _contentBox = new Box(_mainLayout);
            _contentBox.Show();
            _mainLayout.PackEnd(_contentBox);

            _drawerBox = new Box(_mainLayout);
            _drawerBox.Show();
            _mainLayout.PackEnd(_drawerBox);

            _drawerContentBox = new Box(_drawerBox);
            _drawerBox.PackEnd(_drawerContentBox);

            _drawerIconBox = new Box(_drawerBox);
            _drawerBox.PackEnd(_drawerIconBox);

            //TODO remove it
            _drawerIconBox.BackgroundColor = EColor.Pink;

            _contentGesture = new GestureLayer(_contentBox);
            _contentGesture.Attach(_contentBox);
            _contentGesture.SetMomentumCallback(GestureLayer.GestureState.Start, OnContentDragStarted);
            _contentGesture.SetMomentumCallback(GestureLayer.GestureState.End, OnContentDragEnded);
            _contentGesture.SetMomentumCallback(GestureLayer.GestureState.Abort, OnContentDragEnded);

            _drawerGesture = new GestureLayer(_drawerIconBox);
            _drawerGesture.Attach(_drawerIconBox);

            _drawerGesture.SetMomentumCallback(GestureLayer.GestureState.Move, OnDrawerDragged);
            _drawerGesture.SetMomentumCallback(GestureLayer.GestureState.End, OnDrawerDragEnded);
            _drawerGesture.SetMomentumCallback(GestureLayer.GestureState.Abort, OnDrawerDragEnded);

            _mainLayout.SetLayoutCallback(OnLayout);
            _drawerBox.SetLayoutCallback(OnDrawerContentLayout);
            _contentBox.SetLayoutCallback(OnContentLayout);

            RotaryEventManager.Rotated += OnRotateEventReceived;
        }

        async Task<bool> ShowAsync(EWidget target, Easing easing = null, uint length = 300, CancellationToken cancelltaionToken = default(CancellationToken))
        {
            var tcs = new TaskCompletionSource<bool>();

            await Task.Delay(1000);

            if (cancelltaionToken.IsCancellationRequested)
            {
                cancelltaionToken.ThrowIfCancellationRequested();
            }

            target.Show();
            var opacity = target.Opacity;

            if (opacity == 255 || opacity == -1)
                return true;

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

        void OnLayout()
        {
            var geometry = Geometry;
            _contentBox.Geometry = geometry;

            geometry.Y = (_isOpen) ? 0 : (geometry.Height - _iconHeight);
            _drawerBox.Geometry = geometry;
        }

        void OnContentLayout()
        {
            if (_content != null)
            {
                _content.Geometry = _contentBox.Geometry;
            }
        }

        void OnDrawerContentLayout()
        {
            var geometry = _drawerBox.Geometry;
            _drawerContentBox.Geometry = _drawerBox.Geometry;

            geometry.Height = _iconHeight;
            _drawerIconBox.Geometry = geometry;

            _drawerIconBox.StackAbove(_drawerContentBox);
        }

        async Task<bool> HideAsync(EWidget target, Easing easing = null, uint length = 300)
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

        async void OnRotateEventReceived(EventArgs args)
        {
            _fadeInCancelTokenSource?.Cancel();

            _fadeInCancelTokenSource = new CancellationTokenSource();
            var token = _fadeInCancelTokenSource.Token;

            if (!_isOpen)
            {
                await HideAsync(_drawerBox);

                _ = ShowAsync(_drawerBox, cancelltaionToken: token);
            }
        }

        void OnContentDragStarted(GestureLayer.MomentumData moment)
        {
            _fadeInCancelTokenSource?.Cancel();
            _fadeInCancelTokenSource = null;

            if (!_isOpen)
            {
                _ = HideAsync(_drawerBox);
            }
        }

        void OnContentDragEnded(GestureLayer.MomentumData moment)
        {
            _fadeInCancelTokenSource = new CancellationTokenSource();

            _ = ShowAsync(_drawerBox, cancelltaionToken: _fadeInCancelTokenSource.Token);
        }

        void OnDrawerDragged(GestureLayer.MomentumData moment)
        {
            var toMove = _drawerBox.Geometry;
            toMove.Y = (moment.Y2 < 0) ? 0 : moment.Y2;

            _drawerBox.Geometry = toMove;
        }

        void OnDrawerDragEnded(GestureLayer.MomentumData moment)
        {
            if (_drawerBox.Geometry.Y < (_mainLayout.Geometry.Height / 2))
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

        void UnsetMainContent()
        {
            if (_content != null)
            {
                _contentBox.UnPack(_content);
                _content.Hide();
                _content = null;
            }
        }

        void UnsetDrawerContent()
        {
            if (_drawerContent != null)
            {
                _drawerContentBox.UnPack(_drawerContent);
                _drawerContent.Hide();
                _drawerContent = null;

                _drawerContentBox.Hide();
                _drawerIconBox.Hide();
            }
        }
    }
}