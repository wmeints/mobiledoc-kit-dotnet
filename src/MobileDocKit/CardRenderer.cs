using HtmlAgilityPack;

namespace MobileDocRenderer
{
    /// <summary>
    /// Renders specific types of cards in a mobile doc instance.
    /// </summary>
    public abstract class CardRenderer
    {
        /// <summary>
        /// Renders the specified card.
        /// </summary>
        /// <param name="parentElement">Parent element to append the rendered content to.</param>
        /// <param name="cardType">Card type to render.</param>
        public abstract void Render(HtmlNode parentElement, CardType cardType);
        
        /// <summary>
        /// Determines whether this renderer can handle the specified card type.
        /// </summary>
        /// <param name="cardType">Card type to render.</param>
        /// <returns>Returns <c>true</c> when the card can be rendered; Otherwise <c>false</c>.</returns>
        public abstract bool CanRender(CardType cardType);
    }
}