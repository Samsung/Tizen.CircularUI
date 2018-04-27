# CircleStackLayout
`CircleStackLayout` is the same as [`Xamarin.Forms.StackLayout`](https://developer.xamarin.com/api/type/Xamarin.Forms.StackLayout/), but it arranges internal components in a form that fits the circular screen.
Rectangular components are placed in close proximity to the circle, and margin is calculated after placement.

If you don't set Orientation, `Vertical` will be used.
The larger the `Spacing` value, the greater the distance between the components placed.
At the right end of the figure below, the `Spacing` value is 50.

<table>
  <tr>
    <th>Horizontal</th>
    <th>Vertical</th> 
    <th>Spacing</th> 
  </tr>
  <tr>
    <td><img src="data/CircleStackLayout_Horizontal.png" alt="Horizontal"/></td>
    <td><img src="data/CircleStackLayout_Vertical.png" alt="Vertical"/></td>
    <td><img src="data/CircleStackLayout_Spacing.png" alt="Spacing"/></td> 
  </tr>
</table>

## Properties
- Orientation : `Horizontal` or `Vertical`. The default is `Vertical`.
- Spacing : An integer or decimal value of spacing between deployed components.

For more information. Please refer to below links
- [CircleStackLayout  API reference](https://github.sec.samsung.net/pages/dotnet/tizen-circular-ui/api/Tizen.Wearable.CircularUI.Forms.CircleStackLayout.html)
- [Xamarin.Forms.StackLayout  API reference](https://developer.xamarin.com/api/type/Xamarin.Forms.StackLayout/)

## Adding CircleStackLayout at ContentPage
You can set CircleStackLayout at [`ContentPage`](https://developer.xamarin.com/api/type/Xamarin.Forms.ContentPage/). To create a new component, use the following XAML code.

_This guide's code example use WearableUIGallery's TCCircleStackLayout.xaml code at the test\WearableUIGallery\WearableUIGallery\TC\TCCircleStackLayout.xaml_

**XAML file**
```xml
<?xml version="1.0" encoding="utf-8" ?>
<w:IndexPage
    x:Class="WearableUIGallery.TC.TCCircleStackLayout"
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:local="clr-namespace:WearableUIGallery"
    xmlns:w="clr-namespace:Tizen.Wearable.CircularUI.Forms;assembly=Tizen.Wearable.CircularUI.Forms">
    ...
    <ContentPage>
        <ScrollView>
            <w:CircleStackLayout>
                <BoxView BackgroundColor="Red" />
                <BoxView BackgroundColor="Orange" />
                <BoxView BackgroundColor="Yellow" />
                <BoxView BackgroundColor="Green" />
                <BoxView BackgroundColor="Blue" />
                <BoxView BackgroundColor="Navy" />
                <BoxView BackgroundColor="Purple" />
                <BoxView BackgroundColor="Red" />
                <BoxView BackgroundColor="Orange" />
                <BoxView BackgroundColor="Yellow" />
                <BoxView BackgroundColor="Green" />
                <BoxView BackgroundColor="Blue" />
                <BoxView BackgroundColor="Navy" />
                <BoxView BackgroundColor="Purple" />
            </w:CircleStackLayout>
        </ScrollView>
    </ContentPage>
    ...
</w:IndexPage>
```


