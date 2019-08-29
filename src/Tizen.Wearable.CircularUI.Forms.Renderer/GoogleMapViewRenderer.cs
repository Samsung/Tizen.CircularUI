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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Tizen.Wearable.CircularUI.Forms;
using Tizen.Wearable.CircularUI.Forms.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;
using TChromium = Tizen.WebView.Chromium;
using TWebView = Tizen.WebView.WebView;
using XForms = Xamarin.Forms.Forms;

[assembly: ExportRenderer(typeof(GoogleMapView), typeof(GoogleMapViewRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class GoogleMapViewRenderer : ViewRenderer<GoogleMapView, WebViewContainer>
    {
        private const string HtmlStyle = "<html><head><style>html, body {height: 100%; margin: 0; padding: 0;} #map {height: 100%;}</style>";
        private const string GoogleMapURL = "http://maps.googleapis.com/maps/api/";
        private const double DefaultLatitude = 41.890202;
        private const double DefaultLongitude = 12.492049;

        private string[] _positions = { "LEFT_TOP", "RIGHT_TOP", "LEFT_CENTER", "RIGHT_CENTER", "LEFT_BOTTOM", "RIGHT_BOTTOM", "TOP_CENTER", "BOTTOM_CENTER" };

        string _maphtml;

        TWebView NativeWebView => Control.WebView;

        protected override void OnElementChanged(ElementChangedEventArgs<GoogleMapView> e)
        {
            if (Control == null)
            {
                TChromium.Initialize();
                XForms.Context.Terminated += (sender, arg) => TChromium.Shutdown();
                SetNativeControl(new WebViewContainer(XForms.NativeParent));
                NativeWebView.LoadStarted += OnLoadStarted;
                NativeWebView.LoadFinished += OnLoadFinished;
                NativeWebView.LoadError += OnLoadError;
            }

            if (e.OldElement != null)
            {
                ((ObservableCollection<Marker>)e.OldElement.Markers).CollectionChanged -= OnCollectionChanged;
                e.OldElement.LoadMapRequested -= OnLoadMapRequested;
            }

            if (e.NewElement != null)
            {
                ((ObservableCollection<Marker>)e.NewElement.Markers).CollectionChanged += OnCollectionChanged;
                e.NewElement.LoadMapRequested += OnLoadMapRequested;
                LoadMap();
            }
            base.OnElementChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Control != null)
                {
                    NativeWebView.StopLoading();
                    NativeWebView.LoadStarted -= OnLoadStarted;
                    NativeWebView.LoadFinished -= OnLoadFinished;
                    NativeWebView.LoadError -= OnLoadError;
                }
                if (Element != null)
                {
                    ((ObservableCollection<Marker>)Element.Markers).CollectionChanged -= OnCollectionChanged;
                    Element.LoadMapRequested -= OnLoadMapRequested;
                }
            }
            base.Dispose(disposing);
        }

        private void OnLoadError(object sender, global::Tizen.WebView.SmartCallbackLoadErrorArgs e)
        {
            string url = e.Url;
            Log.Error(FormsCircularUI.Tag, $"OnLoadError() url:{url}");
        }

        private void OnLoadStarted(object sender, EventArgs e)
        {
            string url = NativeWebView.Url;
            Log.Debug(FormsCircularUI.Tag, "OnLoadStarted()");
        }

        private void OnLoadFinished(object sender, EventArgs e)
        {
            string url = NativeWebView.Url;
            Log.Debug(FormsCircularUI.Tag, "OnLoadFinished()");
            NativeWebView.SetFocus(true);
        }

        private void LoadMap()
        {
            _maphtml = CreateMapViewScript();
            var baseUrl = default(string);
            NativeWebView.LoadHtml(_maphtml, baseUrl);
        }

        private string CreateMapViewScript()
        {
            string source = null;
            StringBuilder sb = new StringBuilder();
            var apiKey = GoogleMaps.GetKey();

            sb.Append(HtmlStyle);
            sb.AppendLine();
            sb.Append("<script src=\"");
            sb.Append(GoogleMapURL);
            sb.Append("js?key=");
            sb.Append(apiKey);
            sb.Append("\"></script>");
            sb.AppendLine();

            sb = CreateMapOptionScript(sb);
            sb.AppendLine();
            sb.Append("function initialize(){\n    var map = new google.maps.Map(document.getElementById(\"map\"), mapProp); ");

            if (Element.Markers.Count > 0)
            {
                sb = CreatePinsScript(sb);
            }

            sb.Append("\n}\ngoogle.maps.event.addDomListener(window, \'load\', initialize);\n");
            sb.Append("</script></head><body><div id =\"map\"></div></body></html>");

            source = sb.ToString();
            return source;
        }

        private StringBuilder CreateMapOptionScript(StringBuilder sb)
        {
            if (Element.MapOption != null)
            {
                if(Element.MapOption.Center.Latitude == 0 && Element.MapOption.Center.Longitude == 0)
                    sb.Append($"<script>\nvar myCenter = new google.maps.LatLng({DefaultLatitude}, {DefaultLongitude});");
                else
                    sb.Append($"<script>\nvar myCenter = new google.maps.LatLng({Element.MapOption.Center.Latitude}, {Element.MapOption.Center.Longitude});");

                sb.AppendLine();
                var type = Element.MapOption.MapType;
                var mapTypeId = type.ToString().ToUpper();
                sb.Append("var mapProp = {\n  center:myCenter,\n");
                sb.Append($"  zoom: {Element.MapOption.Zoom},\n  mapTypeId: google.maps.MapTypeId.{mapTypeId},\n");

                if (Element.MapOption.HasGestureEnabled == false)
                {
                    sb.Append("  gestureHandling: 'none',\n");
                }

                if (Element.MapOption.IsZoomControlVisible == true)
                {
                    sb.Append("  mapTypeControl: false,\n  rotateControl: false,\n  fullscreenControl: false,\n  streetViewControl: false,\n");
                    sb.Append("  zoomControl: true,\n");
                    sb.Append("  zoomControlOptions:{\n");
                    int index = (int)Element.MapOption.ZoomControlPosition;
                    sb.Append($"    position: google.maps.ControlPosition.{_positions[index]},\n");
                    sb.Append("  }\n");
                    sb.Append("};");
                }
                else
                {
                    sb.Append("  disableDefaultUI: true\n};");
                }
            }
            else
            {
                sb.Append($"<script>\nvar myCenter = new google.maps.LatLng({DefaultLatitude}, {DefaultLongitude});");
                sb.AppendLine();
                sb.Append("var mapProp = {\n  center:myCenter,\n  zoom: 10,\n  mapTypeId: google.maps.MapTypeId.ROADMAP,\n  disableDefaultUI: true\n};");
            }

            return sb;
        }

        private StringBuilder CreatePinsScript(StringBuilder _sb)
        {
            int index = 1;
            if (_sb == null || _sb.Length == 0) return _sb;

            Log.Debug(FormsCircularUI.Tag, $"Markers count:{Element.Markers.Count}");
            foreach (var marker in Element.Markers)
            {
                _sb.AppendLine();
                _sb.Append($"    var marker{index} = new google.maps.Marker({{position: new google.maps.LatLng({marker.Position.Latitude}, {marker.Position.Longitude}), title:\"{marker.Description}\" }}); ");
                _sb.AppendLine();
                _sb.Append($"    marker{index}.setMap(map);");
                _sb.AppendLine();
                _sb.Append($"    var infowindow{index} = new google.maps.InfoWindow({{ content: \"{marker.Description}\" }});");
                _sb.AppendLine();
                if (marker.IsPopupOpened == true)
                {
                    _sb.Append($"    infowindow{index}.open(map, marker{index});");
                }
                else
                {
                    _sb.Append($"    marker{index}.addListener('click', function() {{ infowindow{index}.open(map, marker{index});}});");
                }
                index++;
            }

            return _sb;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Log.Debug(FormsCircularUI.Tag, $"e.Action:{e.Action}");
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Reset:
                    LoadMap();
                    break;
                case NotifyCollectionChangedAction.Move:
                    //do nothing
                    break;
            }
        }

        private void OnLoadMapRequested(object sender, EventArgs eventArgs)
        {
            Log.Debug(FormsCircularUI.Tag, $"Load Requested");
            LoadMap();
        }
    }
}
