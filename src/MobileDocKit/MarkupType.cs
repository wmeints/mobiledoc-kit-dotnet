using System.Collections.Generic;

namespace MobileDocRenderer
{
    /// <summary>
    /// Defines a piece of markup
    /// </summary>
    public class MarkupType
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MarkupType"/>.
        /// </summary>
        /// <param name="name">Name of the markup type.</param>
        /// <param name="attributes">Attributes for the markup type.</param>
        public MarkupType(string name, IEnumerable<Attribute> attributes)
        {
            Name = name;
            Attributes = attributes;
        }

        /// <summary>
        /// Gets the name for the markup type.
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Gets the attributes associated with the markup type. 
        /// </summary>
        public IEnumerable<Attribute> Attributes { get; }
    }
}