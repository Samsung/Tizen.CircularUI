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
using ElmSharp.Wearable;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native.Watch;
using XPageRenderer = Xamarin.Forms.Platform.Tizen.PageRenderer;
using CPageRenderer = Tizen.Wearable.CircularUI.Forms.Renderer.PageRenderer;
using EColor = ElmSharp.Color;
using NPage = Xamarin.Forms.Platform.Tizen.Native.Page;
using NLayoutEventArgs = Xamarin.Forms.Platform.Tizen.Native.LayoutEventArgs;
using XForms = Xamarin.Forms.Forms;
using System.Collections.Specialized;

[assembly: ExportRenderer(typeof(Page), typeof(CPageRenderer))]
namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
	// TODO: need to change the implementation to inhert the PageRenderer later
	public class PageRenderer : VisualElementRenderer<Page>
	{
		NPage _page;
		Lazy<MoreOption> _moreOption;

		public PageRenderer()
		{
			RegisterPropertyHandler(Page.BackgroundImageSourceProperty, UpdateBackgroundImage);
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
		{
			if (null == _page)
			{
				_page = new NPage(XForms.NativeParent);
				_page.LayoutUpdated += OnLayoutUpdated;
				SetNativeView(_page);
			}
			base.OnElementChanged(e);
		}

		protected override void OnElementReady()
		{
			_moreOption = new Lazy<MoreOption>(CreateMoreOption);
			if (Element.ToolbarItems is INotifyCollectionChanged items)
			{
				items.CollectionChanged += OnToolbarCollectionChanged;
			}
			if (Element.ToolbarItems.Count > 0)
			{
				UpdateToolbarItems(true);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_page != null)
				{
					_page.LayoutUpdated -= OnLayoutUpdated;
				}

				if (Element.ToolbarItems is INotifyCollectionChanged items)
				{
					items.CollectionChanged -= OnToolbarCollectionChanged;
				}

				if (_moreOption.IsValueCreated)
				{
					_moreOption.Value.Clicked -= OnMoreOptionItemClicked;
					_moreOption.Value.Closed -= SendMoreOptionClosed;
					_moreOption.Value.Opened -= SendMoreOptionOpened;
					_moreOption.Value.Items.Clear();
					_moreOption.Value.Unrealize();
				}
			}
			base.Dispose(disposing);
		}

		protected override void UpdateBackgroundColor(bool initialize)
		{
			if (initialize && Element.BackgroundColor.IsDefault)
				return;

			// base.UpdateBackgroundColor() is not called on purpose, we don't want the regular background setting
			if (Element.BackgroundColor.IsDefault || Element.BackgroundColor.A == 0)
				_page.Color = EColor.Transparent;
			else
				_page.Color = Element.BackgroundColor.ToNative();
		}

		protected override void UpdateLayout()
		{
			// empty on purpose
		}

		protected virtual FormsMoreOptionItem CreateMoreOptionItem(ToolbarItem item)
		{
			var moreOptionItem = new FormsMoreOptionItem
			{
				MainText = item.Text,
				ToolbarItem = item
			};
			var icon = item.IconImageSource as FileImageSource;
			if (icon != null)
			{
				var img = new ElmSharp.Image(_moreOption.Value);
				img.Load(ResourcePath.GetPath(icon));
				moreOptionItem.Icon = img;
			}
			if (item is CircleToolbarItem circleToolbarItem)
			{
				moreOptionItem.SubText = circleToolbarItem.SubText;
			}
			return moreOptionItem;
		}

		protected virtual void OnMoreOptionClosed()
		{
		}

		protected virtual void OnMoreOptionOpened()
		{
		}

		void UpdateBackgroundImage(bool initialize)
		{
			if (initialize && Element.BackgroundImageSource.IsNullOrEmpty())
				return;

			// TODO: investigate if we can use the other image source types: stream, font, uri

			var bgImage = Element.BackgroundImageSource as FileImageSource;
			if (bgImage.IsNullOrEmpty())
				_page.File = null;
			else
				_page.File = ResourcePath.GetPath(bgImage);
		}

		void OnLayoutUpdated(object sender, NLayoutEventArgs e)
		{
			Element.Layout(e.Geometry.ToDP());

			if (_moreOption != null && _moreOption.IsValueCreated)
			{
				_moreOption.Value.Geometry = _page.Geometry;
			}
		}

		MoreOption CreateMoreOption()
		{
			var moreOption = new MoreOption(_page);
			moreOption.Geometry = _page.Geometry;
			_page.Children.Add(moreOption);
			moreOption.Show();
			moreOption.Clicked += OnMoreOptionItemClicked;
			moreOption.Closed += SendMoreOptionClosed;
			moreOption.Opened += SendMoreOptionOpened;
			return moreOption;
		}

		void SendMoreOptionClosed(object sender, EventArgs e)
		{
			OnMoreOptionClosed();
		}

		void SendMoreOptionOpened(object sender, EventArgs e)
		{
			OnMoreOptionOpened();
		}

		void OnToolbarCollectionChanged(object sender, EventArgs eventArgs)
		{
				UpdateToolbarItems(false);
		}

		void UpdateToolbarItems(bool initialize)
		{
			//clear existing more option items and add toolbar item again on purpose.
			if (!initialize && _moreOption.Value.Items.Count > 0)
			{
				_moreOption.Value.Items.Clear();
			}

			if (Element.ToolbarItems.Count > 0)
			{
				_moreOption.Value.Show();
				foreach (var item in Element.ToolbarItems)
				{
					_moreOption.Value.Items.Add(CreateMoreOptionItem(item));
				}
			}
			else
			{
				_moreOption.Value.Hide();
			}
		}

		void OnMoreOptionItemClicked(object sender, MoreOptionItemEventArgs e)
		{
			var formsMoreOptionItem = e.Item as FormsMoreOptionItem;
			if (formsMoreOptionItem != null)
			{
				((IMenuItemController)formsMoreOptionItem.ToolbarItem)?.Activate();
			}
			_moreOption.Value.IsOpened = false;
		}
	}
}
