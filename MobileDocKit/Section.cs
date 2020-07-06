using Newtonsoft.Json;

namespace MobileDocRenderer
{
    /// <summary>
    /// Derive from this base type to introduce a new section type.
    /// </summary>
    public abstract class Section
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Section"/> 
        /// </summary>
        /// <param name="sectionType"></param>
        protected Section(int sectionType)
        {
            SectionType = sectionType;
        }

        /// <summary>
        /// Gets the number identifying the section type.
        /// </summary>
        public int SectionType { get; }
    }
}