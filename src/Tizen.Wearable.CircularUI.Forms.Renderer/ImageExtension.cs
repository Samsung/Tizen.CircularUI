
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    internal static class ImageExtension
    {
        internal static bool IsNullOrEmpty(this ImageSource imageSource) =>
        imageSource == null || imageSource.IsEmpty;
    }
}
