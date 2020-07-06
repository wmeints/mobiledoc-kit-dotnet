using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace MobileDocRenderer
{
    /// <summary>
    /// Defines a card in a mobiledoc
    /// </summary>
    public class CardType
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CardType"/>
        /// </summary>
        /// <param name="name">Name of the card</param>
        /// <param name="payload">Payload for the card</param>
        public CardType(string name, JObject payload)
        {
            Name = name;
            Payload = payload;
        }

        /// <summary>
        /// Gets the name of the card
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Gets the payload for the card
        /// </summary>
        public JObject Payload { get; }
    }
}