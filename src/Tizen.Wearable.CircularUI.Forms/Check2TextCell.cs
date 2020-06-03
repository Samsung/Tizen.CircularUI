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

using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
	/// <summary>
	/// A Cell with two labels and a Check.
	/// </summary>
	public class Check2TextCell : CheckCell
	{
		/// <summary>
		/// Identifies the Detail bindable property.
		/// </summary>
		public static readonly BindableProperty DetailProperty = BindableProperty.Create(nameof(Detail), typeof(string), typeof(Check2TextCell), default(string));

		/// <summary>
		/// Gets or sets the sub text displayed next to the checkbox. This is a bindable property.
		/// </summary>
		public string Detail
		{
			get { return (string)GetValue(DetailProperty); }
			set { SetValue(DetailProperty, value); }
		}
	}
}
