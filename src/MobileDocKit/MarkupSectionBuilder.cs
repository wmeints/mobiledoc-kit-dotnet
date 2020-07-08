using System.Collections.Generic;

namespace MobileDocRenderer
{
    /// <summary>
    /// Creates <see cref="MarkupSection"/> instances.
    /// </summary>
    public class MarkupSectionBuilder
    {
        private string _tagName;
        private List<Marker> _markers = new List<Marker>();
        private List<Attribute> _attributes = new List<Attribute>();

        /// <summary>
        /// Defines the tag name for the section
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public MarkupSectionBuilder WithTagName(string tagName)
        {
            _tagName = tagName;

            return this;
        }
        
        /// <summary>
        /// Defines a marker for the section
        /// </summary>
        /// <param name="textType"></param>
        /// <param name="openedMarkupTypes"></param>
        /// <param name="closedMarkups"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public MarkupSectionBuilder WithMarkupMarker(int[] openedMarkupTypes, int closedMarkups, string text)
        {
            _markers.Add(new MarkupMarker(openedMarkupTypes, closedMarkups,text));
            
            return this;
        }

        public MarkupSectionBuilder WithAtomMarker(int[] openedMarkupTypes, int closedMarkups, int atomIndex)
        {
            _markers.Add(new AtomMarker(openedMarkupTypes, closedMarkups,atomIndex));
            
            return this;
        }

        /// <summary>
        /// Defines an attribute for the section
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public MarkupSectionBuilder WithAttribute(string name, string value)
        {
            _attributes.Add(new Attribute(name, value));

            return this;
        }
        
        /// <summary>
        /// Builds the section
        /// </summary>
        /// <returns></returns>
        public MarkupSection Build()
        {
            return new MarkupSection(_tagName, _markers, _attributes);
        }
    }
}