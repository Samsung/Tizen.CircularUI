using System;
using Xamarin.Forms;
using Tizen.Applications;
using Tizen.Wearable.CircularUI.Forms;
using Tizen.Wearable.CircularUI.Forms.Renderer.Widget;

namespace SimpleWidgetApp
{
    class MyWidgetBase : FormsWidgetBase
    {
        public override void OnCreate(Bundle content, int w, int h)
        {
            base.OnCreate(content, w, h);
            var app = new WidgetApplication();
            LoadApplication(app);
        }
    }

    class SimpleWidgetApp : FormsWidgetApplication
    {
        public SimpleWidgetApp(Type type) : base(type)
        {
        }

        static void Main(string[] args)
        {
            var app = new SimpleWidgetApp(typeof(MyWidgetBase));
            Forms.Init(app);
            FormsCircularUI.Init();
            app.Run(args);
        }
    }
}
