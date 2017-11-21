using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.CircularUI
{
    public class ContextPopup : BindableObject
    {
        IContextPopup _contextPopup;

        ObservableCollection<ContextPopupItem> _items;

        /// <summary>
        /// BindableProperty. Identifies the SelectedIndex bindable property.
        /// </summary>
        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(ContextPopup), defaultValue: -1,
            propertyChanged: OnSelectedIndexChanged, coerceValue: CoerceSelectedIndex);

        /// <summary>
        /// BindableProperty. Identifies the SelectedItem bindable property.
        /// </summary>
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(ContextPopup), null,
            propertyChanged: OnSelectedItemChanged);


        /// <summary>
        /// The constructor, which creates a new ContextPopup instance.
        /// </summary>
        public ContextPopup()
        {
            _contextPopup = DependencyService.Get<IContextPopup>(DependencyFetchTarget.NewInstance);

            _items = new ObservableCollection<ContextPopupItem>();
            _items.CollectionChanged += ItemsCollectionChanged;

            SetBinding(SelectedItemProperty, new Binding(nameof(SelectedItem), mode: BindingMode.TwoWay, source: _contextPopup));

            _contextPopup.Dismissed += (s, e) => Dismissed?.Invoke(this, EventArgs.Empty);
            _contextPopup.ItemSelected += (s, e) =>
            {
                SelectedItem = e.SelectedItem as ContextPopupItem;
                ItemSelected?.Invoke(this, new SelectedItemChangedEventArgs(e.SelectedItem));
            };
        }

        /// <summary>
        /// Occurs when the ContextPopup is dismissed.
        /// </summary>
        public event EventHandler Dismissed;

        /// <summary>
        /// Occurs when a ContextPopupItem is selected.
        /// </summary>
        public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

        /// <summary>
        /// Gets or sets the index of the selected item of the ContextPopup.
        /// It is -1 when no item is selected.
        /// </summary>
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected item of the ContextPopup.
        /// </summary>
        public ContextPopupItem SelectedItem
        {
            get { return (ContextPopupItem)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Gets the list of items in the ContextPopup.
        /// </summary>
        public IList<ContextPopupItem> Items
        {
            get
            {
                return _items;
            }
        }

        /// <summary>
        /// Shows the ContextPopup. The ContextPopup is positioned at the horizontal and the vertical position of a specific anchor.
        /// </summary>
        /// <param name="anchor">The view to which the popup should be anchored.</param>
        public void Show(View anchor)
        {
            Show(anchor, 0, 0);
        }

        /// <summary>
        /// Shows the ContextPopup. The ContextPopup is positioned at the horizontal and the vertical position of a specific anchor with offsets.
        /// </summary>
        /// <param name="anchor">The view to which the popup should be anchored.</param>
        /// <param name="xOffset">The horizontal offset from the anchor.</param>
        /// <param name="yOffset">The vertical offset from the anchor.</param>
        public void Show(View anchor, int xOffset, int yOffset)
        {
            _contextPopup.Show(anchor, xOffset, yOffset);
        }

        /// <summary>
        /// Shows the ContextPopup. The ContextPopup is positioned at the horizontal and the vertical position of a specific anchor with offsets.
        /// </summary>
        /// <param name="anchor">The view to which the popup should be anchored.</param>
        /// <param name="xOffset">The horizontal offset from the anchor.</param>
        /// <param name="yOffset">The vertical offset from the anchor.</param>
        public void Show(View anchor, double xOffset, double yOffset)
        {
            Show(anchor, (int)xOffset, (int)yOffset);
        }

        /// <summary>
        /// Dismisses the ContextPopup.
        /// </summary>
        public void Dismiss()
        {
            _contextPopup.Dismiss();
        }

        void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddItems(e);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    RemoveItems(e);
                    break;

                default: // Move, replace, reset
                    ResetItems();
                    break;
            }

            SelectedIndex = SelectedIndex.Clamp(-1, Items.Count - 1);
            UpdateSelectedItem();
        }

        void ResetItems()
        {
            _contextPopup.ClearItems();
        }

        void RemoveItems(NotifyCollectionChangedEventArgs e)
        {
            _contextPopup.RemoveItems(e.OldItems.OfType<ContextPopupItem>());
        }

        void AddItems(NotifyCollectionChangedEventArgs e)
        {
            _contextPopup.AddItems(e.NewItems.OfType<ContextPopupItem>());
        }

        static void OnSelectedIndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var contextPopup = (ContextPopup)bindable;
            contextPopup.UpdateSelectedItem();
        }

        static object CoerceSelectedIndex(BindableObject bindable, object value)
        {
            var contextPopup = (ContextPopup)bindable;
            return contextPopup.Items == null ? -1 : ((int)value).Clamp(-1, contextPopup.Items.Count - 1);
        }

        void UpdateSelectedItem()
        {
            if (SelectedIndex == -1)
            {
                SelectedItem = null;
                return;
            }

            SelectedItem = Items[SelectedIndex];
        }

        static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var contextPopup = (ContextPopup)bindable;
            contextPopup.UpdateSelectedIndex(newValue);
        }

        void UpdateSelectedIndex(object selectedItem)
        {
            SelectedIndex = Items.IndexOf(selectedItem);
        }
    }

    internal static class NumericExtensions
    {
        public static int Clamp(this int self, int min, int max)
        {
            return Math.Min(max, Math.Max(self, min));
        }
    }
}
