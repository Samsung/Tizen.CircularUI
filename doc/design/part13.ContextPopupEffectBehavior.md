# ContextPopupEffectBehavior

![ContextPopupEffectBehavior design](data/ctxpopup.png)

`ContextPopupEffectBehavior`는 특정 widget에 비례하거나 화면상의 어느 위치에 1개 혹은 2개의 버튼을 가지는 popup을 나타낼 수 있습니다.

Accept 버튼을 기본으로 하나 가지며, 기본으로 공백 Text가 Accept에 표시 됩니다.

`Visibility`가 `True`가 될때, 화면에 나타나며,
Accept나 Cancel시에 주어진 Command가 실행 되며 자동으로 사라지며, `Visibility`가 `False`가 됩니다.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<w:CirclePage
    x:Class="WearableUIGallery.TC.TCConfirm"
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:w="clr-namespace:Tizen.Wearable.CircularUI.Forms;assembly=Tizen.Wearable.CircularUI.Forms"
    x:Name="mypage">
    <ContentPage.Content>
        <AbsoluteLayout>
            <w:Check
                x:Name="CtxCheck"
                AbsoluteLayout.LayoutBounds="0.5, 0.2, 1, 0.25"
                AbsoluteLayout.LayoutFlags="All"
                HorizontalOptions="CenterAndExpand"
                IsToggled="False"
                VerticalOptions="CenterAndExpand">
                <w:Check.Behaviors>
                    <w:ContextPopupEffectBehavior
                        AcceptCommand="{Binding AcceptedCommand, Source={x:Reference mypage}}"
                        AcceptText="Yes"
                        CancelCommand="{Binding CancelCommand, Source={x:Reference mypage}}"
                        CancelText="No"
                        Visibility="{Binding IsToggled, Source={x:Reference CtxCheck}, Mode=TwoWay}"
                        PositionOption="BottomOfView"/>
                </w:Check.Behaviors>
            </w:Check>
        </AbsoluteLayout>
    </ContentPage.Content>
</w:CirclePage>
```

이 Behavior의 Xamarin Forms 속성은 다음 코드와 같다.

```cs
public class ContextPopupEffectBehavior : Behavior<View>
{
    public static BindableProperty AcceptTextProperty = BindableProperty.Create(nameof(AcceptText), typeof(string), typeof(ContextPopupEffectBehavior), null);
    public static BindableProperty AcceptCommandProperty = BindableProperty.Create(nameof(AcceptCommand), typeof(ICommand), typeof(ContextPopupEffectBehavior), null);
    public static BindableProperty AcceptCommandParameterProperty = BindableProperty.Create(nameof(AcceptCommandParameter), typeof(object), typeof(ContextPopupEffectBehavior), null);
    public static BindableProperty CancelTextProperty = BindableProperty.Create(nameof(CancelText), typeof(string), typeof(ContextPopupEffectBehavior), null);
    public static BindableProperty CancelCommandProperty = BindableProperty.Create(nameof(CancelCommand), typeof(ICommand), typeof(ContextPopupEffectBehavior), null);
    public static BindableProperty CancelCommandParameterProperty = BindableProperty.Create(nameof(CancelCommandParameter), typeof(object), typeof(ContextPopupEffectBehavior), null);
    public static BindableProperty VisibilityProperty = BindableProperty.Create(nameof(Visibility), typeof(bool), typeof(ContextPopupEffectBehavior), false);
    public static BindableProperty PositionOptionProperty = BindableProperty.Create(nameof(PositionOption), typeof(PositionOption), typeof(ContextPopupEffectBehavior), PositionOption.BottomOfView);
    public static BindableProperty OffsetProperty = BindableProperty.Create(nameof(Offset), typeof(Point), typeof(ContextPopupEffectBehavior), default(Point));
    public string AcceptText { get; set; }
    public ICommand AcceptCommand { get; set; }
    public object AcceptCommandParameter { get; set; }
    public string CancelText { get; set; }
    public ICommand CancelCommand { get; set; }
    public object CancelCommandParameter { get; set; }
    public bool Visibility { get; set; }
    public PositionOption PositionOption { get; set; }
    public Point Offset { get; set; }
}
public enum PositionOption
{
    BottomOfView,
    CenterOfWindow,
    Absolute,
    Relative
}
```

각 속성의 의미는 다음과 같다.

* AcceptText : Accep 버튼에서 쓰일 Text 값이 주어지지 않을 경우 Accept Button은 공백으로 표시된다.
* AcceptCommand : Accept 시에 수행 될 Command
* AcceptCommandParameter : AcceptCommand에 적용 될 Parameter
* CancelText : Cancel 버튼으로 쓰일 Text
* CancelCommand : Cancel 시에 수행 될 Command, popup 외부를 선택해서 popup이 사라져도 수행된다.
* CancelCommandParameter : CancelCommand에 적용 될 Parameter
* Visibility : True시에 Popup이 나타나며, False가 되면 사라집니다.
* PositionOption 
  * `BottomOfView` : Effect를 사용한 View의 하단에 popup이 나타납니다. View의 중앙에서 Offset만큼 위치가 변경됩니다.
  * `CenterOfWindow` : 화면 중앙에 Popup에서 Offset만큼 이동합니다.
  * `Absolute` : Offset의 값을 X, Y로 하여 popup이 화면에 배치됩니다.
  * `Relative` : Offset.X * Window.Width, Offset.Y * Window.Height 에 배치됩니다.
* Offset : PositionOption에 따라 적용되는 값. 0,0 시작 위치는 popup 위의 꼭지 부분 입니다.