using UIComponents.Extensions;
using UIComponents.Tizen.Wearable.Renderers;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(MultilineCell), typeof(MultilineCellRenderer))]
namespace UIComponents.Tizen.Wearable.Renderers
{
    public class MultilineCellRenderer : CellRenderer
    {
        /// <summary>
        /// Constructor with style paramter
        /// </summary>
        protected MultilineCellRenderer(string style) : base(style)
        {
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MultilineCellRenderer() : this("multiline")
        {
            MainPart = "elm.text";
        }

        /// <summary>
        /// Part name of theme
        /// </summary>
        protected string MainPart { get; set; }

        /// <summary>
        /// Getter for text in cell
        /// </summary>
        /// <param name="cell">Cell</param>
        /// <param name="part">part name</param>
        /// <returns>Span</returns>
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
