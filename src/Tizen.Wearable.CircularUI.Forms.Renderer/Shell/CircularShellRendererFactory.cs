using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen.Watch;

namespace Tizen.Wearable.CircularUI.Forms.Renderer.Shell
{
    class CircularShellRendererFactory : ShellRendererFactory
    {
        public static void Setup()
        {
            Default = new CircularShellRendererFactory();
        }

        public override IShellItemRenderer CreateItemRenderer(ShellSection item)
        {
            if (item.Items.Count == 1)
            {
                return CreateItemRenderer(item.CurrentItem);
            }
            return new CircularShellSectionItemsRenderer(item);
        }
    }
}
