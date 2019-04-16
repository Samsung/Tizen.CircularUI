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
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;
using Tizen.Location;
using Tizen.Security;
using System;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCMapViewCurrentPosition : CirclePage
    {
        GoogleMapOption _option;
        Locator _locator;

        bool _locatorInitialized = false;

        public TCMapViewCurrentPosition()
        {
            InitializeComponent();

            PrivilegeCheck();

            if (_locatorInitialized == false)
            {
                LocationInitialize();
            }
        }

        /// <summary>
        /// Permission check
        /// </summary>
        public void PrivilegeCheck()
        {
            try
            {
                /// Check location permission
                CheckResult result = PrivacyPrivilegeManager.CheckPermission("http://tizen.org/privilege/location");

                switch (result)
                {
                    case CheckResult.Allow:
                        break;
                    case CheckResult.Deny:
                        break;
                    case CheckResult.Ask:
                        /// Request to privacy popup
                        PrivacyPrivilegeManager.RequestPermission("http://tizen.org/privilege/location");
                        break;
                }
            }
            catch (Exception ex)
            {
                /// Exception handling
                positionLabel.Text = ex.Message;
                positionLabel.IsVisible = true;
                Tizen.Log.Error("CircularUI", $"[Pravacy]Exception {ex.Message}");
            }
        }

        void LocationInitialize()
        {
            try
            {
                _locator = new Locator(LocationType.Hybrid);
                if (_locator != null)
                {
                    _locator.Start();
                    _locator.ServiceStateChanged += LocatorServiceStateChanged;
                }

                _locatorInitialized = true;
            }
            catch (Exception ex)
            {
                /// Exception handling
                positionLabel.Text = ex.Message;
                positionLabel.IsVisible = true;
                Tizen.Log.Error("CircularUI", $"[Locator]Exception:{ex.Message}");
            }
        }

        public void LocatorServiceStateChanged(object sender, ServiceStateChangedEventArgs e)
        {
            if (e.ServiceState == ServiceState.Enabled)
            {
                GetCurrentPosition();
            }
            else
            {
                positionLabel.Text = "Service Not avaiable";
                positionLabel.IsVisible = true;
                Tizen.Log.Error("CircularUI", $"[Locator] Service not available");
            }
        }

        private void GetCurrentPosition()
        {
            var marker = new Marker();

            try
            {
                Location location = _locator.GetLocation();
                var current = new LatLng(location.Latitude, location.Longitude);
                Tizen.Log.Debug("CircularUI", $"Current:[{location.Latitude},{location.Longitude} ]");
                marker.Location = current;
            }
            catch (Exception ex)
            {
                /// Exception handling
                positionLabel.Text = ex.Message;
                positionLabel.IsVisible = true;
                Tizen.Log.Error("CircularUI", $"[Locator]Exception:{ex.Message}");
            }
            finally
            {
                LocatorDispose();
            }

            marker.Label = "Current position";
            _option.Center = marker.Location;
            _option.Zoom = 14;
            positionLabel.Text = $"Position({marker.Location.Latitude}, {marker.Location.Longitude})";
            positionLabel.IsVisible = true;
            mapviewPosition.Update(_option);
            if(mapviewPosition.Markers.Count > 0)
                mapviewPosition.Markers.Clear();

            mapviewPosition.Markers.Add(marker);
        }

        public void LocatorDispose()
        {
            if(_locatorInitialized)
            {
                _locator.ServiceStateChanged -= LocatorServiceStateChanged;
                _locator.Stop();
                _locator.Dispose();
                _locator = null;
                _locatorInitialized = false;
            }
        }
    }
}