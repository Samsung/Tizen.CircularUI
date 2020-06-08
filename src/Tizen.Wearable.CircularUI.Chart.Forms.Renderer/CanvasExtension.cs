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

using SkiaSharp;
using Xamarin.Forms;
using XForms = Xamarin.Forms.Forms;
using SkiaSharp.Views.Forms;

namespace Tizen.Wearable.CircularUI.Chart.Forms.Renderer
{
    internal static class CanvasExtension
    {
        internal static void DrawAxisLine(this SKCanvas canvas, SKRect rect, Color color)
        {
            using (var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = color.ToSKColor(),
            })
            {
                canvas.DrawRect(rect, paint);
            }
        }

        internal static void DrawReferenceLine(this SKCanvas canvas, float startX, float startY, float endX, float endY, float lineSize, Color color, ReferenceLineMode mode)
        {
            using (var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = lineSize,
                Color = color.ToSKColor(),
                IsAntialias = true,
            })
            {
                switch (mode)
                {
                    case ReferenceLineMode.Dashed:
                        paint.PathEffect = SKPathEffect.CreateDash(new[] { lineSize * 2, lineSize * 2 }, 0);
                        break;
                    case ReferenceLineMode.Straight:
                    default:
                        break;
                }

                canvas.DrawLine(startX, startY, endX, endY, paint);
            }
        }

        internal static void DrawText(this SKCanvas canvas, float x, float y, TextItem textItem, bool isEndOfBoundX = false, bool isEndOfBoundY = false)
        {
            using (var paint = new SKPaint())
            {
                paint.TextSize = (float)(XForms.ConvertToEflFontPoint(textItem.FontSize));
                paint.IsAntialias = true;
                var color = textItem.TextColor == Color.Default ? Color.White : textItem.TextColor;
                paint.Color = color.ToSKColor();
                paint.IsStroke = false;
                var text = textItem.Text;
                var bounds = new SKRect();
                paint.MeasureText(text, ref bounds);
                x = x - (isEndOfBoundX ? bounds.Width : bounds.Width / 2);
                x = x < 0 ? 0 : x;
                y = y + (isEndOfBoundY ? bounds.Height : bounds.Height / 2);
                canvas.DrawText(text, x, y, paint);
            }
        }

        internal static void DrawLinePoint(this SKCanvas canvas, SKPoint point, SKColor color, float size)
        {
            using (var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                IsAntialias = true,
                Color = color,
            })
            {
                canvas.DrawCircle(point.X, point.Y, size, paint);
            }
        }

        internal static void DrawLegendText(this SKCanvas canvas, float x, float y, TextItem textItem)
        {
            using (var paint = new SKPaint())
            {
                paint.TextSize = (float)(XForms.ConvertToEflFontPoint(textItem.FontSize));
                paint.IsAntialias = true;
                var color = textItem.TextColor == Color.Default ? Color.White : textItem.TextColor;
                paint.Color = color.ToSKColor();
                paint.IsStroke = false;
                var text = textItem.Text;
                canvas.DrawText(text, x, y, paint);
            }
        }
    }
}
