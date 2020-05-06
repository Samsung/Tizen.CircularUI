using ElmSharp;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using XShell = Xamarin.Forms.Shell;
using NavigationDrawer = Xamarin.Forms.Platform.Tizen.Watch.NavigationDrawer;
using NavigationView = Xamarin.Forms.Platform.Tizen.Watch.NavigationView;

[assembly: ExportRenderer(typeof(XShell), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.CircularShellRenderer))]
namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class CircularShellRenderer : Xamarin.Forms.Platform.Tizen.Watch.ShellRenderer
    {

        NavigationDrawer _drawer;
        NavigationView _navigationView;

        public CircularShellRenderer()
        {
            RegisterPropertyHandler(CircularShell.FlyoutIconBackgroundColorProperty, UpdateFlyoutIconBackgroundColor);
            RegisterPropertyHandler(CircularShell.FlyoutForegroundColorProperty, UpdateFlyoutForegroundColor);
        }

        protected override NavigationDrawer CreateNavigationDrawer(EvasObject parent)
        {
            return _drawer = base.CreateNavigationDrawer(parent);
        }

        protected override NavigationView CreateNavigationView(EvasObject parent)
        {
            return _navigationView = base.CreateNavigationView(parent);
        }

        protected override void OnNavigationDrawerToggled(object sender, EventArgs e)
        {
            base.OnNavigationDrawerToggled(sender, e);
            if (!Element.FlyoutIsPresented)
            {
                var currentPage = GetCurrentPage(Element);

                if (currentPage != null)
                {
                    var renderer = Platform.GetOrCreateRenderer(currentPage);
                    (renderer as CirclePageRenderer)?.UpdateRotaryFocusObject(false);
                }
            }
        }

        void UpdateFlyoutIconBackgroundColor()
        {
            _drawer.HandlerBackgroundColor = CircularShell.GetFlyoutIconBackgroundColor(Element).ToNative();
        }

        void UpdateFlyoutForegroundColor(bool init)
        {
            if (init && CircularShell.GetFlyoutForegroundColor(Element).IsDefault)
                return;

            if (_navigationView != null)
            {
                _navigationView.ForegroundColor = CircularShell.GetFlyoutForegroundColor(Element).ToNative();
            }
        }

        static Page GetCurrentPage(XShell shell)
        {
            var stack = (shell.CurrentItem.CurrentItem as ShellSection)?.Stack;
            var currentPage = stack?.LastOrDefault<Page>();

            if (currentPage == null)
            {
                currentPage = (shell.CurrentItem.CurrentItem.CurrentItem as IShellContentController)?.Page;
            }
            return currentPage;
        }
    }
}
