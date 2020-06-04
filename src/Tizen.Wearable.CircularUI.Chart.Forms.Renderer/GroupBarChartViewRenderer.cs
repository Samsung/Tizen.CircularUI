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
                base.CalculateAxisSize();
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
                int entryCount = dataSet.DataItems.Count;
                if (entriesMaxCount < entryCount)
                    entriesMaxCount = entryCount;
            }

            return entriesMaxCount;
        }

        protected override void GenerateDataTable()
        {
            _groupBarChartDataTable = new double[_groupDataCount, _categoryCount];
            for (int i = 0; i < _groupDataCount; i++)
            {
                if (Element.Data.DataItemGroups.Count <= i) break;
                var items = Element.Data.DataItemGroups[i].DataItems;
                for (int j = 0; j < _categoryCount; j++)
                {
                    if (items.Count <= j)
                    {
                        _groupBarChartDataTable[i, j] = Element.Minimum;
                    }
                    else
                    {
                        _groupBarChartDataTable[i, j] = items[j]?.Value ?? Element.Minimum;
                    }
                }
            }
        }

        protected override SKSize CalculateBarSize()
        {
            var groupChart = Element as GroupBarChartView;
            _dataSetArrayCount = groupChart.Data.DataItemGroups.Count();
            _groupDataCount = Math.Min(_dataSetArrayCount, 5); // Maximum group data count is 5
            _groupBarMargin = groupChart != null ? (float)XForms.ConvertToScaledPixel(groupChart.GroupBarMargin) : -1;
            float barWidth = (float)XForms.ConvertToScaledPixel(Element.BarWidth);
            float barHeight = 0;

            _maxEntriesCount = GetMaxEntriesCount();
            _categoryLabelCount = Element.AxisOption.CategoryLabels?.Count() ?? 0;
            _categoryCount = Math.Max(_categoryLabelCount, _maxEntriesCount);
            var totalDataCount = _categoryCount * _groupDataCount;
            if (Element.BarChartType == BarChartType.Vertical)
            {
                //If user don't set BarWidth, Calculate barwidth using canvas size.
                if (barWidth <= 0)
                {
                    var chartWidth = _canvasSize.Width - _minorAxisSize.Width;
                    if (_groupBarMargin == -1)
                    {
                        //If user don't set GroupBarMargin,temporary set barWidth = _barHmargin = _groupBarMargin * (groupcnt -1).
                        barWidth = chartWidth / (totalDataCount + _categoryCount * 2 + 1);
                        _groupBarMargin = barWidth / (_groupDataCount - 1);
                        _barHmargin = barWidth;
                    }
                    else
                    {
                        var totalGroupBarMargin = _groupBarMargin * _categoryCount * (_groupDataCount - 1); //Total space in bar group.
                        var totalMargin = _groupBarMargin * (_groupDataCount - 1) * (_categoryCount + 1); //Total space between groups.
                        barWidth = (chartWidth - totalGroupBarMargin - totalMargin) / totalDataCount;
                        _barHmargin = _groupBarMargin * (_groupDataCount - 1); //set margin between groups using _groupBarMargin.
                    }
                }
                else
                {
                    if (_groupBarMargin == -1)
                    {
                        _groupBarMargin = barWidth / (_groupDataCount - 1);
                    }

                    var spareHSize = _canvasSize.Width - _minorAxisSize.Width - (barWidth * totalDataCount) - (_groupBarMargin * _categoryCount * (_groupDataCount - 1));
                    _barHmargin = spareHSize / (_categoryCount + 1);
                }

                barHeight = _canvasSize.Height - _majorAxisSize.Height;
            }
            else
            {
                if (barWidth <= 0)
                {
                    var chartHeight = _canvasSize.Height - _minorAxisSize.Height;
                    if (_groupBarMargin == -1)
                    {
                        //If user don't set GroupBarMargin,temporary set barWidth = _barVmargin = _groupBarMargin * (groupcnt -1).
                        barWidth = chartHeight / (totalDataCount + _categoryCount * 2 + 1);
                        _groupBarMargin = barWidth / (_groupDataCount - 1);
                        _barVmargin = barWidth;
                    }
                    else
                    {
                        var totalGroupBarMargin = _groupBarMargin * _categoryCount * (_groupDataCount - 1); //Total space in bar group.
                        var totalMargin = _groupBarMargin * (_groupDataCount - 1) * (_categoryCount + 1); //Total space between groups.
                        barWidth = (chartHeight - totalGroupBarMargin - totalMargin) / totalDataCount;
                        _barVmargin = _groupBarMargin * (_groupDataCount - 1); //set margin between groups using _groupBarMargin.
                    }
                }
                else
                {
                    if (_groupBarMargin == -1)
                    {
                        _groupBarMargin = barWidth / (_groupDataCount - 1);
                    }

                    var spareVSize = _canvasSize.Height - _minorAxisSize.Height - (barWidth * totalDataCount) - (_groupBarMargin * _categoryCount * (_groupDataCount - 1));
                    _barVmargin = spareVSize / (_categoryCount + 1);
                }

                barHeight = _canvasSize.Width - _majorAxisSize.Width;
            }

            return new SKSize(barWidth, barHeight);
        }

        protected override IList<SKPoint> CalculatePoints(SKSize barSize)
        {
            var result = new List<SKPoint>();
            float yAxisWidth = Element.BarChartType == BarChartType.Vertical ? _minorAxisSize.Width : _majorAxisSize.Width;
            var groupOffset = barSize.Width * _groupDataCount + _groupBarMargin * (_groupDataCount - 1);
            float x;
            float y;

            if (Element.BarChartType == BarChartType.Vertical)
            {
                for (int i = 0; i < _categoryCount; i++)
                {
                    for (int j = 0; j < _groupDataCount; j++)
                    {
                        var value = _groupBarChartDataTable[j, i];
                        value = Math.Min(Math.Max(value, Element.Minimum), Element.Maximum);
                        if (_barHmargin < 0)
                        {
                            var groupPoint = (_canvasSize.Width - _minorAxisSize.Width) / _categoryCount;
                            var barPoint = groupPoint / _groupDataCount;
                            x = yAxisWidth + groupPoint * i + barPoint *(j + 1);
                        }
                        else
                        {
                            x = yAxisWidth + _barHmargin * (i + 1) + (barSize.Width / 2) + groupOffset * i + (barSize.Width + _groupBarMargin) * j;
                        }

                        y = (float)(((Element.Maximum - value) / Element.ValueRange) * barSize.Height);
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
                        if (_barVmargin < 0)
                        {
                            var groupPoint = (_canvasSize.Height - _minorAxisSize.Height) / _categoryCount;
                            var barPoint = groupPoint / _groupDataCount;
                            y = groupPoint * i + barPoint * (j + 1);
                        }
                        else
                        {
                            y = _barVmargin * (i + 1) + (barSize.Width / 2) + groupOffset * i + (barSize.Width + _groupBarMargin) * j;
                        }

                        x = yAxisWidth + (float)(((value - Element.Minimum) / Element.ValueRange) * barSize.Height);
                        var point = new SKPoint(x, y);
                        result.Add(point);
                    }
                }
            }

            return result;
        }

        protected override void DrawCategoryLabels(SKCanvas canvas, IList<SKPoint> points)
        {
            bool IsVertical = Element.BarChartType == BarChartType.Vertical;
            float x = 0;
            float y = 0;
            TextItem label;

            for (int i = 0; i < _categoryLabelCount; i++)
            {
                var categoryLabel = Element.AxisOption.CategoryLabels[i];
                label = categoryLabel.Label;
                if (string.IsNullOrEmpty(label.Text))
                {
                    continue;
                }

                var index = categoryLabel.ItemIndex != -1 ? categoryLabel.ItemIndex : i;
                int categoryStartIndex = index * _groupDataCount;
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

        private void DrawGroupBarBackground(SKCanvas canvas, IList<SKPoint> points, SKSize barSize)
        {
            var halfWidth = barSize.Width / 2;
            var topRadius = (float)Element.BarTopRadius > halfWidth ? halfWidth : (float)Element.BarTopRadius;
            var totalBarCount = _categoryCount * _groupDataCount;
            bool IsVertical = Element.BarChartType == BarChartType.Vertical;
            SKColor bgColor;
            SKRect rect;

            if (points.Count > 0)
            {
                GetBarBackgroundColor();
                for (int i = 0; i < totalBarCount; i++)
                {
                    var point = points[i];
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

        protected void DrawGroupBars(SKCanvas canvas, IList<SKPoint> points, SKSize barSize)
        {
            var halfWidth = barSize.Width / 2;
            var topRadius = (float)Element.BarTopRadius > halfWidth ? halfWidth : (float)Element.BarTopRadius;
            var bottomRadius = (float)Element.BarBottomRadius > halfWidth ? halfWidth : (float)Element.BarBottomRadius;
            var totalBarCount = _categoryCount * _groupDataCount;
            bool IsVertical = Element.BarChartType == BarChartType.Vertical;

            if (points.Count > 0)
            {
                GetBarColor();
                for (int i = 0; i < totalBarCount; i++)
                {
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
