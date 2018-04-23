using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

namespace UIComponents.Samples.Button
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DefaultButton : CirclePage
    {
		public DefaultButton ()
		{
			InitializeComponent ();
		}
	}
}