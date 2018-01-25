using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// The IndexPage is extension of Xamarin.Forms.MultiPage.
    /// When Page is added/removed at Multipage. circular index is added/removed automatically at the top of window.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    /// <example>
    /// <code>
    /// <w:IndexPage xmlns = "http://xamarin.com/schemas/2014/forms"
    ///     xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    ///     xmlns:w="clr-namespace:Xamarin.Forms.CircularUI;assembly=Xamarin.Forms.CircularUI"
    ///     xmlns:local="clr-namespace:WearableUIGallery"
    ///     x:Class="WearableUIGallery.TC.TCIndexPage">
    ///     <ContentPage>
    ///         <StackLayout>
    ///             <BoxView Color = "Red" VerticalOptions="FillAndExpand"/>
    ///             <Label Text = "Red" HorizontalOptions="CenterAndExpand"/>
    ///        </StackLayout>
    ///    </ContentPage>
    ///    <ContentPage>
    ///        <StackLayout>
    ///            <BoxView Color = "Green" VerticalOptions="FillAndExpand"/>
    ///            <Label Text = "Green" HorizontalOptions="CenterAndExpand"/>
    ///        </StackLayout>
    ///    </ContentPage>
    ///    <ContentPage>
    ///        <StackLayout>
    ///            <BoxView Color = "Blue" VerticalOptions="FillAndExpand"/>
    ///            <Label Text = "Blue" HorizontalOptions="CenterAndExpand"/>
    ///        </StackLayout>
    ///    </ContentPage>
    /// </w:IndexPage>
    /// </code>
    /// </example>
    public class IndexPage : MultiPage<ContentPage>
    {
        protected override ContentPage CreateDefault(object item)
        {
            var page = new ContentPage();
            return page;
        }
    }
}
