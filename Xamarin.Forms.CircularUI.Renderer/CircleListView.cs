using ElmSharp.Wearable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElmSharp;
using System.Collections;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Internals;
using System.ComponentModel;

namespace Xamarin.Forms.CircularUI.Renderer
{
    using GroupList = TemplatedItemsList<ItemsView<Cell>, Cell>;

    public class CircleListView : CircleGenList
    {
        readonly Dictionary<Cell, GenListItem> _itemContexts = new Dictionary<Cell, GenListItem>();

        VisualElement _header;
        VisualElement _footer;

        public CircleListView(EvasObject parent, CircleSurface surface) : base(parent, surface)
        {
        }
        public VisualElement Header
        {
            get => _header;
            set
            {
                if (_header == value) return;
                _header = value;
                UpdateHeader();
            }
        }
        public VisualElement Footer
        {
            get => _footer;
            set
            {
                if (_footer == value) return;
                _footer = value;
                UpdateFooter();
            }
        }

        public void ResetGroup(GroupList group)
        {
            GenListItem groupItem;
            if (_itemContexts.TryGetValue(group.HeaderContent, out groupItem))
            {
                groupItem.ClearSubitems();
            }
        }

        public void AddSource(IEnumerable source)
        {
            foreach (var data in source)
            {
                var groupList = data as GroupList;
                if (groupList != null)
                {
                    AddGroup(groupList);
                    foreach (var item in groupList)
                    {
                        AddItem(item as Cell, groupList);
                    }
                }
                else
                {
                    AddItem(data as Cell, null);
                }
            }
        }
        public void AddSource(IEnumerable source, Cell before)
        {
            foreach (var data in source)
            {
                var groupList = data as GroupList;
                if (groupList != null)
                {
                    InsertGroup(groupList, before);
                    foreach (var item in groupList)
                    {
                        AddItem(item as Cell, groupList);
                    }
                }
                else
                {
                    InsertItem(data as Cell, before, null);
                }
            }
        }
        public void AddSource(IEnumerable source, GroupList group, Cell before)
        {
            foreach (var data in source)
            {
                InsertItem(data as Cell, before, group);
            }
        }
        public void AddSource(IEnumerable source, GroupList group)
        {
            foreach (var data in source)
            {
                AddItem(data as Cell, group);
            }
        }

        public void RemoveSource(IEnumerable source)
        {
            foreach (var data in source)
            {
                if (data is GroupList)
                    Remove(data as GroupList);
                else
                    Remove(data as Cell);
            }
        }

        public void ApplyScrollTo(Cell cell, ScrollToPosition position, bool animated)
        {
            GenListItem item;
            if (_itemContexts.TryGetValue(cell, out item))
            {
                ScrollTo(item, position.ToNative(), animated);
            }
        }
        public void ApplySelectedItem(Cell cell)
        {
            GenListItem item;
            if (_itemContexts.TryGetValue(cell, out item))
            {
                item.IsSelected = true;
            }
        }
        public void AddItem(Cell cell, GroupList group)
        {
            CellRenderer renderer = ListViewCache.Get(cell);
            GenListItem groupItem = group == null ? null : _itemContexts.GetValueOrDefault(group.HeaderContent);
            var item = InsertBefore(renderer.Class, new ListViewItemContext(cell, group), LastItem, GenListItemType.Normal, groupItem);
            RegisterItem(cell, item);
        }
        public void InsertItem(Cell cell, Cell before, GroupList group)
        {
            CellRenderer renderer = ListViewCache.Get(cell);
            GenListItem groupItem = group == null ? null : _itemContexts.GetValueOrDefault(group.HeaderContent);
            GenListItem beforeItem = _itemContexts.GetValueOrDefault(before);
            var item = InsertBefore(renderer.Class, new ListViewItemContext(cell, group), beforeItem, GenListItemType.Normal, groupItem);
            RegisterItem(cell, item);
        }
        public void AddGroup(GroupList group)
        {
            CellRenderer renderer = ListViewCache.Get(group.HeaderContent);
            var item = InsertBefore(renderer.Class, new ListViewItemContext(group), LastItem, GenListItemType.Group);
            RegisterItem(group.HeaderContent, item, true);
        }
        public void InsertGroup(GroupList group, Cell before)
        {
            CellRenderer renderer = ListViewCache.Get(group.HeaderContent);
            GenListItem beforeItem = _itemContexts.GetValueOrDefault(before);
            var item = InsertBefore(renderer.Class, new ListViewItemContext(group), beforeItem, GenListItemType.Group);
            RegisterItem(group.HeaderContent, item, true);
        }
        public void Remove(Cell cell)
        {
            GenListItem item;
            if (_itemContexts.TryGetValue(cell, out item))
            {
                item.Delete();
            }
        }
        public void Remove(GroupList group)
        {
            Remove(group.HeaderContent);
            foreach (var data in group)
            {
                Remove(data as Cell);
            }
        }

