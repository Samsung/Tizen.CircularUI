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

using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// A MediaSource that reads a media from a file.
    /// </summary>
    [TypeConverter(typeof(FileMediaSourceConverter))]
    public sealed class FileMediaSource : MediaSource
    {
        /// <summary>
        /// Identifies the File bindable property.
        /// </summary>
        public static readonly BindableProperty FileProperty = BindableProperty.Create("File", typeof(string), typeof(FileMediaSource), default(string));

        /// <summary>
        /// Gets or sets the file from which this FileMediaSource will load a media.
        /// </summary>
        public string File
        {
            get { return (string)GetValue(FileProperty); }
            set { SetValue(FileProperty, value); }
        }

        /// <summary>
        /// Returns a string representation of `File`.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"File: {File}";
        }

        /// <summary>
        /// Allows implicit casting from a string.
        /// </summary>
        /// <param name="file"></param>
        public static implicit operator FileMediaSource(string file)
        {
            return (FileMediaSource)FromFile(file);
        }

        /// <summary>
        /// Allows implicit casting to a string.
        /// </summary>
        /// <param name="file"></param>
        public static implicit operator string(FileMediaSource file)
        {
            return file?.File;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            if (propertyName == FileProperty.PropertyName)
                OnSourceChanged();
            base.OnPropertyChanged(propertyName);
        }
    }
}