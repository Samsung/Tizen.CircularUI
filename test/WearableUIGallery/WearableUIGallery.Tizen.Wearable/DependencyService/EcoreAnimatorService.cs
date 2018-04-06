/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using ElmSharp;
using WearableUIGallery.Extensions;
using WearableUIGallery.Tizen.Wearable.DependencyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(EcoreAnimatorService))]
namespace WearableUIGallery.Tizen.Wearable.DependencyService
{

    public class EcoreAnimatorService : IEcoreAnimator
	{
        IDictionary<IntPtr, EventHandler<EcoreAnimatorEventArgs>> _handlers = new Dictionary<IntPtr, EventHandler<EcoreAnimatorEventArgs>>();
        public double CurrentTime => EcoreAnimator.GetCurrentTime();

        event EventHandler<EcoreAnimatorEventArgs> IEcoreAnimator.Animation
        {
            add
            {
                Func<bool> handler = () =>
                {
                    EcoreAnimatorEventArgs args = new EcoreAnimatorEventArgs(true);
                    value?.Invoke(this, args);
                    return args.Repeat;
                };

                _handlers.Add(EcoreAnimator.AddAnimator(handler), value);
             }

            remove
            {
                var targetHandler = _handlers.Where(p => p.Value == value).FirstOrDefault();
                EcoreAnimator.RemoveAnimator(targetHandler.Key);
                _handlers.Remove(targetHandler.Key);
            }
        }
    }
}
