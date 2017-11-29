using Xamarin.Forms.Platform.Tizen;

namespace Xamarin.Forms.CircularUI.Renderer
{
    class GroupTextCellRenderer : TextCellRenderer
    {
        public GroupTextCellRenderer() : base("group_index")
        {
            DetailPart = "elm.text.end";
        }
    }
}
