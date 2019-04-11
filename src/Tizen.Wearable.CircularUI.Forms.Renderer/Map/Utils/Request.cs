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

using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tizen.Wearable.CircularUI.Forms.Renderer.Map.Utils
{
    public class Request<T> : IDisposable
    {
        #region fields
        private HttpClient _httpClient;

        private bool _isFirstParameter = true;
        #endregion

        #region properties

        public string RequestUri { get; protected set; }

        #endregion

        #region methods

        public Request(string address)
        {
            _httpClient = new HttpClient();
            RequestUri = address;
        }

        public string AddParameter(string name, string value)
        {
            if (_isFirstParameter)
            {
                RequestUri += $"?{name}={value}";
                _isFirstParameter = false;
            }
            else
            {
                RequestUri += $"&{name}={value}";
            }

            return RequestUri;
        }

        public async Task<T> Get()
        {
            Debug.Assert(_httpClient != null, "_httpClient is not initialized!");

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.GetAsync(RequestUri);
                if (!response.IsSuccessStatusCode)
                {
                    Log.Error(FormsCircularUI.Tag, $"IsSuccessStatusCode:{response.IsSuccessStatusCode}");
                    throw new HttpRequestException($"GetAsync Exception StatusCode:{response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Log.Error(FormsCircularUI.Tag, $"[Exception] GetAsync Exception:{ex.Message}");
                throw new HttpRequestException($"GetAsync Exception:{ex.Message}");
            }

            return ReadStream(await response.Content.ReadAsStreamAsync());
        }

        private static T ReadStream(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            using (var reader = new JsonTextReader(streamReader))
            {
                var serializer = JsonSerializer.Create();
                return serializer.Deserialize<T>(reader);
            }
        }

        public void Dispose()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
                _httpClient = null;
            }
        }
        #endregion
    }
}
