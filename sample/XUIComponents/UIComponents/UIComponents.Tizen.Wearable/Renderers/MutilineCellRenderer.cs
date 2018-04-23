using UIComponents.Extensions;
using UIComponents.Tizen.Wearable.Renderers;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(MultilineCell), typeof(MultilineCellRenderer))]
namespace UIComponents.Tizen.Wearable.Renderers
{
    public class MultilineCellRenderer : CellRenderer
    {
        protected MultilineCellRenderer(string style) : base(style)
        {
        }

        public MultilineCellRenderer() : this("multiline")
        {
            MainPart = "elm.text";
        }

        protected string MainPart { get; set; }

        protected override Span OnGetText(Cell cell, string part)
        {
            if (part == MainPart)
            {
                return new Span()
                {
                    Text = (cell as MultilineCell).Text
                };
            }
            return null;
        }
    }
}
