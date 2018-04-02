using System;
using System.Collections.Generic;
using System.Linq;
using ElmSharp;
using WearableUIGallery.Extensions;
using WearableUIGallery.Tizen.Wearable.DependencyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(EcoreAnimatorService))]
namespace WearableUIGallery.Tizen.Wearable.DependencyService
{

    public class EcoreAnimatorService : IEcoreAnimator
	{
        IDictionary<IntPtr, EventHandler<EcoreAnimatorEventArgs>> _handlers = new Dictionary<IntPtr, EventHandler<EcoreAnimatorEventArgs>>();
        public double CurrentTime => EcoreAnimator.GetCurrentTime();

        event EventHandler<EcoreAnimatorEventArgs> IEcoreAnimator.Animation
        {
            add
            {
                Func<bool> handler = () =>
                {
                    EcoreAnimatorEventArgs args = new EcoreAnimatorEventArgs(true);
                    value?.Invoke(this, args);
                    return args.Repeat;
                };

                _handlers.Add(EcoreAnimator.AddAnimator(handler), value);
             }

            remove
            {
                var targetHandler = _handlers.Where(p => p.Value == value).FirstOrDefault();
                EcoreAnimator.RemoveAnimator(targetHandler.Key);
                _handlers.Remove(targetHandler.Key);
            }
        }
    }
}
