using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace MobileDocRenderer
{
    /// <summary>
    /// Creates instances of <see cref="CardType"/>.
    /// </summary>
    public class CardTypeBuilder
    {
        private JObject _payload;
        private string _name;

        /// <summary>
        /// Defines the name for the card.
        /// </summary>
        /// <param name="name">Name of the card.</param>
        /// <returns>Returns the card type builder instance.</returns>
        public CardTypeBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        /// <summary>
        /// Sets the payload for the card.
        /// </summary>
        /// <param name="payload">Payload for the card.</param>
        /// <returns>Returns the card type builder instance.</returns>
        public CardTypeBuilder WithPayload(JObject payload)
        {
            _payload = payload;
            return this;
        }
        
        /// <summary>
        /// Creates the instance of the <see cref="CardType"/>.
        /// </summary>
        /// <returns>Returns the created instance.</returns>
        public CardType Build()
        {
            return new CardType(_name, _payload);
        }
    }
}