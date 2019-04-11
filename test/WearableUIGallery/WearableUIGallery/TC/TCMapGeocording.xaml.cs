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
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCMapGeocording : CirclePage
    {
        public TCMapGeocording()
        {
            InitializeComponent ();
        }

        async void OnClickGetPostion(object sender, EventArgs args)
        {
            poistionLabel.Text = await GetPositionAsync();
            poistionLabel.IsVisible = true;
        }

        async void OnClickGetAddress(object sender, EventArgs args)
        {
            addressLabel.Text = await GetAddressAsync();
        }

        public async Task<string> GetPositionAsync()
        {
            var geocoding = new Geocoding();
            string testAddress = addressEntry.Text;
            var approximateLocations = await geocoding.GetPositionsForAddressAsync(testAddress);
            string result = "";
            foreach (var position in approximateLocations)
                result += position.Latitude + ", " + position.Longitude;
            return result;
        }

        public async Task<string> GetAddressAsync()
        {
            Geocoding geocoder = new Geocoding();
            var possibleAddress = await geocoder.GetAddressesForPositionAsync(new Position(40.714224, -73.961452));
            string result = "";
            int index = 1;
            foreach (var address in possibleAddress)
            {
                result += "[" + index + "]" + address + "\n";
                index++;
            }
            return result;
        }
    }
}