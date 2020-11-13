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
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// Abstract class whose implementors load media contents from files or the Web.
    /// </summary>
    [TypeConverter(typeof(MediaSourceConverter))]
    public abstract class MediaSource : Element
    {
        protected MediaSource()
        {
        }

        /// <summary>
        /// Returns a new MediaSource that reads from file.
        /// </summary>
        /// <param name="file">The file path to use as a media source.</param>
        /// <returns>Returns the MediaSource.</returns>
        public static MediaSource FromFile(string file)
        {
            return new FileMediaSource { File = file };
        }

        /// <summary>
        /// Returns a new MediaSource that reads from uri.
        /// </summary>
        /// <param name="uri">The uri path to use as a media source.</param>
        /// <returns>Returns the MediaSource.</returns>
        public static MediaSource FromUri(Uri uri)
        {
            if (!uri.IsAbsoluteUri)
                throw new ArgumentException("uri is relative");
            return new UriMediaSource { Uri = uri };
        }

        public static implicit operator MediaSource(string source)
        {
            return Uri.TryCreate(source, UriKind.Absolute, out Uri uri) && uri.Scheme != "file" ? FromUri(uri) : FromFile(source);
        }

        public static implicit operator MediaSource(Uri uri)
        {
            if (!uri.IsAbsoluteUri)
                throw new ArgumentException("uri is relative");
            return FromUri(uri);
        }

        protected void OnSourceChanged()
        {
            SourceChanged?.Invoke(this, EventArgs.Empty);
        }

        internal event EventHandler SourceChanged;
    }
}