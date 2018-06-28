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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TCCtxPopup2 : CirclePage
	{
		public TCCtxPopup2()
		{
            CtxCheck1AcceptedCommand = new Command( 
                execute: () => 
                {
                    BackgroundColor = Color.Green;
                    CtxCheck1EffectBehavior.AcceptText="Green";
                    CtxCheck1EffectBehavior.AcceptCommandParameter = "Accept is clicked";

                    UpdateDescription();
                });
            CtxCheck1CancelCommand = new Command(
                execute: () =>
                {
                    BackgroundColor = Color.Red;
                    CtxCheck1EffectBehavior.CancelText = "Red";
                    CtxCheck1EffectBehavior.CancelCommandParameter = "Cancel is clicked";

                    UpdateDescription();
                });

            InitializeComponent ();

            CtxCheck1.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == Check.IsToggledProperty.PropertyName)
                    System.Diagnostics.Debug.WriteLine($"IsToggled of CtxCheck1= {CtxCheck1.IsToggled}");
            };
        }

        void UpdateDescription()
        {
            labelOfAcceptCommandParamter.Text = CtxCheck1EffectBehavior.AcceptCommandParameter.ToString();
            labelOfCancelCommandParamter.Text = CtxCheck1EffectBehavior.CancelCommandParameter.ToString();
        }

        public ICommand CtxCheck1AcceptedCommand { get; private set; }
        public ICommand CtxCheck1CancelCommand { get; private set; }

    }
}