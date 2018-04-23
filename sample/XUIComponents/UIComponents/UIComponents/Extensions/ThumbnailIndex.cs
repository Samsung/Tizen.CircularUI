using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Xamarin.Forms;

namespace UIComponents.Extensions
{

    public class ThumbnailIndex : View
    {
        ObservableCollection<ThumbnailItem> _items;

        public ThumbnailIndex()
        {
            _items = new ObservableCollection<ThumbnailItem>();
            _items.CollectionChanged += ItemsCollectionChanged;
        }

        public IList<ThumbnailItem> ThumbnailItems
        {
            get
            {
                return _items;
            }
        }

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
