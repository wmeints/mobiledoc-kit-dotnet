using System.Collections.Generic;

namespace MobileDocRenderer
{
    public abstract class MobileDocListener: IMobileDocListener
    {
        public virtual void ExitMobileDoc(MobileDoc mobileDoc)
        {
            
        }

        public virtual void EnterMobileDoc(MobileDoc mobileDoc)
        {
            
        }

        public virtual void EnterMarkups(IEnumerable<MarkupType> mobileDocMarkups)
        {
            
        }

        public virtual void ExitMarkups(IEnumerable<MarkupType> mobileDocMarkups)
        {
            
        }

        public virtual void EnterMarkup(MarkupType markup)
        {
            
        }

        public virtual void ExitMarkup(MarkupType markup)
        {
            
        }

        public virtual void EnterAtoms(IEnumerable<AtomType> mobileDocAtoms)
        {
            
        }

        public virtual void ExitAtoms(IEnumerable<AtomType> mobileDocAtoms)
        {
            
        }

        public virtual void EnterAtom(AtomType atom)
        {
            
        }

        public virtual void ExitAtom(AtomType atom)
        {
            
        }

        public virtual void EnterCards(IEnumerable<CardType> cardTypes)
        {
            
        }

        public virtual void ExitCards(IEnumerable<CardType> cardTypes)
        {
            
        }

        public virtual void EnterCard(CardType cardType)
        {
            
        }

        public void ExitCard(CardType cardType)
        {
            
        }

        public virtual void EnterSections(IEnumerable<Section> mobileDocSections)
        {
            
        }

        public virtual void ExitSections(IEnumerable<Section> mobileDocSections)
        {
            
        }
    }
}