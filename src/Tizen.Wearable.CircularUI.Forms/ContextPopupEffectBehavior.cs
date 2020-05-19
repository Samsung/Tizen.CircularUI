using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// The ContextPopupEffectBehavior is a behavior which allows you to add a context popup.
    /// </summary>
    public class ContextPopupEffectBehavior : Behavior<View>
    {
        /// <summary>
        /// BindableProperty. Identifies the AcceptText bindable property. AcceptText is to use as Accept button
        /// </summary>
        public static readonly BindableProperty AcceptTextProperty = BindableProperty.Create(nameof(AcceptText), typeof(string), typeof(ContextPopupEffectBehavior), null);

        /// <summary>
        /// BindableProperty. Identifies the AcceptCommand bindable property. AcceptCommand is executed when the Accept button is pressed.
        /// </summary>
        public static readonly BindableProperty AcceptCommandProperty = BindableProperty.Create(nameof(AcceptCommand), typeof(ICommand), typeof(ContextPopupEffectBehavior), null);

        /// <summary>
        /// BindableProperty. Identifies the AcceptCommandParameter bindable property.
        /// </summary>
        public static readonly BindableProperty AcceptCommandParameterProperty = BindableProperty.Create(nameof(AcceptCommandParameter), typeof(object), typeof(ContextPopupEffectBehavior), null);

        /// <summary>
        /// BindableProperty. Identifies the CancelText bindable property. CancelText is to use as Cancel button
        /// </summary>
        public static readonly BindableProperty CancelTextProperty = BindableProperty.Create(nameof(CancelText), typeof(string), typeof(ContextPopupEffectBehavior), null);

        /// <summary>
        /// BindableProperty. Identifies the CancelCommand bindable property. CancelCommand is executed when the Cancel button is pressed, even if the popup disappears by selecting outside the popup.
        /// </summary>
        public static readonly BindableProperty CancelCommandProperty = BindableProperty.Create(nameof(CancelCommand), typeof(ICommand), typeof(ContextPopupEffectBehavior), null);

        /// <summary>
        /// BindableProperty. Identifies the CancelCommandParameter bindable property.
        /// </summary>
        public static readonly BindableProperty CancelCommandParameterProperty = BindableProperty.Create(nameof(CancelCommandParameter), typeof(object), typeof(ContextPopupEffectBehavior), null);

        /// <summary>
        /// BindableProperty. Identifies the ConfirmVisibility bindable property. Popup appears if ConfirmVisibility is True, and disappears when it becomes False. CancelCommand works even if it disappears to False.
        /// </summary>
        public static readonly BindableProperty VisibilityProperty = BindableProperty.Create(nameof(Visibility), typeof(bool), typeof(ContextPopupEffectBehavior), false);

        /// <summary>
        /// BindableProperty. Identifies the PositionOption bindable property.
        /// </summary>
        public static readonly BindableProperty PositionOptionProperty = BindableProperty.Create(nameof(PositionOption), typeof(PositionOption), typeof(ContextPopupEffectBehavior), PositionOption.BottomOfView);

        /// <summary>
        /// BindableProperty. Identifies the Offset bindable property. Offset is a value of how far to move from the base tap position represented by the PositionOption type. If it isn't set, it returns 0,0.
        /// </summary>
        public static readonly BindableProperty OffsetProperty = BindableProperty.Create(nameof(Offset), typeof(Point), typeof(ContextPopupEffectBehavior), default(Point));

        internal static readonly BindableProperty ContextPopupEffectBehaviorProperty = BindableProperty.CreateAttached("ContextPopupEffectBehavior", typeof(ContextPopupEffectBehavior), typeof(View), null);

        View TargetView { get; set; }

        /// <summary>
        /// Gets or sets text of Accept button
        /// </summary>
        public string AcceptText
        {
            get => (string)GetValue(AcceptTextProperty);
            set => SetValue(AcceptTextProperty, value);
        }

        /// <summary>
        /// Gets or sets command that is executed when the Accept button is pressed.
        /// </summary>
        public ICommand AcceptCommand
        {
            get => (ICommand)GetValue(AcceptCommandProperty);
            set => SetValue(AcceptCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets command paramter that is executed when the Accept button is pressed.
        /// </summary>
        public object AcceptCommandParameter
        {
            get => GetValue(AcceptCommandParameterProperty);
            set => SetValue(AcceptCommandParameterProperty, value);
        }

        /// <summary>
        /// Gets or sets text of Cancel button
        /// </summary>
        public string CancelText
        {
            get => (string)GetValue(CancelTextProperty);
            set => SetValue(CancelTextProperty, value);
        }

        /// <summary>
        /// Gets or sets command that is executed when the Cancel button is pressed, even if the popup disappears by selecting outside the popup.
        /// </summary>
        public ICommand CancelCommand
        {
            get => (ICommand)GetValue(CancelCommandProperty);
            set => SetValue(CancelCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets command that is executed when the Cancel button is pressed, even if the popup disappears by selecting outside the popup.
        /// </summary>
        public object CancelCommandParameter
        {
            get => GetValue(CancelCommandParameterProperty);
            set => SetValue(CancelCommandParameterProperty, value);
        }

        /// <summary>
        /// Gets visibility of Confirmation popup. Popup appears if ConfirmVisibility is True, and disappears when it becomes False. CancelCommand works even if it disappears to False.
        /// </summary>
        public bool Visibility
        {
            get => (bool)GetValue(VisibilityProperty);
            set => SetValue(VisibilityProperty, value);
        }

        /// <summary>
        /// Gets or sets position type of popup
        /// </summary>
        public PositionOption PositionOption
        {
            get => (PositionOption)GetValue(PositionOptionProperty);
            set => SetValue(PositionOptionProperty, value);
        }

        /// <summary>
        /// Gets or sets offset. Offset is a value of how far to move from the base tap position represented by the PositionOption type. If it isn't set, it returns 0,0.
        /// </summary>
        public Point Offset
        {
            get => (Point)GetValue(OffsetProperty);
            set => SetValue(OffsetProperty, value);
        }

        protected override void OnAttachedTo(View view)
        {
            base.OnAttachedTo(view);
            TargetView = view;
            view.SetValue(ContextPopupEffectBehaviorProperty, this);

            System.Diagnostics.Debug.WriteLine($"TargetView = {TargetView}");

            var effect = Effect.Resolve("CircleUI.ContextPopupEffectBehavior");
            if (effect != null)
            {
                view.Effects.Add(effect);
            }

            global::System.Diagnostics.Debug.WriteLine($"OnAttachedTo");
        }

        protected override void OnDetachingFrom(View view)
        {
            view.RemoveBinding(ContextPopupEffectBehaviorProperty);

            var effect = Effect.Resolve("CircleUI.ContextPopupEffectBehavior");
            if (effect != null)
            {
                view.Effects.Remove(effect);
            }
            base.OnDetachingFrom(view);

            global::System.Diagnostics.Debug.WriteLine($"OnDettachedTo");
        }
    }
}
