---
uid: Tizen.Wearable.CircularUI.doc.ContentButton
summary: ContentButton guide
---
# ContentButton

`ContentButton` is a type of Xamarin.Forms.ContentView that contains a single child element (called Content) and is typically used for custom, reusable controls. Also, as its name implies, ContentButton is designed to be used like a Button that implements `Xamarin.Forms.IButtonController`.

![](data/ContentButton.png)

## How to customize the button using `ContentButton`?

`ContentButton` provides the view to show and the states(Clicked, Pressed and Released) of the button. You can customize the button through changing the view according to the state.
The following example shows the CustomButton composed of a combination of Images that define the icon, background, and border of a button.
To show a border, this example has set an outlined image with blending color as a Content, and the background color of the button will change to gray when the button is pressed for click-effect.

For more information, see the following links:

- [ContentButton API reference](https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.ContentButton.html)
- [Style your Buttons using Tizen .NET Guide](https://developer.samsung.com/tizen/blog/en-us/2020/04/06/style-your-buttons-using-tizen-net)


## Create CircleStepper

You can easily add Check control with C# or XAML file. 

_The code example of this guide uses WearableUIGallery's TCContentButton code. The code is available in test\WearableUIGallery\WearableUIGallery\TC\TCContentButton.xaml_

**C# file**

```cs
public partial class ContentButtonTestPage : ContentPage
{
    public ContentButtonTestPage()
    {
        InitializeComponent();

        ClickCommand = new Command(execute: () =>
        {
            label.Text = "clicked";
        });
    }

    public ICommand ClickCommand { get; private set; }

    private void OnButtonClicked(object sender, EventArgs e)
    {
        Console.WriteLine($"ContentButton clicked event is invoked!!");
    }

    private void OnButtonPressed(object sender, EventArgs e)
    {
        Xamarin.Forms.PlatformConfiguration.TizenSpecific.Image.SetBlendColor(buttonBg, Color.Gray);
    }

    private void OnButtonReleased(object sender, EventArgs e)
    {
        Xamarin.Forms.PlatformConfiguration.TizenSpecific.Image.SetBlendColor(buttonBg, Color.Transparent);
    }
}
```

**XAML file**

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:w="clr-namespace:Tizen.Wearable.CircularUI.Forms;assembly=Tizen.Wearable.CircularUI.Forms"
             xmlns:tizen="clr-namespace:Xamarin.Forms.PlatformConfiguration.TizenSpecific;assembly=Xamarin.Forms.Core"
             x:Class="WearableUIGallery.TC.ContentButtonTestPage">
    <ContentPage.Content>
        <StackLayout VerticalOptions="CenterAndExpand"
                     HorizontalOptions="CenterAndExpand">
            <Label x:Name="label"
                   HorizontalOptions="CenterAndExpand"
                   HorizontalTextAlignment="Center"
                   Text="Test"/>
            <w:ContentButton x:Name="button"
                             Clicked="OnButtonClicked"
                             Pressed="OnButtonPressed"
                             Released="OnButtonReleased"
                             Command="{Binding ClickCommand}">
                <w:ContentButton.Content>
                    <AbsoluteLayout VerticalOptions="FillAndExpand" HorizontalOptions="FillAndExpand">
                        <Image x:Name="buttonBg" Source="button_bg.png" Opacity="0.25" Aspect="AspectFill" tizen:Image.BlendColor="Transparent" AbsoluteLayout.LayoutBounds=".5,.5,89,66" AbsoluteLayout.LayoutFlags="PositionProportional" />
                        <Image x:Name="buttonBorder" Source="button_border.png" Aspect="AspectFill" tizen:Image.BlendColor="DarkGreen" AbsoluteLayout.LayoutBounds=".5,.5,89,66" AbsoluteLayout.LayoutFlags="PositionProportional" />
                        <Image x:Name="buttonIcon" Source="home.png" tizen:Image.BlendColor="DarkGreen" AbsoluteLayout.LayoutBounds=".5,.5,36,36" AbsoluteLayout.LayoutFlags="PositionProportional" />
                    </AbsoluteLayout>
                </w:ContentButton.Content>
            </w:ContentButton>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```
