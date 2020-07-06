using Newtonsoft.Json;

namespace MobileDocRenderer
{
    /// <summary>
    /// Classes that derive from this base class implement a parser that handles a specific section type.
    /// </summary>
    public abstract class SectionParser
    {
        /// <summary>
        /// Gets the section type that is supported by the parser.
        /// </summary>
        public abstract int SectionType { get; }
        
        /// <summary>
        /// Parses the JSON element into a specific section
        /// </summary>
        /// <param name="element">JSON element content to parse</param>
        /// <returns>Returns the parsed section</returns>
        public abstract Section Parse(JsonReader element);
    }
}