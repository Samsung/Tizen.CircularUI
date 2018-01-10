using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Xamarin.Forms.CircularUI
{
    public class ConfirmPopupEffect : RoutingEffect
    {
        public static BindableProperty AcceptTextProperty = BindableProperty.CreateAttached("AcceptText", typeof(string), typeof(ConfirmPopupEffect), null);
        public static BindableProperty AcceptCommandProperty = BindableProperty.CreateAttached("AcceptCommand", typeof(ICommand), typeof(ConfirmPopupEffect), null);
        public static BindableProperty AcceptCommandParameterProperty = BindableProperty.CreateAttached("AcceptCommand", typeof(object), typeof(ConfirmPopupEffect), null);

        public static BindableProperty CancelTextProperty = BindableProperty.CreateAttached("CancelText", typeof(string), typeof(ConfirmPopupEffect), null);
        public static BindableProperty CancelCommandProperty = BindableProperty.CreateAttached("CancelCommand", typeof(ICommand), typeof(ConfirmPopupEffect), null);
        public static BindableProperty CancelCommandParameterProperty = BindableProperty.CreateAttached("CancelCommandParameter", typeof(object), typeof(ConfirmPopupEffect), null);

        public static BindableProperty ConfirmVisibilityProperty = BindableProperty.CreateAttached("ConfirmVisibility", typeof(bool), typeof(ConfirmPopupEffect), false);

        public static BindableProperty PositionOptionProperty = BindableProperty.CreateAttached("PositionOption", typeof(PositionOption), typeof(ConfirmPopupEffect), PositionOption.BottomOfView);
        public static BindableProperty OffsetProperty = BindableProperty.CreateAttached("Offset", typeof(Point), typeof(ConfirmPopupEffect), default(Point));

        public static string GetAcceptText(BindableObject obj) => (string)obj.GetValue(AcceptTextProperty);
        public static void SetAcceptText(BindableObject obj, string value) => obj.SetValue(AcceptTextProperty, value);
        public static ICommand GetAcceptCommand(BindableObject obj) => (ICommand)obj.GetValue(AcceptCommandProperty);
        public static void SetAcceptCommand(BindableObject obj, ICommand value) => obj.SetValue(AcceptCommandProperty, value);
        public static object GetAcceptCommandParameter(BindableObject obj) => obj.GetValue(AcceptCommandParameterProperty);
        public static void SetAcceptCommandParameter(BindableObject obj, object value) => obj.SetValue(AcceptCommandParameterProperty, value);

        public static string GetCancelText(BindableObject obj) => (string)obj.GetValue(CancelTextProperty);
        public static void SetCancelText(BindableObject obj, string value) => obj.SetValue(CancelTextProperty, value);
        public static ICommand GetCancelCommand(BindableObject obj) => (ICommand)obj.GetValue(CancelCommandProperty);
        public static void SetCancelCommand(BindableObject obj, ICommand value) => obj.SetValue(CancelCommandProperty, value);
        public static object GetCancelCommandParameter(BindableObject obj) => obj.GetValue(CancelCommandParameterProperty);
        public static void SetCancelCommandParameter(BindableObject obj, object value) => obj.SetValue(CancelCommandParameterProperty, value);

        public static bool GetConfirmVisibility(BindableObject obj) => (bool)obj.GetValue(ConfirmVisibilityProperty);
        public static void SetConfirmVisibility(BindableObject obj, bool value) => obj.SetValue(ConfirmVisibilityProperty, value);

        public static PositionOption GetPositionOption(BindableObject obj) => (PositionOption)obj.GetValue(PositionOptionProperty);
        public static void SetPositionOption(BindableObject obj, PositionOption value) => obj.SetValue(PositionOptionProperty, value);

        public static Point GetOffset(BindableObject obj) => (Point)obj.GetValue(OffsetProperty);
        public static void SetOffset(BindableObject obj, Point value) => obj.SetValue(OffsetProperty, value);

        public ConfirmPopupEffect() : base("CircleUI.ConfirmPopupEffect")
        {
        }
    }
    public enum PositionOption
    {
        BottomOfView,
        CenterOfWindow,
        Absolute,
        Relative
    }
}
