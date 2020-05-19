/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCCircleImageIndexPage : ContentPage
    {
        public TCCircleImageIndexPage()
        {
            InitializeComponent();
            BindingContext = new CircleImageViewModel();
        }
    }

    public class CircleImageItem
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public Color BGColor { get; set; }
    }

    public class CircleImageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CircleImageItem> CircleImageItems { get; private set; } = new ObservableCollection<CircleImageItem>();

        public CircleImageViewModel()
        {
            AddItems();
        }

        public void AddItems()
        {
            CircleImageItems.Add(new CircleImageItem
            {
                Name = "Image1",
                ImageUrl = "image/100_1.jpg",
                Width = 100,
                Height = 100,
                BGColor = Color.Black
            });
            CircleImageItems.Add(new CircleImageItem
            {
                Name = "Image2",
                ImageUrl = "image/100_2.jpg",
                Width = 100,
                Height = 100,
                BGColor = Color.Black
            });
            CircleImageItems.Add(new CircleImageItem
            {
                Name = "Image3",
                ImageUrl = "image/100_3.jpg",
                Width = 200,
                Height = 200,
                BGColor = Color.Green
            });
            CircleImageItems.Add(new CircleImageItem
            {
                Name = "Image4",
                ImageUrl = "image/100_4.jpg",
                Width = 200,
                Height = 200,
                BGColor = Color.LightSkyBlue
            });
            CircleImageItems.Add(new CircleImageItem
            {
                Name = "Image5",
                ImageUrl = "image/100_5.jpg",
                Width = 200,
                Height = 200,
                BGColor = Color.Black
            });
            CircleImageItems.Add(new CircleImageItem
            {
                Name = "Image6",
                ImageUrl = "image/100_6.jpg",
                Width = 200,
                Height = 200,
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}