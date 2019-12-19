using System;
using ElmSharp;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public interface IShellItemRenderer : IDisposable
    {
        BaseShellItem Item { get; }
        EvasObject NativeView { get; }
    }
}
