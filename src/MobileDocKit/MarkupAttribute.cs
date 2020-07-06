namespace MobileDocRenderer
{
    /// <summary>
    /// Defines an attribute for a piece of markup.
    /// </summary>
    public class MarkupAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MarkupAttribute"/>
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <param name="value">Value of the attribute</param>
        public MarkupAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Gets the name of the attribute
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Gets the value of the attribute
        /// </summary>
        public string Value { get; }
    }
}