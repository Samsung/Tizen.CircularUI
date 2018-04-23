using Tizen.Wearable.CircularUI.Forms;

namespace UIComponents.Samples.Popup
{
    public class TextScrollable : InformationPopup
    {
        public TextScrollable()
        {
            Text = @"This is scrollable popup text.
This part is made by adding long text in popup. Popup internally added
scroller to this layout when size of text is greater than total popup
height. This popup does not have buttons.";

            BackButtonPressed += (s, e) => { this.Dismiss(); };
        }
    }
}