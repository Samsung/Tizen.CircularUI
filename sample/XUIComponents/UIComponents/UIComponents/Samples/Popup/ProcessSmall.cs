using Tizen.Wearable.CircularUI.Forms;

namespace UIComponents.Samples.Popup
{
    public class ProcessSmall : InformationPopup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProcessSmall()
        {
            IsProgressRunning = true;
            Text = "Description about progress";
            BackButtonPressed += (s, e) => { this.Dismiss(); };
        }
    }
}