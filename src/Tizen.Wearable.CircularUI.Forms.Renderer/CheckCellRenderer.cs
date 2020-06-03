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
using ElmSharp;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Tizen.Wearable.CircularUI.Forms.Renderer;
using Tizen.Wearable.CircularUI.Forms;

[assembly: ExportRenderer(typeof(CheckCell), typeof(CheckCellRenderer))]
namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class CheckCellRenderer : CellRenderer
    {
		readonly Dictionary<EvasObject, VisualElement> _cacheCandidate = new Dictionary<EvasObject, VisualElement>();

		protected CheckCellRenderer(string style) : base(style)
		{

		}

		public CheckCellRenderer() : this("1text.1icon.1")
		{
			MainPart = "elm.text";
			CheckBoxPart = "elm.icon";
		}

		protected string MainPart { get; set; }

		protected string CheckBoxPart { get; set; }

		protected override Span OnGetText(Cell cell, string part)
		{
			if (part == MainPart)
			{
				return new Span()
				{
					Text = (cell as CheckCell).Text
				};
			}
			return null;
		}

		protected override EvasObject OnGetContent(Cell cell, string part)
		{
			if (part == CheckBoxPart)
			{
				var check = new CheckBox()
				{
					BindingContext = cell,
					Parent = cell.Parent
				};
				check.SetBinding(CheckBox.IsCheckedProperty, new Binding(CheckCell.OnProperty.PropertyName));
				check.SetBinding(CheckBox.ColorProperty, new Binding(CheckCell.OnColorProperty.PropertyName));
				var nativeView = Platform.GetOrCreateRenderer(check).NativeView;
				nativeView.PropagateEvents = false;
				return nativeView;
			}
			return null;
		}

		protected override EvasObject OnReusableContent(Cell cell, string part, EvasObject old)
		{
			if (!_cacheCandidate.ContainsKey(old))
			{
				return null;
			}
			_cacheCandidate[old].BindingContext = cell;
			return old;
		}

		protected override bool OnCellPropertyChanged(Cell cell, string property, Dictionary<string, EvasObject> realizedView)
		{
			if (property == CheckCell.TextProperty.PropertyName || property == CheckCell.OnProperty.PropertyName || property == CheckCell.OnColorProperty.PropertyName)
			{
				return true;
			}
			return base.OnCellPropertyChanged(cell, property, realizedView);
		}
	}
}
