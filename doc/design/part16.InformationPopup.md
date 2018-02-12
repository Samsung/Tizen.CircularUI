# InformationPopup

![InformationPopup design](data/InformationPopup.png)

`InformationPopup`는 `ElmSharp.Popup`의 여러가지 형태의 popup을 표현하며, 화면을 구성하는 layer의 최상단에 Popup형태로 display 된다.
IsProgressRuning이 true일 경우 `small circle progress`가 화면 중앙에 표시되며 pulse 동작을 한다. 이때 title의 text는 무시된다.


![InformationPopup Diagram](uml/InformationPopup.png)

InformationPopup Diagram은 위와 같으며, 다음과 같이 코드로 표현된다.

```C#
public class InformationPopup : BindableObject
{
    public static readonly BindableProperty IsProgressRuningProperty
    public static readonly BindableProperty TitleProperty;
    public static readonly BindableProperty TextProperty;
    public static readonly BindableProperty BottomButtonProperty;

    public event EventHandler BackButtonPressed;

    public bool IsProgressRuning { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public MenuItem BottomButton { get; set; }
}
```

ElmSharp의 Popup을 사용하며, Layout이 아니므로 Parent가 존재하지 않는다(Xaml을 사용하여 Layouting 불가).

ElmSharp Level에서의 Scene Graph는 다음과 같이 표현된다.

![InformationPopup Scene Graph](uml/InformationPopup_SceneGraph.png)
