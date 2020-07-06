using System;
using System.Collections.Generic;
using System.Linq;

namespace MobileDocRenderer
{
    /// <summary>
    /// Use the walker when translating mobile doc instances to another format.
    /// </summary>
    public class MobileDocWalker
    {
        /// <summary>
        /// Walks through the mobile doc, calling the provided listeners for processing
        /// each individual element in the document.
        /// </summary>
        /// <param name="mobileDoc">Mobile document to walk through</param>
        /// <param name="listener">Listener for the overall structure of the document.</param>
        /// <param name="sectionListeners">Set of listeners for specific section types.</param>
        /// <param name="skipUnsupportedSectionTypes">
        /// Flag indicating that unsupported section types should be skipped.
        /// </param>
        /// <exception cref="Exception">Gets thrown when a section could not be processed.</exception>
        public void Walk(MobileDoc mobileDoc, IMobileDocListener listener,
            IEnumerable<ISectionListener> sectionListeners, bool skipUnsupportedSectionTypes = false)
        {
            listener.EnterMobileDoc(mobileDoc);

            listener.EnterMarkups(mobileDoc.Markups);

            foreach (var markup in mobileDoc.Markups)
            {
                listener.EnterMarkup(markup);
                listener.ExitMarkup(markup);
            }

            listener.ExitMarkups(mobileDoc.Markups);
            listener.EnterAtoms(mobileDoc.Atoms);

            foreach (var atom in mobileDoc.Atoms)
            {
                listener.EnterAtom(atom);
                listener.ExitAtom(atom);
            }

            listener.ExitAtoms(mobileDoc.Atoms);

            listener.EnterCards(mobileDoc.Cards);

            foreach (var card in mobileDoc.Cards)
            {
                listener.EnterCard(card);
                listener.ExitCard(card);
            }
            
            listener.ExitCards(mobileDoc.Cards);
            
            listener.EnterSections(mobileDoc.Sections);

            foreach (var section in mobileDoc.Sections)
            {
                var sectionListener = sectionListeners.FirstOrDefault(
                    x => x.SectionType == section.SectionType);

                if (sectionListener != null)
                {
                    sectionListener.EnterSection(section);
                    sectionListener.ExitSection(section);
                }
                else if (!skipUnsupportedSectionTypes)
                {
                    throw new Exception("Unsupported section encountered.");
                }
            }

            listener.ExitSections(mobileDoc.Sections);
            listener.ExitMobileDoc(mobileDoc);
        }
    }
}