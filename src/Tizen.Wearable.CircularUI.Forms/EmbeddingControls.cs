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
	/// The default embedding controller of MediaPlayer
	/// </summary>
	public class EmbeddingControls : ContentView
	{
		/// <summary>
		/// Gets the Play image button
		/// </summary>
		public ImageButton PlayImage { get; private set; }

		/// <summary>
		/// Gets the Pause image button
		/// </summary>
		public ImageButton PauseImage { get; private set; }

		/// <summary>
		/// Initializes a new instance of the EmbeddingControls class.
		/// </summary>
		public EmbeddingControls()
		{
			PlayImage = new ImageButton
			{
				Source = ImageSource.FromResource("Tizen.Wearable.CircularUI.Forms.Resources.img_button_play.png", typeof(EmbeddingControls).Assembly),
				IsVisible = false
			};
			PlayImage.Clicked += OnImageButtonClicked;
			AbsoluteLayout.SetLayoutFlags(PlayImage, AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutBounds(PlayImage, new Rectangle(0.5, 0.5, 0.25, 0.25));

			PauseImage = new ImageButton
			{
				Source = ImageSource.FromResource("Tizen.Wearable.CircularUI.Forms.Resources.img_button_pause.png", typeof(EmbeddingControls).Assembly),
				IsVisible = false
			};
			PauseImage.Clicked += OnImageButtonClicked;
			AbsoluteLayout.SetLayoutFlags(PauseImage, AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutBounds(PauseImage, new Rectangle(0.5, 0.5, 0.25, 0.25));

			var bufferingLabel = new Label
			{
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label), false),
				HorizontalTextAlignment = TextAlignment.Center,
				TextColor = Color.FromHex("#eeeeeeee")
			};
			bufferingLabel.SetBinding(Label.TextProperty, new Binding
			{
				Path = "BufferingProgress",
				StringFormat = "{0:0%}"
			});
			bufferingLabel.SetBinding(IsVisibleProperty, new Binding
			{
				Path = "IsBuffering",
			});
			AbsoluteLayout.SetLayoutFlags(bufferingLabel, AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutBounds(bufferingLabel, new Rectangle(0.5, 0.5, 0.25, 0.25));

			var progressBoxView = new BoxView
			{
				Color = Color.FromHex($"#4286f4")
			};
			progressBoxView.SetBinding(AbsoluteLayout.LayoutBoundsProperty, new Binding
			{
				Path = "Progress",
				Converter = new ProgressToBoundTextConverter()
			});
			AbsoluteLayout.SetLayoutFlags(progressBoxView, AbsoluteLayoutFlags.All);

			var posLabel = new Label
			{
				Margin = new Thickness(10, 0, 0, 0),
				FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
				HorizontalTextAlignment = TextAlignment.Start
			};
			posLabel.SetBinding(Label.TextProperty, new Binding
			{
				Path = "Position",
				Converter = new MillisecondToTextConverter()
			});
			AbsoluteLayout.SetLayoutFlags(posLabel, AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutBounds(posLabel, new Rectangle(0, 0, 1, 1));

			var durationLabel = new Label
			{
				Margin = new Thickness(0, 0, 10, 0),
				FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
				HorizontalTextAlignment = TextAlignment.End
			};
			durationLabel.SetBinding(Label.TextProperty, new Binding
			{
				Path = "Duration",
				Converter = new MillisecondToTextConverter()
			});
			AbsoluteLayout.SetLayoutFlags(durationLabel, AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutBounds(durationLabel, new Rectangle(0, 0, 1, 1));

			var progressInnerLayout = new AbsoluteLayout
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				HeightRequest = 23,
				BackgroundColor = Color.FromHex("#80000000"),
				Children =
				{
					progressBoxView,
					posLabel,
					durationLabel
				}
			};

			var progressLayout = new StackLayout
			{
				Children =
				{
					new StackLayout { VerticalOptions = LayoutOptions.FillAndExpand },
					new StackLayout
					{
						Margin =  new Thickness(80, 0, 80, 0),
						VerticalOptions = LayoutOptions.End,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						BackgroundColor = Color.FromHex("#50000000"),
						Children = { progressInnerLayout }
					}
				}
			};
			AbsoluteLayout.SetLayoutFlags(progressLayout, AbsoluteLayoutFlags.All);
			AbsoluteLayout.SetLayoutBounds(progressLayout, new Rectangle(0, 0, 1, 1));

			Content = new AbsoluteLayout
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = {
					progressLayout,
					PlayImage,
					PauseImage,
					bufferingLabel
				}
			};
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			if (BindingContext is MediaPlayer player)
			{
				player.PlaybackPaused += OnPlaybackStateChanged;
				player.PlaybackStarted += OnPlaybackStateChanged;
				player.PlaybackStopped += OnPlaybackStateChanged;
			}
		}

		async void OnPlaybackStateChanged(object sender, EventArgs e)
		{
			if (BindingContext is MediaPlayer player)
			{
				if (player.State == PlaybackState.Playing)
				{
					var unused = PlayImage.FadeTo(0, 100);
					await PlayImage.ScaleTo(3.0, 300);
					PlayImage.IsVisible = false;
					PlayImage.Scale = 1.0;

					PauseImage.IsVisible = true;
					unused = PauseImage.FadeTo(1, 50);
				}
				else
				{
					var unused = PauseImage.FadeTo(0, 100);
					await PauseImage.ScaleTo(3.0, 300);
					PauseImage.IsVisible = false;
					PauseImage.Scale = 1.0;

					PlayImage.IsVisible = true;
					unused = PlayImage.FadeTo(1, 50);
				}
			}
		}

		async void OnImageButtonClicked(object sender, EventArgs e)
		{
			if (BindingContext is MediaPlayer player)
			{
				if (player.State == PlaybackState.Playing)
				{
					player.Pause();
				}
				else
				{
					await player.Start();
				}
			}
		}
	}
}