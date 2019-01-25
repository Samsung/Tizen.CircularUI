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

using ElmSharp.Wearable;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ElmSharp;
using System.Collections;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Internals;
using System.ComponentModel;

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    using GroupList = TemplatedItemsList<ItemsView<Cell>, Cell>;

    public class CircleListView : CircleGenList
    {
        const int HeaderMinimumHeight = 115;
        const int GroupHeaderMinimumHeight = 83;

        readonly Dictionary<Cell, GenListItem> _itemContexts = new Dictionary<Cell, GenListItem>();

        VisualElement _header;
        VisualElement _footer;
        int _rowHeight;

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

        public int HeaderRowHeight
        {
            get => _rowHeight;
            set
            {
                if (_rowHeight == value) return;
                _rowHeight = value;
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
            UpdateHeader();
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
            UpdateFooter();
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

        public void ApplyScrollTo(Cell cell, Xamarin.Forms.ScrollToPosition position, bool animated)
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
            GenListItem item = null;
            var lastCtx = LastItem?.Data as TypedItemContext;
            if (lastCtx != null && (lastCtx.Type == ItemType.Footer || lastCtx.Type == ItemType.BottomPadding))
            {
                item = InsertBefore(renderer.Class, new ListViewItemContext(cell, group), LastItem, GenListItemType.Normal, groupItem);
            }
            else
            {
                item = Append(renderer.Class, new ListViewItemContext(cell, group), GenListItemType.Normal, groupItem);
            }
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
            CellRenderer renderer = ListViewCache.Get(group.HeaderContent, true);
            var lastCtx = LastItem?.Data as TypedItemContext;
            GenListItem item = null;
            if (lastCtx != null && (lastCtx.Type == ItemType.Footer || lastCtx.Type == ItemType.BottomPadding))
            {
                item = InsertBefore(renderer.Class, new ListViewItemContext(group), LastItem, GenListItemType.Group);
            }
            else
            {
                item = Append(renderer.Class, new ListViewItemContext(group), GenListItemType.Group);
            }
            RegisterItem(group.HeaderContent, item, true);
        }
        public void InsertGroup(GroupList group, Cell before)
        {
            CellRenderer renderer = ListViewCache.Get(group.HeaderContent, true);
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
        }

        protected override void OnRealized()
        {
            base.OnRealized();

            ItemRealized += OnItemAppear;
            ItemUnrealized += OnItemDisappear;
        }

        void OnItemDisappear(object sender, GenListItemEventArgs e)
        {
            ListViewItemContext ctx = e?.Item?.Data as ListViewItemContext;
            if (ctx?.Cell != null)
            {
                ctx.Cell.SendDisappearing();
                var renderer = ListViewCache.Get(ctx.Cell);
                renderer.SendUnrealizedCell(ctx.Cell);
            }
        }

        void OnItemAppear(object sender, GenListItemEventArgs e)
        {
            ListViewItemContext ctx = e?.Item?.Data as ListViewItemContext;
            if (ctx?.Cell != null)
            {
                ctx.Cell.SendAppearing();
            }
        }

        void UpdateHeaderHeightForGroup(bool isPreviousOfGroup)
        {
            if (Header == null) return;
            if (isPreviousOfGroup)
            {
                Header.MinimumHeightRequest = GroupHeaderMinimumHeight; // correct visible height on the Group Header
            }
            else
            {
                if (Header.HeightRequest < 0 && HeaderRowHeight < 0)
                {
                    Header.MinimumHeightRequest = HeaderMinimumHeight;  // correct visible height on the None group header
                }
                else if(HeaderRowHeight > 0)
                {
                    Header.MinimumHeightRequest = HeaderRowHeight;
                }
            }
        }

        void UpdateHeader()
        {
            GenItemClass cls = null;
            ItemType type = ItemType.TopPadding;
            var ctx = FirstItem?.Data as TypedItemContext;

            if (Header == null)
            {
                cls = ListViewCache.PaddingItemClass;
            }
            else
            {
                cls = Forms.CircleListView.GetCancelEffect(Header) ? ListViewCache.InformalItemClassWithoutFishEye : ListViewCache.InformalItemClass;
                type = ItemType.Header;
            }

            if (FirstItem == null)
            {
                Append(cls, new TypedItemContext(Header, type));
            }
            else
            {
                if (ctx == null || ctx.Type == ItemType.Footer || ctx.Type == ItemType.BottomPadding)
                {
                    InsertBefore(cls, new TypedItemContext(Header, type), FirstItem);
                }
                else
                {
                    FirstItem.UpdateItemClass(cls, new TypedItemContext(Header, type));
                }
            }

            if (Header != null && FirstItem?.Next != null)
            {
                var nextCtx = FirstItem.Next?.Data as ListViewItemContext;
                var isNextItemIsGroupHeader = nextCtx == null ? false : nextCtx.IsGroupItem;

                UpdateHeaderHeightForGroup(isNextItemIsGroupHeader);
            }
        }

        void UpdateFooter()
        {
            GenItemClass cls = null;
            ItemType type;
            var ctx = LastItem?.Data as TypedItemContext;
            if (Footer == null)
            {
                cls = ListViewCache.PaddingItemClass;
                type = ItemType.BottomPadding;
            }
            else
            {
                cls = Forms.CircleListView.GetCancelEffect(Footer) ? ListViewCache.InformalItemClassWithoutFishEye : ListViewCache.InformalItemClass;
                type = ItemType.Footer;
            }

            if (ctx == null || ctx.Type == ItemType.Header || ctx.Type == ItemType.TopPadding)
            {
                Append(cls, new TypedItemContext(Footer, type));
            }
            else
            {
                LastItem.UpdateItemClass(cls, new TypedItemContext(Footer, type));
            }
        }

        void RegisterItem(Cell cell, GenListItem item, bool IsGroup = false)
        {
            item.SelectionMode = IsGroup ? GenItemSelectionMode.None : GenItemSelectionMode.Always;
            item.IsEnabled = cell.IsEnabled;
            item.Deleted += ItemDeletedHandler;
            _itemContexts[cell] = item;


            if (Header != null && item == FirstItem.Next)
            {
                UpdateHeaderHeightForGroup(IsGroup);
            }

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

    public class ListViewItemContext : Xamarin.Forms.Platform.Tizen.Native.ListView.ItemContext
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

        public ListViewItemContext(GroupList group) : this(group.HeaderContent, group, true)
        {
        }

        public GroupList Group { get; set; }
    }
}
