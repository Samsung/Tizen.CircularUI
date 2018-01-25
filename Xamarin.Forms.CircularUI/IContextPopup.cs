using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// The IContextPopup is an interface to show and pop up a list of items
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    internal interface IContextPopup
    {
        /// <summary>
        /// Occurs when the item of ContextPopup is selected.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

        /// <summary>
        /// Occurs when the ContextPopup is dismissed.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        event EventHandler Dismissed;

        /// <summary>
        /// Gets or sets selected item from ContextPopup
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        ContextPopupItem SelectedItem { get; set; }

        /// <summary>
        /// Adds ContextPopupItems
        /// </summary>
        /// <param name="items">Items to be added</param>
        /// <since_tizen> 4 </since_tizen>
        void AddItems(IEnumerable<ContextPopupItem> items);

        /// <summary>
        /// Removes ContextPopupItems
        /// </summary>
        /// <param name="items">Items to be removed</param>
        /// <since_tizen> 4 </since_tizen>
        void RemoveItems(IEnumerable<ContextPopupItem> items);

        /// <summary>
        /// Removes all the ContextPopupItems
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        void ClearItems();

        /// <summary>
        /// Shows the ContextPopup
        /// </summary>
        /// <param name="anchor">The view to which the popup should be anchored</param>
        /// <param name="xAnchorOffset">The horizontal offset from the anchor</param>
        /// <param name="yAnchorOffset">The vertical offset from the anchor</param>
        /// <since_tizen> 4 </since_tizen>
        void Show(View anchor, int xAnchorOffset, int yAnchorOffset);

        /// <summary>
        /// Dismisses the ContextPopup
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        void Dismiss();
    }
}