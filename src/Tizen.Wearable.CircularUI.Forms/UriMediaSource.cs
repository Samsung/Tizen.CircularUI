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
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// A MediaSource that loads a media from a URI
    /// </summary>
    [TypeConverter(typeof(MediaSourceConverter))]
    public sealed class UriMediaSource : MediaSource
    {
        /// <summary>
        /// Identifies the File bindable property.
        /// </summary>
        public static readonly BindableProperty UriProperty = BindableProperty.Create("Uri", typeof(Uri), typeof(UriImageSource), default(Uri), validateValue: (bindable, value) => value == null || ((Uri)value).IsAbsoluteUri);

        /// <summary>
        /// Gets or sets the URI for the media to get.
        /// </summary>
        public Uri Uri
        {
            get { return (Uri)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        /// <summary>
        /// Returns the path to the file for the media, prefixed with the string, "Uri: ".
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Uri: {Uri}";
        }

        /// <summary>
        /// Allows implicit casting from a Uri.
        /// </summary>
        /// <param name="uri"></param>
        public static implicit operator UriMediaSource(Uri uri)
        {
            return (UriMediaSource)FromUri(uri);
        }

        /// <summary>
        /// Allows implicit casting to a string.
        /// </summary>
        /// <param name="uri"></param>
        public static implicit operator string(UriMediaSource uri)
        {
            return uri?.ToString();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            if (propertyName == UriProperty.PropertyName)
                OnSourceChanged();
            base.OnPropertyChanged(propertyName);
        }
    }
}