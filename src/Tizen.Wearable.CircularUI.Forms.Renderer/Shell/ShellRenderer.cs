using ElmSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using EColor = ElmSharp.Color;
using EWidget = ElmSharp.Widget;
using XForms = Xamarin.Forms.Forms;
using XShell = Xamarin.Forms.Shell;

[assembly: ExportRenderer(typeof(Tizen.Wearable.CircularUI.Forms.CircularShell), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.ShellRenderer))]
namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class ShellRenderer : VisualElementRenderer<CircularShell>
    {
        Box _mainLayout;
        EvasObject _currentItem;
        NavigationDrawer _drawer;
        NavigationView _navigationView;
        GestureLayer _gestureLayer;

        List<CancellationTokenSource> _fadeInQueue = new List<CancellationTokenSource>();

        Dictionary<BaseShellItem, IShellItemRenderer> _rendererCache = new Dictionary<BaseShellItem, IShellItemRenderer>();

        public ShellRenderer()
        {
            RegisterPropertyHandler(XShell.CurrentItemProperty, UpdateCurrentItem);
            RegisterPropertyHandler(XShell.FlyoutIsPresentedProperty, UpdateFlyoutIsPresented);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CircularShell> e)
        {
            InitializeComponent();
            base.OnElementChanged(e);
        }

        protected override void OnElementReady()
        {
            base.OnElementReady();
            UpdateFlyoutMenu();
            (Element as IShellController).StructureChanged += OnNavigationStructureChanged;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var renderer in _rendererCache.Values)
                {
                    renderer.Dispose();
                }
                (Element as IShellController).StructureChanged -= OnNavigationStructureChanged;
            }
            base.Dispose(disposing);
        }

        void InitializeComponent()
        {
            if (_mainLayout == null)
            {
                _mainLayout = new Box(XForms.NativeParent);
                _mainLayout.SetLayoutCallback(OnLayout);

                // TODO : NavigationDrawer disappearing anmation is triggered by user's gesture only.
                // It doesn't work for the rotary event.
                _gestureLayer = new GestureLayer(_mainLayout);
                _gestureLayer.Attach(_mainLayout);

                _gestureLayer.SetMomentumCallback(GestureLayer.GestureState.Start, OnStart);
                _gestureLayer.SetMomentumCallback(GestureLayer.GestureState.End, OnEnd);
                _gestureLayer.SetMomentumCallback(GestureLayer.GestureState.Abort, OnEnd);

                SetNativeView(_mainLayout);
            }
        }

        void OnStart(GestureLayer.MomentumData moment)
        {
            foreach (var cts in _fadeInQueue)
            {
                cts.Cancel();
            }
            _fadeInQueue.Clear();

            _ = FadeIn(_drawer);
        }

        void OnEnd(GestureLayer.MomentumData moment)
        {
            _ = FadeOut(_drawer);
        }

        Task FadeOut(EWidget target, Easing easing = null, uint length = 300)
        {
            var tcs = new TaskCompletionSource<bool>();
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            Task.Run(() =>
            {
                Task.Delay(1000).Wait();

                if (token.IsCancellationRequested)
                    token.ThrowIfCancellationRequested();

                var opacity = _drawer.Opacity;
                new Animation((progress) =>
                {
                    _drawer.Opacity = opacity + (int)((255 - opacity) * progress);

                }).Commit(Element, "FadeIn", length: length, finished: (p, e) =>
                {
                    _drawer.Opacity = 255;
                    tcs.SetResult(true);
                });
            });
            _fadeInQueue.Add(cts);

            return tcs.Task;
        }

        Task FadeIn(EvasObject target, Easing easing = null, uint length = 300)
        {
            var tcs = new TaskCompletionSource<bool>();

            var opacity = _drawer.Opacity;
            new Animation((progress) =>
            {
                _drawer.Opacity = opacity - (int)(progress * opacity);

            }).Commit(Element, "FadeOut", length: length, finished: (p, e) =>
            {
                tcs.SetResult(true);
            });

            return tcs.Task;
        }

        void OnNavigationStructureChanged(object sender, EventArgs e)
        {
            // TODO. Need to optimize, this event was called evey time when CurrentItem was changed even if structure of menu was not changed
            UpdateFlyoutMenu();
        }

        void UpdateFlyoutMenu()
        {
            var flyoutItems = (Element as IShellController).GenerateFlyoutGrouping();
            if (flyoutItems.Count > 0)
            {
                InitializeNavigationDrawer();
                _navigationView.Build(flyoutItems);
                OnLayout();
            }
            else
            {
                DeinitializeNavigationDrawer();
            }
        }

        void InitializeNavigationDrawer()
        {
            if (_drawer != null)
            {
                return;
            }

            _navigationView = new NavigationView(XForms.NativeParent)
            {
                AlignmentX = -1,
                AlignmentY = -1,
                WeightX = 1,
                WeightY = 1,
                BackgroundColor = EColor.Yellow
            };
            _navigationView.Show();
            _navigationView.ItemSelected += OnMenuItemSelected;

            _drawer = new NavigationDrawer(XForms.NativeParent);
            _drawer.Show();
            _drawer.SetContent(_navigationView);

            _drawer.NavigationDrawerDragged += OnNavigationDrawerDragged;
        }

        void OnNavigationDrawerDragged(object sender, NavigationDrawerDragEventArgs e)
        {
            if (e.State == GestureLayer.GestureState.Move)
            {
                MoveNaviationDrawer(e.Data.Y2);
            }
            else if (e.State == GestureLayer.GestureState.End || e.State == GestureLayer.GestureState.Abort)
            {
                if (_drawer.Geometry.Y < _drawer.NavigationDrawOpenThredhold)
                {
                    OpenNaviationDrawerAsync();
                }
                else
                {
                    CloseNaviationDrawerAsync();
                }
            }
        }

        void MoveNaviationDrawer(int dy)
        {
            var toMove = _drawer.Geometry;
            toMove.Y = (dy < 0) ? 0 : dy;

            _drawer.Geometry = toMove;
        }

        async void OpenNaviationDrawerAsync()
        {
            var toMove = _drawer.Geometry;
            toMove.Y = 0;
            
            await RunMoveAnimation(_drawer, toMove);

            _drawer.IsLabelVisible = false;
            _navigationView.Activate();
        }

        async void CloseNaviationDrawerAsync()
        {
            var toMove = _drawer.Geometry;
            toMove.Y = NativeView.Geometry.Height - _drawer.MinimumDrawerHeight;

            await RunMoveAnimation(_drawer, toMove);
        }

        Task RunMoveAnimation(EvasObject target, Rect dest, Easing easing = null, uint length = 300)
        {
            var tcs = new TaskCompletionSource<bool>();

            var dx = target.Geometry.X - dest.X;
            var dy = target.Geometry.Y - dest.Y;

            new Animation((progress) =>
            {
                var toMove = dest;
                toMove.Y += (int)(dy * (1 -progress));
                toMove.X += (int)(dx * (1 - progress));

                _drawer.Geometry = toMove;
            }).Commit(Element, "Move", length: length, finished: (s, e) =>
            {
                _drawer.Geometry = dest;
                tcs.SetResult(true);
            });
            return tcs.Task;
        }

        void DeinitializeNavigationDrawer()
        {
            if (_drawer == null)
                return;

            _mainLayout.UnPack(_drawer);
            _drawer.Unrealize();
            _drawer = null;
            _navigationView = null;
        }

        void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((IShellController)Element).OnFlyoutItemSelected(e.SelectedItem as Element);
        }

        void UpdateCurrentItem()
        {
            ResetCurrentItem();
            if (Element.CurrentItem != null)
            {
                if (!_rendererCache.TryGetValue(Element.CurrentItem, out IShellItemRenderer renderer))
                {
                    renderer = ShellRendererFactory.Default.CreateItemRenderer(Element.CurrentItem);
                    _rendererCache[Element.CurrentItem] = renderer;
                }
                SetCurrentItem(renderer.NativeView);
            }
        }

        void UpdateFlyoutIsPresented()
        {
            if(Element.FlyoutIsPresented)
            {
                var geometry = _mainLayout.Geometry;
                _drawer.IsLabelVisible = false;
                _drawer.Geometry = geometry;
                _navigationView.Activate();

            } else
            {
                OnLayout();
            }
        }

        void OnLayout()
        {
            if (_currentItem != null)
            {
                _currentItem.Geometry = _mainLayout.Geometry;
                _currentItem.StackAbove(_drawer);
            }
            if(_drawer != null)
            {
                var geometry = _mainLayout.Geometry;
                geometry.Y = NativeView.Geometry.Height - _drawer.MinimumDrawerHeight;

                _drawer.IsLabelVisible = true;
                _drawer.Geometry = geometry;

                _navigationView.Deactivate();
            }
        }

        void SetCurrentItem(EvasObject item)
        {
            _currentItem = item;
            _currentItem.Show();
            _mainLayout.PackEnd(_currentItem);
        }

        void ResetCurrentItem()
        {
            if (_currentItem != null)
            {
                _mainLayout.UnPack(_currentItem);
                _currentItem.Hide();
                _currentItem = null;
            }
        }
    }
}