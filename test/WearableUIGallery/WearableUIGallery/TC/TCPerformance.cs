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
using WearableUIGallery.Extensions;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WearableUIGallery.TC
{
    public class MyViewCell : TextCell
    {
    }

    public class TCPerformance : ContentPage
    {
        const int TestItemMax = 2000;

        class Data
        {
            public string Label { get; set; }
            public Data(string str)
            {
                Label = str;
            }
        }

        IList<Data> _testData = new List<Data>();
        RenderCircleListView _listView;
        Button _startButton;
        bool _started;

        Stopwatch _stopWatch = new Stopwatch();
        Stopwatch _animationStopWatch = new Stopwatch();
        int _frameCount;
        int _animationCount;

        public TCPerformance()
        {
            InitializeListItem();

            int index = 0;
            _listView = new RenderCircleListView
            {
                ItemsSource = _testData,
                ItemTemplate = new DataTemplate(() =>
                {
                    var cell = new MyViewCell
                    {
                        Text = _testData[index++ % _testData.Count].Label
                    };
                    return cell;
                }),
            };
            _listView.RenderPost += OnRenderPost;


            _startButton = new Button
            {
                Text = "Start",
            };

            _startButton.Clicked += OnStarted;

            Content = new StackLayout
            {
                Children =
                {
                    _startButton,
                    _listView,
                }
            };
        }

        void OnRenderPost(object sender, EventArgs e)
        {
            if (_started)
            {
                if (_frameCount == 0)
                {
                    _stopWatch.Start();
                }
                _frameCount++;
            }
        }

        private void OnAnimation(object sender, EcoreAnimatorEventArgs e)
        {
            if (_started)
            {
                if (_animationCount == 0)
                {
                    _animationStopWatch.Start();
                }
                _animationCount++;
            }
        }

        async void OnStarted(object sender, EventArgs e)
        {
            TaskCompletionSource<bool> tcs = null;
            _startButton.IsEnabled = false;
            _listView.ScrollTo(_testData[0], ScrollToPosition.MakeVisible, false);
            await Task.Delay(100);

            var animator = DependencyService.Get<IEcoreAnimator>(DependencyFetchTarget.GlobalInstance);

            _started = true;
            _frameCount = 0;
            _animationCount = 0;
            object target = null;

            _stopWatch.Reset();
            _animationStopWatch.Reset();
            _listView.ItemAppearing += itemAppearing;
            animator.Animation += OnAnimation;

            for (int i = 10; i < 300; i += 20)
            {
                tcs = new TaskCompletionSource<bool>();
                target = _testData[i];
                _listView.ScrollTo(target, ScrollToPosition.MakeVisible, true);
                await tcs.Task;
            }

            _listView.ItemAppearing -= itemAppearing;
            animator.Animation -= OnAnimation;

            _stopWatch.Stop();
            _animationStopWatch.Stop();

            if (_stopWatch.ElapsedMilliseconds > 0)
            {
                double fps = (_frameCount * 1000.0) / _stopWatch.ElapsedMilliseconds;
                double aniFps = (_animationCount * 1000.0) / _animationStopWatch.ElapsedMilliseconds;
                _ = DisplayAlert("FPS", $"EvasFPS {fps:f2} fps \nAnimator FPS :  {aniFps:f2} fps", "OK");
            }

            _started = false;
            _startButton.IsEnabled = true;

            void itemAppearing(object s, ItemVisibilityEventArgs evt)
            {
                if (evt.Item == target)
                {
                    tcs.TrySetResult(true);
                }
            }
        }

        void InitializeListItem()
        {
            string[] arrLabel = {
                "Time Warner Cable(Cable)",
                "ComCast (Cable)",
                "Dish (Satellite)",
                "DirecTV (Satellite)",
                "Tata Sky (Satellite)",
                "Nextra Cable(Cable)",
                "DD Plus (Cable)",
                "Tikona Cable(Cable)",
                "True Provider (Cable)",
                "Vodafone (Satellite)",
                "Sample Text"
            };

            for (int i = 0; i < TestItemMax; ++i)
            {
                _testData.Add(new Data(arrLabel[i % 10]));
            }
        }
    }
}
