using System;
using System.ComponentModel;
using UIComponents.Extensions;
using UIComponents.Tizen.Wearable.Renderers;
using Xamarin.Forms.Platform.Tizen;
using ELayout = ElmSharp.Layout;
using EBackgroundOptions = ElmSharp.BackgroundOptions;
using TForms = Xamarin.Forms.Platform.Tizen.Forms;

[assembly: ExportRenderer(typeof(NoContentView), typeof(NoContentViewRenderer))]
namespace UIComponents.Tizen.Wearable.Renderers
{
    public class NoContentViewRenderer : ViewRenderer<NoContentView, ELayout>
    {
        ELayout _layout;

        public NoContentViewRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<NoContentView> e)
        {
            Console.WriteLine("OnElementChanged");
            if (Control == null)
            {
                _layout = new ELayout(TForms.NativeParent);
                _layout.SetTheme("layout", "nocontents", "default");
                var rect = new ElmSharp.Rectangle(_layout);
                rect.Show();
                _layout.SetPartContent("elm.swallow.icon", rect);
                _layout.SetPartText("elm.text", "No Item");

                SetNativeControl(_layout);
            }
            if (e.NewElement != null)
            {
                UpdateTitle();
            }
            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("OnElementPropertyChanged:" + e.PropertyName);
            if (e.PropertyName == NoContentView.TitleProperty.PropertyName)
            {
                UpdateTitle();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        void UpdateTitle()
        {
            if (string.IsNullOrEmpty(Element.Title))
            {
                Console.WriteLine("UpdateTitle Title is null or empty");
                return;
            }

            Console.WriteLine($"UpdateTitle Title:{Element.Title}");
            _layout.SetPartText("elm.text.title", Element.Title);
            _layout.SignalEmit("elm,state,title,enable", "elm");
        }
    }
}
