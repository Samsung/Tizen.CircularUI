using ElmSharp.Wearable;

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    interface ICircleSurfaceItemRenderer
    {
        ICircleWidget GetCircleWidget(ICircleSurfaceItem item);
    }
}
