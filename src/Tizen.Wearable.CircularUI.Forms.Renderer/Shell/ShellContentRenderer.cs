﻿using ElmSharp;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class ShellContentRenderer : IShellItemRenderer
    {
        public ShellContentRenderer(ShellContent content)
        {
            ShellContent = content;
            NativeView = GetNativeView(content);
        }

        public ShellContent ShellContent { get; protected set; }

        public BaseShellItem Item => ShellContent;

        public EvasObject NativeView { get; protected set; }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                NativeView?.Unrealize();
            }
        }

        static EvasObject GetNativeView(ShellContent content)
        {
            var page = (content as IShellContentController).GetOrCreateContent();
            return Platform.GetOrCreateRenderer(page).NativeView;
        }
    }
}
