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
	public partial class TCCtxPopup3 : CirclePage
	{
        public TCCtxPopup3()
        {
            CtxCheck1AcceptedCommand = new Command(
                execute: () =>
                {
                    BackgroundColor = Color.Green;
                });
            CtxCheck1CancelCommand = new Command(
                execute: () =>
                {
                    BackgroundColor = Color.Red;
                });

            InitializeComponent();

            CtxCheck1.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == Check.IsToggledProperty.PropertyName)
                    System.Diagnostics.Debug.WriteLine($"IsToggled of CtxCheck1= {CtxCheck1.IsToggled}");
            };
        }

        void OnClickAttach(object sender, EventArgs args)
        {
            var behavior = new ContextPopupEffectBehavior();
            behavior.AcceptCommand = CtxCheck1AcceptedCommand;
            behavior.AcceptText = "Yes";
            behavior.CancelCommand = CtxCheck1CancelCommand;
            behavior.CancelText = "No";
            behavior.PositionOption = PositionOption.BottomOfView;
            behavior.Visibility = true;

            CtxCheck1.Behaviors.Add(behavior);

            State.Text = "Attached";
        }

        void OnClickDetach(object sender, EventArgs args)
        {
            CtxCheck1.Behaviors.Clear();
            State.Text = "Detached";
        }

        public ICommand CtxCheck1AcceptedCommand { get; private set; }
        public ICommand CtxCheck1CancelCommand { get; private set; }
    }
}