using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ResolutionGroupName("SEC")]
[assembly: ExportEffect(typeof(UIComponents.Tizen.Wearable.Effects.TizenItemLongPressEffect), "ItemLongPressEffect")]
namespace UIComponents.Tizen.Wearable.Effects
{
    public class TizenItemLongPressEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var genlist = Control as ElmSharp.GenList;
            if (genlist != null)
            {
                genlist.ItemLongPressed += ItemLongPressed;
            }
        }

        protected override void OnDetached()
        {
            var genlist = this.Control as ElmSharp.GenList;
            if (genlist != null)
            {
                genlist.ItemLongPressed -= ItemLongPressed;
            }
        }

        void ItemLongPressed(object sender, ElmSharp.GenListItemEventArgs e)
        {
            var command = UIComponents.Extensions.Effects.ItemLongPressEffect.GetCommand(Element);
            command?.Execute(UIComponents.Extensions.Effects.ItemLongPressEffect.GetCommandParameter(Element));
        }
    }
}
