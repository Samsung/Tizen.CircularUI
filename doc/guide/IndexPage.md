# IndexPage
`IndexPage` is extension of [`Xamarin.Forms.MultiPage`](https://developer.xamarin.com/api/type/Xamarin.Forms.MultiPage%3CT%3E/).
When [`Page`](https://developer.xamarin.com/api/type/Xamarin.Forms.Page/) is added and removed at [`Xamarin.Forms.MultiPage`](https://developer.xamarin.com/api/type/Xamarin.Forms.MultiPage%3CT%3E/). Circular index is added and removed automatically at the top of window.
It is similar to [`CarouselPage`](https://developer.xamarin.com/api/type/Xamarin.Forms.CarouselPage/) in Xamarin.Forms, with the addition of `Index`.
When [`Page`](https://developer.xamarin.com/api/type/Xamarin.Forms.Page/) is scrolled, `Index` operates select internally.

<img src="data/IndexPage.png" alt="Drawing" style="width: 150px;"/>

## Properties
XAML for Xamarin.Forms supports the following properties:
- ItemsSource : A static collection of data objects.
- ItemTemplate : A view that has bindings to properties on the members of the collection that is specified by ItemsSource.

`IndexPage` has the following properties:
- Children : `System.Collections.Generic.IList<T>`. Gets an IList<Page> of child elements of [`Xamarin.Forms.MultiPage`](https://developer.xamarin.com/api/type/Xamarin.Forms.MultiPage%3CT%3E/).
- CurrentPage : [`Xamarin.Forms.MultiPage`](https://developer.xamarin.com/api/type/Xamarin.Forms.MultiPage%3CT%3E/). Gets or sets the currently selected page.
- ItemsSource : [`System.Collections.IEnumerable`](https://developer.xamarin.com/api/type/System.Collections.IEnumerable/). he source for the items to be displayed.
- ItemTemplate : [`Xamarin.Forms.DataTemplate`](https://developer.xamarin.com/api/type/Xamarin.Forms.DataTemplate/). The template for displaying items.
- SelectedItem : [`System.Object`](https://developer.xamarin.com/api/type/System.Object/). The currently selected item.

## Events
- CurrentPageChanged : Raised when the CurrentPage property changes.
- PagesChanged : Raised when the children pages of `IndexPage` have changed.

For more information. Please refer to below links
- [IndexPage  API reference](https://github.sec.samsung.net/pages/dotnet/tizen-circular-ui/api/Tizen.Wearable.CircularUI.Forms.IndexPage.html)
- [Xamarin.Forms.MultiPage  API reference](https://developer.xamarin.com/api/type/Xamarin.Forms.MultiPage%3CT%3E/)

## Adding IndexPage
To create a new index component, use the following XAML code.

_This guide's code example use WearableUIGallery's TCIndexPage.xaml code at the test\WearableUIGallery\WearableUIGallery\TC\TCIndexPage.xaml_

**XAML file**
```xml
<?xml version="1.0" encoding="utf-8" ?>
<w:IndexPage
    x:Class="WearableUIGallery.TC.TCIndexPage"
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:local="clr-namespace:WearableUIGallery"
    xmlns:w="clr-namespace:Tizen.Wearable.CircularUI.Forms;assembly=Tizen.Wearable.CircularUI.Forms">
    <ContentPage>
        <StackLayout>
            <BoxView VerticalOptions="FillAndExpand" Color="Red" />
            <Label HorizontalOptions="CenterAndExpand" Text="Red" />
        </StackLayout>
    </ContentPage>
    <ContentPage>
        <StackLayout>
            <BoxView VerticalOptions="FillAndExpand" Color="Green" />
            <Label HorizontalOptions="CenterAndExpand" Text="Green" />
        </StackLayout>
    </ContentPage>
    <ContentPage>
        <StackLayout>
            <BoxView VerticalOptions="FillAndExpand" Color="Blue" />
            <Label HorizontalOptions="CenterAndExpand" Text="Blue" />
        </StackLayout>
    </ContentPage>
</w:IndexPage>
```


