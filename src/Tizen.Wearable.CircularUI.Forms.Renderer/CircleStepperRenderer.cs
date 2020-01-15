﻿/*
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
using ESpinner = ElmSharp.Wearable.CircleSpinner;
using ESize = ElmSharp.Size;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using XForms = Xamarin.Forms.Forms;

[assembly: ExportRenderer(typeof(Tizen.Wearable.CircularUI.Forms.CircleStepper), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.CircleStepperRenderer))]


namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class CircleStepperRenderer : ViewRenderer<CircleStepper, ESpinner>
    {
        public CircleStepperRenderer()
        {
#pragma warning disable CS0618 // MarkerColorProperty and MarkerLineWidthProperty are obsolete
            RegisterPropertyHandler(CircleStepper.MarkerColorProperty, UpdateMarkerColor);
            RegisterPropertyHandler(CircleStepper.MarkerLineWidthProperty, UpdateMarkerLineWidth);
#pragma warning restore CS0618 // MarkerColorProperty and MarkerLineWidthProperty are obsolete
            RegisterPropertyHandler(CircleStepper.LabelFormatProperty, UpdateLabelFormat);
            RegisterPropertyHandler(CircleStepper.TitleProperty, UpdateTitle);
            RegisterPropertyHandler(Stepper.MinimumProperty, UpdateMinimum);
            RegisterPropertyHandler(Stepper.MaximumProperty, UpdateMaximum);
            RegisterPropertyHandler(Stepper.ValueProperty, UpdateValue);
            RegisterPropertyHandler(Stepper.IncrementProperty, UpdateIncrement);
            RegisterPropertyHandler(CircleStepper.IsWrapEnabledProperty, UpdateWrapEnabled);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CircleStepper> e)
        {
            if (Control == null)
            {
                var surface = this.GetSurface();
                if (null != surface)
                {
                    var spinner = new ESpinner(XForms.NativeParent, surface);
                    spinner.Style = "circle";

                    SetNativeControl(spinner);
                    Control.ValueChanged += OnValueChanged;
                }
                else
                {
                    throw new CirclePageNotFoundException();
                }
            }

            base.OnElementChanged(e);
        }

        private void OnValueChanged(object sender, EventArgs e)
        {
            double newValue = Control.Value;
            ((IElementController)Element).SetValueFromRenderer(Stepper.ValueProperty, newValue);

            if (Element.LabelFormat == null)
            {
                // Determines how many decimal places are there in current Stepper's value.
                // The 15 pound characters below correspond to the maximum precision of Double type.
                var decimalValue = Decimal.Parse(newValue.ToString("0.###############"));

                // GetBits() method returns an array of four 32-bit integer values.
                // The third (0-indexing) element of an array contains the following information:
                //     bits 00-15: unused, required to be 0
                //     bits 16-23: an exponent between 0 and 28 indicating the power of 10 to divide the integer number passed as a parameter.
                //                 Conversely this is the number of decimal digits in the number as well.
                //     bits 24-30: unused, required to be 0
                //     bit     31: indicates the sign. 0 means positive number, 1 is for negative numbers.
                //
                // The precision information needs to be extracted from bits 16-23 of third element of an array
                // returned by GetBits() call. Right-shifting by 16 bits followed by zeroing anything else results
                // in a nice conversion of this data to integer variable.
                var precision = (Decimal.GetBits(decimalValue)[3] >> 16) & 0x000000FF;

                // Sets Stepper's inner label decimal format to use exactly as many decimal places as needed:
                Control.LabelFormat = string.Format("%.{0}f", precision);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Control != null)
                {
                    Control.ValueChanged -= OnValueChanged;
                }
            }
            base.Dispose(disposing);
        }

        protected override Size MinimumSize()
        {
            // This width and height are values taken from SPINNER_LAYOUT_CONTENT_AREA_SIZE of elm-theme-tizen-wearable module
            return new ESize(360, 110).ToDP();
        }

        protected override ESize Measure(int availableWidth, int availableHeight)
        {
            return new ESize(360, 110);
        }

        void UpdateMarkerColor()
        {
#pragma warning disable CS0618 // MarkerColor is obsolete
            if (null != Control && null != Element && Element.MarkerColor != Color.Default)
            {
                Control.MarkerColor = Element.MarkerColor.ToNative();
            }
#pragma warning restore CS0618 // MarkerColor is obsolete
        }

        void UpdateMarkerLineWidth()
        {
            if (null != Control && null != Element)
            {
#pragma warning disable CS0618 // MarkerLineWidth is obsolete
                Control.MarkerLineWidth = Element.MarkerLineWidth;
#pragma warning restore CS0618 // MarkerLineWidth is obsolete
            }
        }

        void UpdateLabelFormat()
        {
            if (null != Control && null != Element)
            {
                Control.LabelFormat = Element.LabelFormat;
            }
        }

        void UpdateTitle()
        {
            if (null != Control && null != Element)
            {
                Control.SetPartText("elm.text", Element.Title);
            }
        }

        void UpdateValue()
        {
            if (null != Control && null != Element)
            {
                Control.Value = Element.Value;
            }
        }

        void UpdateMaximum()
        {
            if (null != Control && null != Element)
            {
                Control.Maximum = Element.Maximum;
            }
        }

        void UpdateMinimum()
        {
            if (null != Control && null != Element)
            {
                Control.Minimum = Element.Minimum;
            }
        }

        void UpdateIncrement()
        {
            if (null != Control && null != Element)
            {
                Control.Step = Element.Increment;
            }
        }

        void UpdateWrapEnabled()
        {
            if (null != Control && null != Element)
            {
                Control.IsWrapEnabled = Element.IsWrapEnabled;
            }
        }
    }
}
