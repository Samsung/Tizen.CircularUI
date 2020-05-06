using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Watch;

namespace Tizen.Wearable.CircularUI.Forms.Renderer.Shell
{
    public class CircularShellSectionItemsRenderer : ShellSectionItemsRenderer
    {
        public CircularShellSectionItemsRenderer(ShellSection section) : base(section)
        {
        }

        protected override bool OnRotated(bool isClockwise)
        {
            if (GetCurrentPage(ShellSection) is CirclePage circlePage)
            {
                var renderer = Platform.GetOrCreateRenderer(circlePage);
                if (renderer is CirclePageRenderer circlePageRenderer)
                {
                    circlePageRenderer.UpdateRotaryFocusObject(false);
                    return false;
                }
            } 
            return base.OnRotated(isClockwise);
        }

        Page GetCurrentPage(ShellSection section)
        {
            return section?.Stack?.LastOrDefault<Page>() ?? (section?.CurrentItem as IShellContentController)?.Page;
        }
    }
}
