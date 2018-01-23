using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// a widget interface that when shown, pops up a list of items
    /// </summary>
    public interface IContextPopup
    {
        /// <summary>
        /// It will be triggered when the item of ContextPopup is selected.
        /// </summary>
        event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

        /// <summary>
        /// It will be triggered when the ContextPopup is dismissed.
        /// </summary>
        event EventHandler Dismissed;

        /// <summary>
        /// Items selected from ContextPopupItem
        /// </summary>
        ContextPopupItem SelectedItem { get; set; }

        /// <summary>
        /// add ContextPopupItem
        /// </summary>
        /// <param name="items">Items to be added</param>
        void AddItems(IEnumerable<ContextPopupItem> items);

        /// <summary>
        /// remove ContextPopupItem
        /// </summary>
        /// <param name="items">Items to be removed</param>
        void RemoveItems(IEnumerable<ContextPopupItem> items);

        /// <summary>
        /// remove all ContextPopupItems
        /// </summary>
        void ClearItems();

        /// <summary>
        /// Show ContextPopup
        /// </summary>
        /// <param name="anchor">Screen to show</param>
        /// <param name="xAnchorOffset">x coordinate</param>
        /// <param name="yAnchorOffset">y coordinate</param>
        void Show(View anchor, int xAnchorOffset, int yAnchorOffset);

        /// <summary>
        /// Dismiss ContextPopup
        /// </summary>
        void Dismiss();
    }
}