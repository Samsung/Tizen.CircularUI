using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCIndexPageSingleStart : IndexPage
    {
        public TCIndexPageSingleStart()
        {
            InitializeComponent();
        }

        private void OnAddButtonClicked(object sender, EventArgs e)
        {
            var page = new ContentPage
            {
                Content = new StackLayout
                {
                    Children = {
                        new BoxView {
                            Color = Color.Red,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.FillAndExpand
                        },
                        new Label {
                            AutomationId = $"label1",
                            Text = "Added Page(1)",
                            HorizontalOptions = LayoutOptions.CenterAndExpand
                        }
                    }
                }
            };
            Children.Add(page);
        }
    }
}