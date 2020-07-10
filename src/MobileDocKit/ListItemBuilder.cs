using System.Collections.Generic;

namespace MobileDocRenderer
{
    /// <summary>
    /// Creates a list item.
    /// </summary>
    public class ListItemBuilder
    {
        private List<Marker> _markers = new List<Marker>();
        
        /// <summary>
        /// Defines a marker for the section
        /// </summary>
        /// <param name="openedMarkupTypes">Indices of the opened markup types </param>
        /// <param name="closedMarkups">Number of closed markup types.</param>
        /// <param name="text">Text to render.</param>
        /// <returns>Returns the list section builder instance.</returns>
        public ListItemBuilder WithMarkupMarker(int[] openedMarkupTypes, int closedMarkups, string text)
        {
            _markers.Add(new MarkupMarker(openedMarkupTypes, closedMarkups,text));
            
            return this;
        }

        /// <summary>
        /// Defines an atom marker for the section
        /// </summary>
        /// <param name="openedMarkupTypes">Indices of the opened markup types.</param>
        /// <param name="closedMarkups">Number of closed markup types at the end of the marker.</param>
        /// <param name="atomIndex">Index of the atom to render.</param>
        /// <returns>Returns the list section builder instance.</returns>
        public ListItemBuilder WithAtomMarker(int[] openedMarkupTypes, int closedMarkups, int atomIndex)
        {
            _markers.Add(new AtomMarker(openedMarkupTypes, closedMarkups,atomIndex));
            
            return this;
        }

        /// <summary>
        /// Builds the list item instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Marker> Build()
        {
            return _markers;
        }
    }
}