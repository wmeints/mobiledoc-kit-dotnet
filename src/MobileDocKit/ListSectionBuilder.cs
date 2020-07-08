using System.Collections.Generic;

namespace MobileDocRenderer
{
    /// <summary>
    /// Constructs new instance of list sections.
    /// </summary>
    public class ListSectionBuilder
    {
        private readonly List<Marker> _markers = new List<Marker>();
        private readonly List<Attribute> _attributes = new List<Attribute>();
        private string _listType;

        /// <summary>
        /// Defines a marker for the section
        /// </summary>
        /// <param name="openedMarkupTypes">Indices of the opened markup types </param>
        /// <param name="closedMarkups">Number of closed markup types.</param>
        /// <param name="text">Text to render.</param>
        /// <returns>Returns the list section builder instance.</returns>
        public ListSectionBuilder WithMarkupMarker(int[] openedMarkupTypes, int closedMarkups, string text)
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
        public ListSectionBuilder WithAtomMarker(int[] openedMarkupTypes, int closedMarkups, int atomIndex)
        {
            _markers.Add(new AtomMarker(openedMarkupTypes, closedMarkups,atomIndex));
            
            return this;
        }
        
        /// <summary>
        /// Defines a new attribute for the section.
        /// </summary>
        /// <param name="name">Name of the attribute.</param>
        /// <param name="value">Value of the attribute.</param>
        /// <returns>Returns the list section builder.</returns>
        public ListSectionBuilder WithAttribute(string name, string value)
        {
            _attributes.Add(new Attribute(name,value));
            
            return this;
        }

        /// <summary>
        /// Sets the type of list to render.
        /// </summary>
        /// <param name="listType">List type to render.</param>
        /// <returns>Returns the list section builder instance.</returns>
        public ListSectionBuilder WithListType(string listType)
        {
            _listType = listType;

            return this;
        }
        
        /// <summary>
        /// Builds the new instance of the list section.
        /// </summary>
        /// <returns>Returns the new list section instance.</returns>
        public ListSection Build()
        {
            return new ListSection(_listType, _markers, _attributes);
        }
    }
}