using System.Collections.Generic;

namespace MobileDocRenderer
{
    /// <summary>
    /// Creates new instances of <see cref="MarkupTypeBuilder"/>.
    /// </summary>
    public class MarkupTypeBuilder
    {
        private string _tagName;
        private List<MarkupAttribute> _attributes = new List<MarkupAttribute>();

        /// <summary>
        /// Defines a new attribute for the markup.
        /// </summary>
        /// <param name="name">Name of the attribute.</param>
        /// <param name="value">Value of the attribute.</param>
        /// <returns>Returns the markup type builder.</returns>
        public MarkupTypeBuilder WithAttribute(string name, string value)
        {
            _attributes.Add(new MarkupAttribute(name,value));
            
            return this;
        }

        /// <summary>
        /// Defines the tag name for the markup
        /// </summary>
        /// <param name="tagName">Tag name to use</param>
        /// <returns>Returns the markup type builder instance.</returns>
        public MarkupTypeBuilder WithTagName(string tagName)
        {
            _tagName = tagName;
            return this;
        }

        /// <summary>
        /// Creates the new markup type
        /// </summary>
        /// <returns></returns>
        public MarkupType Build()
        {
            return new MarkupType(_tagName, _attributes);
        }
    }
}