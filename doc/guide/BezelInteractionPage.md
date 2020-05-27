---
uid: Tizen.Wearable.CircularUI.doc.BezelInteractionPage
summary: BezelInteractionPage guide
---

# BezelInteractionPage
[`BezelInteractionPage`](https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.BezelInteractionPage.html) is basically a [`Xamarin.Forms.ContentPage`](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.contentpage?view=xamarin-forms) but with an additional property, [`RotaryFocusObject`](https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.BezelInteractionPage.html#Tizen_Wearable_CircularUI_Forms_BezelInteractionPage_RotaryFocusObject), that helps developers interact with the bezel rotation.
Normally, a focused control in an application gets the bezel interaction. However, `BezelInteractionPage` gives the full control of which control gets the bezel interaction in the current page to developers.


## How to set `RotaryFocusObject`?
Here is the list of controls that can be directly set to [`RotaryFocusObject`](https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.BezelInteractionPage.html#Tizen_Wearable_CircularUI_Forms_BezelInteractionPage_RotaryFocusObject) and that react to bezel rotation.
- Xamarin.Forms controls
  [ScrollView](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.scrollview?view=xamarin-forms), [ListView](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.listview?view=xamarin-forms), [DatePicker](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.datepicker?view=xamarin-forms), [TimePicker](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.timepicker?view=xamarin-forms), [Stepper](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.stepper?view=xamarin-forms), [CollectionView](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.collectionview?view=xamarin-forms)
- Tizen CircularUI controls
  [CircleScrollView](https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.CircleScrollView.html), [CircleListView](https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.CircleListView.html), [CircleDateTimeSelector](https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.CircleDateTimeSelector.html), [CircleStepper](CircleStepper)

Or, developers can customize how the object reacts when it gets bezel interaction by inheriting [`IRotaryEventReceiver`](https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.IRotaryEventReceiver.html). [`IRotaryEventReceiver`](https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.IRotaryEventReceiver.html) is a receiver interface to take rotary events, and it includes [`Rotate(RotaryEventArgs`](https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.IRotaryEventReceiver.html#Tizen_Wearable_CircularUI_Forms_IRotaryEventReceiver_Rotate_Tizen_Wearable_CircularUI_Forms_RotaryEventArgs_) method to implement.

## Create `BezelInteractionPage`
You can easily create and use `BezelInteractionPage` in C# or XAML file.

_Refer to TCBezelInteractionPage code at the Tizen.CircularUI\test\WearableUIGallery\WearableUIGallery\TC\TCBezelInteractionPage.xaml_

```xml
<?xml version="1.0" encoding="utf-8" ?>
<w:BezelInteractionPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             xmlns:w="clr-namespace:Tizen.Wearable.CircularUI.Forms;assembly=Tizen.Wearable.CircularUI.Forms"
             xmlns:local="clr-namespace:WearableUIGallery.TC"
             mc:Ignorable="d"
             x:Class="WearableUIGallery.TC.TCBezelInteractionPage"
             RotaryFocusObject="{x:Reference Spaceman}">
    <ContentPage.Content>
        <AbsoluteLayout>
            <Image x:Name="Universe" Source="image/stars_background.png" AbsoluteLayout.LayoutBounds="0, 0, 1, 1" AbsoluteLayout.LayoutFlags="All"/>
            <local:RotaryFocusImage x:Name="Spaceman" Source="image/spaceman.png" AbsoluteLayout.LayoutBounds="0.5, 0.2" AbsoluteLayout.LayoutFlags="PositionProportional"
                                    AnchorX="0.5" AnchorY="1.5"/>
            <Image x:Name="SliderTarget" Source="image/tw_ic_popup_btn_check.png" AbsoluteLayout.LayoutBounds="0.5, 0.5" AbsoluteLayout.LayoutFlags="PositionProportional"
                                    AutomationId="check"/>
            <w:CircleSurfaceView AbsoluteLayout.LayoutBounds="0, 0, 1, 1" AbsoluteLayout.LayoutFlags="All" InputTransparent="True">
                <w:CircleSurfaceView.CircleSurfaceItems>
                    <w:CircleSliderSurfaceItem x:Name="Slider"
                                   BackgroundColor="Blue"
                                   BackgroundLineWidth="12"
                                   BackgroundRadius="168"
                                   BarRadius="168"
                                   BarColor="Silver"
                                   BarLineWidth="10"
                                   Increment="1"
                                   Minimum="0"
                                   Maximum="11"
                                   Value="0"/>
                </w:CircleSurfaceView.CircleSurfaceItems>
            </w:CircleSurfaceView>
        </AbsoluteLayout>
    </ContentPage.Content>
</w:BezelInteractionPage>
```

**C# file**
```cs
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCBezelInteractionPage : BezelInteractionPage
    {
        TapGestureRecognizer _universeClicked;
        TapGestureRecognizer _spacemanClicked;
        TapGestureRecognizer _sliderClicked;

        public TCBezelInteractionPage()
        {
            InitializeComponent();
            var universe = new RotaryFocusProxy(Universe);

            _universeClicked = new TapGestureRecognizer();
            _universeClicked.Command = new Command(() => RotaryFocusObject = universe);

            _spacemanClicked = new TapGestureRecognizer();
            _spacemanClicked.Command = new Command(() => RotaryFocusObject = Spaceman);

            _sliderClicked = new TapGestureRecognizer
            {
                Command = new Command(() => RotaryFocusObject = Slider)
            };

            Universe.GestureRecognizers.Add(_universeClicked);
            Spaceman.GestureRecognizers.Add(_spacemanClicked);
            SliderTarget.GestureRecognizers.Add(_sliderClicked);

        }
    }
}
```
