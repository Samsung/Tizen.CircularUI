using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace UIComponents.Extensions.Effects
{
    public class TizenStyleEffect : RoutingEffect
    {
        public static readonly BindableProperty StyleProperty = BindableProperty.CreateAttached("Style", typeof(string), typeof(TizenStyleEffect), null);

        public static string GetStyle(BindableObject velement) => (string)velement.GetValue(StyleProperty);
        public static void SetStyle(BindableObject velement, string value) => velement.SetValue(StyleProperty, value);

        public TizenStyleEffect() : base("SEC.TizenStyleEffect")
        {
        }
    }
}
