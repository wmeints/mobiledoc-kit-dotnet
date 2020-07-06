using System.Collections.Generic;

namespace MobileDocRenderer
{
    /// <summary>
    /// Implement this interface to translate mobile doc components.
    /// </summary>
    public interface IMobileDocListener
    {
        /// <summary>
        /// Called after the document is processed.
        /// </summary>
        /// <param name="mobileDoc"></param>
        void ExitMobileDoc(MobileDoc mobileDoc);
        
        /// <summary>
        /// Called before the document is processed.
        /// </summary>
        /// <param name="mobileDoc"></param>
        void EnterMobileDoc(MobileDoc mobileDoc);
        
        /// <summary>
        /// Called before the markups are processed.
        /// </summary>
        /// <param name="mobileDocMarkups"></param>
        void EnterMarkups(IEnumerable<MarkupType> mobileDocMarkups);
        
        /// <summary>
        /// Called after the markups are processed.
        /// </summary>
        /// <param name="mobileDocMarkups"></param>
        void ExitMarkups(IEnumerable<MarkupType> mobileDocMarkups);
        
        /// <summary>
        /// Called before a markup is processed.
        /// </summary>
        /// <param name="markup"></param>
        void EnterMarkup(MarkupType markup);
        
        /// <summary>
        /// Called after a markup is processed.
        /// </summary>
        /// <param name="markup"></param>
        void ExitMarkup(MarkupType markup);
        
        /// <summary>
        /// Called before atoms are processed.
        /// </summary>
        /// <param name="mobileDocAtoms"></param>
        void EnterAtoms(IEnumerable<AtomType> mobileDocAtoms);
        
        /// <summary>
        /// Called after atoms are processed.
        /// </summary>
        /// <param name="mobileDocAtoms"></param>
        void ExitAtoms(IEnumerable<AtomType> mobileDocAtoms);
        
        /// <summary>
        /// Called before an atom is processed.
        /// </summary>
        /// <param name="atom"></param>
        void EnterAtom(AtomType atom);
        
        /// <summary>
        /// Called after an atom is processed.
        /// </summary>
        /// <param name="atom"></param>
        void ExitAtom(AtomType atom);
        
        /// <summary>
        /// Called before sections are processed.
        /// </summary>
        /// <param name="mobileDocSections"></param>
        void EnterSections(IEnumerable<Section> mobileDocSections);
        
        /// <summary>
        /// Called after sections are processed.
        /// </summary>
        /// <param name="mobileDocSections"></param>
        void ExitSections(IEnumerable<Section> mobileDocSections);
    }
}