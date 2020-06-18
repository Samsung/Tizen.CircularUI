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
	static class SelectModeCell
	{
		public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(ISelectModeCell.IsSelected), typeof(bool), typeof(ISelectModeCell), false, propertyChanged: (obj, oldValue, newValue) =>
		{
			ISelectModeCell selectModetCell = (ISelectModeCell)obj;
			selectModetCell.OnIsSelectedChanged(obj, new ToggledEventArgs((bool)newValue));
		}, defaultBindingMode: BindingMode.TwoWay);

		public static readonly BindableProperty IsSelectionModeEnabledProperty = BindableProperty.Create(nameof(ISelectModeCell.IsSelectionModeEnabled), typeof(bool), typeof(ISelectModeCell), default(bool));
	}
}
