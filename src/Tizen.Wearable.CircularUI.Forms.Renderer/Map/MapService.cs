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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tizen.Wearable.CircularUI.Forms.Renderer.Map.Geocoding;
using Tizen.Wearable.CircularUI.Forms.Renderer.Map.Utils;
using Xamarin.Forms.Maps;

namespace Tizen.Wearable.CircularUI.Forms.Renderer.Map
{
    public class MapService
    {
        public static readonly string GeocodeBaseUri = "https://maps.googleapis.com/maps/api/geocode/json";
        private string _apiKey;
        private string _channel;
        private GoogleSignedType _signType;
        private byte[] _privateKeyBytes;

        public MapService(string apiKey, GoogleSignedType signType = GoogleSignedType.ApiKey, string channel = null, string privateKey = null)
        {
            _apiKey = apiKey;
            _signType = signType;
            if (privateKey != null)
            {
                _channel = channel;
            }

            if (privateKey != null)
            {
                privateKey = privateKey.Replace("-", "+").Replace("_", "/");
                _privateKeyBytes = Convert.FromBase64String(privateKey);
            }
        }

        public async Task<IList<Position>> CreateGeocodingRequest(string address, string region = null)
        {
            IList<Position> positions = new ObservableCollection<Position>();
            using (var request = new Request<GeocodingResponse>(GeocodeBaseUri))
            {
                var encodedAddress = HttpUtility.UrlEncode(address);
                request.AddParameter("address", encodedAddress);

                if (!String.IsNullOrWhiteSpace(region))
                {
                    request.AddParameter("region", region);
                }

                GetKeyUri(request);
                GeocodingResponse response = await request.Get();
                foreach (var result in response.Results)
                {
                    positions.Add(new Position(result.Geometry.Location.Latitude, result.Geometry.Location.Longitude));
                }
            }

            return positions;
        }

        public async Task<IList<string>> CreateReverseGeocodingRequest(Position postion)
        {
            IList<string> addresses = new ObservableCollection<string>();
            using (var request = new Request<GeocodingResponse>(GeocodeBaseUri))
            {
                request.AddParameter("latlng", postion.Latitude + "," + postion.Longitude);
                GetKeyUri(request);
                GeocodingResponse response = await request.Get();
                foreach (var result in response.Results)
                {
                    addresses.Add(result.FormattedAddress);
                }
            }
            return addresses;
        }

        public string GetSignature(string uri)
        {
            byte[] encodedPathQuery = Encoding.ASCII.GetBytes(uri);
            var hashAlgorithm = new HMACSHA1(_privateKeyBytes);
            byte[] hashed = hashAlgorithm.ComputeHash(encodedPathQuery);
            string base64 = Convert.ToBase64String(hashed);
            string signature = new StringBuilder(base64).Replace("+", "-").Replace("/", "_").ToString();
            return signature;
        }

        public void GetKeyUri(Request<GeocodingResponse> request)
        {
            if(request == null)
            {
                Log.Error(FormsCircularUI.Tag, "request is null");
            }
            else if (String.IsNullOrWhiteSpace(_apiKey))
            {
                Log.Error(FormsCircularUI.Tag, "API Key is required");
            }
            else
            {
                if (_signType == GoogleSignedType.ApiKey)
                {
                    request.AddParameter("key" , _apiKey);
                }
                else
                {
                    request.AddParameter("client", _apiKey);
                    if (_channel != null)
                    {
                        request.AddParameter("channel", _channel);
                    }
                }

                if (_privateKeyBytes != null)
                {
                    string signature = "&signature=" + GetSignature(request.RequestUri);
                    request.AddParameter("signature", signature);
                }
            }
        }

        internal string GetKey()
        {
            return _apiKey;
        }
    }
}
