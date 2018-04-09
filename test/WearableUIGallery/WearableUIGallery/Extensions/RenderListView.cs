﻿/*
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
using Xamarin.Forms;
using CircularUI;

namespace WearableUIGallery.Extensions
{
    public class RenderCircleListView : CircleListView
    {
        public event EventHandler Changed;
        public event EventHandler RenderPost;
        public event EventHandler ScrollStarted;
        public event EventHandler ScrollStopped;

        public void SendChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public void SendRenderPost()
        {
            RenderPost?.Invoke(this, EventArgs.Empty);
        }

        public void SendScrollStarted()
        {
            ScrollStarted?.Invoke(this, EventArgs.Empty);
        }

        public void SendScrollStopped()
        {
            ScrollStopped?.Invoke(this, EventArgs.Empty);
        }
    }
}