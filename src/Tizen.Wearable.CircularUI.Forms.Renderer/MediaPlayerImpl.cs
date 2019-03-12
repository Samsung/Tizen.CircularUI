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
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tizen.Multimedia;
using Tizen.Wearable.CircularUI.Forms.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Tizen;

[assembly: Xamarin.Forms.Dependency(typeof(MediaPlayerImpl))]
namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class MediaPlayerImpl : IPlatformMediaPlayer
    {
        Player _player;

        bool _cancelToStart;
        DisplayAspectMode _aspectMode = DisplayAspectMode.AspectFit;
        Task _taskPrepare;
        TaskCompletionSource<bool> _tcsForStreamInfo;
        IVideoOutput _videoOutput;
        MediaSource _source;

        public MediaPlayerImpl()
        {
            _player = new Player();
            _player.PlaybackCompleted += OnPlaybackCompleted;
            _player.BufferingProgressChanged += OnBufferingProgressChanged;
        }

        public bool UsesEmbeddingControls
        {
            get; set;
        }

        public bool AutoPlay { get; set; }

        public bool AutoStop { get; set; }

        public double Volume
        {
            get => _player.Volume;
            set => _player.Volume = (float)value;
        }

        public int Duration => _player.StreamInfo.GetDuration();

        public bool IsMuted
        {
            get => _player.Muted;
            set => _player.Muted = value;
        }

        public int Position
        {
            get
            {
                if (_player.State == PlayerState.Idle || _player.State == PlayerState.Preparing)
                    return 0;
                return _player.GetPlayPosition();
            }
        }

        public DisplayAspectMode AspectMode
        {
            get { return _aspectMode; }
            set
            {
                _aspectMode = value;
                ApplyAspectMode();
            }
        }

        bool HasSource => _source != null;

        IVideoOutput VideoOutput
        {
            get { return _videoOutput; }
            set
            {
                if (TargetView != null)
                    TargetView.PropertyChanged -= OnTargetViewPropertyChanged;

                _videoOutput = value;

                if (TargetView != null)
                    TargetView.PropertyChanged += OnTargetViewPropertyChanged;
            }
        }

        VisualElement TargetView => VideoOutput?.MediaView;

        Task TaskPrepare
        {
            get => _taskPrepare ?? Task.CompletedTask;
            set => _taskPrepare = value;
        }

        public event EventHandler UpdateStreamInfo;
        public event EventHandler PlaybackCompleted;
        public event EventHandler PlaybackStarted;
        public event EventHandler<BufferingProgressUpdatedEventArgs> BufferingProgressUpdated;
        public event EventHandler PlaybackStopped;
        public event EventHandler PlaybackPaused;


        public async Task<bool> Start()
        {
            Log.Debug(FormsCircularUI.Tag, "Start");

            _cancelToStart = false;
            if (!HasSource)
                return false;

            if (_player.State == PlayerState.Idle)
            {
                await Prepare();
            }

            if (_cancelToStart)
                return false;

            try
            {
                _player.Start();
            }
            catch (Exception e)
            {
                Log.Error(FormsCircularUI.Tag, $"Error On Start : {e.Message}");
                return false;
            }
            PlaybackStarted?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public void Pause()
        {
            Log.Debug(FormsCircularUI.Tag, "Pause");

            try
            {
                _player.Pause();
                PlaybackPaused.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Log.Error(FormsCircularUI.Tag, $"Error on Pause : {e.Message}");
            }
        }

        public void Stop()
        {
            Log.Debug(FormsCircularUI.Tag, "Stop");

            _cancelToStart = true;
            var unusedTask = ChangeToIdleState();
            PlaybackStopped.Invoke(this, EventArgs.Empty);
        }

        public void SetDisplay(IVideoOutput output)
        {
            VideoOutput = output;
        }

        public async Task<int> Seek(int ms)
        {
            try
            {
                await _player.SetPlayPositionAsync(ms, false);
            }
            catch (Exception e)
            {
                Log.Error(FormsCircularUI.Tag, $"Fail to seek : {e.Message}");
            }
            return Position;
        }

        public void SetSource(MediaSource source)
        {
            _source = source;
        }

        public async Task<Stream> GetAlbumArts()
        {
            if (_player.State == PlayerState.Idle)
            {
                if (_tcsForStreamInfo == null || _tcsForStreamInfo.Task.IsCompleted)
                {
                    _tcsForStreamInfo = new TaskCompletionSource<bool>();
                }
                await _tcsForStreamInfo.Task;
            }
            await TaskPrepare;

            var imageData = _player.StreamInfo.GetAlbumArt();
            if (imageData == null)
                return null;
            return new MemoryStream(imageData);
        }

        public async Task<IDictionary<string, string>> GetMetadata()
        {
            if (_player.State == PlayerState.Idle)
            {
                if (_tcsForStreamInfo == null || _tcsForStreamInfo.Task.IsCompleted)
                {
                    _tcsForStreamInfo = new TaskCompletionSource<bool>();
                }
                await _tcsForStreamInfo.Task;
            }
            await TaskPrepare;

            Dictionary<string, string> metadata = new Dictionary<string, string>
            {
                [nameof(StreamMetadataKey.Album)] = _player.StreamInfo.GetMetadata(StreamMetadataKey.Album),
                [nameof(StreamMetadataKey.Artist)] = _player.StreamInfo.GetMetadata(StreamMetadataKey.Artist),
                [nameof(StreamMetadataKey.Author)] = _player.StreamInfo.GetMetadata(StreamMetadataKey.Author),
                [nameof(StreamMetadataKey.Genre)] = _player.StreamInfo.GetMetadata(StreamMetadataKey.Genre),
                [nameof(StreamMetadataKey.Title)] = _player.StreamInfo.GetMetadata(StreamMetadataKey.Title),
                [nameof(StreamMetadataKey.Year)] = _player.StreamInfo.GetMetadata(StreamMetadataKey.Year)
            };
            return metadata;
        }

        void ApplyDisplay()
        {
            if (VideoOutput == null)
            {
                _player.Display = null;
            }
            else
            {
                var renderer = Platform.GetRenderer(TargetView);
                if (renderer is IMediaViewProvider provider && provider.GetMediaView() != null)
                {
                    try
                    {
                        Display display = new Display(provider.GetMediaView());
                        _player.Display = display;
                        _player.DisplaySettings.Mode = _aspectMode.ToMultimeida();
                    }
                    catch
                    {
                        Log.Error(FormsCircularUI.Tag, "Error on MediaView");
                    }
                }
            }
        }

        async Task ApplySource()
        {
            if (_source == null)
            {
                return;
            }
            IMediaSourceHandler handler = Registrar.Registered.GetHandlerForObject<IMediaSourceHandler>(_source);
            await handler.SetSource(_player, _source);
        }

        async void OnTargetViewPropertyChanged(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Renderer")
            {
                if (Platform.GetRenderer(sender as BindableObject) != null && HasSource && AutoPlay)
                {
                    await Start();
                }
                else if (Platform.GetRenderer(sender as BindableObject) == null && AutoStop)
                {
                    Stop();
                }
            }
        }

        async Task Prepare()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            var prevTask = TaskPrepare;
            TaskPrepare = tcs.Task;
            await prevTask;

            if (_player.State == PlayerState.Ready)
                return;

            ApplyDisplay();
            await ApplySource();

            try {
                await _player.PrepareAsync();
                UpdateStreamInfo?.Invoke(this, EventArgs.Empty);
                _tcsForStreamInfo?.TrySetResult(true);
            }
            catch (Exception e)
            {
                Log.Error(FormsCircularUI.Tag, $"Error on prepare : {e.Message}");
            }
            tcs.SetResult(true);
        }

        async void ApplyAspectMode()
        {
            if (_player.State == PlayerState.Preparing)
            {
                await TaskPrepare;
            }
            _player.DisplaySettings.Mode = AspectMode.ToMultimeida();
        }

        void OnBufferingProgressChanged(object sender, BufferingProgressChangedEventArgs e)
        {
            BufferingProgressUpdated?.Invoke(this, new BufferingProgressUpdatedEventArgs { Progress = e.Percent / 100.0 });
        }

        void OnPlaybackCompleted(object sender, EventArgs e)
        {
            PlaybackCompleted?.Invoke(this, EventArgs.Empty);
        }

        async Task ChangeToIdleState()
        {
            switch (_player.State)
            {
                case PlayerState.Playing:
                case PlayerState.Paused:
                    _player.Stop();
                    _player.Unprepare();
                    break;
                case PlayerState.Ready:
                    _player.Unprepare();
                    break;
                case PlayerState.Preparing:
                    await TaskPrepare;
                    _player.Unprepare();
                    break;
            }
        }
    }

    public static class MultimediaConvertExtensions
    {
        public static Multimedia.Rectangle ToMultimedia(this ElmSharp.Rect rect)
        {
            return new Multimedia.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static PlayerDisplayMode ToMultimeida(this DisplayAspectMode mode)
        {
            PlayerDisplayMode ret = PlayerDisplayMode.LetterBox;
            switch (mode)
            {
                case DisplayAspectMode.AspectFill:
                    ret = PlayerDisplayMode.CroppedFull;
                    break;
                case DisplayAspectMode.AspectFit:
                    ret = PlayerDisplayMode.LetterBox;
                    break;
                case DisplayAspectMode.Fill:
                    ret = PlayerDisplayMode.FullScreen;
                    break;
                case DisplayAspectMode.OrignalSize:
                    ret = PlayerDisplayMode.OriginalOrFull;
                    break;
            }
            return ret;
        }
    }
}
