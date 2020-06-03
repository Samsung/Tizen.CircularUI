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
using Xamarin.Forms.Platform.Tizen;
using Tizen.Wearable.CircularUI.Forms.Renderer;
using Tizen.Wearable.CircularUI.Forms;

[assembly: ExportRenderer(typeof(SingleTextCell), typeof(SingleTextCellRenderer))]
namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class SingleTextCellRenderer : TextCellRenderer
    {
		protected SingleTextCellRenderer(string style) : base(style)
		{
		}

		public SingleTextCellRenderer() : this("1text")
		{
			MainPart = "elm.text";
		}

		protected override Span OnGetText(Cell cell, string part)
		{
			if (part == MainPart)
			{
				return OnMainText((SingleTextCell)cell);
			}
			return null;
		}
	}
}
