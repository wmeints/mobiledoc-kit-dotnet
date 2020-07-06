using System.Collections.Generic;

namespace MobileDocRenderer
{
    /// <summary>
    /// Defines a marker for a markup section
    /// </summary>
    public class MarkupMarker: Marker
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MarkupMarker"/>
        /// </summary>
        /// <param name="openMarkupIndices"></param>
        /// <param name="closedMarkups"></param>
        /// <param name="text"></param>
        public MarkupMarker(IEnumerable<int> openMarkupIndices, int closedMarkups, string text):base(openMarkupIndices, closedMarkups)
        {
            Text = text;
        }

        /// <summary>
        /// Gets the text for the marker
        /// </summary>
        public string Text { get; }
    }
}