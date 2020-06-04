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
using System;

[assembly: ExportRenderer(typeof(RadioCell), typeof(RadioCellRenderer))]
namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class RadioCellRenderer : CellRenderer
    {
		protected RadioCellRenderer(string style) : base(style)
		{

		}

		public RadioCellRenderer() : this("1text.1icon.1")
		{
			MainPart = "elm.text";
			RadioPart = "elm.icon";
		}

		protected string MainPart { get; set; }
		protected string RadioPart { get; set; }

		protected override Span OnGetText(Cell cell, string part)
		{
			if (part == MainPart)
			{
				return new Span()
				{
					Text = (cell as RadioCell).Text
				};
			}
			return null;
		}

		protected override EvasObject OnGetContent(Cell cell, string part)
		{
			if (part == RadioPart)
			{
				var nativeView = Platform.GetOrCreateRenderer(((RadioCell)cell).RadioButton).NativeView;
				nativeView.PropagateEvents = false;
				return nativeView;
			}
			return null;
		}

		protected override bool OnCellPropertyChanged(Cell cell, string property, Dictionary<string, EvasObject> realizedView)
		{
			if (property == RadioCell.TextProperty.PropertyName || property == RadioCell.OnProperty.PropertyName || property == RadioCell.GroupNameProperty.PropertyName)
			{
				return true;
			}
			return base.OnCellPropertyChanged(cell, property, realizedView);
		}
	}
}
