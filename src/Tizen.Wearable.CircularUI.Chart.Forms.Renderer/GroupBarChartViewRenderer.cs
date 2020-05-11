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
using SkiaSharp.Views.Forms;

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
        IList<DataItemGroup> _prevDataSetArray;

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
            if (Element == null || Element.Data == null)
            {
                Log.Debug(FormsCircularUIChart.Tag, "GroupBar Chart Element == null || Element.Data == null");
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

            if (_categoryLabelCount > 0)
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

            DrawGroupBarBackground(canvas, points, barSize);

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
                var dataSet = Element.Data.DataItemGroups[i];
                int entryCount = dataSet.DataItems.Count();
                if (entriesMaxCount < entryCount)
                    entriesMaxCount = entryCount;
            }

            return entriesMaxCount;
        }

        protected override void GenerateDataTable()
        {
            if (Element.Data == _prevDataSetArray)
                return;

            _groupBarChartDataTable = new double[_groupDataCount, _categoryCount];
            for (int i = 0; i < _groupDataCount; i++)
            {
                if (Element.Data.DataItemGroups.Count <= i) break;
                var items = Element.Data.DataItemGroups[i].DataItems;
                for (int j = 0; j < items.Count; j++)
                {
                    var index = items[j].Key == 0 ? j : items[j].Key - 1;
                    index = index < _categoryCount ? index : _categoryCount - 1;
                    _groupBarChartDataTable[i, index] = items[j].Value;
                }
            }

            _prevDataSetArray = Element.Data.DataItemGroups;
        }

        protected override SKSize CalculateBarSize()
        {
            var groupChart = Element as GroupBarChartView;
            _dataSetArrayCount = groupChart.Data.DataItemGroups.Count();
            _groupDataCount = Math.Min(_dataSetArrayCount, 5); // Maximum group data count is 5
            float barWidth = (float)XForms.ConvertToScaledPixel(Element.BarWidth);
            _groupBarMargin = groupChart != null ? (float)XForms.ConvertToScaledPixel(groupChart.GroupBarMargin) : 0;
            float barHeight = 0;
            int totalDataCount = 0;

            _maxEntriesCount = GetMaxEntriesCount();
            _categoryLabelCount = Element.AxisOption.CategoryLabels?.Count() ?? 0;
            _categoryCount = Math.Max(_categoryLabelCount, _maxEntriesCount);
            totalDataCount = _categoryCount * _groupDataCount;
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

        protected override IEnumerable<SKPoint> CalculatePoints(SKSize barSize)
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
                        value = Math.Min(Math.Max(value, Element.Minimum), Element.Maximum);
                        var x = yAxisWidth + _barHmargin * (i + 1) + (barSize.Width / 2) + groupOffset * i + (barSize.Width + _groupBarMargin) * j;
                        var y = (float)(((Element.Maximum - value) / Element.ValueRange) * barSize.Height);
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
                        value = Math.Min(Math.Max(value, Element.Minimum), Element.Maximum);
                        var y = _barVmargin * (i + 1) + (barSize.Width / 2) + groupOffset * i + (barSize.Width + _groupBarMargin) * j;
                        var x = yAxisWidth + (float)(((value - Element.Minimum) / Element.ValueRange) * barSize.Height);
                        var point = new SKPoint(x, y);
                        result.Add(point);
                    }
                }
            }

            return result.ToArray();
        }

        protected override void DrawCategoryLabels(SKCanvas canvas, IEnumerable<SKPoint> points)
        {
            bool IsVertical = Element.BarChartType == BarChartType.Vertical;
            float x = 0;
            float y = 0;
            TextItem label;

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
                    x = (points.ElementAt(categoryStartIndex).X + points.ElementAt(categoryStartIndex + _groupDataCount - 1).X) / 2;
                    y = _canvasSize.Height - (_majorAxisSize.Height / 2);
                }
                else
                {
                    x = _majorAxisSize.Width / 2;
                    y = (points.ElementAt(categoryStartIndex).Y + points.ElementAt(categoryStartIndex + _groupDataCount - 1).Y) / 2;
                }

                canvas.DrawText(x, y, label);
            }
        }

        private void DrawGroupBarBackground(SKCanvas canvas, IEnumerable<SKPoint> points, SKSize barSize)
        {
            var halfWidth = barSize.Width / 2;
            var topRadius = (float)Element.BarTopRadius > halfWidth ? halfWidth : (float)Element.BarTopRadius;
            var totalBarCount = _categoryCount * _groupDataCount;
            bool IsVertical = Element.BarChartType == BarChartType.Vertical;
            SKColor bgColor;
            SKRect rect;

            if (points.Count() > 0)
            {
                GetBarBackgroundColor();
                for (int i = 0; i < totalBarCount; i++)
                {
                    var point = points.ElementAt(i);
                    var r = i % _groupDataCount;
                    bgColor = _barBackgroundColor[r];

                    if (bgColor == SKColor.Empty)
                        continue;

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

        protected void DrawGroupBars(SKCanvas canvas, IEnumerable<SKPoint> points, SKSize barSize)
        {
            var halfWidth = barSize.Width / 2;
            var topRadius = (float)Element.BarTopRadius > halfWidth ? halfWidth : (float)Element.BarTopRadius;
            var bottomRadius = (float)Element.BarBottomRadius > halfWidth ? halfWidth : (float)Element.BarBottomRadius;
            var totalBarCount = _categoryCount * _groupDataCount;
            bool IsVertical = Element.BarChartType == BarChartType.Vertical;
            float x = 0;
            float y = 0;
            float w = 0;
            float h = 0;
            SKRect rect;
            bool isValueOverRadius = false;

            if (points.Count() > 0)
            {
                GetBarColor();
                for (int i = 0; i < totalBarCount; i++)
                {
                    isValueOverRadius = false;
                    var point = points.ElementAt(i);
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
            SKColor barColor = BarDefaultColor.ToSKColor();
            _barColor = new SKColor[_groupDataCount];

            for (int i = 0; i < _groupDataCount; i++)
            {
                if (_dataSetArrayCount > i)
                {
                    var dataSet = Element.Data.DataItemGroups[i];
                    if (dataSet != null && dataSet.Color != Color.Default)
                    {
                        barColor = dataSet.Color.ToSKColor();
                    }
                }

                _barColor[i] = barColor;
            }
        }

        private void GetBarBackgroundColor()
        {
            SKColor bgColor = SKColor.Empty;
            _barBackgroundColor = new SKColor[_groupDataCount];

            for (int i = 0; i < _groupDataCount; i++)
            {
                if (_dataSetArrayCount > i)
                {
                    var dataItemGroup = Element.Data.DataItemGroups[i];
                    var barDataItemGroup = dataItemGroup as BarDataItemGroup;
                    if (barDataItemGroup != null && barDataItemGroup.BarBackgroundColor != Color.Transparent)
                    {
                        bgColor = barDataItemGroup.BarBackgroundColor.ToSKColor();
                    }
                }

                _barBackgroundColor[i] = bgColor;
            }
        }
    }
}
