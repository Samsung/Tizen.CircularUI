using Tizen.Wearable.CircularUI.Forms;

namespace UIComponents.Samples.Popup
{
    public class TitleText : InformationPopup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TitleText()
        {
            Title = "Title";
            Text = "This Popup has title area";

            // Add event handler of button
            BackButtonPressed += (s, e) => { this.Dismiss(); };
        }
    }
}