using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace UIComponents.Samples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Radio : CirclePage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Radio()
        {
            InitializeComponent();
        }
    }
}
