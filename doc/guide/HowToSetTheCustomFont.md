# How to set the custom font

You can add the font path via [Elmsharp.Utility.AppendGlobalFontPath](https://developer.tizen.org/dev-guide/csapi/api/ElmSharp.Utility.html#ElmSharp_Utility_AppendGlobalFontPath_System_String_).

The parameter `path` must be a directory in which the fonts to be added are present.

If you use only ElmSharp, you should make get the following code.

```C#
using ElmSharp;
using Tizen.Applications;

namespace ElmSharpNewFont
{
    class Program : CoreUIApplication
    {
        protected override void OnCreate()
        {
            var window = new Window("NewFontWindow");
            Utility.AppendGlobalFontPath(Application.Current.DirectoryInfo.Resource);
            var layout = new Box(window)
            {
                WeightX = NamedHint.Expand,
                WeightY = NamedHint.Expand,
                BackgroundColor = Color.Black
            };
            window.AddResizeObject(layout);
            layout.Show();
            var label = new Label(layout)
            {
                AlignmentX = 0.5,
                AlignmentY = 0.5,
                WeightX = NamedHint.Expand,
                WeightY = NamedHint.Expand,
                Text = "안녕하세요.",
                TextStyle = "DEFAULT='font=YiSunShinBold font_size=39'",
            };
            label.Show();
            layout.PackEnd(label);
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
```
[Application.Current.DirectoryInfo.Resource](https://github.sec.samsung.net/pages/dotnet/apidoc/devel/api/Tizen.Applications.DirectoryInfo.html#Tizen_Applications_DirectoryInfo_Resource) is application's resource directory in which the `YiSunShin-Bold.ttf` font is located

In the `Label.TextStyle`, `font=` part should be `font name` not `font family`.
`YiSunShinBold` is the `font name` and `font family` is `YiSunShin Bold`,
but you can see that it is written as `font name`.

> [!NOTE]
> The `font name` is actually `postscript name` in your TTF or OTF file.

> [!TIP]
> You can find `postscript name` from the font file with `fc-query` in the font config installed shell.  
> `fc-query -f "%{postscriptname}\n" Font.ttf`

Of course, it can also be used with Xamarin.Forms.

``` C#
using ElmSharp;
using Tizen.Applications;

namespace XamarinFormsNewFont
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();

            Utility.AppendGlobalFontPath(Application.Current.DirectoryInfo.Resource);
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
            Tizen.Wearable.CircularUI.Forms.Renderer.FormsCircularUI.Init();
            app.Run(args);
        }
    }
}
```

``` C#
using Xamarin.Forms;

namespace XamarinFormsNewFont
{
    public class App : Application
    {
        public App()
        {
            MainPage = new ContentPage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            HorizontalTextAlignment = TextAlignment.Center,
                            FontFamily = "YiSunShinBold",
                            Text = "Welcome to Xamarin Forms!"
                        }
                    }
                }
            };
        }
    }
}

```

Similarly, you can call `Utility.AppendGlobalFontPath` before the `Label` is used.

> [!TIP]
> You can find `YiSunShin` font from [`here`](https://www.asan.go.kr/main/cms/?no=49)
