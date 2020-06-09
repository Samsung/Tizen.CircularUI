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

[assembly: ExportRenderer(typeof(LineChartView), typeof(LineChartViewRenderer))]

namespace Tizen.Wearable.CircularUI.Chart.Forms.Renderer
{
    public class LineChartViewRenderer : ViewRenderer<LineChartView, SkiaSharp.Views.Tizen.SKCanvasView>
    {
        const float AxisLineSize = 2;
        const float TextHorizontalMargin = 5;
        const float TextVerticalMargin = 8;

        SKCanvas _canvas;
        SKSize _canvasSize;
        int _categoryLabelCount;
        int _dataCount;
        int _lineCount;
        float _topMargin;

        SKSize _majorAxisSize;
        SKSize _minorAxisSize;
        SKSize _referenceItemSize;

        IDataItem[,] _dataTable;

        public LineChartViewRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<LineChartView> e)
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
                e.PropertyName == LineChartView.LineModeProperty.PropertyName ||
                e.PropertyName == LineChartView.LineWidthProperty.PropertyName ||
                e.PropertyName == LineChartView.AxisOptionProperty.PropertyName ||
                e.PropertyName == LineChartView.PointIsVisibleProperty.PropertyName ||
                e.PropertyName == LineChartView.PointRadiusProperty.PropertyName)
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
                Log.Debug(FormsCircularUIChart.Tag,"Line Chart Element == null || Element.Data == null");
                return;
            }

            CalculateAxisSize();

            var itemSize = CalculateItemSize();
            if (Element.AxisOption.IsVisibleOfMajorAxisLine)
            {
                DrawMajorAxisLine(canvas);
            }

            if (_categoryLabelCount > 0)
            {
                DrawCategoryLabels(canvas, itemSize);
            }

            if (Element.AxisOption.IsVisibleOfMinorAxisLine)
            {
                DrawMinorAxisLine(canvas);
            }

            if (Element.AxisOption.IsVisibleOfReferenceLabel)
            {
                DrawReferenceLabels(canvas, itemSize);
            }

            if (Element.AxisOption.IsVisibleOfReferenceLine)
            {
                DrawReferenceLines(canvas, itemSize);
            }

            GenerateDataTable();
            for (int i = 0; i < _lineCount; i++)
            {
                var points = CalculatePoints(i, itemSize);
                DrawLines(i, canvas, points, itemSize);
                DrawForegroundArea(i, canvas, points, itemSize);
                DrawValueLabels(i, canvas, points);
                if (Element.PointIsVisible)
                {
                    DrawPoints(i, canvas, points);
                }
            }
        }

        protected virtual void GenerateDataTable()
        {
            _dataTable = new IDataItem[_lineCount, _dataCount];
            for (int i = 0; i < _lineCount; i++)
            {
                var items = Element.Data.DataItemGroups[i].DataItems;
                for (int j = 0; j < _dataCount; j++)
                {
                    if( j >= items.Count)
                    {
                        _dataTable[i, j] = null;
                    }
                    else
                    {
                        _dataTable[i, j] = items[j] as IDataItem;
                    }
                }
            }
        }

        protected void CalculateAxisSize()
        {
            if (Element.AxisOption.IsVisibleOfMajorAxisLine)
            {
                _majorAxisSize.Height = AxisLineSize;
            }

            if (Element.AxisOption.IsVisibleOfMinorAxisLine)
            {
                _minorAxisSize.Width = AxisLineSize;
            }

            CalculateCategoryItemSize();

            if (Element.AxisOption.IsVisibleOfReferenceLabel)
            {
                CalculateReferenceItemSize();
            }
        }

        private IEnumerable<SKRect> MeasureCategoryLabels()
        {
            _categoryLabelCount = 0;
            return Element.Data.DataItemGroups[0].DataItems.Select(e =>
            {
                using (var paint = new SKPaint())
                {
                    if (e == null || e.Label == null || string.IsNullOrEmpty(e.Label.Text))
                    {
                        return SKRect.Empty;
                    }

                    _categoryLabelCount++;
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
            float maxLabelWidth = 0;
            float maxLabelHeight = 0;
            var categoryLabelSizes = MeasureCategoryLabels();
            if (categoryLabelSizes.Count() > 0)
            {
                maxLabelWidth = categoryLabelSizes.Max(x => x.Width);
                maxLabelHeight = categoryLabelSizes.Max(x => x.Height);
            }

            var lineSize = Element.AxisOption.IsVisibleOfMajorAxisLine ? AxisLineSize : 0;
            _majorAxisSize.Width = _canvasSize.Width - _minorAxisSize.Width;
            _majorAxisSize.Height = maxLabelHeight == 0 ? maxLabelHeight + lineSize : maxLabelHeight + TextVerticalMargin * 2 + lineSize;
            if (Element.AxisOption.IsVisibleOfReferenceLabel)
            {
                _minorAxisSize.Height = _canvasSize.Height - _majorAxisSize.Height;
            }
        }

        private IEnumerable<SKRect> MeasureReferenceLabels()
        {
            return Element.AxisOption.ReferenceDataItems.Select(e =>
            {
                using (var paint = new SKPaint())
                {
                    if (string.IsNullOrEmpty(e.ValueLabel.Text))
                    {
                        return SKRect.Empty;
                    }

                    var bounds = new SKRect();
                    var text = e.ValueLabel.Text;
                    paint.TextSize = (float)(XForms.ConvertToEflFontPoint(e.ValueLabel.FontSize));
                    paint.MeasureText(text, ref bounds);
                    return bounds;
                }
            });
        }

        void CalculateReferenceItemSize()
        {
            var referenceLabelSizes = MeasureReferenceLabels();
            var maxLabelWidth = referenceLabelSizes.Max(x => x.Width);
            var maxLabelHeight = referenceLabelSizes.Max(x => x.Height);
            var lineSize = Element.AxisOption.IsVisibleOfMinorAxisLine ? AxisLineSize : 0;
            _minorAxisSize.Width = maxLabelWidth + TextHorizontalMargin * 2 + lineSize;
            _majorAxisSize.Width = _majorAxisSize.Width - _minorAxisSize.Width + lineSize;
            _referenceItemSize = new SKSize(maxLabelWidth, maxLabelHeight);
        }

        SKSize CalculateItemSize()
        {
            var dataSetCount = Element.Data.DataItemGroups.Count;
            _lineCount = Math.Min(dataSetCount, 3);
            _dataCount = 0;

            for (int i = 0; i < _lineCount; i++)
            {
                var itemCount = Element.Data.DataItemGroups[i].DataItems?.Count ?? 0;
                _dataCount = Math.Max(_dataCount, itemCount);
            }

            _topMargin = Element.PointIsVisible ? (float)Element.PointRadius : 0;
            var w = _dataCount != 0 ? (int)(_canvasSize.Width - _minorAxisSize.Width) / _dataCount : _canvasSize.Width - _minorAxisSize.Width;
            var h = _canvasSize.Height - _majorAxisSize.Height - _topMargin;
            return new SKSize(w, h);
        }

        protected virtual IList<SKPoint> CalculatePoints(int lineIndex, SKSize itemSize)
        {
            var result = new List<SKPoint>();
            float yAxisWidth = _minorAxisSize.Width;
            for (int i = 0; i < _dataCount; i++)
            {
                var x = yAxisWidth + (itemSize.Width / 2) + itemSize.Width * i;
                double value = _dataTable[lineIndex, i] != null ? Math.Abs(_dataTable[lineIndex, i].Value) : 0;
                var y = value >= Element.Maximum ? _topMargin : (float)(((Element.Maximum - value) / Element.ValueRange) * itemSize.Height + _topMargin);
                var point = new SKPoint(x, y);
                result.Add(point);
            }

            return result;
        }

        protected void DrawMajorAxisLine(SKCanvas canvas)
        {
            var x = _minorAxisSize.Width;
            var y = _canvasSize.Height - _majorAxisSize.Height;
            var w = _canvasSize.Width - _minorAxisSize.Width;
            var h = AxisLineSize;
            canvas.DrawAxisLine(SKRect.Create(x, y, w, h), Element.AxisOption.AxisLineColor);
        }

        protected void DrawMinorAxisLine(SKCanvas canvas)
        {
            var x = _minorAxisSize.Width - AxisLineSize;
            var y = 0;
            var w = AxisLineSize;
            var h = _canvasSize.Height - _majorAxisSize.Height + AxisLineSize;
            canvas.DrawAxisLine(SKRect.Create(x, y, w, h), Element.AxisOption.AxisLineColor);
        }

        protected virtual void DrawCategoryLabels(SKCanvas canvas, SKSize itemSize)
        {
            for (int i = 0; i < _dataCount; i++)
            {
                var dataItem = Element.Data.DataItemGroups[0].DataItems[i];
                var label = dataItem?.Label;
                if (label == null || string.IsNullOrEmpty(label.Text))
                    continue;

                var x = _minorAxisSize.Width + (itemSize.Width / 2) + itemSize.Width * i;
                var y = _canvasSize.Height - (_majorAxisSize.Height / 2);
                canvas.DrawText(x, y, label);
            }
        }

        protected void DrawReferenceLabels(SKCanvas canvas, SKSize itemSize)
        {
            float x = 0;
            float y = 0;
            bool isEndOfBoundX = false;
            bool isEndOfBoundY = false;
            var refCount = Element.AxisOption.ReferenceDataItems.Count;

            for (int i = 0; i < refCount; i++)
            {
                var data = Element.AxisOption.ReferenceDataItems[i];
                y = (float)(((Element.Maximum - data.Value) / Element.ValueRange) * itemSize.Height) + _topMargin;
                if (y < _referenceItemSize.Height) //max value
                {
                    isEndOfBoundY = true;
                }
                else if (y >= _minorAxisSize.Height) //min value
                {
                    y = _minorAxisSize.Height - _referenceItemSize.Height / 2;
                }

                x = _minorAxisSize.Width / 2;
                canvas.DrawText(x, y, data.ValueLabel, isEndOfBoundX, isEndOfBoundY);
            }
        }

        protected void DrawReferenceLines(SKCanvas canvas, SKSize itemSize)
        {
            var refCount = Element.AxisOption.ReferenceDataItems.Count;
            float lineSize = 1;

            for (int i = 0; i < refCount; i++)
            {
                var data = Element.AxisOption.ReferenceDataItems[i];
                if (data.Value == Element.Minimum)  //skip min value.
                    continue;

                var startX = _minorAxisSize.Width;
                var endX = _canvasSize.Width;
                var startY = (float)(((Element.Maximum - data.Value) / Element.ValueRange) * itemSize.Height) + _topMargin;
                startY = startY == 0 ? lineSize : startY;
                var endY = startY;
                canvas.DrawReferenceLine(startX, startY, endX, endY, lineSize, Color.Cyan, ReferenceLineMode.Dashed);
            }
        }

        protected void DrawForegroundArea(int lineIndex, SKCanvas canvas, IList<SKPoint> points, SKSize itemSize)
        {
            int prevIndex = -1;
            SKPoint last = new SKPoint(0, 0);
            DataItemGroup dataSet = Element.Data.DataItemGroups[lineIndex];
            var lineDataSet = dataSet as LineDataItemGroup;
            if(lineDataSet == null || lineDataSet?.ForegroundColor == Color.Transparent )
            {
                return;
            }

            var color = lineDataSet.ForegroundColor.ToSKColor();
            using (var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = color,
                IsAntialias = true,
            })
            {
                var path = new SKPath();
                path.MoveTo(points.First().X, _canvasSize.Height - _majorAxisSize.Height);
                path.LineTo(points.First());
                for (int i = 0; i < points.Count; i++)
                {
                    var dataItem = _dataTable[lineIndex, i];
                    if (dataItem == null)
                        continue;

                    if (last.X < points[i].X) last = points[i];
                    if (Element.LineMode == LineMode.Spline)
                    {
                        if (prevIndex == -1)
                        {
                            prevIndex = i;
                            continue;
                        }

                        var cubicInfo = CalculateCubicPoints(points[prevIndex], points[i], itemSize);
                        path.CubicTo(cubicInfo.midPrev, cubicInfo.midCurrent, cubicInfo.currentPoint);
                        prevIndex = i;
                    }
                    else if (Element.LineMode == LineMode.Straight)
                    {
                        path.LineTo(points[i]);
                    }
                }

                path.LineTo(last.X, _canvasSize.Height - _majorAxisSize.Height);
                path.Close();
                canvas.DrawPath(path, paint);
            }
        }

        private (SKPoint midPrev, SKPoint midCurrent, SKPoint currentPoint) CalculateCubicPoints(SKPoint prevPoint, SKPoint currentPoint, SKSize itemSize)
        {
            var offset = new SKPoint(itemSize.Width * 0.8f, 0);
            var midPoitPrev = prevPoint + offset;
            var midPointCurrent = currentPoint - offset;
            return (midPoitPrev, midPointCurrent, currentPoint);
        }

        protected void DrawLines(int lineIndex, SKCanvas canvas, IList<SKPoint> points, SKSize itemSize)
        {
            if (points.Count == 0) 
                return;

            bool hasEntryColor = false;
            int prevIndex = -1;
            SKPoint first = new SKPoint(0, 0);
            SKPoint last = new SKPoint(0, 0);
            DataItemGroup dataSet = Element.Data.DataItemGroups[lineIndex];
            Color dataSetColor = dataSet.Color != Color.Default ? dataSet.Color : Color.White;

            using (var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = (float)Element.LineWidth,
                IsAntialias = true,
                Color = dataSetColor.ToSKColor()
            })
            {
                var path = new SKPath();
                path.MoveTo(points.First());
                for (int i = 0; i < points.Count; i++)
                {
                    var dataItem = _dataTable[lineIndex, i];
                    if (dataItem == null)
                        continue;

                    if (first.X == 0) first = points[i];
                    if (last.X < points[i].X) last = points[i];

                    if (dataItem.Color != Color.Default && dataSetColor != dataItem.Color)
                    {
                        hasEntryColor = true;
                    }

                    if (Element.LineMode == LineMode.Spline)
                    {
                        if (prevIndex == -1)
                        {
                            prevIndex = i;
                            continue;
                        }

                        var cubicInfo = CalculateCubicPoints(points[prevIndex], points[i], itemSize);
                        path.CubicTo(cubicInfo.midPrev, cubicInfo.midCurrent, cubicInfo.currentPoint);
                        prevIndex = i;
                    }
                    else if (Element.LineMode == LineMode.Straight)
                    {
                        path.LineTo(points[i]);
                    }
                }

                if (hasEntryColor)
                {
                    var colors = new List<SKColor>();
                    foreach (IDataItem DataItem in dataSet.DataItems)
                    {
                        if(DataItem == null)
                        {
                            continue;
                        }

                        var LineColor = DataItem.Color != Color.Default ? DataItem.Color : Color.White;
                        colors.Add(LineColor.ToSKColor());
                    }

                    paint.Shader = SKShader.CreateLinearGradient(first, last, colors.ToArray(), null, SKShaderTileMode.Clamp);
                }

                canvas.DrawPath(path, paint);
            }
        }

        protected void DrawPoints(int lineIndex, SKCanvas canvas, IList<SKPoint> points)
        {
            DataItemGroup dataSet = Element.Data.DataItemGroups[lineIndex];
            Color dataSetColor = dataSet?.Color != Color.Default ? dataSet?.Color ?? Color.White : Color.White;
            for (int i = 0; i < points.Count; i++)
            {
                var dataItem = _dataTable[lineIndex, i];
                var point = points[i];
                if (dataItem == null)
                    continue;

                var color = dataItem.Color != Color.Default ? dataItem.Color : dataSetColor;
                canvas.DrawLinePoint(point, color.ToSKColor(), (float)Element.PointRadius);
            }
        }

        protected void DrawValueLabels(int lineIndex, SKCanvas canvas, IList<SKPoint> points)
        {
            bool endOfBoundY = false;
            for (int i = 0; i < _dataCount; i++)
            {
                var dataItem = _dataTable[lineIndex, i];
                if (dataItem == null)
                    continue;

                if (dataItem.ValueLabel != null && !string.IsNullOrEmpty(dataItem.ValueLabel.Text))
                {
                    var x = points[i].X;
                    var y = points[i].Y - TextVerticalMargin - (float)(Element.PointIsVisible? Element.PointRadius : 0);
                    if (y <= _topMargin + (float)XForms.ConvertToScaledPixel(dataItem.ValueLabel.FontSize))
                    {
                        endOfBoundY = true;
                        y = points[i].Y + TextVerticalMargin + (float)(Element.PointIsVisible ? Element.PointRadius : 0);
                    }

                    canvas.DrawText(x, y, dataItem.ValueLabel, false, endOfBoundY);
                }
            }
        }

        private void OnDrawChartRequested(object sender, EventArgs e)
        {
            Control?.Invalidate();
        }
    }
}
