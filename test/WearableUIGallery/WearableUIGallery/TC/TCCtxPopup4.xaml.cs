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
	public partial class TCCtxPopup4 : CirclePage
	{
        StatusContextPopupEffectBehavior behavior;

        public TCCtxPopup4()
        {
            AcceptedCommand = new Command(
                execute: () =>
                {
                    BackgroundColor = Color.Green;
                });
            CancelCommand = new Command(
                execute: () =>
                {
                    BackgroundColor = Color.Red;
                });

            InitializeComponent();
        }

        void OnClickButton(object sender, EventArgs args)
        {
            if(CtxButton.Text != "Button" && behavior != null)
            {
                behavior.Visibility = true;
            }
        }

        void OnClickAttach(object sender, EventArgs args)
        {
            if(behavior == null)
            {
                behavior = new StatusContextPopupEffectBehavior();
                behavior.AcceptCommand = AcceptedCommand;
                behavior.AcceptText = "Green";
                behavior.CancelCommand = CancelCommand;
                behavior.CancelText = "Red";
                behavior.PositionOption = PositionOption.CenterOfParent;
                behavior.Visibility = false;
            }

            if (behavior != null)
            {
                behavior.Status = "Added";
                behavior.OnAttachedTo(CtxButton);

                CtxButton.Text = "Button with ContextPopup";
                State.Text = "Attached";
                Status.Text = behavior.Status.ToString();
            }
        }

        void OnClickDetach(object sender, EventArgs args)
        {
            if(behavior != null)
            {
                behavior.Status = "Removed";
                behavior.OnDetachingFrom(CtxButton);
                CtxButton.Text = "Button";
                State.Text = "Detached";
                Status.Text = behavior.Status.ToString();

                CtxButton.Behaviors.Clear();
            }
        }

        void OnClickStatus(object sender, EventArgs args)
        {
            if (behavior != null && behavior.Status != "Removed")
            {
                behavior.Status = "Set";
                Status.Text = behavior.Status.ToString();
            }
        }

        public ICommand AcceptedCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
    }
}