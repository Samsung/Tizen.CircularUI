# CircleStepper

`CircleStepper` is extension of [`Xamarin.Forms.Stepper`](https://developer.xamarin.com/api/type/Xamarin.Forms.Stepper/).
Marker color, MarkerLineWidth, and LabelFormat have been added to the existing [`Xamarin.Forms.Stepper`](https://developer.xamarin.com/api/type/Xamarin.Forms.Stepper/).
You can change value with Bezel Action.

This widget related circle UI can be expressed as Child of [`CirclePage`](CirclePage.md),
In order to receive Bezel Action, it must be registered as `RotaryEventConsumer` property of [`CirclePage`](CirclePage.md).

<img src="data/CircleStepper.png" alt="Drawing" style="width: 150px;"/>

**WARNNING: [`CircleListView`](CircleListView.md), [`CircleDateTimeSelector`](CircleDateTimeSelector.md), [`CircleScrollView`](CircleScrollView.md), [`CircleStepper`](CircleStepper.md) must be contained by [`CirclePage`](CirclePage.md) or `CircleSurfaceEffectBehavior` should be added in `Behaviors` of `Page` that contain these Control. If other `page` contains these control. It may cause exception or control can not be displayed.**

## Properties
XAML for Xamarin.Forms supports the following properties:
 - Increment : An integer or decimal literal.
 - Maximum : An integer or decimal literal.
 - Minimum : An integer or decimal literal. If this value is nonnegative, it must appear lexically below Maximum, so that validation can succeed.
 - Value : An integer or decimal literal that represents a number that is in the range [Minimum,Maximum].
 - ValueChanged : The name of an event handler. Note that this tag must appear below Value. 

 `CircleStepper` has the following properties:
- Increment : Double. Gets or sets the increment by which Value is increased or decreased.
- LabelFormat : string. Gets or sets format in which Value is shown.
- MarkerColor : [`Xamarin.Forms.Color`](https://developer.xamarin.com/api/type/Xamarin.Forms.Color/). Change color of marker to select value.
- MarkerLineWidth : int. Gets or sets length of marker.
- Maximum	: Double. Gets or sets the maximum selectable value.
- Minimum	: Double. Gets or sets the minimum selectabel value.
- Value	: Double. Gets or sets the current value.

## Events
- ValueChanged : Raised when the Value property changes.

For more information. Please refer to below links
- [CircleStepper  API reference](https://github.sec.samsung.net/pages/dotnet/tizen-circular-ui/api/Tizen.Wearable.CircularUI.Forms.CircleStepper.html)
- [Xamarin.Forms.Stepper  API reference](https://developer.xamarin.com/api/type/Xamarin.Forms.Stepper/)

## Adding CircleStepper at CirclePage
You can set CircleStepper at [`CirclePage.Content`](CirclePage.md). The following code show CirclePage set content with `CircleStepper`.
`RotaryFocusTargetName` attribute sets the current focused widget that is handled by rotating and display the focused widget's circle object.
If you don't set this value properly. Widget can't receive rotary event.

_This guide's code example use XUIComponent's SpinnerDefault of CircleSpinner code at the sample\XUIComponents\UIComponents\UIComponents\Samples\CircleSpinner\SpinnerViewModel.cs and SpinnerDefault.xaml_

**C# file**
```cs
    public class SpinnerViewModel : INotifyPropertyChanged
    {
        double _value= 9.0;
        ...

        public double Value
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = value;
                OnPropertyChanged();
            }
        }
```

**XAML file**
```xml
<w:CirclePage
    x:Class="UIComponents.Samples.CircleSpinner.SpinnerDefault"
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:local="clr-namespace:UIComponents.Samples.CircleSpinner"
    xmlns:sys="clr-namespace:System;assembly=netstandard"
    xmlns:w="clr-namespace:Tizen.Wearable.CircularUI.Forms;assembly=Tizen.Wearable.CircularUI.Forms"
    RotaryFocusTargetName="stepper">
    <w:CirclePage.BindingContext>
        <local:SpinnerViewModel />
    </w:CirclePage.BindingContext>
    <w:CirclePage.Content>
        <StackLayout Padding="0,50,0,0" Orientation="Vertical">
            <Label
                FontAttributes="Bold"
                FontSize="11"
                HorizontalTextAlignment="Center"
                Text="Title"
                TextColor="#0FB4EF" />
            <Label
                FontSize="8"
                HorizontalTextAlignment="Center"
                Text="unit"
                TextColor="White" />
            <w:CircleStepper
                x:Name="stepper"
                HorizontalOptions="CenterAndExpand"
                Increment="7.5"
                LabelFormat="%1.1f"
                MarkerColor="Coral"
                Maximum="99.0"
                Minimum="9.0"
                Value="{Binding Value}" />
        </StackLayout>
    </w:CirclePage.Content>
    <w:CirclePage.ActionButton>
        <w:ActionButtonItem Command="{Binding ButtonPressedExit}" Text="SET" />
    </w:CirclePage.ActionButton>
</w:CirclePage>
```


