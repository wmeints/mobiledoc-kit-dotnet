using System;
using System.Collections.Generic;

namespace MobileDocRenderer
{
    /// <summary>
    /// Allows the construction of <see cref="MobileDoc"/> instances
    /// </summary>
    public class MobileDocBuilder
    {
        private List<Section> _sections = new List<Section>();
        private List<MarkupType> _markups = new List<MarkupType>();
        private List<CardType> _cardTypes = new List<CardType>();
        private List<AtomType> _atomTypes = new List<AtomType>();

        /// <summary>
        /// Attaches a markup section to the document.
        /// </summary>
        /// <param name="sectionBuilder">Action used to build the section.</param>
        /// <returns>Returns the mobile doc builder instance.</returns>
        public MobileDocBuilder WithMarkupSection(Action<MarkupSectionBuilder> sectionBuilder)
        {
            var markupSectionBuilder = new MarkupSectionBuilder();
            sectionBuilder(markupSectionBuilder);

            _sections.Add(markupSectionBuilder.Build());

            return this;
        }

        /// <summary>
        /// Attaches a new markup to the document.
        /// </summary>
        /// <param name="markupBuilder">Action used to build the markup.</param>
        /// <returns>Returns the mobile doc builder instance.</returns>
        public MobileDocBuilder WithMarkup(Action<MarkupTypeBuilder> markupBuilder)
        {
            var markupTypeBuilder = new MarkupTypeBuilder();
            markupBuilder(markupTypeBuilder);

            _markups.Add(markupTypeBuilder.Build());

            return this;
        }

        /// <summary>
        /// Attaches a new atom to the document.
        /// </summary>
        /// <param name="atomBuilder">Action to build the atom.</param>
        /// <returns>Returns the mobile doc builder instance.</returns>
        public MobileDocBuilder WithAtom(Action<AtomTypeBuilder> atomBuilder)
        {
            var atomTypeBuilder = new AtomTypeBuilder();
            atomBuilder(atomTypeBuilder);
            
            _atomTypes.Add(atomTypeBuilder.Build());
            
            return this;
        }

        /// <summary>
        /// Attaches a card type to the document.
        /// </summary>
        /// <param name="cardBuilder">Action to build card types.</param>
        /// <returns>Returns the mobile doc builder instance.</returns>
        public MobileDocBuilder WithCard(Action<CardTypeBuilder> cardBuilder)
        {
            var cardTypeBuilder = new CardTypeBuilder();
            cardBuilder(cardTypeBuilder);
            
            _cardTypes.Add(cardTypeBuilder.Build());
            
            return this;
        }

        /// <summary>
        /// Builds the output document
        /// </summary>
        /// <returns></returns>
        public MobileDoc Build()
        {
            return new MobileDoc("0.3.0", _atomTypes, _cardTypes, _markups, _sections);
        }
    }
}