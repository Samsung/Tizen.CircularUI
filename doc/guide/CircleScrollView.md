---
uid: Tizen.Wearable.CircularUI.doc.CircleScrollView
summary: CircleScrollView control guide
---
# CircleScrollView

`CircleScrollView` is extension of [`Xamarin.Forms.ScrollView`](https://developer.xamarin.com/api/type/Xamarin.Forms.ScrollView/).
Same as [`Xamarin.Forms.ScrollView`](https://developer.xamarin.com/api/type/Xamarin.Forms.ScrollView/), Scroller is rendered to CircleSurface and scrolling is possible with bezel interaction.
In order to receive bezel interaction, it must be registered as `RotaryEventConsumer` property of [`CirclePage`](xref:Tizen.Wearable.CircularUI.doc.CirclePage).

|![Horizontal](data/CircleScrollView_Horizontal.png)|![Vertical](data/CircleScrollView_Vertical.png)|
|:-----------------------------------------------:|:-----------------------------------------------:|
|                      Horizontal                 |                    Vertical                     |

**WARNNING: [CircleListView](xref:Tizen.Wearable.CircularUI.doc.CircleListView), [CircleDateTimeSelector](xref:Tizen.Wearable.CircularUI.doc.CircleDateTimeSelector), [CircleScrollView](xref:Tizen.Wearable.CircularUI.doc.CircleScrollView), [CircleStepper](xref:Tizen.Wearable.CircularUI.doc.CircleStepper) must be contained by `CirclePage` or [CircleSurfaceEffectBehavior](xref:Tizen.Wearable.CircularUI.doc.CircleSurfaceEffectBehavior) should be added in [Behaviors](https://developer.xamarin.com/api/type/Xamarin.Forms.Behavior/) of [Page](https://developer.xamarin.com/api/type/Xamarin.Forms.Page/) that contain these Control. If other `page` contains these control. It may cause exception or control can not be displayed.**

## Adding CircleScrollView at CirclePage

You can set `CircleScrollView` at [`CirclePage.Content`](xref:Tizen.Wearable.CircularUI.doc.CirclePage). The following XAML code show [`CirclePage`](xref:Tizen.Wearable.CircularUI.doc.CirclePage) with `CircleScrollView`.
`RotaryFocusTargetName` attribute sets the current focused control that is handled by rotating and display the focused control's circle object.
If you don't set this property, control can't receive rotary event.
The orientation of the scroller depends on the setting of the Orientation value.

For more information. Please refer to below links

- [CircleScrollView API reference](https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.CircleScrollView.html)
- [Xamarin.Forms.ScrollView API reference](https://developer.xamarin.com/api/type/Xamarin.Forms.ScrollView/)
- [Xamarin.Forms.ScrollView Guide](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/layouts/scroll-view)

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