        public new void Clear()
        {
            base.Clear();
            Initialize();
        }

        protected override void OnRealized()
        {
            base.OnRealized();
            Initialize();
        }
        void Initialize()
        {
            UpdateHeader();
            UpdateFooter();
        }
        void UpdateHeader()
        {
            GenItemClass cls = Header == null ? ListViewCache.PaddingItemClass : ListViewCache.InformalItemClass;
            if (FirstItem == null)
            {
                Append(cls, new TypedItemContext(Header, ItemType.TopPadding));
            }
            else
            {
                var ctx = FirstItem.Data as TypedItemContext;
                if (ctx != null && (ctx.Type == ItemType.TopPadding || ctx.Type == ItemType.Header))
                {
                    FirstItem.UpdateItemClass(cls, new TypedItemContext(Header, Header == null ? ItemType.TopPadding : ItemType.Header));
                }
                else
                {
                    InsertBefore(cls, new TypedItemContext(Header, Header == null ? ItemType.TopPadding : ItemType.Header), FirstItem);
                }
            }
        }
        void UpdateFooter()
        {
            GenItemClass cls = Footer == null ? ListViewCache.PaddingItemClass : ListViewCache.InformalItemClass;
            if (LastItem == null)
            {
                Append(cls, new TypedItemContext(Footer, ItemType.BottomPadding));
            }
            else
            {
                var ctx = LastItem.Data as TypedItemContext;
                if (ctx != null && (ctx.Type == ItemType.BottomPadding || ctx.Type == ItemType.Footer))
                {
                    LastItem.UpdateItemClass(cls, new TypedItemContext(Footer, Footer == null ? ItemType.BottomPadding : ItemType.Footer));
                }
                else
                {
                    Append(cls, new TypedItemContext(Footer, Footer == null ? ItemType.BottomPadding : ItemType.Footer));
                }
            }
        }
        void RegisterItem(Cell cell, GenListItem item, bool IsGroup = false)
        {
            item.SelectionMode = IsGroup ? GenItemSelectionMode.None : GenItemSelectionMode.Always;
            item.IsEnabled = cell.IsEnabled;
            item.Deleted += ItemDeletedHandler;
            _itemContexts[cell] = item;

            if (!IsGroup)
            {
                cell.PropertyChanged += OnCellPropertyChanged;
                (cell as ICellController).ForceUpdateSizeRequested += OnForceUpdateSizeRequested;
            }
        }
        void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var cell = sender as Cell;
            var renderer = ListViewCache.Get(cell);
            GenListItem item;
            if (_itemContexts.TryGetValue(cell, out item))
            {
                renderer.SendCellPropertyChanged(cell, item, e.PropertyName);
            }
        }
        void OnForceUpdateSizeRequested(object sender, EventArgs e)
        {
            var cell = sender as Cell;
            GenListItem item;
            if (_itemContexts.TryGetValue(cell, out item))
            {
                item.Update();
            }
        }
        void ItemDeletedHandler(object sender, EventArgs e)
        {
            var context = (sender as GenListItem)?.Data as ListViewItemContext;
            if (context != null)
            {
                Cell cell;
                if (!context.IsGroupItem)
                {
                    cell = context.Cell;
                    cell.PropertyChanged -= OnCellPropertyChanged;
                    (cell as ICellController).ForceUpdateSizeRequested -= OnForceUpdateSizeRequested;
                }
                else
                {
                    cell = context.Group.HeaderContent;
                }
                _itemContexts.Remove(cell);
            }
        }
    }

    enum ItemType
    {
        TopPadding,
        BottomPadding,
        Header,
        Footer
    }

    class TypedItemContext
    {
        public TypedItemContext(VisualElement element, ItemType type)
        {
            Element = element;
            Type = type;
        }
        public VisualElement Element { get; set; }
        public ItemType Type { get; set; }
    }

    class ListViewItemContext : Xamarin.Forms.Platform.Tizen.Native.ListView.ItemContext
    {
        public ListViewItemContext(Cell cell, GroupList group, bool isGroup)
        {
            Cell = cell;
            Group = group;
            IsGroupItem = isGroup;
        }
        public ListViewItemContext(Cell cell, GroupList group) : this(cell, group, false)
        {
        }

        public ListViewItemContext(GroupList group) : this(null, group, true)
        {
        }

        public GroupList Group { get; set; }
    }
}
