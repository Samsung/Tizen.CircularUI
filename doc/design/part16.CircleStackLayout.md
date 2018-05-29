# CircleStackLayout

![CircleStackLayout\\ design](data/CircleStackLayout.png)

CircleStackLayout 은 StackLayout과 동일하지만, 원형 화면에 맞는 형태로 내부 컴포넌트의 배치를 한다.

사각형 컴포넌트가 원에 내접하게 배치되며, 배치 후에 margin등이 계산된다.

```C#
public class CircleStackLayout : StackLayout
{
}
```
