using System;

namespace WearableUIGallery.Extensions
{
    public interface IEcoreAnimator
    {
        double CurrentTime { get; }

        event EventHandler<EcoreAnimatorEventArgs> Animation;
    }
}
