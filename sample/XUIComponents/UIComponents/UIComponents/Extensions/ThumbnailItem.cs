using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace UIComponents.Extensions
{
    public class ThumbnailItem : View
    {
        public static readonly BindableProperty ImageProperty = BindableProperty.Create("Image", typeof(FileImageSource), typeof(ThumbnailItem), default(FileImageSource));

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
