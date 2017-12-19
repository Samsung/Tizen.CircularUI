using ElmSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(Xamarin.Forms.CircularUI.CircleListView), typeof(Xamarin.Forms.CircularUI.Tizen.CircleListViewRenderer))]

namespace Xamarin.Forms.CircularUI.Tizen
{
    using GroupList = TemplatedItemsList<ItemsView<Cell>, Cell>;

    public class CircleListViewRenderer : ViewRenderer<Xamarin.Forms.CircularUI.CircleListView, CircleListView>
    {
        int _selectedItemChanging = 0;

        public CircleListViewRenderer()
        {
            RegisterPropertyHandler(Xamarin.Forms.CircularUI.CircleListView.HasUnevenRowsProperty, UpdateHasUnevenRows);
            RegisterPropertyHandler(Xamarin.Forms.CircularUI.CircleListView.RowHeightProperty, UpdateRowHeight);
            RegisterPropertyHandler(Xamarin.Forms.CircularUI.CircleListView.SelectedItemProperty, UpdateSelectedItem);
            RegisterPropertyHandler(Xamarin.Forms.CircularUI.CircleListView.ItemsSourceProperty, UpdateSource);
            RegisterPropertyHandler("HeaderElement", UpdateHeader);
            RegisterPropertyHandler("FooterElement", UpdateFooter);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.CircularUI.CircleListView> e)
        {
            if (Control == null)
            {
                var surface = this.GetSurface();
                SetNativeControl(new CircleListView(Platform.Tizen.Forms.Context.MainWindow, surface));

                Control.ItemSelected += OnListViewItemSelected;
            }

            if (e.NewElement != null)
            {
                e.NewElement.ScrollToRequested += OnScrollToRequested;
                e.NewElement.TemplatedItems.GroupedCollectionChanged += OnGroupedCollectionChanged;
                e.NewElement.TemplatedItems.CollectionChanged += OnCollectionChanged;
            }
            if (e.OldElement != null)
            {
                e.OldElement.ScrollToRequested -= OnScrollToRequested;
                e.OldElement.TemplatedItems.GroupedCollectionChanged -= OnGroupedCollectionChanged;
                e.OldElement.TemplatedItems.CollectionChanged -= OnCollectionChanged;
            }
            base.OnElementChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Element != null)
                {
                    Element.ScrollToRequested -= OnScrollToRequested;
                    Element.TemplatedItems.GroupedCollectionChanged -= OnGroupedCollectionChanged;
                    Element.TemplatedItems.CollectionChanged -= OnCollectionChanged;
                }
                if (Control != null)
                {
                    Control.ItemSelected -= OnListViewItemSelected;
                }
            }
            base.Dispose(disposing);
        }

        void UpdateFooter()
        {
            var footer = (Element as IListViewController)?.FooterElement as VisualElement;
            Control.Footer = footer;
        }

        void UpdateHeader()
        {
            var header = (Element as IListViewController)?.HeaderElement as VisualElement;
            Control.Header = header;
        }

        void UpdateSource(bool init)
        {
            if (!init)
                Control.Clear();
            Control.AddSource(Element.TemplatedItems);
            UpdateSelectedItem();
        }

        void UpdateSelectedItem()
        {
            if (Element.SelectedItem == null)
            {
                if (Control.SelectedItem != null)
                    Control.SelectedItem.IsSelected = false;
            }
            else
            {
                var results = Element.TemplatedItems.GetGroupAndIndexOfItem(Element.SelectedItem);
                if (results.Item1 != -1 && results.Item2 != -1)
                {
                    var group = (Element.TemplatedItems as ITemplatedItemsList<Cell>).GetGroup(results.Item1);
                    var cell = group[results.Item2];

                    _selectedItemChanging++;
                    Control.ApplySelectedItem(cell);
                    _selectedItemChanging--;
                }
            }
        }

        void UpdateRowHeight(bool initialize)
        {
            if (!initialize)
                Control.UpdateRealizedItems();
        }

        void UpdateHasUnevenRows()
        {
            Control.Homogeneous = !Element.HasUnevenRows;
            Control.UpdateRealizedItems();
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (e.NewStartingIndex + e.NewItems.Count < Element.TemplatedItems.Count)
                {
                    Cell before = Element.TemplatedItems[e.NewStartingIndex + e.NewItems.Count];
                    Control.AddSource(e.NewItems, before);
                }
                else
                {
                    Control.AddSource(e.NewItems);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                Control.RemoveSource(e.OldItems);
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                UpdateSource(false);
            }
        }

        void OnGroupedCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                GroupList group = sender as GroupList;
                if (e.NewStartingIndex + e.NewItems.Count < Element.TemplatedItems.Count)
                {
                    Cell before = Element.TemplatedItems[e.NewStartingIndex + e.NewItems.Count];
                    Control.AddSource(e.NewItems, group, before);
                }
                else
                {
                    Control.AddSource(e.NewItems, group);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                Control.RemoveSource(e.OldItems);
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                Control.ResetGroup(sender as GroupList);
            }
        }

        void OnScrollToRequested(object sender, ScrollToRequestedEventArgs e)
        {
            Cell cell;
            var args = e as ITemplatedItemsListScrollToRequestedEventArgs;
            if (Element.IsGroupingEnabled)
            {
                var results = Element.TemplatedItems.GetGroupAndIndexOfItem(args.Group, args.Item);
                if (results.Item1 == -1 || results.Item2 == -1)
                    return;

                var group = (Element.TemplatedItems as ITemplatedItemsList<Cell>).GetGroup(results.Item1);
                cell = group[results.Item2];
            }
            else
            {
                int index = (Element.TemplatedItems as ITemplatedItemsList<Cell>).GetGlobalIndexOfItem(args.Item);
                cell = Element.TemplatedItems[index];
            }

            Control.ApplyScrollTo(cell, e.Position, e.ShouldAnimate);
        }

        void OnListViewItemSelected(object sender, GenListItemEventArgs e)
        {
            var context = e.Item?.Data as ListViewItemContext;

            if (context == null)
            {
                return;
            }

            if (_selectedItemChanging != 0 || context.IsGroupItem)
            {
                return;
            }

            if (Element.IsGroupingEnabled)
            {
                int groupIndex = (Element.TemplatedItems as IList).IndexOf(context.Group);
                int subIndex = context.Group.IndexOf(context.Cell);
                _selectedItemChanging++;
                Element.NotifyRowTapped(groupIndex, subIndex);
                _selectedItemChanging--;
            }
            else
            {
                int index = Element.TemplatedItems.IndexOf(context.Cell);
                _selectedItemChanging++;
                Element.NotifyRowTapped(index);
                _selectedItemChanging--;
            }
        }
    }
}
