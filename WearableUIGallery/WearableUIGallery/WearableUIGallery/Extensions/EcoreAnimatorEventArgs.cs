using System;

namespace WearableUIGallery.Extensions
{
    public class EcoreAnimatorEventArgs : EventArgs
	{
		public bool Repeat { get; set; } = true;
		public EcoreAnimatorEventArgs(bool repeat)
		{
			Repeat = repeat;
		}
	}
}
