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
using Xamarin.Forms.Maps;
using Tizen.Location;
using Tizen.Security;
using System;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCMapViewCurrentPosition : CirclePage
    {
        private static double Latitude = 40.7157961;
        private static double Longitude = -74.0252194;

        GoogleMapOption _option;
        Locator _locator;

        bool _locatorInitialized = false;

        public TCMapViewCurrentPosition()
        {
            InitializeComponent();
            var position = new Position(Latitude, Longitude);
            _option = new GoogleMapOption(position, GoogleMapType.Roadmap, 12);
            mapviewPosition.SetMapOption(_option);

            PrivilegeCheck();
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
            var pin = new Pin();

            try
            {
                Location location = _locator.GetLocation();
                var current = new Position(location.Latitude, location.Longitude);
                Tizen.Log.Debug("CircularUI", $"Current:[{location.Latitude},{location.Longitude} ]");
                pin.Position = current;
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

            pin.Label = "Current position";
            _option.Center = pin.Position;
            _option.Zoom = 14;
            _option.IsPinsPopupOpened = true;
            positionLabel.Text = $"Position({pin.Position.Latitude}, {pin.Position.Longitude})";
            positionLabel.IsVisible = true;
            mapviewPosition.SetMapOption(_option);
            if(mapviewPosition.Pins.Count > 0) 
                mapviewPosition.Pins.Clear();

            mapviewPosition.Pins.Add(pin);
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

        void OnClickCurrentPosition(object sender, EventArgs args)
        {
            if (_locatorInitialized == false)
            {
                LocationInitialize();
            }
            else
            {
                GetCurrentPosition();
            }
        }
    }
}