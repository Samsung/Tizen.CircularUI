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
using Xamarin.Forms.Platform.Tizen;
using Tizen.Wearable.CircularUI.Chart.Forms;
using Tizen.Wearable.CircularUI.Chart.Forms.Renderer;
using SkiaSharp.Views.Tizen;
using SkiaSharp;
using System.Linq;
using System.Collections.Generic;
using XForms = Xamarin.Forms.Forms;
using SkiaSharp.Views.Forms;
using System.ComponentModel;
using System.Globalization;

[assembly: ExportRenderer(typeof(DonutChartView), typeof(DonutChartViewRenderer))]

namespace Tizen.Wearable.CircularUI.Chart.Forms.Renderer
{
    public class DonutChartViewRenderer : PieChartViewRenderer
    {
        public DonutChartViewRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<PieChartView> e)
        {
            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == DonutChartView.ThicknessRatioProperty.PropertyName )
            {
                Control?.Invalidate();
            }
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void DrawContent(SKCanvas canvas)
        {
            if (Element == null || Element.Data == null)
            {
                Log.Debug(FormsCircularUIChart.Tag, "Donut Chart Element == null || Element.Data == null");
                return;
            }

            if (Element.LegendIsVisible == true)
            {
                base.DrawLegend(canvas);
            }
            else
            {
                _legendWidth = 0;
                _legendHeight = 0;
            }

            this.DrawChart(canvas);
        }

        protected override void DrawChart(SKCanvas canvas)
        {
            var dataItems = Element.Data.DataItemGroups[0].DataItems;
            var totalValue = (float)dataItems.Sum(i => Math.Abs(i.Value));
            var isVerticalLegend = Element.LegendPosition == LegendPosition.Left || Element.LegendPosition == LegendPosition.Right ? true : false;
            var width = isVerticalLegend ? _canvasSize.Width - _legendWidth : _canvasSize.Width;
            var height = isVerticalLegend ? _canvasSize.Height : _canvasSize.Height - _legendHeight;
            var radius = (Math.Min(width, height) - ChartMargin * 2) / 2;
            float ratio= (float)((Element as DonutChartView)?.ThicknessRatio ?? 1);
            var innerRadius = radius * (1 - ratio);

            var x = Element.LegendPosition == LegendPosition.Left ? _legendWidth + width / 2 - radius : width / 2 - radius;
            var y = Element.LegendPosition == LegendPosition.Top ? _legendHeight + height / 2 - radius : height / 2 - radius;
            SKPoint center = new SKPoint(x + radius, y + radius);
            SKRect outterRect = new SKRect(x, y, x + radius * 2, y + radius * 2);
            SKRect innerRect = new SKRect(center.X - innerRadius, center.Y - innerRadius, center.X + innerRadius, center.Y + innerRadius);
            float startAngle = -90; //rotate start angle
            int colorIndex = 0;

            foreach (IDataItem item in dataItems)
            {

                float sweepAngle = 360f * (float)item.Value / totalValue;
                double startRadianAngle = Math.PI * startAngle / 180.0;
                double endRadiandAngle = Math.PI * (startAngle + sweepAngle) / 180.0;
                var xStart = center.X + innerRadius * (float)Math.Cos(startRadianAngle);
                var yStart = center.Y + innerRadius * (float)Math.Sin(startRadianAngle);
                var xEnd = center.X + innerRadius * (float)Math.Cos(endRadiandAngle);
                var yEnd = center.Y + innerRadius * (float)Math.Sin(endRadiandAngle);

                using (SKPath path = new SKPath())
                using (SKPaint fillPaint = new SKPaint())
                {
                    path.MoveTo(xStart, yStart);
                    path.ArcTo(outterRect, startAngle, sweepAngle, false);
                    if (ratio != 1f)
                    {
                        path.LineTo(xEnd, yEnd);
                        path.ArcTo(innerRect, startAngle + sweepAngle, -sweepAngle, false); ;
                    }
                    path.Close();

                    fillPaint.IsAntialias = true;
                    fillPaint.Style = SKPaintStyle.Fill;
                    fillPaint.Color = item.Color != Color.Default ? item.Color.ToSKColor() : Colors[colorIndex++ % 12];

                    // Fill and stroke the path
                    canvas.DrawPath(path, fillPaint);

                    //Draw value text
                    if (Element.ValueLabelIsVisible == true)
                    {
                        base.DrawValueText(canvas, center, item, innerRadius, radius, startAngle, sweepAngle, totalValue);
                    }
                }

                startAngle += sweepAngle;
            }
        }
    }
}
