using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// CircleSurface is similar to ContentPage, which allows you to put in views that you need, and you can show ToolbarItems with MoreOption.
    /// It has an ActionButton, and can use the MenuItem type as text, icon, command, and so on.
    /// </summary>
    /// <example>
    /// <code>
    /// <w:CirclePage BackgroundColor="Blue" RotaryFocusTargetName="DateSelector">
    /// <w:CirclePage.Content>
    ///   <StackLayout>
    ///     <w:CircleDateTimeSelector x:Name="DateSelector"/>
    ///     <w:Button Text = "OK" />
    ///   </ StackLayout >
    /// </ w:CirclePage.Content>
    /// </w:CirclePage>
    /// </code>
    /// </example>
    public class CirclePage : ContentPage
    {
        /// <summary>
        /// BindableProperty type of Action button
        /// </summary>
        public static readonly BindableProperty ActionButtonProperty = BindableProperty.Create(nameof(ActionButton), typeof(ActionButtonItem), typeof(CirclePage), null,
            propertyChanged: (b, o, n) =>
            {
                if (n != null)
                    ((Element)n).Parent = (Element)b;
            });
        /// <summary>
        /// BindableProperty type of RotaryFocusTargetName
        /// </summary>
        public static readonly BindableProperty RotaryFocusTargetNameProperty = BindableProperty.Create(nameof(RotaryFocusTargetName), typeof(string), typeof(CirclePage), null,
            propertyChanged: RotaryFocusTargetNameChanged);
        /// <summary>
        /// BindableProperty type of RotaryFocusObjectPropertyKey
        /// </summary>
        static readonly BindablePropertyKey RotaryFocusObjectPropertyKey = BindableProperty.CreateReadOnly(nameof(RotaryFocusObject), typeof(IRotaryFocusable), typeof(CirclePage), null);
        /// <summary>
        /// BindableProperty type of RotaryFocusObject
        /// </summary>
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
        /// <summary>
        /// Creates and initializes a new instance of the CirclePage class.
        /// </summary>
        public CirclePage()
        {
            var surfaceItems = new ObservableCollection<ICircleSurfaceItem>();
            surfaceItems.CollectionChanged += OnSurfaceItemsChanged;
            CircleSurfaceItems = surfaceItems;
        }
        /// <summary>
        /// Objects represented in CircleSurface are represented through CircleSurface.
        /// </summary>
        public IList<ICircleSurfaceItem> CircleSurfaceItems { get; }

        /// <summary>
        /// presents a menu item and associates it with a command
        /// </summary>
        public ActionButtonItem ActionButton
        {
            get => (ActionButtonItem)GetValue(ActionButtonProperty);
            set => SetValue(ActionButtonProperty, value);
        }
        /// <summary>
        /// register only one Consumer in the RotaryFocusObject property to receive a Bezel Action (take a Rotary Event) from the current page.
        /// </summary>
        public IRotaryFocusable RotaryFocusObject
        {
            get => (IRotaryFocusable)GetValue(RotaryFocusObjectProperty);
            private set => SetValue(RotaryFocusObjectPropertyKey, value);
        }

        /// <summary>
        /// specify a RotaryFocusObject by name on Xaml.
        /// </summary>
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
