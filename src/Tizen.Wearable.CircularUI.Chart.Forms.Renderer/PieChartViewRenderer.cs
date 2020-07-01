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

[assembly: ExportRenderer(typeof(PieChartView), typeof(PieChartViewRenderer))]

namespace Tizen.Wearable.CircularUI.Chart.Forms.Renderer
{
    public class PieChartViewRenderer : ViewRenderer<PieChartView, SkiaSharp.Views.Tizen.SKCanvasView>
    {
        const float TextHorizontalMargin = 5;
        const float LegendBoxTextMargin = 3;
        const float TextVerticalMargin = 3;
        const float BoxScaleMultiple = 0.8f;
        protected const float ChartMargin = 10;
        protected const float ValueTextSize = 4;

        protected SKColor[] Colors = { SKColors.Red, SKColors.Orange, SKColors.Yellow, SKColors.Green, SKColors.Blue, SKColors.Indigo, SKColors.Purple, SKColors.Gray, SKColors.DarkRed, SKColors.Brown, SKColors.SkyBlue, SKColors.Ivory};

        SKCanvas _canvas;
        protected SKSize _canvasSize;
        protected float _legendHeight;
        protected float _legendWidth;

        public PieChartViewRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<PieChartView> e)
        {
            if (Control == null)
            {
                SetNativeControl(new SkiaSharp.Views.Tizen.SKCanvasView(XForms.NativeParent));
                Control.PaintSurface += OnPaintSurface;
            }

            if (e.OldElement != null)
            {
                e.NewElement.DrawChartRequested -= OnDrawChartRequested;
            }

            if (e.NewElement != null)
            {
                e.NewElement.DrawChartRequested += OnDrawChartRequested;
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ChartView.DataProperty.PropertyName ||
                e.PropertyName == ChartView.MaximumProperty.PropertyName ||
                e.PropertyName == ChartView.MinimumProperty.PropertyName ||
                e.PropertyName == PieChartView.ValueLabelFontSizeProperty.PropertyName ||
                e.PropertyName == PieChartView.ValueLabelColorProperty.PropertyName ||
                e.PropertyName == PieChartView.ValueLabelTextFormatProperty.PropertyName ||
                e.PropertyName == PieChartView.ValueLabelIsVisibleProperty.PropertyName ||
                e.PropertyName == PieChartView.LegendPositionProperty.PropertyName )
            {
                Control?.Invalidate();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Element != null)
                {
                    Element.DrawChartRequested -= OnDrawChartRequested;
                }

                if (Control != null)
                {
                    Control.PaintSurface -= OnPaintSurface;
                }
            }

