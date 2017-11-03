# CirclePage

![CircpePage design](data/CirclePage.png)

`CirclePage`는 `CircleSurface`가 필요한 View들을 넣을 수 있는 `ContentPage`와 흡사하며, `ToolbarItems`를 `MoreOption`으로 보여줄 수 있다. 
또한 Left, Right, Bottom 3개 영역에 `ButtonItems`을 사용해 Button을 보여준다.
`ICircleSurfaceItem`으로 View는 아니지만 CircleSurface에 표현되는 object들을 `CircleSurface`를 통해 표현한다.

![CirclePage Class Diagram](uml/CirclePage.png)

`CirclePage`의 class Diagram은 위와 같으며, Class 중 Xamarin interface 부분은 다음과 같이 Code로 표현된다.

 ```C#
 public class CirclePage : Xamarin.Forms.ContentPage
 {
     public static readonly BindableProperty RotaryEventCunsumerProperty; 

     public IRotaryEventConsumer RotaryEventConsumer { get; set; }
     public IList<CircleButtonsItem> ButtonItems { get; }
     public IList<ICircleSurfaceItem> CircleSurfaceItems { get; }
 }

 public interface ICircleSurfaceItem
 {
     public bool IsVisible { get; set; }
 }

 public interface IRotaryEventConsumer
 {
     event RotaryEventHandler Rotated;
 }

 public delegate RotaryEventHandler(RotaryEventArgs args);

 public class RotaryEventArgs : EventArgs
 {
     public bool IsClockwise { get; set; }
 }

 public CircleButtonsItem : Xamarin.Forms.MenuItem
 {
     public CircleButtonsItemDirection Direction { get; set; }
 }

 public enum CircleButtonsItemDirection
 {
     Left,
     Right,
     Bottom
 }
 ```
 이미 Page에 속성으로 있는 `ToolbarItems`과 `Content` Property를 사용하며,  
**현재 Page에서 Bezel Action을 받을 (Rotary Event를 가져갈) 단 하나의 Consumer를 `RotaryEventConsumer` property에 등록할 수 있다.  
 Page가 제거되거나, Hide 될때, `RotaryEventConsumer`는 동작을 중단하며, 만약 Hide에서 Show 될때,
 `RotaryEventConsumer`에 등록된 Child가 있다면, Rotary Event를 가져가게 된다.**

 ElmSharp level에서의 Scene Graph는 다음과 같이 표현된다.

![CirclePage Scene Graph](uml/CirclePage_SceneGraph.png)
