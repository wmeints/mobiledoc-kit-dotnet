namespace MobileDocRenderer
{
    /// <summary>
    /// Implement this interface to create section specific translators.
    /// </summary>
    public interface ISectionListener
    {
        /// <summary>
        /// Gets the type of section that can be processed by the listener.
        /// </summary>
        int SectionType { get; }
        
        /// <summary>
        /// Called before the section is processed.
        /// </summary>
        /// <param name="section"></param>
        void EnterSection(Section section);
        
        /// <summary>
        /// Called after the section is processed.
        /// </summary>
        /// <param name="section"></param>
        void ExitSection(Section section);
    }
}