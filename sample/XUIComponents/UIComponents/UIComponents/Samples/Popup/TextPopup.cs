using Tizen.Wearable.CircularUI.Forms;

namespace UIComponents.Samples.Popup
{
    public class TextPopup : InformationPopup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TextPopup()
        {
            Text = "This has only texts. This is set by object";
            // Add event handler of button
            BackButtonPressed += (s, e) => { this.Dismiss(); };
        }
    }
}