using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace UIComponents.Extensions
{
    /// <summary>
    /// View for Thumbnail item
    /// </summary>
    public class ThumbnailItem : View
    {
        /// <summary>
        /// Image source property
        /// </summary>
        public static readonly BindableProperty ImageProperty = BindableProperty.Create("Image", typeof(FileImageSource), typeof(ThumbnailItem), default(FileImageSource));

        /// <summary>
        /// Image source
        /// </summary>
        public FileImageSource Image
        {
            get { return (FileImageSource)GetValue(ImageProperty); }
            set
            {
                SetValue(ImageProperty, value);
            }
        }
    }
}
