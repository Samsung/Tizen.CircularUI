using System;
using ESpinner = ElmSharp.Wearable.CircleSpinner;
using ESize = ElmSharp.Size;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(Xamarin.Forms.CircularUI.CircleStepper), typeof(Xamarin.Forms.CircularUI.Tizen.CircleStepperRenderer))]


namespace Xamarin.Forms.CircularUI.Tizen
{
    public class CircleStepperRenderer : ViewRenderer<CircleStepper, ESpinner>
    {
        public CircleStepperRenderer()
        {
            RegisterPropertyHandler(CircleStepper.MarkerColorProperty, UpdateMarkerColor);
            RegisterPropertyHandler(CircleStepper.MarkerLineWidthProperty, UpdateMarkerLineWidth);
            RegisterPropertyHandler(Stepper.ValueProperty, UpdateValue);
            RegisterPropertyHandler(Stepper.MinimumProperty, UpdateMinimum);
            RegisterPropertyHandler(Stepper.MaximumProperty, UpdateMaximum);
            RegisterPropertyHandler(Stepper.IncrementProperty, UpdateIncrement);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CircleStepper> e)
        {
            if (Control == null)
            {
                var surface = this.GetSurface();
                if (null != surface)
                {
                    var spinner = new ESpinner(Xamarin.Forms.Platform.Tizen.Forms.Context.MainWindow, surface);
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

        void UpdateMarkerColor(bool initialize)
        {
            if (null != Control && null != Element && Element.MarkerColor != Color.Default)
            {
                Control.MarkerColor = Element.MarkerColor.ToNative();
            }
        }

        void UpdateMarkerLineWidth(bool initialize)
        {
            if (null != Control && null != Element)
            {
                Control.MarkerLineWidth = Element.MarkerLineWidth;
            }
        }

        void UpdateValue(bool initialize)
        {
            if (null != Control && null != Element)
            {
                Control.Value = Element.Value;
            }
        }

        void UpdateMaximum(bool initialize)
        {
            if (null != Control && null != Element)
            {
                Control.Maximum = Element.Maximum;
            }
        }

        void UpdateMinimum(bool initialize)
        {
            if (null != Control && null != Element)
            {
                Control.Minimum = Element.Minimum;
            }
        }

        void UpdateIncrement(bool initialize)
        {
            if (null != Control && null != Element)
            {
                Control.Step = Element.Increment;
            }
        }
    }
}
