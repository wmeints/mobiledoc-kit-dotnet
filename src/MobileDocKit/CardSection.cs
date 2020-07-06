namespace MobileDocRenderer
{
    /// <summary>
    /// A section that renders a card from the document.
    /// </summary>
    public class CardSection: Section
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CardSection"/>
        /// </summary>
        /// <param name="cardIndex">Card index to render in the section</param>
        public CardSection(int cardIndex):base(SectionTypes.Card)
        {
            CardIndex = cardIndex;
        }
        
        /// <summary>
        /// Gets the card to render from the listed card types.
        /// </summary>
        public int CardIndex { get; }
    }
}