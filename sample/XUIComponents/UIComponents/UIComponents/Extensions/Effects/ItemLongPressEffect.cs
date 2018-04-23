using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace UIComponents.Extensions.Effects
{
    public class ItemLongPressEffect : RoutingEffect
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.CreateAttached("Command", typeof(ICommand), typeof(ItemLongPressEffect), null);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.CreateAttached("CommandParameter", typeof(object), typeof(ItemLongPressEffect), null);

        public static Command GetCommand(BindableObject view) => (Command)view.GetValue(CommandProperty);
        public static void SetCommand(BindableObject view, ICommand value) => view.SetValue(CommandProperty, value);

        public static object GetCommandParameter(BindableObject view) => view.GetValue(CommandParameterProperty);
        public static void SetCommandParameter(BindableObject view, object value) => view.SetValue(CommandParameterProperty, value);

        public ItemLongPressEffect() : base("SEC.ItemLongPressEffect")
        {
        }
    }
}
