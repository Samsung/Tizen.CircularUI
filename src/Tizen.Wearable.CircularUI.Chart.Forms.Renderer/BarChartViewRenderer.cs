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

[assembly: ExportRenderer(typeof(BarChartView), typeof(BarChartViewRenderer))]

namespace Tizen.Wearable.CircularUI.Chart.Forms.Renderer
{
    public class BarChartViewRenderer : ViewRenderer<BarChartView, SkiaSharp.Views.Tizen.SKCanvasView>
    {
        protected const float LineSize = 2; //temp line size
        protected const byte BarBackgroundAlpha = 0x60;
        protected Color BarDefaultColor = Color.Cyan;

        const float TextHorizontalMargin = 5;
        const float TextVerticalMargin = 8;

        protected SKSize _canvasSize;
        protected int _barHmargin;
        protected int _barVmargin;
        protected int _categoryCount;
        protected int _categoryLabelCount;
        protected int _dataCount;
        protected SKSize _majorAxisSize;
        protected SKSize _minorAxisSize;
        protected SKSize _referenceItemSize;

        SKCanvas _canvas;
        IDataItem[] _barChartDataTable;

        public BarChartViewRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<BarChartView> e)
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
                e.PropertyName == BarChartView.BarChartTypeProperty.PropertyName ||
                e.PropertyName == BarChartView.BarWidthProperty.PropertyName ||
                e.PropertyName == BarChartView.BarTopRadiusProperty.PropertyName ||
                e.PropertyName == BarChartView.BarBottomRadiusProperty.PropertyName ||
                e.PropertyName == BarChartView.AxisOptionProperty.PropertyName ||
                e.PropertyName == GroupBarChartView.GroupBarMarginProperty.PropertyName)
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
                Log.Debug(FormsCircularUIChart.Tag, "Bar Chart Element == null || Element.Data == null");
                return;
            }

            if (Element.AxisOption.IsVisibleOfMajorAxisLine == false &&
                Element.AxisOption.IsVisibleOfMinorAxisLine == false &&
                Element.AxisOption.IsVisibleOfReferenceLabel == false &&
                Element.AxisOption.CategoryLabels == null)
            {
                _majorAxisSize = new SKSize(0, 0);
                _minorAxisSize = new SKSize(0, 0);
                _referenceItemSize = new SKSize(0, 0);
            }
            else
            {
                CalculateAxisSize(canvas);
            }

            var barSize = CalculateBarSize();
            GenerateDataTable();
            var points = CalculatePoints(barSize);
            if (Element.AxisOption.IsVisibleOfMajorAxisLine)
            {
                DrawMajorAxisLine(canvas);
            }

            if (_categoryLabelCount > 0)
            {
                DrawCategoryLabels(canvas, points);
            }

            if (Element.AxisOption.IsVisibleOfMinorAxisLine)
            {
                DrawMinorAxisLine(canvas);
            }

            if (Element.AxisOption.IsVisibleOfReferenceLabel)
            {
                DrawReferenceLabels(canvas, barSize);
            }

            DrawBarBackground(canvas, points, barSize);

            if (Element.AxisOption.IsVisibleOfReferenceLine)
            {
                DrawReferenceLines(canvas, barSize);
            }

            DrawBars(canvas, points, barSize);
            DrawValueLabels(canvas, points, barSize);
        }

        protected virtual void GenerateDataTable()
        {
            _barChartDataTable = new IDataItem[_categoryCount];
            var items = Element.Data.DataItemGroups[0].DataItems;
            for (int i = 0; i < items.Count; i++)
            {
                _barChartDataTable[i] = items[i];
            }
        }

        protected void CalculateAxisSize(SKCanvas canvas)
        {
            if (Element.AxisOption.IsVisibleOfMajorAxisLine)
            {
                _majorAxisSize.Height = LineSize;
            }

            if (Element.AxisOption.IsVisibleOfMinorAxisLine)
            {
                _minorAxisSize.Width = LineSize;
            }

            if (Element.AxisOption.CategoryLabels != null && Element.AxisOption.CategoryLabels.Count() > 0)
            {
                CalculateCategoryItemSize();
            }

            if (Element.AxisOption.IsVisibleOfReferenceLabel)
            {
                CalculateReferenceItemSize();
            }
        }

        private IEnumerable<SKRect> MeasureCategoryLabels()
        {
            return Element.AxisOption.CategoryLabels.Select(e =>
            {
                using (var paint = new SKPaint())
                {
                    if (string.IsNullOrEmpty(e.Label.Text))
                    {
                        return SKRect.Empty;
                    }

                    var bounds = new SKRect();
                    var text = e.Label.Text;
                    paint.TextSize = (float)(XForms.ConvertToEflFontPoint(e.Label.FontSize));
                    paint.MeasureText(text, ref bounds);
                    return bounds;
                }
            });
        }

        private void CalculateCategoryItemSize()
        {
            var categoryLabelSizes = MeasureCategoryLabels();
            var maxLabelWidth = categoryLabelSizes.Max(x => x.Width);
            var maxLabelHeight = categoryLabelSizes.Max(x => x.Height);
            var lineSize = Element.AxisOption.IsVisibleOfMajorAxisLine ? LineSize : 0;
            if (Element.BarChartType == BarChartType.Vertical)
            {
                _majorAxisSize.Width = _canvasSize.Width - _minorAxisSize.Width;
                _majorAxisSize.Height = maxLabelHeight + TextVerticalMargin * 2 + lineSize;
                if (Element.AxisOption.IsVisibleOfReferenceLabel)
                {
                    _minorAxisSize.Height = _canvasSize.Height - _majorAxisSize.Height;
                }
            }
            else
            {
                _majorAxisSize.Width = maxLabelWidth + TextHorizontalMargin * 2 + lineSize;
                _majorAxisSize.Height = _canvasSize.Height - _minorAxisSize.Height;
                if (Element.AxisOption.IsVisibleOfReferenceLabel)
                {
                    _minorAxisSize.Width = _canvasSize.Width - _majorAxisSize.Width;
                }
            }
        }

        private IEnumerable<SKRect> MeasureReferenceLabels()
        {
            return Element.AxisOption.ReferenceDataItems.Select(e =>
            {
                using (var paint = new SKPaint())
                {
                    if (string.IsNullOrEmpty(e.ValueText.Text))
                    {
                        return SKRect.Empty;
                    }

                    var bounds = new SKRect();
                    var text = e.ValueText.Text;
                    paint.TextSize = (float)(XForms.ConvertToEflFontPoint(e.ValueText.FontSize));
                    paint.MeasureText(text, ref bounds);
                    return bounds;
                }
            });
        }

        private void CalculateReferenceItemSize()
        {
            int itemCount = Element.AxisOption.ReferenceDataItems.Count;
            var referenceLabelSizes = MeasureReferenceLabels();
            var maxLabelWidth = referenceLabelSizes.Max(x => x.Width);
            var maxLabelHeight = referenceLabelSizes.Max(x => x.Height);

            var lineSize = Element.AxisOption.IsVisibleOfMinorAxisLine ? LineSize : 0;
            if (Element.BarChartType == BarChartType.Vertical)
            {
                _minorAxisSize.Width = maxLabelWidth + TextHorizontalMargin * 2 + lineSize;
                _majorAxisSize.Width = _majorAxisSize.Width - _minorAxisSize.Width + lineSize;
            }
            else
            {
                _minorAxisSize.Height = maxLabelHeight + TextVerticalMargin * 2 + lineSize;
                _majorAxisSize.Height = _canvasSize.Height - _minorAxisSize.Height;
            }

            _referenceItemSize = new SKSize(maxLabelWidth, maxLabelHeight);
        }

        protected virtual SKSize CalculateBarSize()
        {
            _dataCount = Element.Data.DataItemGroups[0].DataItems.Count();
            float barWidth = (float)XForms.ConvertToScaledPixel(Element.BarWidth);
            float barHeight = 0;

            _categoryLabelCount = Element.AxisOption.CategoryLabels?.Count()?? 0;
            _categoryCount = Math.Max(_categoryLabelCount, _dataCount);
            if (Element.BarChartType == BarChartType.Vertical)
            {
                var spareHSize = _canvasSize.Width - _minorAxisSize.Width - _categoryCount * barWidth;
                if (spareHSize < 0)
                {
                    barWidth = (int)(_canvasSize.Width - _minorAxisSize.Width) / _categoryCount;
                    _barHmargin = 0;
                }
                else
                {
                    _barHmargin = (int)(spareHSize / (_categoryCount + 1));
                }

                barHeight = _canvasSize.Height - _majorAxisSize.Height;
            }
            else
            {
                var spareVSize = _canvasSize.Height - _minorAxisSize.Height - _categoryCount * barWidth;
                if (spareVSize < 0)
                {
                    barWidth = (int)(_canvasSize.Height - _minorAxisSize.Height) / _categoryCount;
                    _barVmargin = 0;
                }
                else
                {
                    _barVmargin = (int)(spareVSize / (_categoryCount + 1));
                }

                barHeight = _canvasSize.Width - _majorAxisSize.Width;
            }

            if(barWidth != (float)XForms.ConvertToScaledPixel(Element.BarWidth))
            {
                Element.BarWidth = XForms.ConvertToScaledDP((double)barWidth);
            }

            return new SKSize(barWidth, barHeight);
        }

        protected virtual IEnumerable<SKPoint> CalculatePoints(SKSize barSize)
        {
            var result = new List<SKPoint>();
            bool isVertical = Element.BarChartType == BarChartType.Vertical;
            float yAxisWidth = isVertical ? _minorAxisSize.Width : _majorAxisSize.Width;

            if (isVertical)
            {
                for (int i = 0; i < _categoryCount; i++)
                {
                    double value = _barChartDataTable[i]?.Value ?? Element.Minimum;
                    value = Math.Min(Math.Max(value, Element.Minimum), Element.Maximum);
                    var x = yAxisWidth + _barHmargin + (barSize.Width / 2) + (barSize.Width + _barHmargin) * i;
                    var y = (float)(((Element.Maximum - value) / Element.ValueRange) * barSize.Height);
                    var point = new SKPoint(x, y);
                    result.Add(point);
                }
            }
            else
            {
                for (int i = 0; i < _categoryCount; i++)
                {
                    double value = _barChartDataTable[i]?.Value?? Element.Minimum;
                    value = Math.Min(Math.Max(value, Element.Minimum), Element.Maximum);
                    var y = _barVmargin + (barSize.Width / 2) + (barSize.Width + _barVmargin) * i;
                    var x = yAxisWidth + (float)(((value - Element.Minimum) / Element.ValueRange) * barSize.Height);
                    var point = new SKPoint(x, y);
                    result.Add(point);
                }
            }

            return result;
        }

        protected void DrawMajorAxisLine(SKCanvas canvas)
        {
            bool IsVertical = Element.BarChartType == BarChartType.Vertical;
            var x = IsVertical ? _minorAxisSize.Width : _majorAxisSize.Width - LineSize;
            var y = IsVertical ? _canvasSize.Height - _majorAxisSize.Height : 0;
            var w = IsVertical ? _canvasSize.Width - _minorAxisSize.Width : LineSize;
            var h = IsVertical ? LineSize : _canvasSize.Height - _minorAxisSize.Height;
            canvas.DrawAxisLine(SKRect.Create(x, y, w, h), Element.AxisOption.AxisLineColor);
        }

        protected virtual void DrawCategoryLabels(SKCanvas canvas, IEnumerable<SKPoint> points)
        {
            bool IsVertical = Element.BarChartType == BarChartType.Vertical;
            float x = 0;
            float y = 0;

            for (int i = 0; i < _categoryLabelCount; i++)
            {
                var categoryLabel = Element.AxisOption.CategoryLabels.ElementAt(i);
                var label = categoryLabel.Label;
                var index = categoryLabel.ItemIndex != -1 ? categoryLabel.ItemIndex : i ;
                if (string.IsNullOrEmpty(label.Text))
                    continue;

                if (IsVertical)
                {
                    x = points.ElementAt(index).X;
                    y = _canvasSize.Height - (_majorAxisSize.Height / 2);
                }
                else
                {
                    x = _majorAxisSize.Width / 2;
                    y = points.ElementAt(index).Y;
                }

                canvas.DrawText(x, y, label);
            }
        }

        protected void DrawMinorAxisLine(SKCanvas canvas)
        {
            bool IsVertical = Element.BarChartType == BarChartType.Vertical;
            var x = IsVertical ? _minorAxisSize.Width - LineSize : _majorAxisSize.Width - LineSize;
            var y = IsVertical ? 0 : _canvasSize.Height - _minorAxisSize.Height;
            var w = IsVertical ? LineSize : _canvasSize.Width - _majorAxisSize.Width + LineSize;
            var h = IsVertical ? _canvasSize.Height - _majorAxisSize.Height + LineSize : LineSize;
            canvas.DrawAxisLine(SKRect.Create(x, y, w, h), Element.AxisOption.AxisLineColor);
        }

        protected void DrawReferenceLabels(SKCanvas canvas, SKSize barSize)
        {
            float x = 0;
            float y = 0;
            bool isEndOfBoundX = false;
            bool isEndOfBoundY = false;
            bool IsVertical = Element.BarChartType == BarChartType.Vertical;
            int refCount = Element.AxisOption.ReferenceDataItems.Count();

            for (int i = 0; i < refCount; i++)
            {
                var data = Element.AxisOption.ReferenceDataItems.ElementAt(i);
                if (IsVertical)
                {
                    y = (float)(((Element.Maximum - data.Value) / Element.ValueRange) * barSize.Height);
                    if (y < _referenceItemSize.Height) //max value
                    {
                        isEndOfBoundY = true;
                    }
                    else if (y >= _minorAxisSize.Height) //min value
                    {
                        y = _minorAxisSize.Height - _referenceItemSize.Height / 2;
                    }

                    x = _minorAxisSize.Width / 2;
                }
                else
                {
                    x = _majorAxisSize.Width + (float)((data.Value - Element.Minimum) / Element.ValueRange) * barSize.Height;
                    if (x + _referenceItemSize.Width > _canvasSize.Width) //max value
                    {
                        x = _canvasSize.Width;
                        isEndOfBoundX = true;
                    }

                    y = _canvasSize.Height - (_minorAxisSize.Height / 2);
                }

                canvas.DrawText(x, y, data.ValueText, isEndOfBoundX, isEndOfBoundY);
            }
        }

        protected void DrawReferenceLines(SKCanvas canvas, SKSize barSize)
        {
            bool IsVertical = Element.BarChartType == BarChartType.Vertical;
            var refCount = Element.AxisOption.ReferenceDataItems.Count();
            float startX = 0;
            float startY = 0;
            float endX = 0;
            float endY = 0;
            float lineSize = 1;

            for (int i = 0; i < refCount; i++)
            {

                var data = Element.AxisOption.ReferenceDataItems.ElementAt(i);
                if (data.Value == Element.Minimum)  //skip min value.
                    continue;

                if (IsVertical)
                {
                    startX = _minorAxisSize.Width;
                    endX = _canvasSize.Width;
                    startY = endY = (float)(((Element.Maximum - data.Value) / Element.ValueRange) * barSize.Height);
                    startY = startY == 0 ? lineSize : startY;
                }
                else
                {
                    startY = 0;
                    endY = _canvasSize.Height - _minorAxisSize.Height;
                    startX = endX = _majorAxisSize.Width + (float)(((data.Value - Element.Minimum) / Element.ValueRange) * barSize.Height);
                }

                canvas.DrawReferenceLine(startX, startY, endX, endY, lineSize, Color.Cyan, ReferenceLineMode.Dashed);
            }
        }

        protected void DrawBarBackground(SKCanvas canvas, IEnumerable<SKPoint> points, SKSize barSize)
        {
            int index = 0;
            var halfWidth = barSize.Width / 2;
            var topRadius = (float)Element.BarTopRadius > halfWidth ? halfWidth : (float)Element.BarTopRadius;
            bool IsVertical = Element.BarChartType == BarChartType.Vertical;
            SKColor barBGColor;
            SKRect rect;

            if (points.Count() > 0)
            {
                var dataSet = Element.Data.DataItemGroups[0];
                var barDataSet = dataSet as BarDataItemGroup;
                for (int i = 0; i < _categoryCount; i++)
                {
                    if (_barChartDataTable[i] == null)
                        continue;

                    barBGColor = SKColor.Empty;
                    var entry = _barChartDataTable[i];
                    var barEntry = entry as BarDataItem;
                    var point = points.ElementAt(i);
                    if (barEntry != null)
                    {
                        if (barEntry.BarBackgroundColor != Color.Transparent)
                        {
                            barBGColor = barEntry.BarBackgroundColor.ToSKColor();
                        }
                    }
                    else if( barDataSet != null && barDataSet.BarBackgroundColor != Color.Transparent)
                    {
                        barBGColor = barDataSet.BarBackgroundColor.ToSKColor();
                    }

                    if (barBGColor == SKColor.Empty)
                        continue;

                    using (var paint = new SKPaint
                    {
                        Style = SKPaintStyle.Fill,
                        Color = barBGColor,
                    })
                    {
                        var x = IsVertical ? point.X - (barSize.Width / 2) : _majorAxisSize.Width ;
                        var y = IsVertical ? 0 : point.Y - (barSize.Width / 2);
                        var w = IsVertical ? barSize.Width : barSize.Height;
                        var h = IsVertical ? barSize.Height : barSize.Width;
                        rect = SKRect.Create(x, y, w, h);
                        if (topRadius > 0)
                        {
                            canvas.DrawRoundRect(rect, topRadius, topRadius, paint);
                        }
                        else
                        {
                            canvas.DrawRect(rect, paint);
                        }
                    }
                    index++;
                }
            }
        }

        protected void DrawBars(SKCanvas canvas, IEnumerable<SKPoint> points, SKSize barSize)
        {
            var halfWidth = barSize.Width / 2;
            var topRadius = (float)Element.BarTopRadius > halfWidth ? halfWidth : (float)Element.BarTopRadius;
            var bottomRadius = (float)Element.BarBottomRadius > halfWidth ? halfWidth : (float)Element.BarBottomRadius;
            bool IsVertical = Element.BarChartType == BarChartType.Vertical;

            if (points.Count() > 0)
            {
                var dataSet = Element.Data.DataItemGroups[0];
                for (int i = 0; i < _categoryCount; i++)
                {
                    if (_barChartDataTable[i] == null)
                        continue;

                    var point = points.ElementAt(i);
                    var entry = _barChartDataTable[i];
                    Color barColor = entry.Color != Color.Default ? entry.Color : dataSet.Color != Color.Default ? dataSet.Color : BarDefaultColor;
                    using (var paint = new SKPaint
                    {
                        Style = SKPaintStyle.Fill,
                        Color = barColor.ToSKColor(),
                    })
                    {
                        if (IsVertical)
                        {
                            DrawVerticalBar(canvas, paint, point, barSize.Width, topRadius, bottomRadius);
                        }
                        else
                        {
                            DrawHorizontalBar(canvas, paint, point, barSize.Width, topRadius, bottomRadius);
                        }
                    }
                }
            }
        }

        protected void DrawVerticalBar(SKCanvas canvas, SKPaint paint, SKPoint point, float barWidth, float topRadius, float bottomRadius)
        {
            bool isRadiusOverAxisLine = false;
            var yOrigin = _canvasSize.Height - _majorAxisSize.Height;
            if (topRadius > 0 && (point.Y + topRadius * 2 > yOrigin))
            {
                isRadiusOverAxisLine = true;
            }
            else if (bottomRadius > 0 && (point.Y + bottomRadius * 2 > yOrigin))
            {
                isRadiusOverAxisLine = true;
            }
            else if (topRadius > 0 &&  bottomRadius > 0 && (point.Y + topRadius + bottomRadius > yOrigin))
            {
                isRadiusOverAxisLine = true;
            }

            if ((topRadius == 0 && bottomRadius == 0) || isRadiusOverAxisLine)
            {
                var x = point.X - (barWidth / 2);
                var y = point.Y;
                var h = Math.Abs(yOrigin - y);
                var rect = SKRect.Create(x, y, barWidth, h);
                canvas.DrawRect(rect, paint);
            }
            else
            {
                var x = point.X - (barWidth / 2);
                var y = topRadius > 0 ? point.Y + topRadius : point.Y;
                yOrigin = bottomRadius > 0 ? yOrigin - bottomRadius : yOrigin;
                var h = Math.Abs(yOrigin - y); ;
                var rect = SKRect.Create(x, y, barWidth, h);
                if (yOrigin > y)
                {
                    canvas.DrawRect(rect, paint);
                }

                //draw top round rect
                if (topRadius > 0)
                {
                    y = point.Y;
                    h = topRadius * 2;
                    rect = SKRect.Create(x, y, barWidth, h);
                    canvas.DrawRoundRect(rect, topRadius, topRadius, paint);
                }

                //draw bottom round rect
                if (bottomRadius > 0)
                {
                    y = _canvasSize.Height - _majorAxisSize.Height - bottomRadius * 2;
                    h = bottomRadius * 2;
                    rect = SKRect.Create(x, y, barWidth, h);
                    canvas.DrawRoundRect(rect, bottomRadius, bottomRadius, paint);
                }
            }
        }

        protected void DrawHorizontalBar(SKCanvas canvas, SKPaint paint, SKPoint point, float barWidth, float topRadius, float bottomRadius)
        {
            bool isRadiusOverAxisLine = false;
            var xOrigin = _majorAxisSize.Width;
            if (topRadius > 0 && (point.X - topRadius * 2 < xOrigin))
            {
                isRadiusOverAxisLine = true;
            }
            else if (bottomRadius > 0 && (point.X - bottomRadius * 2 < xOrigin))
            {
                isRadiusOverAxisLine = true;
            }
            else if (topRadius > 0 && bottomRadius > 0 && (point.X - topRadius - bottomRadius < xOrigin))
            {
                isRadiusOverAxisLine = true;
            }

            if ((topRadius == 0 && bottomRadius == 0) || isRadiusOverAxisLine)
            {
                var x = point.X;
                var y = point.Y - (barWidth / 2);
                var w = Math.Abs(x - xOrigin);
                var h = barWidth;
                var rect = SKRect.Create(xOrigin, y, w, h);
                canvas.DrawRect(rect, paint);
            }
            else
            {
                var x = topRadius > 0 ? point.X - topRadius : point.X;
                var y = point.Y - (barWidth / 2);

                xOrigin = bottomRadius > 0 ? xOrigin + bottomRadius : xOrigin;
                var w = Math.Abs(x - xOrigin);
                var h = barWidth;
                var rect = SKRect.Create(xOrigin, y, w, h);
                if (xOrigin < x)
                {
                    canvas.DrawRect(rect, paint);
                }

                //draw right round rect
                if (topRadius > 0)
                {
                    x = point.X - topRadius * 2;
                    w = topRadius * 2;
                    rect = SKRect.Create(x, y, w, h);
                    canvas.DrawRoundRect(rect, topRadius, topRadius, paint);
                }

                //draw left round rect
                if (bottomRadius > 0)
                {
                    x = _majorAxisSize.Width;
                    w = bottomRadius * 2;
                    rect = SKRect.Create(x, y, w, h);
                    canvas.DrawRoundRect(rect, bottomRadius, bottomRadius, paint);
                }
            }
        }

        protected void DrawValueLabels(SKCanvas canvas, IEnumerable<SKPoint> points, SKSize barSize)
        {
            bool IsVertical = Element.BarChartType == BarChartType.Vertical;
            float x = 0;
            float y = 0;
            bool endOfBoundX = false;
            bool endOfBoundY = false;
            for (int i = 0; i < _categoryCount; i++)
            {
                if (_barChartDataTable[i] == null)
                    continue;

                var entry = _barChartDataTable[i];
                if (entry.ValueText != null && !string.IsNullOrEmpty(entry.ValueText.Text))
                {
                    if (IsVertical)
                    {
                        x = points.ElementAt(i).X;
                        if ((entry as DataItem)?.ValueTextPosition == ValueTextPosition.End)
                        {
                            y = points.ElementAt(i).Y + TextVerticalMargin;
                            endOfBoundY = true;
                        }
                        else 
                        {
                            y = _canvasSize.Height - _majorAxisSize.Height - TextVerticalMargin;
                        }
                    }
                    else
                    {
                        y = points.ElementAt(i).Y;
                        if ((entry as DataItem)?.ValueTextPosition == ValueTextPosition.End)
                        {
                            x = points.ElementAt(i).X - TextHorizontalMargin;
                            endOfBoundX = true;
                        }
                        else
                        {
                            x = _majorAxisSize.Width + TextHorizontalMargin;
                        }
                    }

                    canvas.DrawText(x, y, entry.ValueText, endOfBoundX, endOfBoundY);
                }
            }
        }

        private void OnDrawChartRequested(object sender, EventArgs e)
        {
            Control?.Invalidate();
        }
    }
}
