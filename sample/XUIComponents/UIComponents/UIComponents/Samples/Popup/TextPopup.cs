using Tizen.Wearable.CircularUI.Forms;

namespace UIComponents.Samples.Popup
{
    public class TextPopup : InformationPopup
    {
        public TextPopup()
        {
            Text = "This has only texts. This is set by object";
            BackButtonPressed += (s, e) => { this.Dismiss(); };
        }
    }
}