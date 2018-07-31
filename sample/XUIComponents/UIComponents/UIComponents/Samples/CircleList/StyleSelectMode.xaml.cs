using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace UIComponents.Samples.CircleList
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StyleSelectMode : CirclePage
    {
        public static BindableProperty IsCheckableProperty = BindableProperty.Create(nameof(IsCheckable), typeof(bool), typeof(StyleSelectMode), false);
        public static BindableProperty PopupVisibilityProperty = BindableProperty.Create(nameof(PopupVisibility), typeof(bool), typeof(StyleSelectMode), false);

        /// <summary>
        /// Constructor
        /// </summary>
        public StyleSelectMode ()
		{
            IsCheckable = false;
            LongClickCommand = new Command(OnLongClick);
            InitializeComponent ();
        }

        public ICommand LongClickCommand { get; set; }

        public bool IsCheckable
        {
            get => (bool)GetValue(IsCheckableProperty);
            set => SetValue(IsCheckableProperty, value);
        }

        public bool PopupVisibility
        {
            get => (bool)GetValue(PopupVisibilityProperty);
            set => SetValue(PopupVisibilityProperty, value);
        }

        void OnLongClick()
        {
            if (!IsCheckable)
            {
                IsCheckable = true;
            }
        }

        void OnCheckedCounterClicked(object sender, EventArgs e)
        {
            PopupVisibility = true;

            Console.WriteLine("Checked is clicked!!!");
        }
    }
}