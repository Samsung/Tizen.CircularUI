using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// A popup that is proportional to a particular widget or has one or two buttons anywhere on the screen
    /// </summary>
    public class ConfirmPopupEffect : RoutingEffect
    {
        /// <summary>
        /// BindableProperty type of AcceptText. AcceptText is to use as Accept button
        /// </summary>
        public static BindableProperty AcceptTextProperty = BindableProperty.CreateAttached("AcceptText", typeof(string), typeof(ConfirmPopupEffect), null);
        /// <summary>
        /// BindableProperty type of AcceptCommand. AcceptCommand is to be executed when Accept
        /// </summary>
        public static BindableProperty AcceptCommandProperty = BindableProperty.CreateAttached("AcceptCommand", typeof(ICommand), typeof(ConfirmPopupEffect), null);
        /// <summary>
        /// BindableProperty type of AcceptCommandParameter
        /// </summary>
        public static BindableProperty AcceptCommandParameterProperty = BindableProperty.CreateAttached("AcceptCommand", typeof(object), typeof(ConfirmPopupEffect), null);

        /// <summary>
        /// BindableProperty type of CancelText. CancelText is to use as Cancel button
        /// </summary>
        public static BindableProperty CancelTextProperty = BindableProperty.CreateAttached("CancelText", typeof(string), typeof(ConfirmPopupEffect), null);
        /// <summary>
        /// BindableProperty type of CancelCommand. CancelCommand is executed at the time of cancellation, even if the popup disappears by selecting outside the popup.
        /// </summary>
        public static BindableProperty CancelCommandProperty = BindableProperty.CreateAttached("CancelCommand", typeof(ICommand), typeof(ConfirmPopupEffect), null);
        /// <summary>
        /// BindableProperty type of CancelCommandParameter
        /// </summary>
        public static BindableProperty CancelCommandParameterProperty = BindableProperty.CreateAttached("CancelCommandParameter", typeof(object), typeof(ConfirmPopupEffect), null);

        /// <summary>
        /// BindableProperty type of ConfirmVisibility. Popup appears if ConfirmVisibility is True, and disappears when it becomes False. CancelCommand works even if it disappears to False.
        /// </summary>
        public static BindableProperty ConfirmVisibilityProperty = BindableProperty.CreateAttached("ConfirmVisibility", typeof(bool), typeof(ConfirmPopupEffect), false);

        /// <summary>
        /// BindableProperty type of PositionOption
        /// </summary>
        public static BindableProperty PositionOptionProperty = BindableProperty.CreateAttached("PositionOption", typeof(PositionOption), typeof(ConfirmPopupEffect), PositionOption.BottomOfView);
        /// <summary>
        /// BindableProperty type of Offset. Offset is a value applied according to PositionOption. 0,0 The start position is the tip of the popup
        /// </summary>
        public static BindableProperty OffsetProperty = BindableProperty.CreateAttached("Offset", typeof(Point), typeof(ConfirmPopupEffect), default(Point));

        /// <summary>
        /// Get Accept Text
        /// </summary>
        /// <param name="obj">binded object</param>
        /// <returns></returns>
        public static string GetAcceptText(BindableObject obj) => (string)obj.GetValue(AcceptTextProperty);
        /// <summary>
        /// Set Accept Text
        /// </summary>
        /// <param name="obj">binded object</param>
        /// <param name="value">Accept Text</param>
        public static void SetAcceptText(BindableObject obj, string value) => obj.SetValue(AcceptTextProperty, value);
        /// <summary>
        /// Get AcceptCommand. AcceptCommand is to be executed when Accept
        /// </summary>
        /// <param name="obj">binded object</param>
        /// <returns></returns>
        public static ICommand GetAcceptCommand(BindableObject obj) => (ICommand)obj.GetValue(AcceptCommandProperty);
        /// <summary>
        /// Set AcceptCommand. AcceptCommand is to be executed when Accept
        /// </summary>
        /// <param name="obj">binded object</param>
        /// <param name="value">AcceptCommand</param>
        public static void SetAcceptCommand(BindableObject obj, ICommand value) => obj.SetValue(AcceptCommandProperty, value);
        /// <summary>
        /// Get AcceptCommand Parameter
        /// </summary>
        /// <param name="obj">binded object</param>
        /// <returns></returns>
        public static object GetAcceptCommandParameter(BindableObject obj) => obj.GetValue(AcceptCommandParameterProperty);
        /// <summary>
        /// Set AcceptCommand Parameter
        /// </summary>
        /// <param name="obj">binded object</param>
        /// <param name="value">AcceptCommand Parameter</param>
        public static void SetAcceptCommandParameter(BindableObject obj, object value) => obj.SetValue(AcceptCommandParameterProperty, value);

        /// <summary>
        /// Get CancelText
        /// </summary>
        /// <param name="obj">binded object</param>
        /// <returns></returns>
        public static string GetCancelText(BindableObject obj) => (string)obj.GetValue(CancelTextProperty);
        /// <summary>
        /// Set CancelText
        /// </summary>
        /// <param name="obj">binded object</param>
        /// <param name="value">CancelText</param>
        public static void SetCancelText(BindableObject obj, string value) => obj.SetValue(CancelTextProperty, value);
        /// <summary>
        /// Get CancelCommand. CancelCommand is executed at the time of cancellation, even if the popup disappears by selecting outside the popup.
        /// </summary>
        /// <param name="obj">binded object</param>
        /// <returns></returns>
        public static ICommand GetCancelCommand(BindableObject obj) => (ICommand)obj.GetValue(CancelCommandProperty);
        /// <summary>
        /// Set CancelCommand. CancelCommand is executed at the time of cancellation, even if the popup disappears by selecting outside the popup.
        /// </summary>
        /// <param name="obj">binded object</param>
        /// <param name="value">CancelCommand</param>
        public static void SetCancelCommand(BindableObject obj, ICommand value) => obj.SetValue(CancelCommandProperty, value);
        /// <summary>
        /// Get CancelCommandParameter
        /// </summary>
        /// <param name="obj">binded object</param>
        /// <returns></returns>
        public static object GetCancelCommandParameter(BindableObject obj) => obj.GetValue(CancelCommandParameterProperty);
        /// <summary>
        /// Set CancelCommandParameter
        /// </summary>
        /// <param name="obj">binded object</param>
        /// <param name="value">CancelCommandParameter</param>
        public static void SetCancelCommandParameter(BindableObject obj, object value) => obj.SetValue(CancelCommandParameterProperty, value);

        /// <summary>
        /// Get visibility of Confirmation popup. Popup appears if ConfirmVisibility is True, and disappears when it becomes False. CancelCommand works even if it disappears to False.
        /// </summary>
        /// <param name="obj">binded object</param>
        /// <returns></returns>
        public static bool GetConfirmVisibility(BindableObject obj) => (bool)obj.GetValue(ConfirmVisibilityProperty);
        /// <summary>
        /// Set visibility of Confirmation popup. Popup appears if ConfirmVisibility is True, and disappears when it becomes False. CancelCommand works even if it disappears to False.
        /// </summary>
        /// <param name="obj">binded object</param>
        /// <param name="value">visibility</param>
        public static void SetConfirmVisibility(BindableObject obj, bool value) => obj.SetValue(ConfirmVisibilityProperty, value);

        /// <summary>
        /// Get Position type of popup
        /// </summary>
        /// <param name="obj">binded object</param>
        /// <returns></returns>
        public static PositionOption GetPositionOption(BindableObject obj) => (PositionOption)obj.GetValue(PositionOptionProperty);
        /// <summary>
        /// Set Position type of popup
        /// </summary>
        /// <param name="obj">binded object</param>
        /// <param name="value">Position type</param>
        public static void SetPositionOption(BindableObject obj, PositionOption value) => obj.SetValue(PositionOptionProperty, value);

        /// <summary>
        /// Get offset. Offset is a value applied according to PositionOption. 0,0 The start position is the tip of the popup
        /// </summary>
        /// <param name="obj">binded object</param>
        /// <returns></returns>
        public static Point GetOffset(BindableObject obj) => (Point)obj.GetValue(OffsetProperty);
        /// <summary>
        /// Set offset. Offset is a value applied according to PositionOption. 0,0 The start position is the tip of the popup
        /// </summary>
        /// <param name="obj">binded object</param>
        /// <param name="value">offset</param>
        public static void SetOffset(BindableObject obj, Point value) => obj.SetValue(OffsetProperty, value);

        /// <summary>
        /// Creates and initializes a new instance of the ConfirmPopupEffect class
        /// </summary>
        public ConfirmPopupEffect() : base("CircleUI.ConfirmPopupEffect")
        {
        }
    }

    /// <summary>
    /// Position type of popup
    /// </summary>
    public enum PositionOption
    {
        /// <summary>
        /// The popup appears at the bottom of the View using the Effect. The position is changed by Offset in the center of View.
        /// </summary>
        BottomOfView,
        /// <summary>
        /// In the center of the screen, move by the Offset in the Popup.
        /// </summary>
        CenterOfParent,
        /// <summary>
        /// The value of Offset is X, Y and popup is placed on the screen.
        /// </summary>
        Absolute,
        /// <summary>
        /// set Offset.X * Window.Width, Offset.Y * Window.Height.
        /// </summary>
        Relative
    }
}
