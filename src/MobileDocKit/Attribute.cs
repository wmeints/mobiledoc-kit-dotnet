namespace MobileDocRenderer
{
    /// <summary>
    /// Defines a section attribute
    /// </summary>
    public class Attribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Attribute"/>
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <param name="value">Value of the attribute</param>
        public Attribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Gets the name of the markup section attribute
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Gets the value of the markup section attribute
        /// </summary>
        public string Value { get; }
    }
}