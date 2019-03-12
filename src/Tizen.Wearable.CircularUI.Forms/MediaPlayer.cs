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
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// MediaPlayer provieds the essential components to play the media contents.
    /// </summary>
    public class MediaPlayer : Element
    {
        /// <summary>
        /// Identifies the Source bindable property.
        /// </summary>
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(MediaSource), typeof(MediaPlayer), default(MediaSource), propertyChanging: OnSourceChanging, propertyChanged: OnSourceChanged);
        /// <summary>
        /// Identifies the VideoOutput bindable property.
        /// </summary>
        public static readonly BindableProperty VideoOutputProperty = BindableProperty.Create(nameof(VideoOutput), typeof(IVideoOutput), typeof(MediaPlayer), null, propertyChanging: null, propertyChanged: (b, o, n) => ((MediaPlayer)b).OnVideoOutputChanged());
        /// <summary>
        /// Identifies the UsesEmbeddingControls bindable property.
        /// </summary>
        public static readonly BindableProperty UsesEmbeddingControlsProperty = BindableProperty.Create(nameof(UsesEmbeddingControls), typeof(bool), typeof(MediaPlayer), true, propertyChanged: (b, o, n) => ((MediaPlayer)b).OnUsesEmbeddingControlsChanged());
        /// <summary>
        /// Identifies the Volume bindable property.
        /// </summary>
        public static readonly BindableProperty VolumeProperty = BindableProperty.Create(nameof(Volume), typeof(double), typeof(MediaPlayer), 1d, coerceValue: (bindable, value) => ((double)value).Clamp(0, 1), propertyChanged: (b, o, n)=> ((MediaPlayer)b).OnVolumeChanged());
        /// <summary>
        /// Identifies the IsMuted bindable property.
        /// </summary>
        public static readonly BindableProperty IsMutedProperty = BindableProperty.Create(nameof(IsMuted), typeof(bool), typeof(MediaPlayer), false, propertyChanged: (b, o, n) => ((MediaPlayer)b).UpdateIsMuted());
        /// <summary>
        /// Identifies the AspectMode bindable property.
        /// </summary>
        public static readonly BindableProperty AspectModeProperty = BindableProperty.Create(nameof(AspectMode), typeof(DisplayAspectMode), typeof(MediaPlayer), DisplayAspectMode.AspectFit, propertyChanged: (b, o, n) => ((MediaPlayer)b).OnAspectModeChanged());
        /// <summary>
        /// Identifies the AutoPlay bindable property.
        /// </summary>
        public static readonly BindableProperty AutoPlayProperty = BindableProperty.Create(nameof(AutoPlay), typeof(bool), typeof(MediaPlayer), false, propertyChanged: (b, o, n) => ((MediaPlayer)b).UpdateAutoPlay());
        /// <summary>
        /// Identifies the AutoStop bindable property.
        /// </summary>
        public static readonly BindableProperty AutoStopProperty = BindableProperty.Create(nameof(AutoStop), typeof(bool), typeof(MediaPlayer), true, propertyChanged: (b, o, n) => ((MediaPlayer)b).UpdateAutoStop());
        static readonly BindablePropertyKey DurationPropertyKey = BindableProperty.CreateReadOnly(nameof(Duration), typeof(int), typeof(MediaPlayer), 0);
        /// <summary>
        /// Identifies the Duration bindable property.
        /// </summary>
        public static readonly BindableProperty DurationProperty = DurationPropertyKey.BindableProperty;
        static readonly BindablePropertyKey BufferingProgressPropertyKey = BindableProperty.CreateReadOnly(nameof(BufferingProgress), typeof(double), typeof(MediaPlayer), 0d);
        /// <summary>
        /// Identifies the BufferingProgress bindable property.
        /// </summary>
        public static readonly BindableProperty BufferingProgressProperty = BufferingProgressPropertyKey.BindableProperty;
        static readonly BindablePropertyKey PositionPropertyKey = BindableProperty.CreateReadOnly(nameof(Position), typeof(int), typeof(MediaPlayer), 0);
        /// <summary>
        /// Identifies the Position bindable property.
        /// </summary>
        public static readonly BindableProperty PositionProperty = PositionPropertyKey.BindableProperty;
        static readonly BindablePropertyKey StatePropertyKey = BindableProperty.CreateReadOnly(nameof(State), typeof(PlaybackState), typeof(MediaPlayer), PlaybackState.Stopped);
        /// <summary>
        /// Identifies the State bindable property.
        /// </summary>
        public static readonly BindableProperty StateProperty = StatePropertyKey.BindableProperty;
        /// <summary>
        /// Identifies the PositionUpdateInterval bindable property.
        /// </summary>
        public static readonly BindableProperty PositionUpdateIntervalProperty = BindableProperty.Create(nameof(PositionUpdateInterval), typeof(int), typeof(MediaPlayer), 500);
        static readonly BindablePropertyKey IsBufferingPropertyKey = BindableProperty.CreateReadOnly(nameof(IsBuffering), typeof(bool), typeof(MediaPlayer), false);
        /// <summary>
        /// Identifies the IsBuffering bindable property.
        /// </summary>
        public static readonly BindableProperty IsBufferingProperty = IsBufferingPropertyKey.BindableProperty;

        IPlatformMediaPlayer _impl;
        bool _isPlaying;
        bool _controlsAlwaysVisible;
        CancellationTokenSource _hideTimerCTS = new CancellationTokenSource();
        Lazy<View> _controls;

        /// <summary>
        /// Initializes a new instance of the MediaPlayer class.
        /// </summary>
        public MediaPlayer()
        {
            _impl = DependencyService.Get<IPlatformMediaPlayer>(fetchTarget: DependencyFetchTarget.NewInstance);
            _impl.UpdateStreamInfo += OnUpdateStreamInfo;
            _impl.PlaybackCompleted += SendPlaybackCompleted;
            _impl.PlaybackStarted += SendPlaybackStarted;
            _impl.PlaybackPaused += SendPlaybackPaused;
            _impl.PlaybackStopped += SendPlaybackStopped;
            _impl.BufferingProgressUpdated += OnUpdateBufferingProgress;
            _impl.UsesEmbeddingControls = true;
            _impl.Volume = 1d;
            _impl.AspectMode = DisplayAspectMode.AspectFit;
            _impl.AutoPlay = false;
            _impl.AutoStop = true;

            _controlsAlwaysVisible = false;
            _controls = new Lazy<View>(() =>
            {
                return new EmbeddingControls
                {
                    BindingContext = this
                };
            });
        }

        /// <summary>
        /// Gets or sets the scaling mode for the media content.
        /// </summary>
        public DisplayAspectMode AspectMode
        {
            get { return (DisplayAspectMode)GetValue(AspectModeProperty); }
            set { SetValue(AspectModeProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value whether the media content plays automatically.
        /// </summary>
        public bool AutoPlay
        {
            get
            {
                return (bool)GetValue(AutoPlayProperty);
            }
            set
            {
                SetValue(AutoPlayProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value whether the media content stops automatically.
        /// </summary>
        public bool AutoStop
        {
            get
            {
                return (bool)GetValue(AutoStopProperty);
            }
            set
            {
                SetValue(AutoStopProperty, value);
            }
        }

        /// <summary>
        /// Gets the value indicating the buffering percentage.
        /// </summary>
        public double BufferingProgress
        {
            get
            {
                return (double)GetValue(BufferingProgressProperty);
            }
            private set
            {
                SetValue(BufferingProgressPropertyKey, value);
            }
        }

        /// <summary>
        /// Gets the duration of a media content.
        /// </summary>
        public int Duration
        {
            get
            {
                return (int)GetValue(DurationProperty);
            }
            private set
            {
                SetValue(DurationPropertyKey, value);
            }
        }

        /// <summary>
        /// Gets or sets the source of the media content.
        /// </summary>
        [Xamarin.Forms.TypeConverter(typeof(MediaSourceConverter))]
        public MediaSource Source
        {
            get { return (MediaSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the video output.
        /// </summary>
        public IVideoOutput VideoOutput
        {
            get { return (IVideoOutput)GetValue(VideoOutputProperty); }
            set { SetValue(VideoOutputProperty, value); }
        }

        /// <summary>
        /// Gets or sets the current volume of a media content.
        /// </summary>
        public double Volume
        {
            get { return (double)GetValue(VolumeProperty); }
            set { SetValue(VolumeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value whether the volume is muted.
        /// </summary>
        public bool IsMuted
        {
            get { return (bool)GetValue(IsMutedProperty); }
            set { SetValue(IsMutedProperty, value); }
        }

        /// <summary>
        /// Gets or sets the desired interval time for updating position.
        /// </summary>
        public int PositionUpdateInterval
        {
            get { return (int)GetValue(PositionUpdateIntervalProperty); }
            set { SetValue(PositionUpdateIntervalProperty, value); }
        }

        /// <summary>
        /// Gets or sets whether to use the embedding controls.
        /// </summary>
        public bool UsesEmbeddingControls
        {
            get
            {
                return (bool)GetValue(UsesEmbeddingControlsProperty);
            }
            set
            {
                SetValue(UsesEmbeddingControlsProperty, value);
                _impl.UsesEmbeddingControls = value;
            }
        }

        /// <summary>
        /// Gets the value of the current position of the media content.
        /// </summary>
        public int Position
        {
            get
            {
                return _impl.Position;
            }
            private set
            {
                SetValue(PositionPropertyKey, value);
                OnPropertyChanged(nameof(Progress));
            }
        }

        /// <summary>
        /// Gets the current playback state.
        /// </summary>
        public PlaybackState State
        {
            get
            {
                return (PlaybackState)GetValue(StateProperty);
            }
            private set
            {
                SetValue(StatePropertyKey, value);
            }
        }

        /// <summary>
        /// Gets a value indicating the buffering status. 
        /// </summary>
        public bool IsBuffering
        {
            get
            {
                return (bool)GetValue(IsBufferingProperty);
            }
            private set
            {
                SetValue(IsBufferingPropertyKey, value);
            }
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public double Progress
        {
            get
            {
                return Position / (double)Math.Max(Position, Duration);
            }
        }

        /// <summary>
        /// For internal use.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Command StartCommand => new Command(() =>
        {
            if (State == PlaybackState.Playing)
            {
                Pause();
            }
            else
            {
                Start();
            }
        });

        /// <summary>
        /// For internal use.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Command FastForwardCommand => new Command(() =>
        {
            if (State != PlaybackState.Stopped)
            {
                Seek(Math.Min(Position + 5000, Duration));
            }
        }, () => State != PlaybackState.Stopped);

        /// <summary>
        /// For internal use.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Command RewindCommand => new Command(() =>
        {
            if (State != PlaybackState.Stopped)
            {
                Seek(Math.Max(Position - 5000, 0));
            }
        }, () => State != PlaybackState.Stopped);

        /// <summary>
        /// Occurs when the playback is completed.
        /// </summary>
        public event EventHandler PlaybackCompleted;
        /// <summary>
        /// Occurs when the playback is started.
        /// </summary>
        public event EventHandler PlaybackStarted;
        /// <summary>
        /// Occurs when the playback is paused.
        /// </summary>
        public event EventHandler PlaybackPaused;
        /// <summary>
        /// Occurs when the playback is stopped.
        /// </summary>
        public event EventHandler PlaybackStopped;
        /// <summary>
        /// Occurs when the buffering for the media content is started.
        /// </summary>
        public event EventHandler BufferingStarted;
        /// <summary>
        /// Occurs when the buffering for the media content is completed.
        /// </summary>
        public event EventHandler BufferingCompleted;

        /// <summary>
        /// Pauses the player.
        /// </summary>
        public void Pause()
        {
            _impl.Pause();
        }

        /// <summary>
        /// Attemps to seek the playback position.
        /// </summary>
        /// <param name="ms">The milliseconds to seek</param>
        /// <returns>Returns a Task that seeks the play position.</returns>
        public Task<int> Seek(int ms)
        {
            ShowController();
            return _impl.Seek(ms).ContinueWith((t) => Position = _impl.Position);
        }

        /// <summary>
        /// Starts or resumes playback.
        /// </summary>
        /// <returns>Returns a Task that prepares the player and play the media content.</returns>
        public Task<bool> Start()
        {
            return _impl.Start();
        }

        /// <summary>
        /// Stops playing the media content.
        /// </summary>
        public void Stop()
        {
            _impl.Stop();
        }

        /// <summary>
        /// Retrieves the album art of the stream, or null if there is no album art data.
        /// </summary>
        /// <returns>Returns a Task that gets the album art of the stream</returns>
        public Task<Stream> GetAlbumArts()
        {
            return _impl.GetAlbumArts();
        }

        /// <summary>
        /// Gets the metadata of the media content.
        /// </summary>
        /// <returns>Returns a Task that has the metadata of the media content.</returns>
        public Task<IDictionary<string, string>> GetMetadata()
        {
            return _impl.GetMetadata();
        }

        void UpdateAutoPlay()
        {
            _impl.AutoPlay = AutoPlay;
        }

        void UpdateAutoStop()
        {
            _impl.AutoStop = AutoStop;
        }

        void UpdateIsMuted()
        {
            _impl.IsMuted = IsMuted;
        }

        void OnUpdateStreamInfo(object sender, EventArgs e)
        {
            Duration = _impl.Duration;
        }

        void SendPlaybackCompleted(object sender, EventArgs e)
        {
            PlaybackCompleted?.Invoke(this, EventArgs.Empty);
        }

        void SendPlaybackStarted(object sender, EventArgs e)
        {
            _isPlaying = true;
            State = PlaybackState.Playing;
            StartPostionPollingTimer();
            PlaybackStarted?.Invoke(this, EventArgs.Empty);
            _controlsAlwaysVisible = false;
            ShowController();
        }

        void SendPlaybackPaused(object sender, EventArgs e)
        {
            _isPlaying = false;
            State = PlaybackState.Paused;
            PlaybackPaused?.Invoke(this, EventArgs.Empty);
            _controlsAlwaysVisible = true;
            ShowController();
        }


        void SendPlaybackStopped(object sender, EventArgs e)
        {
            _isPlaying = false;
            State = PlaybackState.Stopped;
            PlaybackStopped?.Invoke(this, EventArgs.Empty);
            _controlsAlwaysVisible = true;
            ShowController();
        }


        void StartPostionPollingTimer()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(PositionUpdateInterval), () =>
            {
                Position = _impl.Position;
                return _isPlaying;
            });
        }

        void OnSourceChanged(object sender, EventArgs e)
        {
            _impl.SetSource(Source);
        }

        void OnSourceChanging(MediaSource oldValue, MediaSource newValue)
        {
            if (oldValue != null)
                oldValue.SourceChanged -= OnSourceChanged;

            if (newValue != null)
                newValue.SourceChanged += OnSourceChanged;
        }

        void OnVideoOutputChanged()
        {
            if (VideoOutput != null)
            {
                if (UsesEmbeddingControls)
                {
                    VideoOutput.Controller = _controls.Value;
                }
                VideoOutput.MediaView.Focused += OnVideoOutputFocused;
                if (VideoOutput.MediaView is View outputView)
                {
                    TapGestureRecognizer tapGesture = new TapGestureRecognizer();
                    tapGesture.Tapped += OnOutputTapped;
                    outputView.GestureRecognizers.Add(tapGesture);
                }
            }
            _impl.SetDisplay(VideoOutput);
        }

        void OnOutputTapped(object sender, EventArgs e)
        {
            if (!UsesEmbeddingControls)
                return;
            if (!_controls.Value.IsVisible)
            {
                ShowController();
            }
        }

        async void OnUsesEmbeddingControlsChanged()
        {
            if (UsesEmbeddingControls)
            {
                if (VideoOutput != null)
                {
                    VideoOutput.Controller = _controls.Value;
                    ShowController();
                }
            }
            else
            {
                if (VideoOutput != null)
                {
                    HideController(0);
                    await Task.Delay(200);
                    VideoOutput.Controller = null;
                }
            }
        }

        void OnVideoOutputFocused(object sender, FocusEventArgs e)
        {
            if (UsesEmbeddingControls)
            {
                ShowController();
            }
        }

        void OnVolumeChanged()
        {
            _impl.Volume = Volume;
        }

        void OnAspectModeChanged()
        {
            _impl.AspectMode = AspectMode;
        }

        void OnUpdateBufferingProgress(object sender, BufferingProgressUpdatedEventArgs e)
        {
            if (!IsBuffering && e.Progress >= 0)
            {
                IsBuffering = true;
                BufferingStarted?.Invoke(this, EventArgs.Empty);
            }
            else if (IsBuffering && e.Progress == 1.0)
            {
                IsBuffering = false;
                BufferingCompleted?.Invoke(this, EventArgs.Empty);
            }
            BufferingProgress = e.Progress;
        }

        async void HideController(int after)
        {
            if (!_controls.IsValueCreated)
                return;

            _hideTimerCTS?.Cancel();
            _hideTimerCTS?.Dispose();
            _hideTimerCTS = new CancellationTokenSource();
            try
            {
                await Task.Delay(after, _hideTimerCTS.Token);

                if (!_controlsAlwaysVisible)
                {
                    await _controls.Value.FadeTo(0, 200);
                    _controls.Value.IsVisible = false;
                }
            }
            catch (Exception)
            {
                //Exception when canceled
            }
        }
        
        void ShowController()
        {
            if (_controls.IsValueCreated)
            {
                _controls.Value.IsVisible = true;
                _controls.Value.FadeTo(1.0, 200);
                HideController(5000);
            }
        }


        static void OnSourceChanging(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as MediaPlayer)?.OnSourceChanging(oldValue as MediaSource, newValue as MediaSource);
        }
        static void OnSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as MediaPlayer)?.OnSourceChanged(bindable, EventArgs.Empty);
        }
    }
}
