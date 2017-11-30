using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ELayout = ElmSharp.Layout;
using EContextPopup = ElmSharp.ContextPopup;
using EContextPopupDirection = ElmSharp.ContextPopupDirection;
using EContextPopupItem = ElmSharp.ContextPopupItem;
using TForms = Xamarin.Forms.Platform.Tizen.Forms;
using XForms = Xamarin.Forms;
using XFPlatformTizen = Xamarin.Forms.Platform.Tizen;


[assembly: XForms.Dependency(typeof(Xamarin.Forms.CircularUI.Renderer.ContextPopupImplementation))]
namespace Xamarin.Forms.CircularUI.Renderer
{

    internal class ContextPopupImplementation : IContextPopup, INotifyPropertyChanged, IDisposable
    {
        EContextPopup _popup;
        ELayout _layout;
        IDictionary<ContextPopupItem, EContextPopupItem> _items;
        ContextPopupItem _selectedItem = null;
        bool _isDisposed;

        public ContextPopupImplementation()
        {
            _layout = new ELayout(TForms.Context.MainWindow)
            {
                WeightX = 1.0, // Expand
                WeightY = 1.0  // Expand
            };
            TForms.Context.MainWindow.AddResizeObject(_layout);
            _layout.SetTheme("layout", "select_mode", "default");
            _layout.Show();

            _popup = new EContextPopup(_layout)
            {
                Style = "select_mode",
            };

            _popup.BackButtonPressed += (s, e) =>
            {
                _popup.Dismiss();
            };

            _popup.Dismissed += (s, e) =>
            {
                Dismissed?.Invoke(this, EventArgs.Empty);
            };

            _popup.SetDirectionPriorty(
              EContextPopupDirection.Down,
              EContextPopupDirection.Down,
              EContextPopupDirection.Down,
              EContextPopupDirection.Down);

            _items = new Dictionary<ContextPopupItem, EContextPopupItem>();
        }

        ~ContextPopupImplementation()
        {
            Dispose(false);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

        public event EventHandler Dismissed;

        public ContextPopupItem SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed)
                return;

            if (isDisposing)
            {
                if (_popup != null)
                {
                    _popup.Unrealize();
                    _popup = null;
                }
            }

            _isDisposed = true;
        }



        public void AddItems(IEnumerable<ContextPopupItem> items)
        {
            foreach (var item in items)
            {
                item.PropertyChanged += ContextPopupItemPropertyChanged;
                AddItem(item);
            }
        }

        public void RemoveItems(IEnumerable<ContextPopupItem> items)
        {
            foreach (var item in items)
            {
                item.PropertyChanged -= ContextPopupItemPropertyChanged;
                if (_items.ContainsKey(item))
                {
                    var nativeItem = _items[item];
                    nativeItem.Delete();
                    _items.Remove(item);
                }
            }
        }

        public void ClearItems()
        {
            foreach (var item in _items.Keys)
                item.PropertyChanged -= ContextPopupItemPropertyChanged;

            _items.Clear();
            _popup.Clear();
        }

        public void Show(View anchor, int xAnchorOffset, int yAnchorOffset)
        {
            //Console.WriteLine("Show() anchor:" + anchor + ", xAnchorOffset:"+ xAnchorOffset + ", yAnchorOffset:" + yAnchorOffset);
            var geometry = XFPlatformTizen.Platform.GetRenderer(anchor).NativeView.Geometry;
            _popup.Move(geometry.X + xAnchorOffset, geometry.Y + yAnchorOffset);
            _popup.Show();

            if (_items.Count >= 2)
            {
                int index = 0;
                foreach (var item in _items)
                {
                    var nativeItem = item.Value;
                    if(index % 2 ==  0)
                    {
                        nativeItem.Style = "select_mode/top";
                    }
                    else
                    {
                        nativeItem.Style = "select_mode/bottom";
                    }
                    index++;
                }
            }
        }

        public void Dismiss()
        {
            _popup.Dismiss();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        void ContextPopupItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = sender as ContextPopupItem;

            if (e.PropertyName == nameof(ContextPopupItem.Label))
            {
                // If the native item already has a label
                EContextPopupItem nativeItem = _items[item];
                nativeItem.SetPartText("default", item.Label);
            }
        }

        void AddItem(ContextPopupItem item)
        {
            if (_items.ContainsKey(item))
                return;

            EContextPopupItem nativeItem;
            nativeItem = _popup.Append(item.Label);
            _items.Add(item, nativeItem);

            nativeItem.Selected += (s, e) =>
            {
                SelectedItem = item; // This will invoke SelectedIndexChanged if the index has changed
                ItemSelected?.Invoke(this, new SelectedItemChangedEventArgs(SelectedItem));
            };
        }
    }
}
