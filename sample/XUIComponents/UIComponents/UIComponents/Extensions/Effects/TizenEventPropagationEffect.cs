using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace UIComponents.Extensions.Effects
{
    public class TizenEventPropagationEffect : RoutingEffect
    {
        public static BindableProperty EnablePropagationProperty = BindableProperty.CreateAttached("EnablePropagation", typeof(bool), typeof(TizenEventPropagationEffect), false);

        public static bool GetEnablePropagation(BindableObject view) => (bool)view.GetValue(EnablePropagationProperty);
        public static void SetEnablePropagation(BindableObject view, bool value) => view.SetValue(EnablePropagationProperty, value);

        public TizenEventPropagationEffect() : base("SEC.TizenEventPropagationEffect")
        {
        }
    }
}
