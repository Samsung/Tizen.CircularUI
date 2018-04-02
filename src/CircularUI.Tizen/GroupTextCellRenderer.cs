using Xamarin.Forms.Platform.Tizen;

namespace CircularUI.Tizen
{
    class GroupTextCellRenderer : TextCellRenderer
    {
        public GroupTextCellRenderer() : base("group_index")
        {
            DetailPart = "elm.text.end";
        }
    }
}
