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
        public class MyData
        {
            public string Text { get; set; }
        }

        ObservableCollection<MyData> myDatas;

        public TCListAppender ()
        {
            myDatas = new ObservableCollection<MyData>();

            for (int i = 1; i <= 3; ++i)
            {
                myDatas.Add(new MyData{ Text = string.Format("TestItem{0}", i) });
            }

            InitializeComponent ();

            mylist.ItemTemplate = new DataTemplate(() =>
            {
                var cell = new TextCell();
                cell.SetBinding(TextCell.TextProperty, new Binding("Text"));
                cell.BindingContextChanged += (s, e) =>
                {
                    if (String.IsNullOrEmpty(cell.AutomationId))
                        cell.AutomationId = cell.Text;
                };
                return cell;
            });

            mylist.ItemsSource = myDatas;
        }

        void DoAdd(object sender, EventArgs e)
        {
            myDatas.Add(new MyData{ Text = string.Format("TestItem{0}", myDatas.Count + 1) });
        }

        void DoDel(object sender, EventArgs e)
        {
            if (myDatas.Count > 0)
                myDatas.RemoveAt(myDatas.Count - 1);
        }
    }
}