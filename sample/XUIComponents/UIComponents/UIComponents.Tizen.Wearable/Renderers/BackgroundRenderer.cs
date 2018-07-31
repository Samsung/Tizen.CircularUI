using System;
using System.ComponentModel;
using UIComponents.Extensions;
using UIComponents.Tizen.Wearable.Renderers;
using Xamarin.Forms.Platform.Tizen;
using EBackground = ElmSharp.Background;
using EBackgroundOptions = ElmSharp.BackgroundOptions;
using TForms = Xamarin.Forms.Platform.Tizen.Forms;

[assembly: ExportRenderer(typeof(Background), typeof(BackgroundRenderer))]
namespace UIComponents.Tizen.Wearable.Renderers
{
    public class BackgroundRenderer : ViewRenderer<Background, EBackground>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BackgroundRenderer()
        {
        }

        /// <summary>
        /// Called when element is changed.
        /// </summary>
        /// <param name="e">Argument for ElementChangedEventArgs<Background></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Background> e)
        {
            if (Control == null)
            {
                var background = new EBackground(TForms.NativeParent);
                SetNativeControl(background);
            }
            if (e.NewElement != null)
            {
                UpdateImage();
                UpdateOption();
            }
            base.OnElementChanged(e);
        }

        /// <summary>
        /// Called when element property is changed.
        /// </summary>
        /// <param name="e">Argument for PropertyChangedEvent</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == Background.ImageProperty.PropertyName)
            {
                UpdateImage();
            }
            else if (e.PropertyName == Background.OptionProperty.PropertyName)
            {
                UpdateOption();
            }
            base.OnElementPropertyChanged(sender, e);
        }

        /// <summary>
        /// Setter for image
        /// </summary>
        void UpdateImage()
        {
            if (Element.Image == null)
                Control.File = "";
            else
                Control.File = ResourcePath.GetPath(Element.Image.File);
            Console.WriteLine("Control.File :" + Control.File);
        }

        /// <summary>
        /// Setter for option
        /// </summary>
        void UpdateOption()
        {
            Control.BackgroundOption = ConvertToNativeBackgroundOptions(((Background)Element).Option);
        }

        /// <summary>
        /// Convert BackgroundOptions of UIComponents.Extensions to NativeBackgroundOptions
        /// </summary>
        /// <param name="option">BackgroundOptions</param>
        /// <returns></returns>
        EBackgroundOptions ConvertToNativeBackgroundOptions(BackgroundOptions option)
        {
            Console.WriteLine("ConvertToNativeBackgroundOptions : " + option);
            if (option == BackgroundOptions.Stretch)
            {
                return EBackgroundOptions.Stretch;
            }
            else if (option == BackgroundOptions.Tile)
            {
                return EBackgroundOptions.Tile;
            }
            else
            {
                return EBackgroundOptions.Scale;
            }
        }
    }
}
