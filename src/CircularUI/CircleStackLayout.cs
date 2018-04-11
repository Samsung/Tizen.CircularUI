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
using System.Linq;
using Xamarin.Forms;

namespace CircularUI
{
    public class CircleStackLayout : StackLayout
    {
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            double r = Math.Min(width, height) / 2;

            System.Diagnostics.Debug.WriteLine($"LayoutChildren : {width}, {height}");

            double gap1 = CalcGap(Children.FirstOrDefault(), r);
            double gap2 = CalcGap(Children.LastOrDefault(), r);
            Rectangle region;

            if (Orientation == StackOrientation.Horizontal)
            {
                region = new Rectangle(x + gap1, y, width - gap1 - gap2, height);
            }
            else
            {
                region = new Rectangle(x, y + gap1, width, height - gap1 - gap2);
            }

            System.Diagnostics.Debug.WriteLine($"LayoutChildren 2 : {gap1}, {gap2}, {region}");
            base.LayoutChildren(region.X, region.Y, region.Width, region.Height);

            foreach (var child in Children)
            {
                if (!child.IsVisible) continue;
                RecalcForCirlce(child, x, y, width, height, region, r);
            }
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            System.Diagnostics.Debug.WriteLine($"--------- OnMeasure start");
            var requested = base.OnMeasure(widthConstraint, heightConstraint);

            var minimum = requested.Minimum;
            var request = requested.Request;

            double r = Math.Min(widthConstraint, heightConstraint) / 2;

            System.Diagnostics.Debug.WriteLine($"OnMeasure :{r}, {widthConstraint}, {heightConstraint} / {minimum.Width}, {minimum.Height} / {request.Width}, {request.Height} / {Width}, {Height}");

            double gap = CalcGap(Children.FirstOrDefault(), r) + CalcGap(Children.LastOrDefault(), r);

            if (Orientation == StackOrientation.Vertical)
            {
                minimum.Height += gap;
                request.Height += gap;
            }
            if (Orientation == StackOrientation.Horizontal)
            {
                minimum.Width += gap;
                request.Width += gap;
            }

            return new SizeRequest(request, minimum);
        }

        void CalcVector(double offset, double r, double field, double start, double end, double marginStart, double marginEnd, double old, Action<double> offsetSetter, Action<double> vectorSetter)
        {
            double center = offset + r;

            double w1, w2;

            if (field > r * 2)
            {
                double midField = field - r * 2;
                double max = r / 1.414213;
                bool centerhanger = false;

                if (start > center && start < center + midField)
                {
                    centerhanger = true;
                    w1 = Math.Max(CircleV(center - (end - start) / 2, r), CircleV(center - max, r));
                }
                else if (start > center + midField)
                {
                    centerhanger = true;
                    w1 = CircleV(start - midField, r);
                }
                else
                {
                    w1 = CircleV(start, r);
                }

                if (end > center && end < center + midField)
                {
                    w2 = Math.Max(CircleV(center - (end - start) / 2, r), CircleV(center - max, r));
                }
                else if (centerhanger && end > center + midField)
                {
                    w2 = CircleV(end - midField, r);
                }
                else
                {
                    w2 = CircleV(end, r);
                }
            }
            else
            {
                w1 = CircleV(start, r);
                w2 = CircleV(end, r);
            }

            double w;
            if (double.IsNaN(w1))
                w = w2;
            else if (double.IsNaN(w2))
                w = w1;
            else
                w = Math.Min(w1, w2);

            if (old + (marginStart + marginEnd) > w)
            {
                offsetSetter(r - w + marginStart);
                vectorSetter(w * 2 - marginStart + marginEnd);
            }
        }

        void RecalcForCirlce(View view, double x, double y, double width, double height, Rectangle region, double r)
        {
            var bounds = view.Bounds;
            var margin = view.Margin;

            if (bounds.Bottom < 0 || bounds.Top > height || bounds.Right < 0 || bounds.Left > width)
                return;

            if (Orientation == StackOrientation.Vertical)
            {
                CalcVector(y, r, height, bounds.Top, bounds.Bottom, margin.Left, margin.Right, bounds.Width, v => bounds.X = v, v => bounds.Width = v);
            }
            else
            {
                CalcVector(x, r, width, bounds.Left, bounds.Right, margin.Top, margin.Bottom, bounds.Height, v => bounds.Y = v, v => bounds.Height = v);
            }

            if (bounds != view.Bounds)
                view.Layout(bounds);
        }

        double CircleV(double v, double r) => Math.Sqrt(r * r - (v-r) * (v-r));

        double CalcGap(View view, double r)
        {
            if (view == null) return 0;
            var m = view.Measure(r, r);
            var minimum = Orientation == StackOrientation.Vertical ? m.Minimum.Width : m.Minimum.Height;
            return r - Math.Sqrt(Math.Pow(r, 2) - Math.Pow(minimum / 2, 2));
        }
    }
}
