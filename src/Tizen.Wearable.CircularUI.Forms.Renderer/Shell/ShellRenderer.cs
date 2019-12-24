using ElmSharp;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using XForms = Xamarin.Forms.Forms;
using XShell = Xamarin.Forms.Shell;

[assembly: ExportRenderer(typeof(Tizen.Wearable.CircularUI.Forms.CircularShell), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.ShellRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class ShellRenderer : VisualElementRenderer<CircularShell>
    {
        Box _mainLayout;
        EvasObject _currentItem;
        Panel _drawer;
        NavigationView _navigationView;

        Dictionary<BaseShellItem, IShellItemRenderer> _rendererCache = new Dictionary<BaseShellItem, IShellItemRenderer>();

        public ShellRenderer()
        {
            RegisterPropertyHandler(XShell.CurrentItemProperty , UpdateCurrentItem);
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
                SetNativeView(_mainLayout);
            }
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

            // TODO. Need to improve, Change to high level component, implement without Panel
            _drawer = new Panel(XForms.NativeParent);
            _drawer.Show();
            _drawer.IsOpen = false;
            _drawer.Direction = PanelDirection.Bottom;
            _drawer.Toggled += OnDrawerToggled;
            _drawer.SetScrollable(true);
            _drawer.SetScrollableArea(1.0);
            _mainLayout.PackEnd(_drawer);
            _navigationView = new NavigationView(XForms.NativeParent)
            {
                AlignmentX = -1,
                AlignmentY = -1,
                WeightX = 1,
                WeightY = 1
            };
            _navigationView.Show();
            _drawer.SetContent(_navigationView);
            if (_currentItem != null)
            {
                _drawer.StackAbove(_currentItem);
            }
            _navigationView.ItemSelected += OnMenuItemSelected;
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

        void OnDrawerToggled(object sender, EventArgs e)
        {
            if (_drawer.IsOpen)
            {
                _navigationView.Activate();
            }
            else
            {
                //TODO. need to fix, 
                // It has bug, Rotary event was sent to a widget that called the Activate() method at last, 
                // so, if a CurrentItem was not changed, Activate method was not called and anybody can't receive Rotary event
                _navigationView.Deactivate();
            }
            Element.SetValueFromRenderer(Shell.FlyoutIsPresentedProperty, _drawer.IsOpen);
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
            if (_drawer != null)
            {
                _drawer.IsOpen = Element.FlyoutIsPresented;
            }
        }

        void OnLayout()
        {
            if (_currentItem != null)
            {
                _currentItem.Geometry = _mainLayout.Geometry;
            }
            if (_drawer != null)
            {
                _drawer.Geometry = _mainLayout.Geometry;
            }
        }

        void SetCurrentItem(EvasObject item)
        {
            _currentItem = item;
            _currentItem.Show();
            _mainLayout.PackEnd(_currentItem);
            if (_drawer != null)
            {
                _drawer.StackAbove(_currentItem);
            }
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