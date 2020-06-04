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
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
	/// <summary>
	/// A Cell with a label and a RadioButton.
	/// </summary>
	public class RadioCell : Cell
	{
		/// <summary>
		/// Identifies the On bindable property.
		/// </summary>
		public static readonly BindableProperty OnProperty = BindableProperty.Create(nameof(On), typeof(bool), typeof(RadioCell), false, propertyChanged: (obj, oldValue, newValue) =>
		{
			var radioCell = (RadioCell)obj;
			radioCell.OnChanged?.Invoke(obj, new CheckedChangedEventArgs((bool)newValue));
		}, defaultBindingMode: BindingMode.TwoWay);

		/// <summary>
		/// Identifies the GroupName bindable property.
		/// </summary>
		public static readonly BindableProperty GroupNameProperty = BindableProperty.Create(nameof(GroupName), typeof(string), typeof(RadioCell), default(string));

		/// <summary>
		/// Identifies the Text bindable property.
		/// </summary>
		public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(RadioCell), default(string));

		/// <summary>
		/// Gets or sets the state of the radio button. This is a bindable property.
		/// </summary>
		public bool On
		{
			get { return (bool)GetValue(OnProperty); }
			set { SetValue(OnProperty, value); }
		}

		/// <summary>
		/// Gets or sets the group name of the radio button. This is a bindable property.
		/// </summary>
		public string GroupName
		{
			get { return (string)GetValue(GroupNameProperty); }
			set { SetValue(GroupNameProperty, value); }
		}

		/// <summary>
		///  Gets or sets the text displayed next to the radio button. This is a bindable property.
		/// </summary>
		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		/// <summary>
		/// Triggered when the radio button has changed value.
		/// </summary>
		public event EventHandler<CheckedChangedEventArgs> OnChanged;


		/// <summary>
		/// Internal use only.
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public RadioButton RadioButton { get; }

		public RadioCell()
		{
			RadioButton = new RadioButton()
			{
				Parent = this,
				BindingContext = this,
			};
			RadioButton.SetBinding(RadioButton.IsCheckedProperty, new Binding(OnProperty.PropertyName));
			RadioButton.SetBinding(RadioButton.GroupNameProperty, new Binding(GroupNameProperty.PropertyName));
		}
	}
}
