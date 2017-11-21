using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xamarin.Forms.CircularUI
{
    public interface IContextPopup
    {
        event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

        event EventHandler Dismissed;

        ContextPopupItem SelectedItem { get; set; }

        void AddItems(IEnumerable<ContextPopupItem> items);

        void RemoveItems(IEnumerable<ContextPopupItem> items);

        void ClearItems();

        void Show(View anchor, int xAnchorOffset, int yAnchorOffset);

        void Dismiss();
    }
}