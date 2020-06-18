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
	/// A TextCell supports a selection mode.
	/// </summary>
	public class SelectModeTextCell : TextCell, ISelectModeCell
	{
		/// <summary>
		/// Identifies the On bindable property.
		/// </summary>
		public static readonly BindableProperty IsSelectedProperty = SelectModeCell.IsSelectedProperty;

		/// <summary>
		/// Identifies the Text bindable property.
		/// </summary>
		public static readonly BindableProperty IsSelectionModeEnabledProperty = SelectModeCell.IsSelectionModeEnabledProperty;

		/// <summary>
		/// Gets or sets the state of the selection. This is a bindable property.
		/// </summary>
		public bool IsSelected
		{
			get { return (bool)GetValue(IsSelectedProperty); }
			set { SetValue(IsSelectedProperty, value); }
		}

		/// <summary>
		/// Gets or sets whether to enable the selection mode or not. This is a bindable property.
		/// </summary>
		public bool IsSelectionModeEnabled
		{
			get { return (bool)GetValue(IsSelectionModeEnabledProperty); }
			set { SetValue(IsSelectionModeEnabledProperty, value); }
		}

		/// <summary>
		/// Triggered when IsSelected changed.
		/// </summary>
		public event EventHandler<ToggledEventArgs> SelectionChanged;

		void ISelectModeCell.OnIsSelectedChanged(object sender, ToggledEventArgs e)
		{
			SelectionChanged?.Invoke(sender, e);
		}
    }
}
