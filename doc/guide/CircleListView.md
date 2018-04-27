# CircleListView
`CircleListView` is extension of [`Xamarin.Forms.ListView`](https://developer.xamarin.com/api/type/Xamarin.Forms.ListView/).
Same as [`Xamarin.Forms.ListView`](https://developer.xamarin.com/api/type/Xamarin.Forms.ListView/), but Scroller is rendered to CircleSurface. You can also move the List to a Bezel Action.
This widget related circle UI can be expressed as Child of [`CirclePage`](CirclePage.md),
In order to receive Bezel Action, it must be registered as `RotaryEventConsumer` property of [`CirclePage`](CirclePage.md).

<table style="width:40%">
  <tr>
    <th>Normal list</th>
    <th>Group list</th>
    <th>2 texts and 1 icon list</th>
  </tr>
  <tr>
    <td><img src="data/CircleListView_noscroll.png" alt="Drawing" style="width: 300px"/></td>
    <td><img src="data/CircleListView_group.png" alt="Drawing" style="width: 300px"/></td>
    <td><img src="data/CircleListView_2text1icon1.png" alt="Drawing" style="width: 300px"/></td>
  </tr>
</table>

**WARNNING: [`CircleListView`](CircleListView.md), [`CircleDateTimeSelector`](CircleDateTimeSelector.md), [`CircleScrollView`](CircleScrollView.md), [`CircleStepper`](CircleStepper.md) must be contained by [`CirclePage`](CirclePage.md) or `CircleSurfaceEffectBehavior` should be added in `Behaviors` of `Page` that contain these Control. If other `page` contains these control. It may cause exception or control can not be displayed.**

## Properties
XAML for Xamarin.Forms supports the following properties:
- HasUnevenRows : true or false, to indicate whether the items in the list all have the same height.
- IsGroupingEnabled : true or false, to indicate whether grouping is enabled.
- RowHeight : An integer that describes the height of the items in the list. This is ignored if HasUnevenRows is true.

`CircleListView` has the following properties: 
- Footer : [`System.Object`](https://developer.xamarin.com/api/type/System.Object/). Gets or sets the string, binding, or view that will be displayed at the bottom of the list view.
- FooterTemplate : [`Xamarin.Forms.DataTemplate`](https://developer.xamarin.com/api/type/Xamarin.Forms.DataTemplate/). Gets or sets a data template to use to format a data object for display at the bottom of the list view.
- GroupDisplayBinding : [`Xamarin.Forms.BindingBase`](https://developer.xamarin.com/api/type/Xamarin.Forms.BindingBase/). Gets or sets the binding to use for display the group header.
- GroupHeaderTemplate : [`Xamarin.Forms.DataTemplate`](https://developer.xamarin.com/api/type/Xamarin.Forms.DataTemplate/). Gets or sets a DataTemplate for group headers.
- GroupShortNameBinding : [`Xamarin.Forms.BindingBase`](https://developer.xamarin.com/api/type/Xamarin.Forms.BindingBase/). Gets or sets a binding for the name to display in grouped jump lists.
- HasUnevenRows : Boolean. Gets or sets a Boolean value that indicates whether this ListView element has uneven rows.
- Header : [`System.Object`](https://developer.xamarin.com/api/type/System.Object/). Gets or sets the string, binding, or view that will be displayed at the top of the list view.
- HeaderTemplate : [`Xamarin.Forms.DataTemplate`](https://developer.xamarin.com/api/type/Xamarin.Forms.DataTemplate/). Gets or sets a data template to use to format a data object for display at the top of the list view.
- IsGroupingEnabled : Boolean. Gets or sets whether or not grouping is enabled for ListView.
- IsPullToRefreshEnabled : Boolean. Gets or sets a value that tells whether the user can swipe down to cause the application to refresh.
- IsRefreshing : Boolean. Gets or sets a value that tells whether the list view is currently refreshing. 
- RefreshAllowed : Boolean. For internal use by the Xamarin.Forms platform.
- RefreshCommand : [`System.Windows.Input.ICommand`](https://developer.xamarin.com/api/type/System.Windows.Input.ICommand/). Gets or sets the command that is run when the list view enters the refreshing state.
- RowHeight : Int32. Gets or sets a value that represents the height of a row.
- SelectedItem : [`System.Object`](https://developer.xamarin.com/api/type/System.Object/). Gets or sets the currently selected item from the ListView.ItemsSource.
- SeparatorColor : [`Xamarin.Forms.Color`](https://developer.xamarin.com/api/type/Xamarin.Forms.Color/). Gets or sets the color of the bar that separates list items.
- SeparatorVisibility : [`Xamarin.Forms.SeparatorVisibility`](https://developer.xamarin.com/api/type/Xamarin.Forms.SeparatorVisibility/). Gets or sets a value that tells whether separators are visible between items.

## Events
- ItemAppearing	: Occurs when the visual representation of an item is being added to the visual layout.
- ItemDisappearing : Occurs when the visual representation of an item is being removed from the visual layout.
- ItemSelected : Event that is raised when a new item is selected.
- ItemTapped : Event that is raised when an item is tapped.
- Refreshing : Event that is raised when the list view refreshes.

For more information. Please refer to below links
- [CircleListView  API reference](https://github.sec.samsung.net/pages/dotnet/tizen-circular-ui/api/Tizen.Wearable.CircularUI.Forms.CircleListView.html)
- [Xamarin.Forms.ListView  API reference](https://developer.xamarin.com/api/type/Xamarin.Forms.ListView/)

## Adding CircleListView at CirclePage
You can set content at [`CirclePage.Content`](CirclePage.md). The following XAML code show CirclePage set content with `CircleListView`.
`RotaryFocusTargetName` attribute sets the current focused widget that is handled by rotating and display the focused widget's circle object.
If you don't set this value properly. Widget can't receive rotary event.

_This guide's code example use WearableUIGallery's TCCircleListView.xaml code at the test\WearableUIGallery\WearableUIGallery\TC\TCCircleListView.xaml_

**XAML file**
```xml
<?xml version="1.0" encoding="utf-8" ?>
<w:CirclePage
    x:Class="WearableUIGallery.TC.TCCircleListView"
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:local="clr-namespace:WearableUIGallery"
    xmlns:sys="clr-namespace:System;assembly=netstandard"
    xmlns:w="clr-namespace:Tizen.Wearable.CircularUI.Forms;assembly=Tizen.Wearable.CircularUI.Forms"
    RotaryFocusTargetName="mylist">
    <w:CirclePage.Content>
        <w:CircleListView x:Name="mylist">
            <w:CircleListView.ItemsSource>
                <x:Array x:Key="array" Type="{x:Type sys:String}">
                    <x:String>Item 1</x:String>
                    <x:String>Item 2</x:String>
                    <x:String>Item 3</x:String>
                    <x:String>Item 4</x:String>
                    <x:String>Item 5</x:String>
                    <x:String>Item 6</x:String>
                    <x:String>Item 7</x:String>
                    <x:String>Item 8</x:String>
                    <x:String>Item 9</x:String>
                    <x:String>Item 10</x:String>
                    <x:String>Item 11</x:String>
                    <x:String>Item 12</x:String>
                </x:Array>
            </w:CircleListView.ItemsSource>
            <w:CircleListView.Header>
                <x:String>Header</x:String>
            </w:CircleListView.Header>
            <w:CircleListView.Footer>
                <x:String>Footer</x:String>
            </w:CircleListView.Footer>
            <w:CircleListView.ItemTemplate>
                <DataTemplate>
                    <TextCell Text="{Binding .}" />
                </DataTemplate>
            </w:CircleListView.ItemTemplate>
            <w:CircleListView.HeaderTemplate>
                <DataTemplate>
                    <Label
                        FontAttributes="Bold"
                        FontSize="Large"
                        HorizontalTextAlignment="Center"
                        Text="{Binding .}"
                        TextColor="Red" />
                </DataTemplate>
            </w:CircleListView.HeaderTemplate>
            <w:CircleListView.FooterTemplate>
                <DataTemplate>
                    <Label
                        FontAttributes="Bold"
                        FontSize="Large"
                        HorizontalTextAlignment="Center"
                        Text="{Binding .}"
                        TextColor="Blue" />
                </DataTemplate>
            </w:CircleListView.FooterTemplate>
        </w:CircleListView>
    </w:CirclePage.Content>
</w:CirclePage>
```

