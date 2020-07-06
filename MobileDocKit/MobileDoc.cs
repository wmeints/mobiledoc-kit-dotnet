using System.Collections.Generic;

namespace MobileDocRenderer
{
    /// <summary>
    /// Represents a markup document in mobiledoc format.
    /// </summary>
    public class MobileDoc
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MobileDoc"/>
        /// </summary>
        /// <param name="version">Version number</param>
        /// <param name="atoms">Atoms defined in the document</param>
        /// <param name="cards">Cards defined in the document</param>
        /// <param name="markups">Markup types defined in the document</param>
        /// <param name="sections">Sections defined in the document</param>
        public MobileDoc(string version, 
            IEnumerable<AtomType> atoms, 
            IEnumerable<CardType> cards, 
            IEnumerable<MarkupType> markups, 
            IEnumerable<Section> sections)
        {
            Version = version;
            Atoms = atoms;
            Cards = cards;
            Markups = markups;
            Sections = sections;
        }

        /// <summary>
        /// Gets the mobiledoc version
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// Gets the atom types defined in the document
        /// </summary>
        public IEnumerable<AtomType> Atoms { get; }

        /// <summary>
        /// Gets the card types defined in the document
        /// </summary>
        public IEnumerable<CardType> Cards { get; }

        /// <summary>
        /// Gets the markup types defined in the document
        /// </summary>
        public IEnumerable<MarkupType> Markups { get; }

        /// <summary>
        /// Gets the sections for the document
        /// </summary>
        public IEnumerable<Section> Sections { get; }
    }
}