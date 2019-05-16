using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;
using System.Windows.Input;

namespace SimpleWidgetApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CirclePage
    {
        static Color[] _colors = new Color[] {
            Color.DarkRed,
            Color.IndianRed,
            Color.MediumVioletRed,
            Color.DeepPink,
            Color.HotPink,
            Color.GreenYellow,
            Color.LawnGreen,
            Color.LightGreen,
            Color.LightSeaGreen,
            Color.LimeGreen,
            Color.Navy,
            Color.Orange,
            Color.PaleGreen,
            Color.PaleVioletRed,
            Color.Purple,
            Color.RoyalBlue,
            Color.DeepSkyBlue,
            Color.BlueViolet,
            Color.LightYellow,
            Color.DarkOliveGreen,
            Color.YellowGreen,
        };

        public ICommand ChangeColorCommand { get; private set; }

        static int index = 0;
        public MainPage ()
        {
            InitializeComponent ();
        }

        void OnButtonClicked(object sender, EventArgs args)
        {
            layout.BackgroundColor = _colors[index++];
        }
    }
}