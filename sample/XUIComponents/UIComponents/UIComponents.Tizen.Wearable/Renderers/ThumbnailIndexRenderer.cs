using System;
using System.ComponentModel;
using UIComponents.Extensions;
using UIComponents.Tizen.Wearable.Renderers;
using Xamarin.Forms.Platform.Tizen;
using ElmSharp;
using Elayout = ElmSharp.Layout;
using TForms = Xamarin.Forms.Platform.Tizen.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ThumbnailIndex), typeof(ThumbnailIndexRenderer))]
namespace UIComponents.Tizen.Wearable.Renderers
{
    public class ThumbnailIndexRenderer : ViewRenderer<ThumbnailIndex, EvasObject>
    {
        List<IndexItem> _items = new List<IndexItem>();
        //Elayout _outterLayout;
        PaddingBox _pbox;
        Scroller _scroller;
        Index _index;

        bool _isFirstItem;

        /// <summary>
        /// Constructor
        /// </summary>
        public ThumbnailIndexRenderer()
        {
        }

        /// <summary>
        /// Called when element is changed
        /// </summary>
        /// <param name="e">Argument of ElementChangedEventArgs<ThumbnailIndex></param>
        protected override void OnElementChanged(ElementChangedEventArgs<ThumbnailIndex> e)
        {
            if (NativeView == null)
            {
                Initialize();
                SetNativeControl(_pbox);
            }

            base.OnElementChanged(e);
        }

        /// <summary>
        /// Initialize UI components
        /// </summary>
        private void Initialize()
        {
            Console.WriteLine("Initialize");
            _isFirstItem = true;
            //_outterLayout = new Elayout(TForms.Context.MainWindow);

            // Initialize PaddingBox
            _pbox = new PaddingBox(TForms.NativeParent)
            {
                Padding = new Thickness { Left = 0, Right = 0, Top = 22, Bottom = 0 },
                HeaderSize = new ElmSharp.Size(200, 19),
                HeaderGap = 38
            };
            _pbox.Show();

            // Initialize Index
            _index = new ElmSharp.Index(_pbox/*_outterLayout*/)
            {
                Style = "thumbnail",
                AutoHide = false,
                IsHorizontal = true,
                AlignmentX = 0.5,
                AlignmentY = 0.5,
            };
            _index.Show();
            _pbox.Header = _index;
            //index.Geometry = new Rect(0, 22, 200, 19);

            // Initialize Scroller
            _scroller = new ElmSharp.Scroller(_pbox/*_outterLayout*/)
            {
                HorizontalLoop = false,
                VerticalLoop = false,
                HorizontalScrollBarVisiblePolicy = ScrollBarVisiblePolicy.Invisible,
                VerticalScrollBarVisiblePolicy = ScrollBarVisiblePolicy.Invisible,
                HorizontalPageScrollLimit = 1,
                VerticalPageScrollLimit = 0,
            };

            _scroller.PageScrolled += (s, e) =>
            {
                Console.WriteLine($" _scroller PageScrolled");
                var pageIndex = _scroller.HorizontalPageIndex;
                _items[pageIndex].Select(true);
            };
            _scroller.Show();
            _pbox.Content = _scroller;
            //_scroller.Geometry = new Rect(0, 79, 360, 281);

            // Initialize Box
            var box = new ElmSharp.Box(_scroller)
            {
                IsHorizontal = true,
                AlignmentX = 0.5,
                AlignmentY = 0.5,
            };
            _scroller.SetContent(box);
            box.Show();

            // Create Rectangle for layout center align in Box
            var padder = new ElmSharp.Rectangle(box);
            box.PackEnd(padder);
            _items.Clear();

            // Initialize ThumbnailItems
            foreach (var item in Element.ThumbnailItems)
            {
                // create layout
                var page = new ElmSharp.Layout(box)
                {
                    WeightX = 1.0,
                    WeightY = 1.0,
                    AlignmentX = -1.0,
                    AlignmentY = 0.5
                };
                page.SetTheme("layout", "body_thumbnail", "default");
                page.Show();

                // set icon
                var img = new ElmSharp.Image(page);
                var icon = item.Image;
                Console.WriteLine($"item.Image File:{icon.File}");
                img.LoadAsync(ResourcePath.GetPath(icon.File));
                page.SetPartContent("elm.icon", img);

                var indexItem = _index.Append(null);
                _items.Add(indexItem);

                // first item case
                if (_isFirstItem)
                {
                    Console.WriteLine($"_isFirstItem is true");
                    page.Resized += (s, e) =>
                    {
                        var g = _scroller.Geometry;
                        var pg = page.Geometry;
                        padder.MinimumWidth = (g.Width - pg.Width) / 2;
                        padder.MinimumHeight = g.Height / 2;
                        _scroller.SetPageSize(pg.Width, pg.Height);
                    };
                    indexItem.Select(true);
                }
                // first item and other items case
                _isFirstItem = false;
                box.PackEnd(page);
            }

            box.PackEnd(padder);
            _index.Update(0);
        }
    }
}
