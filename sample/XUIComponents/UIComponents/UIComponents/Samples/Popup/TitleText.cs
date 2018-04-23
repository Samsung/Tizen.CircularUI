using Tizen.Wearable.CircularUI.Forms;

namespace UIComponents.Samples.Popup
{
    public class TitleText : InformationPopup
    {
        public TitleText()
        {
            Title = "Title";
            Text = "This Popup has title area";
            BackButtonPressed += (s, e) => { this.Dismiss(); };
        }
    }
}