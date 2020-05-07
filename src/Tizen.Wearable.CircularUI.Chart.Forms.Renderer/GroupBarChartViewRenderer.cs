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
using SkiaSharp;
using System.Linq;
using System.Collections.Generic;
using XForms = Xamarin.Forms.Forms;
using CLabel = Tizen.Wearable.CircularUI.Chart.Forms.Label;

[assembly: ExportRenderer(typeof(GroupBarChartView), typeof(GroupBarChartViewRenderer))]

namespace Tizen.Wearable.CircularUI.Chart.Forms.Renderer
{
    public class GroupBarChartViewRenderer : BarChartViewRenderer
    {
        int _dataSetArrayCount;
        int _groupDataCount;
        int _maxEntriesCount;
        float _groupBarMargin;
        double[,] _groupBarChartDataTable;
        SKColor[] _barColor;
        SKColor[] _barBackgroundColor;
        IList<DataSet> _prevDataSetArray;

        public GroupBarChartViewRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<BarChartView> e)
        {
            base.OnElementChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void DrawContent(SKCanvas canvas)
        {
            if (Element == null || Element.DataSetArray == null)
            {
                Log.Debug(FormsCircularUIChart.Tag, "GroupBar Chart Element == null || Element.DataSetArray == null");
                return;
            }

            if (Element.AxisOption != null)
            {
                base.CaculateAxisSize(canvas);
            }
            else
            {
                _majorAxisSize = new SKSize(0, 0);
                _minorAxisSize = new SKSize(0, 0);
                _referenceItemSize = new SKSize(0, 0);
            }

            var barSize = this.CalculateBarSize();
            GenerateDataTable();
            var points = CalculatePoints(barSize);
            if (Element.AxisOption.IsVisibleOfMajorAxisLine)
            {
                base.DrawMajorAxisLine(canvas);
            }

            if (Element.AxisOption.IsVisibleOfCategoryLabel)
            {
                DrawCategoryLabels(canvas, points);
            }

            if (Element.AxisOption.IsVisibleOfMinorAxisLine)
            {
                base.DrawMinorAxisLine(canvas);
            }

            if (Element.AxisOption.IsVisibleOfReferenceLabel)
            {
                base.DrawReferenceLabels(canvas, barSize);
            }

            if (Element.BarBackgroundColorIsVisible)
            {
                DrawGroupBarBackground(canvas, points, barSize);
            }

            if (Element.AxisOption.IsVisibleOfReferenceLine)
            {
                base.DrawReferenceLines(canvas, barSize);
            }

            DrawGroupBars(canvas, points, barSize);
        }

        int GetMaxEntriesCount()
        {
            int entriesMaxCount = 0;
            for (int i = 0; i < _dataSetArrayCount; i++)
            {
                var dataSet = Element.DataSetArray[i];
                int entryCount = dataSet.Entries.Count();
                if (entriesMaxCount < entryCount)
                    entriesMaxCount = entryCount;
            }

            return entriesMaxCount;
        }

        protected override void GenerateDataTable()
        {
            if (Element.DataSetArray == _prevDataSetArray)
                return;

            _groupBarChartDataTable = new double[_groupDataCount, _categoryCount];
            for (int i = 0; i < _groupDataCount; i++)
            {
                if (Element.DataSetArray.Count <= i) break;
                var entries = Element.DataSetArray[i].Entries;
                for (int j = 0; j < entries.Count; j++)
                {
                    var index = entries[j].Key == 0 ? j : entries[j].Key - 1;
                    index = index < _categoryCount ? index : _categoryCount - 1;
                    _groupBarChartDataTable[i, index] = entries[j].Value;
                }
            }

            _prevDataSetArray = Element.DataSetArray;
        }

        protected override SKSize CalculateBarSize()
        {
            var groupChart = Element as GroupBarChartView;
            _groupDataCount = groupChart != null ? groupChart.GroupDataCount : 1;
            _dataSetArrayCount = Element.DataSetArray.Count();
            float barWidth = (float)XForms.ConvertToScaledPixel(Element.BarWidth);
            _groupBarMargin = groupChart != null ? (float)XForms.ConvertToScaledPixel(groupChart.GroupBarMargin) : 0;
            float barHeight = 0;
            int totalDataCount = 0;

            _maxEntriesCount = GetMaxEntriesCount();
            if (Element.AxisOption.IsVisibleOfCategoryLabel)
            {
                _categoryLabelCount = Element.AxisOption.CategoryLabels.Count();
                _categoryCount = _categoryLabelCount > _maxEntriesCount ? _categoryLabelCount : _maxEntriesCount;
                totalDataCount = _categoryCount * _groupDataCount;
            }
            else
            {
                _categoryCount = _maxEntriesCount;
                totalDataCount = _maxEntriesCount * _groupDataCount;
            }

            if (Element.BarChartType == BarChartType.Vertical)
            {
                var spareHSize = _canvasSize.Width - _minorAxisSize.Width - (barWidth * totalDataCount) - (_groupBarMargin * _categoryCount * (_groupDataCount - 1));
                if (spareHSize < 0)
                {
                    var tmpbarWidth = (int)(_canvasSize.Width - _minorAxisSize.Width) / totalDataCount;
                    _groupBarMargin = 0;
                    if (tmpbarWidth < barWidth)
                    {
                        barWidth = tmpbarWidth;
                        _barHmargin = 0;
                    }
                    else
                    {
                        spareHSize = _canvasSize.Width - _minorAxisSize.Width - (barWidth * totalDataCount);
                        _barHmargin = (int)(spareHSize / (_categoryCount + 1));
                    }
                }
                else
                {
                    _barHmargin = (int)(spareHSize / (_categoryCount + 1));
                }

                barHeight = _canvasSize.Height - _majorAxisSize.Height;
            }
            else
            {
                var spareVSize = _canvasSize.Height - _minorAxisSize.Height - (barWidth * totalDataCount) - (_groupBarMargin * _categoryCount * (_groupDataCount - 1));
                if (spareVSize < 0)
                {
                    var tmpbarWidth = (int)(_canvasSize.Height - _minorAxisSize.Height) / totalDataCount;
                    _groupBarMargin = 0;
                    if (tmpbarWidth < barWidth)
                    {
                        barWidth = tmpbarWidth;
                        _barVmargin = 0;
                    }
                    else
                    {
                        spareVSize = _canvasSize.Height - _minorAxisSize.Height - (barWidth * totalDataCount);
                        _barVmargin = (int)(spareVSize / (_categoryCount + 1));
                    }
                }
                else
                {
                    _barVmargin = (int)(spareVSize / (_categoryCount + 1));
                }

                barHeight = _canvasSize.Width - _majorAxisSize.Width;
            }

            if (barWidth != (float)XForms.ConvertToScaledPixel(Element.BarWidth))
            {
                Element.BarWidth = XForms.ConvertToScaledDP(barWidth);
            }

            return new SKSize(barWidth, barHeight);
        }

        protected override SKPoint[] CalculatePoints(SKSize barSize)
        {
            var result = new List<SKPoint>();
            float yAxisWidth = Element.BarChartType == BarChartType.Vertical ? _minorAxisSize.Width : _majorAxisSize.Width;
            var groupOffset = barSize.Width * _groupDataCount + _groupBarMargin * (_groupDataCount - 1);
            if (Element.BarChartType == BarChartType.Vertical)
            {
                for (int i = 0; i < _categoryCount; i++)
                {
                    for (int j = 0; j < _groupDataCount; j++)
                    {
                        var value = _groupBarChartDataTable[j, i];
                        var x = yAxisWidth + _barHmargin * (i + 1) + (barSize.Width / 2) + groupOffset * i + (barSize.Width + _groupBarMargin) * j;
                        var y = value >= Element.Maximum ? 0 : (float)(((Element.Maximum - value) / Element.ValueRange) * barSize.Height);
                        var point = new SKPoint(x, y);
                        result.Add(point);
                    }
                }
            }
            else
            {
                for (int i = 0; i < _categoryCount; i++)
                {
                    for (int j = 0; j < _groupDataCount; j++)
                    {
                        var value = _groupBarChartDataTable[j, i];
                        var y = _barVmargin * (i + 1) + (barSize.Width / 2) + groupOffset * i + (barSize.Width + _groupBarMargin) * j;
                        var x = value >= Element.Maximum ? yAxisWidth + barSize.Height : yAxisWidth + (float)(((value - Element.Minimum) / Element.ValueRange) * barSize.Height);
                        var point = new SKPoint(x, y);
                        result.Add(point);
                    }
                }
            }

            return result.ToArray();
        }

        protected override void DrawCategoryLabels(SKCanvas canvas, SKPoint[] points)
        {
            bool IsVertical = Element.BarChartType == BarChartType.Vertical ? true : false;
            float x = 0;
            float y = 0;
            CLabel label;

            for (int i = 0; i < _categoryLabelCount; i++)
            {
                var categoryLabel = Element.AxisOption.CategoryLabels.ElementAt(i);
                label = categoryLabel.Label;
                if (string.IsNullOrEmpty(label.Text))
                {
                    continue;
                }

                int categoryStartIndex = (categoryLabel.Key - 1) * _groupDataCount;
                if (IsVertical)
                {
                    x = (points[categoryStartIndex].X + points[categoryStartIndex + _groupDataCount - 1].X) / 2;
                    y = _canvasSize.Height - (_majorAxisSize.Height / 2);
                }
                else
                {
                    x = _majorAxisSize.Width / 2;
                    y = (points[categoryStartIndex].Y + points[categoryStartIndex + _groupDataCount - 1].Y) / 2;
                }

                canvas.DrawText(x, y, label);
            }
        }

        private void DrawGroupBarBackground(SKCanvas canvas, SKPoint[] points, SKSize barSize)
        {
            var halfWidth = barSize.Width / 2;
            var topRadius = (float)Element.BarTopRadius > halfWidth ? halfWidth : (float)Element.BarTopRadius;
            var totalBarCount = _categoryCount * _groupDataCount;
            bool IsVertical = Element.BarChartType == BarChartType.Vertical ? true : false;
            SKColor bgColor;
            SKRect rect;

            if (points.Length > 0)
            {
                GetBarBackgroundColor();
                for (int i = 0; i < totalBarCount; i++)
                {
                    var point = points[i];
                    var r = i % _groupDataCount;
                    bgColor = _barBackgroundColor[r];

                    using (var paint = new SKPaint
                    {
                        Style = SKPaintStyle.Fill,
                        Color = bgColor,
                    })
                    {
                        var x = IsVertical ? point.X - (barSize.Width / 2) : _majorAxisSize.Width;
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
                }
            }
        }

        protected void DrawGroupBars(SKCanvas canvas, SKPoint[] points, SKSize barSize)
        {
            var halfWidth = barSize.Width / 2;
            var topRadius = (float)Element.BarTopRadius > halfWidth ? halfWidth : (float)Element.BarTopRadius;
            var bottomRadius = (float)Element.BarBottomRadius > halfWidth ? halfWidth : (float)Element.BarBottomRadius;
            var totalBarCount = _categoryCount * _groupDataCount;
            bool IsVertical = Element.BarChartType == BarChartType.Vertical ? true : false;
            float x = 0;
            float y = 0;
            float w = 0;
            float h = 0;
            SKRect rect;
            bool isValueOverRadius = false;

            if (points.Length > 0)
            {
                GetBarColor();
                for (int i = 0; i < totalBarCount; i++)
                {
                    isValueOverRadius = false;
                    var point = points[i];
                    if ((IsVertical && point.Y == _canvasSize.Height - _majorAxisSize.Height) ||
                        (!IsVertical && point.X == _majorAxisSize.Width))
                        continue;

                    var r = i % _groupDataCount;
                    var barColor = _barColor[r];
                    using (var paint = new SKPaint
                    {
                        Style = SKPaintStyle.Fill,
                        Color = barColor,
                    })
                    {
                        if (IsVertical)
                        {
                            x = point.X - (barSize.Width / 2);
                            y = topRadius > 0 ? point.Y + topRadius : point.Y;
                            var yOrigin = _canvasSize.Height - _majorAxisSize.Height;
                            if (y > yOrigin && topRadius > 0)
                                isValueOverRadius = true;
                            yOrigin = bottomRadius > 0 ? yOrigin - bottomRadius : yOrigin;
                            h = Math.Abs(yOrigin - y);
                            rect = SKRect.Create(x, y, barSize.Width, h);
                        }
                        else
                        {
                            x = topRadius > 0 ? point.X - topRadius : point.X;
                            y = point.Y - (barSize.Width / 2);
                            var xOrigin = _majorAxisSize.Width;
                            if (x <= xOrigin && topRadius > 0)
                                isValueOverRadius = true;
                            xOrigin = bottomRadius > 0 ? xOrigin + bottomRadius : xOrigin;
                            w = Math.Abs(x - xOrigin);
                            h = barSize.Width;
                            rect = SKRect.Create(xOrigin, y, w, h);
                        }

                        if (!isValueOverRadius)
                            canvas.DrawRect(rect, paint);

                        if (topRadius > 0 && !isValueOverRadius)
                        {
                            if (IsVertical)
                            {
                                y = point.Y;
                                h = topRadius * 2;
                                rect = SKRect.Create(x, y, barSize.Width, h);
                            }
                            else
                            {
                                x = point.X - topRadius * 2;
                                w = topRadius * 2;
                                rect = SKRect.Create(x, y, w, h);
                            }

                            canvas.DrawRoundRect(rect, topRadius, topRadius, paint);
                        }

                        if (bottomRadius > 0)
                        {
                            if (IsVertical)
                            {
                                y = _canvasSize.Height - _majorAxisSize.Height - bottomRadius * 2;
                                h = bottomRadius * 2;
                                rect = SKRect.Create(x, y, barSize.Width, h);
                            }
                            else
                            {
                                x = _majorAxisSize.Width;
                                w = +bottomRadius * 2;
                                rect = SKRect.Create(x, y, w, h);
                            }

                            canvas.DrawRoundRect(rect, bottomRadius, bottomRadius, paint);
                        }
                    }
                }
            }
        }

        private void GetBarColor()
        {
            SKColor barColor = SkiaSharp.Views.Forms.Extensions.ToSKColor(BarDefaultColor);
            _barColor = new SKColor[_groupDataCount];

            for (int i = 0; i < _groupDataCount; i++)
            {
                if (_dataSetArrayCount > i)
                {
                    var dataSet = Element.DataSetArray[i];
                    if (dataSet != null && dataSet.Color != Color.Default)
                    {
                        var color = dataSet.Color;
                        barColor = SkiaSharp.Views.Forms.Extensions.ToSKColor(color);
                    }
                }

                _barColor[i] = barColor;
            }
        }

        private void GetBarBackgroundColor()
        {
            Color color = BarDefaultColor;
            SKColor bgColor = SkiaSharp.Views.Forms.Extensions.ToSKColor(color).WithAlpha(BarBackgroundAlpha);
            _barBackgroundColor = new SKColor[_groupDataCount];

            for (int i = 0; i < _groupDataCount; i++)
            {
                if (_dataSetArrayCount > i)
                {
                    var dataSet = Element.DataSetArray[i];
                    var barDataSet = dataSet as BarDataSet;
                    if (barDataSet != null && barDataSet.BarBackgroundColor != Color.Default)
                    {
                        //If barDataSet is not null and BarBackgroundColor is set. set BarDataSet bg color.
                        color = barDataSet.BarBackgroundColor;
                        bgColor = SkiaSharp.Views.Forms.Extensions.ToSKColor(color);
                    }
                    else
                    {
                        //If barDataSet is null. set DataSet color for Bar bg color.
                        var dataSetColor = dataSet.Color;
                        color = dataSetColor == Color.Default ? color : dataSetColor;
                        bgColor = SkiaSharp.Views.Forms.Extensions.ToSKColor(color).WithAlpha(BarBackgroundAlpha);
                    }
                }

                _barBackgroundColor[i] = bgColor;
            }
        }
    }
}
