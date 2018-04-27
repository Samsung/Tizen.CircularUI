# CircleScrollView
`CircleScrollView` is extension of [`Xamarin.Forms.ScrollView`](https://developer.xamarin.com/api/type/Xamarin.Forms.ScrollView/).
Same as [`Xamarin.Forms.ScrollView`](https://developer.xamarin.com/api/type/Xamarin.Forms.ScrollView/), Scroller is rendered to CircleSurface and scrolling is possible with Bezel Action.
This widget related circle UI can be expressed as Child of [`CirclePage`](CirclePage.md),
In order to receive Bezel Action, it must be registered as `RotaryEventConsumer` property of [`CirclePage`](CirclePage.md).

The orientation of the scroller depends on the setting of the Orientation value.

<table style="width:30%">
  <tr>
    <th>Horizontal</th>
    <th>Vertical</th> 
  </tr>
  <tr>
    <td><img src="data/CircleScrollView_Horizontal.png" alt="Drawing" style="width: 150px;"/></td>
    <td><img src="data/CircleScrollView_Vertical.png" alt="Drawing" style="width: 150px;"/></td> 
  </tr>
</table>

**WARNNING: [`CircleListView`](CircleListView.md), [`CircleDateTimeSelector`](CircleDateTimeSelector.md), [`CircleScrollView`](CircleScrollView.md), [`CircleStepper`](CircleStepper.md) must be contained by [`CirclePage`](CirclePage.md) or `CircleSurfaceEffectBehavior` should be added in `Behaviors` of `Page` that contain these Control. If other `page` contains these control. It may cause exception or control can not be displayed.**

## Properties
XAML for Xamarin.Forms supports the following properties:
- Content : Markup that specifies a view to display in the `CircleScrollView`.
- Orientation : *Horizontal* or *Vertical*, to indicate the scroll direction.

 `CircleScrollView` has the following properties:
- Content : [`Xamarin.Forms.View`](https://developer.xamarin.com/api/type/Xamarin.Forms.View/). Gets or sets a view to display in the `CircleScrollView`.
- ContentSize :	[`Xamarin.Forms.Size`](https://developer.xamarin.com/api/type/Xamarin.Forms.Size/). Gets the size of the Content. This is a bindable property.
- Orientation :	[`Xamarin.Forms.ScrollOrientation`](https://developer.xamarin.com/api/type/Xamarin.Forms.ScrollOrientation/). Gets or sets the scrolling direction of the `CircleScrollView`. This is a bindable property.
- ScrollX :	Double. Gets the current X scroll position.
- ScrollY :	Double. Gets the current Y scroll position.

## Events
- Scrolled : Event that is raised after a scroll completes.

For more information. Please refer to below links
- [CircleScrollView  API reference](https://github.sec.samsung.net/pages/dotnet/tizen-circular-ui/api/Tizen.Wearable.CircularUI.Forms.CircleScrollView.html)
- [Xamarin.Forms.ScrollView  API reference](https://developer.xamarin.com/api/type/Xamarin.Forms.ScrollView/)

## Adding CircleScrollView at CirclePage
You can set `CircleScrollView` at [`CirclePage.Content`](CirclePage.md). The following XAML code show CirclePage set content with `CircleScrollView`.
`RotaryFocusTargetName` attribute sets the current focused widget that is handled by rotating and display the focused widget's circle object.
If you don't set this value properly. Widget can't receive rotary event.

_This guide's code example use XUIComponent's HorizontalScroller.xaml code at the sample\XUIComponents\UIComponents\UIComponents\Samples\CircleScroller/HorizontalScroller.xaml_

**XAML file**
```xml
<w:CirclePage
    x:Class="UIComponents.Samples.CircleScroller.HorizontalScroller"
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:local="clr-namespace:UIComponents.Samples.CircleScroller"
    xmlns:sys="clr-namespace:System;assembly=netstandard"
    xmlns:w="clr-namespace:Tizen.Wearable.CircularUI.Forms;assembly=Tizen.Wearable.CircularUI.Forms"
    RotaryFocusTargetName="myscroller">
    <w:CirclePage.Content>
        <w:CircleScrollView x:Name="myscroller" Orientation="Horizontal">
            <StackLayout
                HorizontalOptions="FillAndExpand"
                Orientation="Horizontal"
                VerticalOptions="FillAndExpand">
                <Image Source="tw_btn_delete_holo_dark.png" VerticalOptions="CenterAndExpand" />
                <Image Source="tw_number_controller_icon_ringtone_mute.png" VerticalOptions="CenterAndExpand" />
                <Image Source="tw_ic_popup_btn_check.png" VerticalOptions="CenterAndExpand" />
                <Image Source="tw_number_controller_icon_alert.png" VerticalOptions="CenterAndExpand" />
                <Image Source="tw_number_controller_icon_bell.png" VerticalOptions="CenterAndExpand" />
                <Image Source="tw_number_controller_icon_ringtone_sound.png" VerticalOptions="CenterAndExpand" />
                <Image Source="tw_btn_delete_holo_dark.png" VerticalOptions="CenterAndExpand" />
                <Image Source="tw_number_controller_icon_ringtone_mute.png" VerticalOptions="CenterAndExpand" />
                <Image Source="tw_ic_popup_btn_check.png" VerticalOptions="CenterAndExpand" />
                <Image Source="tw_number_controller_icon_alert.png" VerticalOptions="CenterAndExpand" />
                <Image Source="tw_number_controller_icon_bell.png" VerticalOptions="CenterAndExpand" />
                <Image Source="tw_number_controller_icon_ringtone_sound.png" VerticalOptions="CenterAndExpand" />
                <Image Source="tw_btn_delete_holo_dark.png" VerticalOptions="CenterAndExpand" />
                <Image Source="tw_number_controller_icon_ringtone_mute.png" VerticalOptions="CenterAndExpand" />
                <Image Source="tw_ic_popup_btn_check.png" VerticalOptions="CenterAndExpand" />
                <Image Source="tw_number_controller_icon_alert.png" VerticalOptions="CenterAndExpand" />
                <Image Source="tw_number_controller_icon_bell.png" VerticalOptions="CenterAndExpand" />
                <Image Source="tw_number_controller_icon_ringtone_sound.png" VerticalOptions="CenterAndExpand" />
            </StackLayout>
        </w:CircleScrollView>
    </w:CirclePage.Content>
</w:CirclePage>

```


