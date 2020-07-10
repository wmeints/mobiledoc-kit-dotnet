using System.Collections.Generic;

namespace MobileDocRenderer
{
    /// <summary>
    /// Defines a list section in a mobiledoc document
    /// </summary>
    public class ListSection: Section
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ListSection"/>
        /// </summary>
        /// <param name="listType"></param>
        /// <param name="listItems">Markers for the section</param>
        /// <param name="attributes">Attributes for the section</param>
        public ListSection(string listType, IEnumerable<IEnumerable<Marker>> listItems, 
            IEnumerable<Attribute> attributes):base(SectionTypes.List)
        {
            ListType = listType;
            ListItems = listItems;
            Attributes = attributes;
        }

        /// <summary>
        /// Gets the list type to render.
        /// </summary>
        public string ListType { get; }

        /// <summary>
        /// Gets the markers for the list section
        /// </summary>
        public IEnumerable<IEnumerable<Marker>> ListItems { get; }
        
        /// <summary>
        /// Gets the attributes for the list section
        /// </summary>
        public IEnumerable<Attribute> Attributes { get; }
    }
}