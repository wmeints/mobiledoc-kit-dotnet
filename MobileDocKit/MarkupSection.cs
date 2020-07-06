using System.Collections.Generic;

namespace MobileDocRenderer
{
    /// <summary>
    /// Defines a markup section
    /// </summary>
    public class MarkupSection: Section
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MarkupSection"/>
        /// </summary>
        /// <param name="tagName">Tag name</param>
        /// <param name="markers">Markers for the section</param>
        /// <param name="attributes">Attributes for the section</param>
        public MarkupSection(string tagName, IEnumerable<Marker> markers, 
            IEnumerable<MarkupSectionAttribute> attributes): base(1)
        {
            TagName = tagName;
            Markers = markers;
            Attributes = attributes;
        }

        /// <summary>
        /// Gets the tag name to wrap the section in
        /// </summary>
        public string TagName { get; }
        
        /// <summary>
        /// Gets the markers for the section
        /// </summary>
        public IEnumerable<Marker> Markers { get; }
        
        /// <summary>
        /// Gets the attributes for the wrapper element
        /// </summary>
        public IEnumerable<MarkupSectionAttribute> Attributes { get; }
    }
}