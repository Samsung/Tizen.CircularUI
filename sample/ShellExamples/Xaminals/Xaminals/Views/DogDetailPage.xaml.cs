﻿using System;
using System.Linq;
using Xamarin.Forms;
using Xaminals.Data;
using Tizen.Wearable.CircularUI.Forms;

namespace Xaminals.Views
{
    [QueryProperty("Name", "name")]
    public partial class DogDetailPage : CirclePage
    {
        public string Name
        {
            set
            {
                BindingContext = DogData.Dogs.FirstOrDefault(m => m.Name == Uri.UnescapeDataString(value));
            }
        }

        public DogDetailPage()
        {
            InitializeComponent();
        }
    }
}
