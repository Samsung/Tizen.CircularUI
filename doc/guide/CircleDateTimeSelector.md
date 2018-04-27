# CircleDateTimeSelector
`CircleDateTimeSelector` is a view for Date picker and Time picker.
You can scroll it by bezel action.
This view is the size that covers the entire screen.
This widget related circle UI can be expressed as Child of [`CirclePage`](CirclePage.md),
In order to receive Bezel Action, it must be registered as `RotaryEventConsumer` property of [`CirclePage`](CirclePage.md).

In the Date type, you can change the value of the item by touching the item to set the year: month: day value.
For the Time type, you can change the hour: minute: AM / PM value.

<table>
  <tr>
    <th>Date</th>
    <th>Time</th> 
  </tr>
  <tr>
    <td><img src="data/CircleDateTimeSelector_DatePicker.png" alt="Date"/></td>
    <td><img src="data/CircleDateTimeSelector_TimePicker.png" alt="Time"/></td>
  </tr>
</table>

**WARNNING: [`CircleListView`](CircleListView.md), [`CircleDateTimeSelector`](CircleDateTimeSelector.md), [`CircleScrollView`](CircleScrollView.md), [`CircleStepper`](CircleStepper.md) must be contained by [`CirclePage`](CirclePage.md) or `CircleSurfaceEffectBehavior` should be added in `Behaviors` of `Page` that contain these Control. If other `page` contains these control. It may cause exception or control can not be displayed.**

## Properties
- MarkerColor : [`Xamarin.Forms.Color`](https://developer.xamarin.com/api/type/Xamarin.Forms.Color/). Change color of marker to select value.
- ValueType : DateTimeType. Time can be changed if it is Time and if it is a Date, the date can be changed.
- DateTime : DateTime. Sets or gets date/time.
- Date of ValueType
    - MaximumDate : DateTime. Sets or gets maximum date.
    - MimimumDate : DateTime. Sets or gets minimum date.
    - IsVisibleOfYear : Boolean. Sets whether or not the year field is shown.
    - IsVisibleOfMonth : Boolean. Sets whether or not the month field is shown.
    - IsVisibleOfDate : Boolean. Sets whether or not the day field is shown.
- Time of ValueType   
    - IsVisibleOfHour : Boolean. Sets whether or not the hour field is shown.
    - IsVisibleOfMinute : Boolean. Sets whether or not the minute field is shown.
    - IsVisibleOfAmPm : Boolean. Sets whether or not the AM/PM field is shown.

For more information. Please refer to [CircleDateTimeSelector  API reference](https://github.sec.samsung.net/pages/dotnet/tizen-circular-ui/api/Tizen.Wearable.CircularUI.Forms.CircleDateTimeSelector.html)

## Adding CircleDateTimeSelector at CirclePage
You can set `CircleDateTimeSelector` at [`CirclePage.Content`](CirclePage.md). The following code show CirclePage set content with `CircleDateTimeSelector`.
`RotaryFocusTargetName` attribute sets the current focused widget that is handled by rotating and display the focused widget's circle object.
If you don't set this value properly. Widget can't receive rotary event.

_This guide's code example use XUIComponent's CircleTime of CircleDateTime code at the sample\XUIComponents\UIComponents\UIComponents\Samples\CircleDateTime/DateTimeViewModel.cs and CircleTime.xaml_

**C# file**
```cs
    public class DateTimeViewModel : INotifyPropertyChanged
    {
        static DateTime _dateTime = DateTime.Now;

        public DateTime Datetime
        {
            get => _dateTime;
            set
            {
                //Console.WriteLine($"Set Datetime value : {value.ToString()}");
                if (_dateTime == value) return;
                _dateTime = value;
                OnPropertyChanged();
            }
        }

```

**XAML file**
```xml
<w:CirclePage
    x:Class="UIComponents.Samples.CircleDateTime.CircleTime"
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:local="clr-namespace:UIComponents.Samples.CircleDateTime"
    xmlns:sys="clr-namespace:System;assembly=netstandard"
    xmlns:w="clr-namespace:Tizen.Wearable.CircularUI.Forms;assembly=Tizen.Wearable.CircularUI.Forms"
    RotaryFocusTargetName="timeSelector">
    <w:CirclePage.BindingContext>
        <local:DateTimeViewModel />
    </w:CirclePage.BindingContext>
    <w:CirclePage.Content>
        <StackLayout
            BackgroundColor="Black"
            HorizontalOptions="FillAndExpand"
            Orientation="Vertical"
            VerticalOptions="FillAndExpand">
            <w:CircleDateTimeSelector
                x:Name="timeSelector"
                DateTime="{Binding Datetime, Mode=TwoWay}"
                MaximumDate="1/1/2022"
                MinimumDate="1/12/2010"
                ValueType="Time" />
        </StackLayout>
    </w:CirclePage.Content>
    <w:CirclePage.ActionButton>
        <w:ActionButtonItem Command="{Binding ButtonPressedExit}" Text="OK" />
    </w:CirclePage.ActionButton>
</w:CirclePage>

```


