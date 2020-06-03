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
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
	/// <summary>
	/// A Cell with a label and a Check.
	/// </summary>
	public class CheckCell : Cell
	{
		/// <summary>
		/// Identifies the On bindable property.
		/// </summary>
		public static readonly BindableProperty OnProperty = BindableProperty.Create(nameof(On), typeof(bool), typeof(CheckCell), false, propertyChanged: (obj, oldValue, newValue) =>
		{
			var checkCell = (CheckCell)obj;
			checkCell.OnChanged?.Invoke(obj, new CheckedChangedEventArgs((bool)newValue));
		}, defaultBindingMode: BindingMode.TwoWay);

		/// <summary>
		/// Identifies the Text bindable property.
		/// </summary>
		public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CheckCell), default(string));

		/// <summary>
		/// The backing store for the OnColor field.
		/// </summary>
		public static readonly BindableProperty OnColorProperty = BindableProperty.Create(nameof(OnColor), typeof(Color), typeof(CheckCell), Color.Default);

		/// <summary>
		/// Gets or sets the color of On state of the checkbox. This is a bindable property.
		/// </summary>
		public Color OnColor
		{
			get { return (Color)GetValue(OnColorProperty); }
			set { SetValue(OnColorProperty, value); }
		}

		/// <summary>
		/// Gets or sets the state of the checkbox. This is a bindable property.
		/// </summary>
		public bool On
		{
			get { return (bool)GetValue(OnProperty); }
			set { SetValue(OnProperty, value); }
		}

		/// <summary>
		/// Gets or sets the text displayed next to the checkbox. This is a bindable property.
		/// </summary>
		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		/// <summary>
		/// Triggered when the checkbox has changed value.
		/// </summary>
		public event EventHandler<CheckedChangedEventArgs> OnChanged;
	}
}
