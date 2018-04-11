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
using CircularUI;

namespace WearableUIGallery.TC
{
    public class MyViewCell : TextCell
    {
    }

    public class TCPerformance : CirclePage
    {
        const int TestItemMax = 2000;
        const double TimeSet = 5.0;

        class Data
        {
            public string Label { get; set; }
            public Data(string str)
            {
                Label = str;
            }
        }

        InformationPopup _popUp = null;

        IList<Data> _testData = new List<Data>();
        RenderCircleListView _listView;
        IGlobalScrollConfig GlobalScrollConfig = null;
        IEcoreAnimator EcoreAnimator = null;

        double _enteringSpeed = 0.0;
        double _startTime = 0.0;
        int _frameCount = 0;
        int _ecoreCount = 0;
        double _frameSet = 0.0;

        bool _startCal = false;

        Data ItemTarget => _testData[999];

        private void InitializeListItem()
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

        public TCPerformance()
        {
            Console.WriteLine($"TCPerformance");
            EcoreAnimator = DependencyService.Get<IEcoreAnimator>(DependencyFetchTarget.NewInstance);
            GlobalScrollConfig = DependencyService.Get<IGlobalScrollConfig>(DependencyFetchTarget.NewInstance);

            InitializeListItem();

            int index = 0;
            _listView = new RenderCircleListView
            {
                ItemsSource = _testData,
                ItemTemplate = new DataTemplate(() =>
                {
                    var cell = new MyViewCell
                    {
                        Text = _testData[index++].Label
                    };
                    return cell;
                }),
            };

            _popUp = new InformationPopup();
            _popUp.BackButtonPressed += (s, e) =>
            {
                _popUp.Dismiss();
            };

            // start performance check
            _listView.Changed += OnStartCalculator;
            _listView.ScrollStarted += OnStartCheckFps;
            _listView.ScrollStopped += StopCheckFps;


            Content = _listView;
        }

        private void StopCheckFps(object sender, EventArgs e)
        {
            Console.WriteLine($"StopCheckFps _enteringSpeed:{_enteringSpeed} ms");
            double endTime = EcoreAnimator.CurrentTime;
            double span = endTime - _startTime;
            double frameFPS = _frameCount / span;
            double animatorFPS = _ecoreCount / span;
            Console.WriteLine($"_startTime:{_startTime}, endTime:{endTime} , , AnimatorFPS:{animatorFPS} fps, FrameFPS:{frameFPS} fps");
            _listView.RenderPost -= OnRenderPost;
            EcoreAnimator.Animation -= OnEcoreAnimationCheck;
            _listView.ScrollStarted -= OnStartCheckFps;
            _listView.ScrollStopped -= StopCheckFps;

            GlobalScrollConfig.BringInScrollFriction = _frameSet;

            _popUp.Text = string.Format("<span color=#FFFFFF , font_size=27> Entering Speed : {0:f1} msec<br>Animator FPS : {1:f1} fps<br>Evas FPS : {2:f1} fps</span>", _enteringSpeed, animatorFPS, frameFPS);
            _popUp.Show();
        }

        private void OnStartCheckFps(object sender, EventArgs e)
        {
            _ecoreCount = 0;
            _startTime = EcoreAnimator.CurrentTime;
            EcoreAnimator.Animation += OnEcoreAnimationCheck;
            _listView.RenderPost += OnRenderPost;
            Console.WriteLine($"OnStartCheckFps  _frameCount:{_frameCount}, _ecoreCount:{_ecoreCount}");
        }

        private void OnRenderPost(object sender, EventArgs e)
        {
            _frameCount++;
            Console.WriteLine($"OnRenderPost  _frameCount:{_frameCount}");
        }

        private void OnEcoreAnimationCheck(object sender, EcoreAnimatorEventArgs e)
        {
            _ecoreCount++;
            Console.WriteLine($"OnEcoreAnimationCheck  _ecoreCount:{_ecoreCount}");
        }

        private void OnStartCalculator(object sender, EventArgs e)
        {
            if (_startCal) return;
            _startCal = true;
            Console.WriteLine($"OnStartCalculator");
            _enteringSpeed = EcoreAnimator.CurrentTime;
            Console.WriteLine($" _enteringSpeed:{_enteringSpeed}");
            _listView.Changed -= OnStartCalculator;
            _listView.RenderPost += OnCheckEnteringSpeed;
        }

        private void OnCheckEnteringSpeed(object sender, EventArgs e)
        {
            _listView.RenderPost -= OnCheckEnteringSpeed;
            double currentTime = EcoreAnimator.CurrentTime;
            _enteringSpeed = (currentTime - _enteringSpeed) * 1000.0;
            Console.WriteLine($"OnCheckEnteringSpeed  currentTime:{currentTime}, _enteringSpeed:{_enteringSpeed}");

            _frameSet = GlobalScrollConfig.BringInScrollFriction;
            GlobalScrollConfig.BringInScrollFriction = TimeSet;
            Console.WriteLine($"OnCheckEnteringSpeed  _frameSet:{_frameSet}, TimeSet:{TimeSet}");
            _listView.ScrollTo(ItemTarget, ScrollToPosition.MakeVisible, true);
        }
    }
}
