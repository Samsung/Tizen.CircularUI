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
    /// Class that takes a string representation of a media file location and returns a MeidaSource from the specified resource.
    /// </summary>
    [TypeConverter(typeof(MediaSource))]
    public sealed class MediaSourceConverter : TypeConverter
    {
        /// <summary>
        /// Returns a media source created from a URI that is contained in value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>MediaSource</returns>
        public override object ConvertFromInvariantString(string value)
        {
            if (value != null)
            {
                return Uri.TryCreate(value, UriKind.Absolute, out Uri uri) && uri.Scheme != "file" ? MediaSource.FromUri(uri) : MediaSource.FromFile(value);
            }

            throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" into {1}", value, typeof(MediaSource)));
        }
    }
}