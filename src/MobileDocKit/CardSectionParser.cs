using Newtonsoft.Json;

namespace MobileDocRenderer
{
    public class CardSectionParser: SectionParser
    {
        /// <summary>
        /// Gets the section type that is supported by the parser.
        /// </summary>
        public override int SectionType => SectionTypes.Card;

        /// <summary>
        /// Parses the JSON element into a specific section
        /// </summary>
        /// <param name="jsonReader">JSON element content to parse</param>
        /// <returns>Returns the parsed section</returns>
        public override Section Parse(JsonReader jsonReader)
        {
            var cardIndex = (int) (long) jsonReader.Match(JsonToken.Integer);
            return new CardSection(cardIndex);
        }
    }
}