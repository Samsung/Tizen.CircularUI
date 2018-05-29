# ConfirmPopupEffect

![ConfirmPopupEffect design](data/ctxpopup.png)

`ConfirmPopupEffect`는 특정 widget에 비례하거나 화면상의 어느 위치에 1개 혹은 2개의 버튼을 가지는 popup을 나타낼 수 있습니다.

Accept 버튼을 기본으로 하나 가지며, 기본으로 "Ok" Text가 Accept에 표시 됩니다.

`ConfirmVisibility`가 `True`가 될때, 화면에 나타나며,
Accept나 Cancel시에 주어진 Command가 실행 되며 자동으로 사라지며, `ConfirmVisibility`가 `False`가 됩니다.

또한 popup 외부를 터치시에, popup은 자동으로 Cancel 됩니다. (Cancel command가 마찬가지로 수행 됩니다.)
```xml
﻿<?xml version="1.0" encoding="utf-8" ?>
<w:CirclePage xmlns="http://xamarin.com/schemas/2014/forms"
              xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
              xmlns:w="clr-namespace:Xamarin.Forms.CircularUI;assembly=Xamarin.Forms.CircularUI"
              x:Class="WearableUIGallery.TC.TCConfirm"
              x:Name="mypage">
    <ContentPage.Content>
        <StackLayout>
            <w:Check x:Name="confirm3"
                     IsToggled="False"
                     VerticalOptions="CenterAndExpand"
                     HorizontalOptions="CenterAndExpand"
                     w:ConfirmPopupEffect.AcceptText="Yes"
                     w:ConfirmPopupEffect.CancelText="No"
                     w:ConfirmPopupEffect.ConfirmVisibility="{Binding IsToggled, Source={x:Reference confirm3}, Mode=TwoWay}"
                     w:ConfirmPopupEffect.AcceptCommand="{Binding AcceptedCommand, Source={x:Reference mypage}}"
                     w:ConfirmPopupEffect.CancelCommand="{Binding CancelCommand, Source={x:Reference mypage}}"
                     w:ConfirmPopupEffect.PositionOption="CenterOfWindow">
                <w:Check.Effects>
                    <w:ConfirmPopupEffect/>
                </w:Check.Effects>
            </w:Check>
        </StackLayout>
    </ContentPage.Content>
</w:CirclePage>
```

이 Effect의 Xamarin Forms 속성은 다음 코드와 같다.

```cs
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Xamarin.Forms.CircularUI
{
    public class ConfirmPopupEffect : RoutingEffect
    {
        public static BindableProperty AcceptTextProperty;
        public static BindableProperty AcceptCommandProperty;
        public static BindableProperty AcceptCommandParameterProperty;

        public static BindableProperty CancelTextProperty;
        public static BindableProperty CancelCommandProperty;
        public static BindableProperty CancelCommandParameterProperty;

        public static BindableProperty ConfirmVisibilityProperty;

        public static BindableProperty PositionOptionProperty;
        public static BindableProperty OffsetProperty;

        public static string GetAcceptText(BindableObject obj);
        public static void SetAcceptText(BindableObject obj, string value);
        public static ICommand GetAcceptCommand(BindableObject obj);
        public static void SetAcceptCommand(BindableObject obj, ICommand value);
        public static object GetAcceptCommandParameter(BindableObject obj);
        public static void SetAcceptCommandParameter(BindableObject obj, object value);

        public static string GetCancelText(BindableObject obj);
        public static void SetCancelText(BindableObject obj, string value);public static ICommand GetCancelCommand(BindableObject obj);
        public static void SetCancelCommand(BindableObject obj, ICommand value);
        public static object GetCancelCommandParameter(BindableObject obj);
        public static void SetCancelCommandParameter(BindableObject obj, object value);

        public static bool GetConfirmVisibility(BindableObject obj);
        public static void SetConfirmVisibility(BindableObject obj, bool value);

        public static PositionOption GetPositionOption(BindableObject obj);
        public static void SetPositionOption(BindableObject obj, PositionOption value);

        public static Point GetOffset(BindableObject obj);
        public static void SetOffset(BindableObject obj, Point value);

        public ConfirmPopupEffect() : base("CircleUI.ConfirmPopupEffect")
        {
        }
    }
    public enum PositionOption
    {
        BottomOfView,
        CenterOfWindow,
        Absolute,
        Relative
    }
}

```

각 속성의 의미는 다음과 같다.

* AcceptText : Accep 버튼에서 쓰일 Text 값이 주어지지 않을 경우 Accept Button은 "Ok"로 표시된다.
* AcceptCommand : Accept 시에 수행 될 Command
* AcceptCommandParameter : AcceptCommand에 적용 될 Parameter
* CancelText : Cancel 버튼으로 쓰일 Text
* CancelCommand : Cancel 시에 수행 될 Command, popup 외부를 선택해서 popup이 사라져도 수행된다.
* CancelCommandParameter : CancelCommand에 적용 될 Parameter
* ConfirmVisibility : True시에 Popup이 나타나며, False가 되면 사라집니다. False로 사라져도 CancelCommand가 작동합니다.
* PositionOption 
  * `BottomOfView` : Effect를 사용한 View의 하단에 popup이 나타납니다. View의 중앙에서 Offset만큼 위치가 변경됩니다.
  * `CenterOfWindow` : 화면 중앙에 Popup에서 Offset만큼 이동합니다.
  * `Absolute` : Offset의 값을 X, Y로 하여 popup이 화면에 배치됩니다.
  * `Relative` : Offset.X * Window.Width, Offset.Y * Window.Height 에 배치됩니다.
* Offset : PositionOption에 따라 적용되는 값. 0,0 시작 위치는 popup 위의 꼭지 부분 입니다.