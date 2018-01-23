using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Xamarin.Forms.CircularUI
{
    public class CirclePage : ContentPage
    {
        public static readonly BindableProperty ActionButtonProperty = BindableProperty.Create(nameof(ActionButton), typeof(ActionButtonItem), typeof(CirclePage), null,
            propertyChanged: (b, o, n) =>
            {
                if (n != null)
                    ((Element)n).Parent = (Element)b;
            });
        public static readonly BindableProperty RotaryFocusTargetNameProperty = BindableProperty.Create(nameof(RotaryFocusTargetName), typeof(string), typeof(CirclePage), null,
            propertyChanged: RotaryFocusTargetNameChanged);

        static readonly BindablePropertyKey RotaryFocusObjectPropertyKey = BindableProperty.CreateReadOnly(nameof(RotaryFocusObject), typeof(IRotaryFocusable), typeof(CirclePage), null);
        public static readonly BindableProperty RotaryFocusObjectProperty = RotaryFocusObjectPropertyKey.BindableProperty;

        static void RotaryFocusTargetNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            CirclePage page = bindable as CirclePage;
            if (page != null && newValue is string)
            {
                string name = newValue as string;
                var obj = page.FindByName<IRotaryFocusable>(name);
                if (obj != null)
                {
                    page.RotaryFocusObject = obj;
                }
            }
        }

        public CirclePage()
        {
            var surfaceItems = new ObservableCollection<ICircleSurfaceItem>();
            surfaceItems.CollectionChanged += OnSurfaceItemsChanged;
            CircleSurfaceItems = surfaceItems;
        }

        public IList<ICircleSurfaceItem> CircleSurfaceItems { get; }

        public ActionButtonItem ActionButton
        {
            get => (ActionButtonItem)GetValue(ActionButtonProperty);
            set => SetValue(ActionButtonProperty, value);
        }

        public IRotaryFocusable RotaryFocusObject
        {
            get => (IRotaryFocusable)GetValue(RotaryFocusObjectProperty);
            private set => SetValue(RotaryFocusObjectPropertyKey, value);
        }

        public string RotaryFocusTargetName
        {
            get => (string)GetValue(RotaryFocusTargetNameProperty);
            set => SetValue(RotaryFocusTargetNameProperty, value);
        }

        void OnSurfaceItemsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action != NotifyCollectionChangedAction.Add) return;

            foreach (Element item in args.NewItems)
            {
                item.Parent = this;
            }
        }
    }
}
