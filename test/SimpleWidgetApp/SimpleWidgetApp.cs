using System;
using Tizen.Applications;
using Tizen.Wearable.CircularUI.Forms.Renderer.Widget;

namespace SimpleWidgetApp
{
    class MyWidget : FormsWidgetBase
    {
        public override void OnCreate(Bundle content, int w, int h)
        {
            base.OnCreate(content, w, h);
            var app = new WidgetApplication();
            LoadApplication(app);
        }
    }

    class Program : FormsWidgetApplication
    {
        public Program(Type type) : base(type)
        {
        }

        protected override void OnCreate()
        {
            base.OnCreate();
        }

        static void Main(string[] args)
        {
            var app = new Program(typeof(MyWidget));
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            global::Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
            app.Run(args);
        }
    }
}
