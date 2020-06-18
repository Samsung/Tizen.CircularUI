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

using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using ElmSharp;
using Tizen.Wearable.CircularUI.Forms;
using Tizen.Wearable.CircularUI.Forms.Renderer;
using ECheck = ElmSharp.Check;

[assembly: ExportRenderer(typeof(SelectModeTextCell), typeof(SelectModeTextCellRenderer))]
namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
	public class SelectModeTextCellRenderer : TextCellRenderer
	{
		protected SelectModeTextCellRenderer(string style) : base(style)
		{
			SelectionPart = "elm.swallow.center_check";
		}

		public SelectModeTextCellRenderer() : this("1icon_2text")
		{
		}

		protected string SelectionPart { get; set; }

		protected override EvasObject OnGetContent(Cell cell, string part)
		{
			if (cell is ISelectModeCell selectCell && selectCell.IsSelectionModeEnabled && part == SelectionPart)
			{
				var check = new CheckBox()
				{
					BindingContext = cell,
					Parent = cell.Parent
				};
				check.SetBinding(CheckBox.IsCheckedProperty, new Binding(SelectModeTextCell.IsSelectedProperty.PropertyName));
				var nativeView = Platform.GetOrCreateRenderer(check).NativeView;
				if (nativeView is ECheck widget)
					widget.Style = "genlist/select_mode";
				nativeView.PropagateEvents = false;
				nativeView.RepeatEvents = false;
				return nativeView;
			}
			return null;
		}

		protected override bool OnCellPropertyChanged(Cell cell, string property, Dictionary<string, EvasObject> realizedView)
		{
			if (property == SelectModeCell.IsSelectedProperty.PropertyName ||
				property == SelectModeCell.IsSelectionModeEnabledProperty.PropertyName)
			{
				return true;
			}
			return base.OnCellPropertyChanged(cell, property, realizedView);
		}
	}
}
