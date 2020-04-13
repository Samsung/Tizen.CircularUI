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
using Tizen.Wearable.CircularUI.Forms.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;
using XMediaPlayerImpl = Xamarin.Forms.Platform.Tizen.Native.MediaPlayerImpl;
using PlayerState = Xamarin.Forms.PlatformConfiguration.TizenSpecific.PlaybackState;

[assembly: Dependency(typeof(MediaPlayerImpl))]
namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    //TODO : This class sholud be removed when the related bug in Xamarin.Forms is fixed (#10287).
    public class EmbeddingControls2 : Xamarin.Forms.Platform.Tizen.Native.EmbeddingControls
    {
        public EmbeddingControls2() : base()
        {
            PlayImage.Clicked += OnImageButtonClicked;
            PauseImage.Clicked += OnImageButtonClicked;
        }

        async void OnImageButtonClicked(object sender, EventArgs e)
        {
            if (BindingContext is MediaPlayer player)
            {
                if (player.State == PlayerState.Playing)
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

    //TODO : This class sholud be removed when the related bug in Xamarin.Forms is fixed (#10287).
    public class MediaPlayerImpl : XMediaPlayerImpl, IPlatformMediaPlayer2
    {
        public MediaPlayerImpl() : base()
        {
        }

        public new View GetEmbeddingControlView(IMediaPlayer player)
        {
            return new EmbeddingControls2
            {
                BindingContext = player
            };
        }
    }
}
