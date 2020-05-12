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

using System.Windows.Input;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCCtxPopup2 : ContentPage
    {
        public TCCtxPopup2()
        {
            CtxCheck1AcceptedCommand = new Command( 
                execute: () => 
                {
                    BackgroundColor = Color.Green;
                    CtxCheck1EffectBehavior.AcceptCommandParameter = "Command param: Accept is clicked";
                    labelOfCommandParamter.Text = CtxCheck1EffectBehavior.AcceptCommandParameter.ToString();
                });
            CtxCheck1CancelCommand = new Command(
                execute: () =>
                {
                    BackgroundColor = Color.Red;
                    CtxCheck1EffectBehavior.CancelCommandParameter = "Command param: Cancel is clicked";
                    labelOfCommandParamter.Text = CtxCheck1EffectBehavior.CancelCommandParameter.ToString();
                });

            InitializeComponent ();

            CtxCheck1.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == Switch.IsToggledProperty.PropertyName)
                    System.Diagnostics.Debug.WriteLine($"IsToggled of CtxCheck1= {CtxCheck1.IsToggled}");
            };
        }

        public ICommand CtxCheck1AcceptedCommand { get; private set; }
        public ICommand CtxCheck1CancelCommand { get; private set; }

    }
}