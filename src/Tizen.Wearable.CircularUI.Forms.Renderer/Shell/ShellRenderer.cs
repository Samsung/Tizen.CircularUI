using ElmSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using EColor = ElmSharp.Color;
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

                InitializeNavigationDrawer();

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

            _drawer = new NavigationDrawer(XForms.NativeParent, Element);
            _drawer.Show();
            _drawer.SetContent(_navigationView);

            _drawer.Toggled += OnNavigationDrawerToggled;
        }

        void OnNavigationDrawerToggled(object sender, EventArgs e)
        {
            if (_drawer.IsOpen)
            {
                _navigationView.Activate();
            }
            else
            {
                _navigationView.Deactivate();

                var stack = (Element.CurrentItem.CurrentItem as ShellSection)?.Stack;
                var currentPage = stack?.LastOrDefault<Page>();

                if(currentPage == null)
                {
                   currentPage = (Element.CurrentItem.CurrentItem.CurrentItem as IShellContentController)?.Page;
                }

                if (currentPage != null)
                {
                    var renderer = Platform.GetOrCreateRenderer(currentPage);
                    (renderer as CirclePageRenderer)?.UpdateRotaryFocusObject(false);
                }
            }

            Element.SetValueFromRenderer(XShell.FlyoutIsPresentedProperty, _drawer.IsOpen);
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
            _drawer.IsOpen = Element.FlyoutIsPresented;
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
                _drawer.Geometry = _mainLayout.Geometry;
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