            base.Dispose(disposing);
        }

        private void OnPaintSurface(object sender, SkiaSharp.Views.Tizen.SKPaintSurfaceEventArgs e)
        {
            _canvas = e.Surface.Canvas;
            _canvasSize.Width = e.Info.Width;
            _canvasSize.Height = e.Info.Height;
            _canvas.Clear();
            DrawContent(_canvas);
        }

        protected virtual void DrawContent(SKCanvas canvas)
        {
            if (Element == null || Element.Data == null)
            {
                Log.Debug(FormsCircularUIChart.Tag, "Pie Chart Element == null || Element.Data == null");
                return;
            }

            if (Element.LegendIsVisible == true)
            {
                DrawLegend(canvas);
            }
            else
            {
                _legendWidth = 0;
                _legendHeight = 0;
            }

            DrawChart(canvas);
        }

        protected virtual void DrawChart(SKCanvas canvas)
        {
            var dataItems = Element.Data.DataItemGroups[0].DataItems;
            var totalValue = (float)dataItems.Sum(i => Math.Abs(i?.Value??0));
            var isVerticalLegend = Element.LegendPosition == LegendPosition.Left || Element.LegendPosition == LegendPosition.Right ? true : false;
            var width = isVerticalLegend ? _canvasSize.Width - _legendWidth : _canvasSize.Width;
            var height = isVerticalLegend ? _canvasSize.Height : _canvasSize.Height - _legendHeight;
            var radius = (Math.Min(width, height) - ChartMargin * 2) / 2;

            var x = Element.LegendPosition == LegendPosition.Left ? _legendWidth + width / 2 - radius : width / 2 - radius;
            var y = Element.LegendPosition == LegendPosition.Top ? _legendHeight + height / 2 - radius : height / 2 - radius;
            SKPoint center = new SKPoint(x + radius, y + radius);
            SKRect rect = new SKRect(x, y, x + radius * 2, y + radius * 2);

            float startAngle = -90; //rotate start angle
            int colorIndex = 0;
            foreach (IDataItem item in dataItems)
            {
                if (item == null ) continue;

                if (item.Value == 0)
                {
                    if(item.Label != null &&  !string.IsNullOrEmpty(item.Label.Text))
                    {
                        colorIndex++;
                    }
                    continue;
                }

                float sweepAngle = 360f * (float)item.Value / totalValue;

                using (SKPath path = new SKPath())
                using (SKPaint fillPaint = new SKPaint())
                {
                    path.MoveTo(center);
                    path.ArcTo(rect, startAngle, sweepAngle, false);
                    path.Close();

                    fillPaint.IsAntialias = true;
                    fillPaint.Style = SKPaintStyle.Fill;
                    fillPaint.Color = item.Color != Color.Default ? item.Color.ToSKColor() : Colors[colorIndex++ % 12];

                    // Fill and stroke the path
                    canvas.DrawPath(path, fillPaint);

                    //Draw value text
                    if (Element.ValueLabelIsVisible == true)
                    {
                        DrawValueText(canvas, center, item, 0, radius, startAngle, sweepAngle, totalValue);
                    }
                }

                startAngle += sweepAngle;
            }
        }

        protected virtual void DrawValueText(SKCanvas canvas, SKPoint center, IDataItem item, float innerRadius, float outterRadius, float startAngle, float sweepAngle, float totalSum)
        {
            var textSize = Element.ValueLabelFontSize;
            SKColor textColor = Element.ValueLabelColor.ToSKColor();
            string format = Element.ValueLabelTextFormat;
            string formattedText;
            float x, y;

            if(item.ValueLabel != null)
            {
                //if ValueLabe is null or Empty , do not display value text in Chart.
                if (string.IsNullOrEmpty(item.ValueLabel.Text))
                    return;

                textColor = item.ValueLabel.TextColor.ToSKColor();
                textSize = item.ValueLabel.FontSize;
                formattedText = item.ValueLabel.Text;
            }
            else if(format.Contains("P") || format.Contains("p"))
            {
                var percent = item.Value / totalSum;
                formattedText = String.Format(format, percent);
            }
            else
            {
                formattedText = String.Format(format, item.Value);
            }

            if (String.IsNullOrEmpty(formattedText))
                return;

            var degree = startAngle + sweepAngle / 2;
            double radianAngle = Math.PI * degree / 180.0;
            float textRadius = sweepAngle < 30 ? outterRadius : innerRadius + (outterRadius - innerRadius) / 2;
            float cosAngle = (float)Math.Cos(radianAngle);
            float sinAngle = (float)Math.Sin(radianAngle);
            x = center.X + textRadius * cosAngle;
            y = center.Y + textRadius * sinAngle;

            using (var paint = new SKPaint())
            {
                paint.TextSize = (float)(XForms.ConvertToEflFontPoint(textSize));
                paint.IsAntialias = true;
                paint.Color = textColor;
                paint.IsStroke = false;
                var bounds = new SKRect();
                paint.MeasureText(formattedText, ref bounds);
                canvas.DrawText(formattedText, x - (bounds.Width / 2), y, paint);
            }
        }

        protected virtual void DrawLegend(SKCanvas canvas)
        {
            var dataItems = Element.Data.DataItemGroups[0].DataItems;
            var dataItemCount = dataItems.Count;
            var legendItemSizes = MeasureLegendItemSize(dataItems);
            var maxHeight = legendItemSizes.Max(i => i.Height);
            var maxWidth = legendItemSizes.Max(i => i.Width);
            bool multiLine = false;
            float x = 0;
            float y = 0;
            int colorIndex = 0;

            if (Element.LegendPosition == LegendPosition.Left || Element.LegendPosition == LegendPosition.Right)
            {
                var totalHeight = legendItemSizes.Sum(i => i.Height);
                if (_canvasSize.Height < totalHeight)
                {
                    multiLine = true;
                    _legendWidth = maxWidth * (totalHeight / _canvasSize.Height + 1);
                    _legendHeight = _canvasSize.Height;
                }
                else
                {
                    _legendWidth = maxWidth;
                    _legendHeight = totalHeight;
                }

                if (Element.LegendPosition == LegendPosition.Left)
                {
                    x = TextHorizontalMargin;
                    y = 0;
                }
                else
                {
                    x = _canvasSize.Width - TextHorizontalMargin - maxWidth;
                    y = 0;
                }
            }
            else // LegendPosition.Bottom ||  LegendPosition.Top
            {
                var totalWidth = legendItemSizes.Sum(i => i.Width);
                if (_canvasSize.Width < totalWidth)
                {
                    multiLine = true;
                    _legendWidth = _canvasSize.Width;
                    _legendHeight = maxHeight * (totalWidth / _canvasSize.Width + 1);
                    x = TextHorizontalMargin;
                    y = Element.LegendPosition == LegendPosition.Top ? 0 : _canvasSize.Height - _legendHeight;
                }
                else
                {
                    _legendWidth = totalWidth;
                    _legendHeight = maxHeight;
                    x = (_canvasSize.Width - totalWidth) / 2;
                    y = Element.LegendPosition == LegendPosition.Top ? 0 : _canvasSize.Height - _legendHeight;
                }
            }

            for (int i = 0; i < dataItemCount; i++)
            {
                var item = dataItems[i];
                var labelItem = item?.Label;
                if (item ==  null || labelItem == null || string.IsNullOrEmpty(labelItem.Text))
                    continue;

                var itemSize = legendItemSizes.ElementAt(i);
                if (multiLine)
                {
                    if (Element.LegendPosition == LegendPosition.Bottom && x + itemSize.Width > _canvasSize.Width)
                    {
                        x = TextHorizontalMargin;
                        y = y + maxHeight;
                    }
                    else if (Element.LegendPosition == LegendPosition.Top && x + itemSize.Width > _canvasSize.Width)
                    {
                        x = TextHorizontalMargin;
                        y = y + maxHeight;
                    }
                    else if (Element.LegendPosition == LegendPosition.Left && y + itemSize.Height > _canvasSize.Height)
                    {
                        x = x + maxWidth + TextHorizontalMargin;
                        y = 0;
                    }
                    else if (Element.LegendPosition == LegendPosition.Right && y + itemSize.Height > _canvasSize.Height)
                    {
                        x = x - maxWidth - TextHorizontalMargin;
                        y = 0;
                    }
                }

                //draw color box
                var boxColor = item.Color != default ? item.Color.ToSKColor() : Colors[colorIndex++ % 12];
                var boxSize = (float)(XForms.ConvertToEflFontPoint(labelItem.FontSize)) * BoxScaleMultiple;
                using (var paint = new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    Color = boxColor,
                    IsAntialias = true
                })
                {
                    var rect = SKRect.Create(x, y + maxHeight - boxSize, boxSize, boxSize);
                    canvas.DrawRect(rect, paint);
                }

                //draw category label
                canvas.DrawLegendText(x + boxSize + LegendBoxTextMargin, y + maxHeight, labelItem);

                if (Element.LegendPosition == LegendPosition.Left || Element.LegendPosition == LegendPosition.Right)
                {
                    y = y + itemSize.Height;
                }
                else  //LegendPosition.Bottom || LegendPosition.Top
                {
                    x = x + itemSize.Width;
                }
            }
        }

        IEnumerable<SKRect> MeasureLegendItemSize(IList<IDataItem> dataItems)
        {
            return dataItems.Select(item =>
            {
                using (var paint = new SKPaint())
                {
                    if (item == null)
                    {
                        return SKRect.Empty;
                    }

                    if (item.Label == null || string.IsNullOrEmpty(item.Label.Text))
                    {
                        return SKRect.Empty;
                    }

                    var bounds = new SKRect();
                    var text = item.Label.Text;
                    paint.TextSize = (float)(XForms.ConvertToEflFontPoint(item.Label.FontSize));
                    paint.MeasureText(text, ref bounds);
                    var legendBoxSize = paint.TextSize * BoxScaleMultiple;
                    bounds.Right = bounds.Left + legendBoxSize + LegendBoxTextMargin + bounds.Width + TextHorizontalMargin;
                    bounds.Bottom = bounds.Top + bounds.Height + TextVerticalMargin * 2;
                    return bounds;
                }
            });
        }

        private void OnDrawChartRequested(object sender, EventArgs e)
        {
            Control?.Invalidate();
        }
    }
}
