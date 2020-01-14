using ElmSharp;
using ElmSharp.Wearable;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using ELayout = ElmSharp.Layout;

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class NavigationView : ELayout
    {
        readonly int _dafaultIconSize = 60;

        class Item
        {
            public Element Source { get; set; } 
            public string Text { get; set; }
            public string Icon { get; set; }
        }

        Box _outterBox;
        ELayout _surfaceLayout;
        CircleSurface _surface;
        CircleGenList _naviMenu;

        GenItemClass _defaultClass;
        SmartEvent _draggedUpCallback;
        SmartEvent _draggedDownCallback;

        GenListItem _header;
        GenListItem _footer;

        public NavigationView(EvasObject parent) : base(parent)
        {
            InitializeComponent();
        }

        public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

        public event EventHandler<DraggedEventArgs> Dragged;

        public void Build(List<List<Element>> items)
        {
            _naviMenu.Clear();
            // header
            _header = _naviMenu.Append(_defaultClass, new Item { Text = "" });

            // TODO. need to improve, need to support group
            foreach (var group in items)
            {
                foreach (var item in group)
                {
                    var data = new Item
                    {
                        Source = item
                    };
                    if (item is BaseShellItem shellItem)
                    {
                        data.Text = shellItem.Title;
                        data.Icon = (shellItem.Icon as FileImageSource)?.ToAbsPath();
                    }
                    else if (item is MenuItem menuItem)
                    {
                        data.Text = menuItem.Text;
                        data.Icon = (menuItem.IconImageSource as FileImageSource)?.ToAbsPath();
                    }
                    _naviMenu.Append(_defaultClass, data, GenListItemType.Normal);
                }
            }
            _footer = _naviMenu.Append(_defaultClass, new Item { Text = "" });
        }

        public void Activate()
        {
            (_naviMenu as IRotaryActionWidget)?.Activate();
        }
        public void Deactivate()
        {
            (_naviMenu as IRotaryActionWidget)?.Deactivate();
        }

        protected override IntPtr CreateHandle(EvasObject parent)
        {
            _outterBox = new Box(parent);
            return _outterBox.Handle;
        }

        void InitializeComponent()
        {
            _outterBox.PassEvents = false;
            _outterBox.SetLayoutCallback(OnLayout);

            _surfaceLayout = new ELayout(this);
            _surfaceLayout.Show();
            _surface = new CircleSurface(_surfaceLayout);

            _naviMenu = new CircleGenList(this, _surface)
            {
                Homogeneous = true,
                BackgroundColor = ElmSharp.Color.Gray
            };
            _naviMenu.Show();

            _draggedUpCallback = new SmartEvent(_naviMenu, "drag,start,up");
            _draggedUpCallback.On += (s, e) =>
            {
                if (_footer.TrackObject.IsVisible)
                {
                    Dragged?.Invoke(this, new DraggedEventArgs(DraggedState.EdgeBottom));
                }
                else
                {
                    Dragged?.Invoke(this, new DraggedEventArgs(DraggedState.Up));
                }
            };

            _draggedDownCallback = new SmartEvent(_naviMenu, "drag,start,down");
            _draggedDownCallback.On += (s, e) =>
            {
                if(_header.TrackObject.IsVisible)
                {
                    Dragged?.Invoke(this, new DraggedEventArgs(DraggedState.EdgeTop));
                }
                else
                {
                    Dragged?.Invoke(this, new DraggedEventArgs(DraggedState.Down));
                }
            };

            _outterBox.PackEnd(_naviMenu);
            _outterBox.PackEnd(_surfaceLayout);

            _surfaceLayout.StackAbove(_naviMenu);

            _defaultClass = new GenItemClass("1icon_2text")
            {
                GetTextHandler = (obj, part) =>
                {
                    if (part == "elm.text")
                    {
                        return (obj as Item).Text;
                    }
                    return null;
                },
                GetContentHandler = (obj, part) =>
                {
                    if (part == "elm.swallow.icon" && obj is Item menuItem && !string.IsNullOrEmpty(menuItem.Icon))
                    {
                        var icon = new ElmSharp.Image(Xamarin.Forms.Forms.NativeParent)
                        {
                            AlignmentX = -1,
                            AlignmentY = -1,
                            WeightX = 1.0,
                            WeightY = 1.0,
                            MinimumWidth = _dafaultIconSize,
                            MinimumHeight = _dafaultIconSize,
                        };
                        icon.Show();
                        icon.Load(menuItem.Icon);
                        return icon;
                    }
                    return null;
                }
            };

            _naviMenu.ItemSelected += OnItemSelected;

        }

        void OnItemSelected(object sender, GenListItemEventArgs e)
        {
            ItemSelected?.Invoke(this, new SelectedItemChangedEventArgs((e.Item.Data as Item).Source, -1));
        }

        void OnLayout()
        {
            _surfaceLayout.Geometry = Geometry;
            _naviMenu.Geometry = Geometry;
        }
    }
    public enum DraggedState
    {
        EdgeTop,
        Up,
        Down,
        EdgeBottom,
    }

    public class DraggedEventArgs
    {
        public DraggedState State { get; private set; }

        public DraggedEventArgs(DraggedState state)
        {
            State = state;
        }
    }

    static class FileImageSourceEX
    {
        public static string ToAbsPath(this FileImageSource source)
        {
            return ResourcePath.GetPath(source.File);
        }
    }


}
