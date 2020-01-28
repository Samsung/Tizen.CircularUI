﻿using ElmSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using XForms = Xamarin.Forms.Forms;
using XShell = Xamarin.Forms.Shell;

[assembly: ExportRenderer(typeof(XShell), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.ShellRenderer))]
namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class ShellRenderer : VisualElementRenderer<XShell>
    {
        NavigationDrawer _drawer;
        NavigationView _navigationView;

        Dictionary<BaseShellItem, IShellItemRenderer> _rendererCache = new Dictionary<BaseShellItem, IShellItemRenderer>();

        public ShellRenderer()
        {
            RegisterPropertyHandler(XShell.CurrentItemProperty, UpdateCurrentItem);
            RegisterPropertyHandler(XShell.FlyoutIsPresentedProperty, UpdateFlyoutIsPresented);
            RegisterPropertyHandler(XShell.FlyoutBehaviorProperty, UpdateFlyoutBehavior);
            RegisterPropertyHandler(XShell.FlyoutIconProperty, UpdateFlyoutIcon);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<XShell> e)
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
            if (_drawer == null)
            {
                _drawer = new NavigationDrawer(XForms.NativeParent);
                _drawer.IsOpen = Element.FlyoutIsPresented;
                _drawer.Toggled += OnNavigationDrawerToggled;
                SetNativeView(_drawer);
            }
        }

        void OnNavigationStructureChanged(object sender, EventArgs e)
        {
            UpdateFlyoutMenu();
        }

        void UpdateFlyoutMenu()
        {
            if (Element.FlyoutBehavior == FlyoutBehavior.Disabled)
                return;

            var flyoutItems = (Element as IShellController).GenerateFlyoutGrouping();
            if (flyoutItems.Count > 1)
            {
                InitializeNavigationDrawer();
                _navigationView.Build(flyoutItems);
            }
            else
            {
                DeinitializeNavigationView();
            }
        }

        void InitializeNavigationDrawer()
        {
            if (_navigationView != null)
            {
                return;
            }

            _navigationView = new NavigationView(XForms.NativeParent)
            {
                AlignmentX = -1,
                AlignmentY = -1,
                WeightX = 1,
                WeightY = 1,
            };
            _navigationView.Show();
            _navigationView.ItemSelected += OnMenuItemSelected;

            _drawer.SetDrawerContent(_navigationView);
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

                if (currentPage == null)
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

        void DeinitializeNavigationView()
        {
            if (_navigationView == null)
                return;
            _drawer.SetDrawerContent(null);
            _navigationView.Unrealize();
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

        void UpdateFlyoutBehavior()
        {
            if (Element.FlyoutBehavior == FlyoutBehavior.Disabled)
            {
                DeinitializeNavigationView();
            }
            else if (Element.FlyoutBehavior == FlyoutBehavior.Flyout)
            {
                UpdateFlyoutMenu();
            }
            else if (Element.FlyoutBehavior == FlyoutBehavior.Locked)
            {
                // Locked behavior is not supported on circularshell
            }
        }

        void UpdateFlyoutIcon(bool init)
        {
            if (init && Element.FlyoutIcon == null)
                return;

            _drawer.UpdateDrawerIcon(Element.FlyoutIcon);
        }

        void UpdateFlyoutIsPresented()
        {
            _drawer.IsOpen = Element.FlyoutIsPresented;
        }

        void SetCurrentItem(EvasObject item)
        {
            _drawer.SetMainContent(item);
        }

        void ResetCurrentItem()
        {
            _drawer.SetMainContent(null);
        }
    }
}