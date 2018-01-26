using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// The ConfirmPopupEffect is an effect that is proportional to a particular widget or has one or two buttons anywhere on the screen
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class ConfirmPopupEffect : RoutingEffect
    {
        /// <summary>
        /// BindableProperty. Identifies the AcceptText bindable property. AcceptText is to use as Accept button
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static BindableProperty AcceptTextProperty = BindableProperty.CreateAttached("AcceptText", typeof(string), typeof(ConfirmPopupEffect), null);
        /// <summary>
        /// BindableProperty. Identifies the AcceptCommand bindable property. AcceptCommand is executed when the Accept button is pressed.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static BindableProperty AcceptCommandProperty = BindableProperty.CreateAttached("AcceptCommand", typeof(ICommand), typeof(ConfirmPopupEffect), null);
        /// <summary>
        /// BindableProperty. Identifies the AcceptCommandParameter bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static BindableProperty AcceptCommandParameterProperty = BindableProperty.CreateAttached("AcceptCommandParameter", typeof(object), typeof(ConfirmPopupEffect), null);

        /// <summary>
        /// BindableProperty. Identifies the CancelText bindable property. CancelText is to use as Cancel button
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static BindableProperty CancelTextProperty = BindableProperty.CreateAttached("CancelText", typeof(string), typeof(ConfirmPopupEffect), null);
        /// <summary>
        /// BindableProperty. Identifies the CancelCommand bindable property. CancelCommand is executed when the Cancel button is pressed, even if the popup disappears by selecting outside the popup.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static BindableProperty CancelCommandProperty = BindableProperty.CreateAttached("CancelCommand", typeof(ICommand), typeof(ConfirmPopupEffect), null);
        /// <summary>
        /// BindableProperty. Identifies the CancelCommandParameter bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static BindableProperty CancelCommandParameterProperty = BindableProperty.CreateAttached("CancelCommandParameter", typeof(object), typeof(ConfirmPopupEffect), null);

        /// <summary>
        /// BindableProperty. Identifies the ConfirmVisibility bindable property. Popup appears if ConfirmVisibility is True, and disappears when it becomes False. CancelCommand works even if it disappears to False.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static BindableProperty ConfirmVisibilityProperty = BindableProperty.CreateAttached("ConfirmVisibility", typeof(bool), typeof(ConfirmPopupEffect), false);

        /// <summary>
        /// BindableProperty. Identifies the PositionOption bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static BindableProperty PositionOptionProperty = BindableProperty.CreateAttached("PositionOption", typeof(PositionOption), typeof(ConfirmPopupEffect), PositionOption.BottomOfView);
        /// <summary>
        /// BindableProperty. Identifies the Offset bindable property. Offset is a value of how far to move from the base tap position represented by the PositionOption type. If it isn't set, it returns 0,0.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static BindableProperty OffsetProperty = BindableProperty.CreateAttached("Offset", typeof(Point), typeof(ConfirmPopupEffect), default(Point));

        /// <summary>
        /// Gets text of Accept button
        /// </summary>
        /// <param name="obj">Binded object</param>
        /// <returns>Text of Accept button</returns>
        /// <since_tizen> 4 </since_tizen>
        public static string GetAcceptText(BindableObject obj) => (string)obj.GetValue(AcceptTextProperty);
        /// <summary>
        /// Sets text of Accept button
        /// </summary>
        /// <param name="obj">Binded object</param>
        /// <param name="value">Text of Accept button</param>
        /// <since_tizen> 4 </since_tizen>
        public static void SetAcceptText(BindableObject obj, string value) => obj.SetValue(AcceptTextProperty, value);
        /// <summary>
        /// Gets command that is executed when the Accept button is pressed.
        /// </summary>
        /// <param name="obj">Binded object</param>
        /// <returns>Command of Accept button</returns>
        /// <since_tizen> 4 </since_tizen>
        public static ICommand GetAcceptCommand(BindableObject obj) => (ICommand)obj.GetValue(AcceptCommandProperty);
        /// <summary>
        /// Sets command that is executed when the Accept button is pressed.
        /// </summary>
        /// <param name="obj">Binded object</param>
        /// <param name="value">Command of Accept button</param>
        /// <since_tizen> 4 </since_tizen>
        public static void SetAcceptCommand(BindableObject obj, ICommand value) => obj.SetValue(AcceptCommandProperty, value);
        /// <summary>
        /// Gets command paramter that is executed when the Accept button is pressed.
        /// </summary>
        /// <param name="obj">Binded object</param>
        /// <returns>Command parameter of Accept button</returns>
        /// <since_tizen> 4 </since_tizen>
        public static object GetAcceptCommandParameter(BindableObject obj) => obj.GetValue(AcceptCommandParameterProperty);
        /// <summary>
        /// Sets command parameter that is executed when the Accept button is pressed.
        /// </summary>
        /// <param name="obj">Binded object</param>
        /// <param name="value">Command parameter of Accept button</param>
        /// <since_tizen> 4 </since_tizen>
        public static void SetAcceptCommandParameter(BindableObject obj, object value) => obj.SetValue(AcceptCommandParameterProperty, value);

        /// <summary>
        /// Gets text of Cancel button
        /// </summary>
        /// <param name="obj">Binded object</param>
        /// <returns>Text of Cancel button</returns>
        /// <since_tizen> 4 </since_tizen>
        public static string GetCancelText(BindableObject obj) => (string)obj.GetValue(CancelTextProperty);
        /// <summary>
        /// Sets text of Cancel button
        /// </summary>
        /// <param name="obj">Binded object</param>
        /// <param name="value">Text of Cancel button</param>
        /// <since_tizen> 4 </since_tizen>
        public static void SetCancelText(BindableObject obj, string value) => obj.SetValue(CancelTextProperty, value);
        /// <summary>
        /// Gets command that is executed when the Cancel button is pressed, even if the popup disappears by selecting outside the popup.
        /// </summary>
        /// <param name="obj">Binded object</param>
        /// <returns>Command of Cancel button</returns>
        /// <since_tizen> 4 </since_tizen>
        public static ICommand GetCancelCommand(BindableObject obj) => (ICommand)obj.GetValue(CancelCommandProperty);
        /// <summary>
        /// Sets command that is executed when the Cancel button is pressed, even if the popup disappears by selecting outside the popup.
        /// </summary>
        /// <param name="obj">Binded object</param>
        /// <param name="value">Command of Cancel button</param>
        /// <since_tizen> 4 </since_tizen>
        public static void SetCancelCommand(BindableObject obj, ICommand value) => obj.SetValue(CancelCommandProperty, value);
        /// <summary>
        /// Gets command paramter that is executed when the Cancel button is pressed, even if the popup disappears by selecting outside the popup.
        /// </summary>
        /// <param name="obj">Binded object</param>
        /// <returns>Command parameter of Cancel button</returns>
        /// <since_tizen> 4 </since_tizen>
        public static object GetCancelCommandParameter(BindableObject obj) => obj.GetValue(CancelCommandParameterProperty);
        /// <summary>
        /// Sets command paramter that is executed when the Cancel button is pressed, even if the popup disappears by selecting outside the popup.
        /// </summary>
        /// <param name="obj">Binded object</param>
        /// <param name="value">Command parameter of Cancel button</param>
        /// <since_tizen> 4 </since_tizen>
        public static void SetCancelCommandParameter(BindableObject obj, object value) => obj.SetValue(CancelCommandParameterProperty, value);

        /// <summary>
        /// Gets visibility of Confirmation popup. Popup appears if ConfirmVisibility is True, and disappears when it becomes False. CancelCommand works even if it disappears to False.
        /// </summary>
        /// <param name="obj">Binded object</param>
        /// <returns>Visibility of Confirmation popup</returns>
        /// <since_tizen> 4 </since_tizen>
        public static bool GetConfirmVisibility(BindableObject obj) => (bool)obj.GetValue(ConfirmVisibilityProperty);
        /// <summary>
        /// Sets visibility of Confirmation popup. Popup appears if ConfirmVisibility is True, and disappears when it becomes False. CancelCommand works even if it disappears to False.
        /// </summary>
        /// <param name="obj">Binded object</param>
        /// <param name="value">Visibility of Confirmation popup</param>
        /// <since_tizen> 4 </since_tizen>
        public static void SetConfirmVisibility(BindableObject obj, bool value) => obj.SetValue(ConfirmVisibilityProperty, value);

        /// <summary>
        /// Gets position type of popup
        /// </summary>
        /// <param name="obj">Binded object</param>
        /// <returns>Position type of popup</returns>
        /// <since_tizen> 4 </since_tizen>
        public static PositionOption GetPositionOption(BindableObject obj) => (PositionOption)obj.GetValue(PositionOptionProperty);
        /// <summary>
        /// Sets position type of popup
        /// </summary>
        /// <param name="obj">Binded object</param>
        /// <param name="value">Position type of popup</param>
        /// <since_tizen> 4 </since_tizen>
        public static void SetPositionOption(BindableObject obj, PositionOption value) => obj.SetValue(PositionOptionProperty, value);

        /// <summary>
        /// Gets offset. Offset is a value of how far to move from the base tap position represented by the PositionOption type. If it isn't set, it returns 0,0.
        /// </summary>
        /// <param name="obj">Binded object</param>
        /// <returns>A value of how far to move from the base tap position represented by the PositionOption type</returns>
        /// <since_tizen> 4 </since_tizen>
        public static Point GetOffset(BindableObject obj) => (Point)obj.GetValue(OffsetProperty);
        /// <summary>
        /// Sets offset. Offset is a value of how far to move from the base tap position represented by the PositionOption type.
        /// </summary>
        /// <param name="obj">Binded object</param>
        /// <param name="value">A value of how far to move from the base tap position represented by the PositionOption type</param>
        /// <since_tizen> 4 </since_tizen>
        public static void SetOffset(BindableObject obj, Point value) => obj.SetValue(OffsetProperty, value);

        /// <summary>
        /// Creates and initializes a new instance of the ConfirmPopupEffect class
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public ConfirmPopupEffect() : base("CircleUI.ConfirmPopupEffect")
        {
        }
    }
}
