# IndexPage

`IndexPage`는 `ElmSharp.Index`를 표현하며,  `Xamarin.Forms`의 `ContentPage`를 확장한다.

`ContentPage`로 동작 하므로 `Content`영역에 `ScrollView`등을 등록할 수 있으며 `Index`의 위치는 고정되어 있다.

 `Scrollview`의 경우`page scroll` 기능이 미지원하므로, `ScrollX`,`ScrollY` 좌표를 이용하며 index를 select해야 한다.

 `ItemCount`값에 따라 index item이 추가된다. `ItemCount`값을 미지정하면 default 값으로 동작한다.

![IndexPage Design](data/IndexPage.png)

`IndexPage`의 Class Diagram은 아래와 같다.

![IndexPage Class Diagram](uml/IndexPage.png)

위 Class 중 Xamarin interface 부분은 다음과 같이 Code로 표현된다.

 ```C#
 public class IndexPage : Xamarin.Forms.ContentPage
 {
     public static readonly BindableProperty IsAutoHidingEnabledProperty;
     public static readonly BindableProperty SelectedIndexProperty;
     public static readonly BindableProperty DisplayStyleProperty;
     public static readonly BindableProperty ItemCountProperty;

     public event EventHandler SelectedIndexChanged;

     public bool IsAutoHidingEnabled  { get; set; }
     public int SelectedIndex { get; set; }
     public IndexDisplayStyle DisplayStyle { get;  set; }
     public int ItemCount { get; set;}
 }

 public enum IndexDisplayStyle
 {
      Thumbnail,
      Circle
 }

 ```
