namespace MobileDocRenderer
{
    /// <summary>
    /// Builder used to construct card sections.
    /// </summary>
    public class CardSectionBuilder
    {
        private int _cardIndex;

        /// <summary>
        /// Sets the card index for the section.
        /// </summary>
        /// <param name="cardIndex">Card index</param>
        /// <returns>Returns the card section builder instance.</returns>
        public CardSectionBuilder WithCardIndex(int cardIndex)
        {
            _cardIndex = cardIndex;
            return this;
        }

        /// <summary>
        /// Builds the card section.
        /// </summary>
        /// <returns>Returns the new card section that was created.</returns>
        public CardSection Build()
        {
            return new CardSection(_cardIndex);
        }
    }
}