using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TCListAppender : TwoButtonPage
	{
        public ObservableCollection<string> Texts { get; set; }

		public TCListAppender ()
		{
            Texts = new ObservableCollection<string>
            {
                "Test 1",
                "Test 2",
                "Test 3"
            };
            InitializeComponent ();
            mylist.ItemsSource = Texts;
        }

        void DoAdd(object sender, EventArgs e)
        {
            Texts.Add("Test " + (Texts.Count+1));
        }

        void DoDel(object sender, EventArgs e)
        {
            if (Texts.Count > 0)
                Texts.RemoveAt(Texts.Count - 1);
        }
    }
}