using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Xamarin.Forms;

namespace UIComponents.Extensions
{
    /// <summary>
    /// Thumbnail index for view 
    /// </summary>
    public class ThumbnailIndex : View
    {
        ObservableCollection<ThumbnailItem> _items;

        public ThumbnailIndex()
        {
            _items = new ObservableCollection<ThumbnailItem>();
            _items.CollectionChanged += ItemsCollectionChanged;
        }

        /// <summary>
        /// List for Thumbnail items
        /// </summary>
        public IList<ThumbnailItem> ThumbnailItems
        {
            get
            {
                return _items;
            }
        }

        /// <summary>
        /// Called when items collection is changed.
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Argument of NotifyCollectionChangedEventArgs</param>
        void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    //
                    break;
                case NotifyCollectionChangedAction.Remove:
                    //
                    break;
                default: 
                    //
                    break;
            }
        }
    }
}
