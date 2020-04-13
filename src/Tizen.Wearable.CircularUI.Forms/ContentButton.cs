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
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The ContentButton is a Button, which allows you to customize the View to be displayed.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class ContentButton : ContentView, IButtonController
    {
        /// <summary>
        /// BindableProperty. Identifies the Command bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ContentButton), null, 
            propertyChanging: OnCommandChanging, propertyChanged: OnCommandChanged);

        /// <summary>
        /// BindableProperty. Identifies the CommandParameter bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ContentButton), null, 
            propertyChanged: (bindable, oldvalue, newvalue) => CommandCanExcuteChanged(bindable, EventArgs.Empty));

        /// <summary>
        /// Gets or sets command that is executed when the button is clicked.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Gets or sets command paramter that is executed when the button is clicked.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// Occurs when the button is clicked.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public event EventHandler Clicked;

        /// <summary>
        /// Occurs when the button is pressed.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public event EventHandler Pressed;

        /// <summary>
        /// Occurs when the button is released.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public event EventHandler Released;

        bool IsEnabledCore
        {
            set => SetValueCore(IsEnabledProperty, value);
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendClicked()
        {
            if (IsEnabled)
            {
                Command?.Execute(CommandParameter);
                Clicked?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendPressed()
        {
            if (IsEnabled)
            {
                Pressed?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendReleased()
        {
            if (IsEnabled)
            {
                Released?.Invoke(this, EventArgs.Empty);
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            View content = Content;
            if (content != null)
            {
                SetInheritedBindingContext(content, BindingContext);
            }
        }

        static void OnCommandChanged(BindableObject bindable, object oldCommand, object newCommand)
        {
            ContentButton button = (ContentButton)bindable;
            if (newCommand is ICommand command)
            {
                command.CanExecuteChanged += button.OnCommandCanExecuteChanged;
            }
            CommandChanged(button);
        }

        static void CommandChanged(ContentButton button)
        {
            if(button.Command != null)
            {
                CommandCanExcuteChanged(button, EventArgs.Empty);
            }
            else
            {
                button.IsEnabledCore = true;
            }
        }

        static void OnCommandChanging(BindableObject bindable, object oldCommand, object newCommand)
        {
            ContentButton button = (ContentButton)bindable;
            if (oldCommand != null)
            {
                (oldCommand as ICommand).CanExecuteChanged -= button.OnCommandCanExecuteChanged;
            }
        }

        static void CommandCanExcuteChanged(object sender, EventArgs e)
        {
            var button = (ContentButton)sender;
            if (button.Command != null)
            {
                button.IsEnabledCore = button.Command.CanExecute(button.CommandParameter);
            }
        }

        void OnCommandCanExecuteChanged(object sender, EventArgs e)
        {
            ContentButton button = (ContentButton)sender;
            if (button.Command != null)
            {
                button.IsEnabledCore = button.Command.CanExecute(button.CommandParameter);
            }
        }
    }
}
