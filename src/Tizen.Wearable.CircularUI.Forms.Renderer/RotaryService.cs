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
using ERotaryEventManager = ElmSharp.Wearable.RotaryEventManager;
using ERotaryEventArgs = ElmSharp.Wearable.RotaryEventArgs;

[assembly: Dependency(typeof(Tizen.Wearable.CircularUI.Forms.Renderer.RotaryService))]
namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class RotaryService : IRotaryService
    {
        EventHandler<RotaryEventArgs> _rotated;

        public event EventHandler<RotaryEventArgs> Rotated
        {
            add
            {
                if (_rotated == null)
                {
                    ERotaryEventManager.Rotated += OnRotaryChanged;
                }
                _rotated += value;
            }
            remove
            {
                _rotated -= value;
                if (_rotated == null)
                {
                    ERotaryEventManager.Rotated -= OnRotaryChanged;
                }
            }
        }

        void OnRotaryChanged(ERotaryEventArgs args)
        {
            _rotated?.Invoke(this, new RotaryEventArgs() { IsClockwise = args.IsClockwise });
        }
    }
}
