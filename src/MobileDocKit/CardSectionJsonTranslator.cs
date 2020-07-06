using Newtonsoft.Json;

namespace MobileDocRenderer
{
    public class CardSectionJsonTranslator: ISectionListener
    {
        private readonly JsonWriter _jsonWriter;

        /// <summary>
        /// Initializes a new instance of <see cref="CardSectionJsonTranslator"/>.
        /// </summary>
        /// <param name="jsonWriter">JSON writer instance to use.</param>
        public CardSectionJsonTranslator(JsonWriter jsonWriter)
        {
            _jsonWriter = jsonWriter;
        }

        /// <summary>
        /// Gets the type of section that can be processed by the listener.
        /// </summary>
        public int SectionType => SectionTypes.Card;

        /// <summary>
        /// Called before the section is processed.
        /// </summary>
        /// <param name="section"></param>
        public void EnterSection(Section section)
        {
            var cardSection = (CardSection) section;
            
            _jsonWriter.WriteStartArray();
            _jsonWriter.WriteValue(cardSection.SectionType);
            _jsonWriter.WriteValue(cardSection.CardIndex);
        }

        /// <summary>
        /// Called after the section is processed.
        /// </summary>
        /// <param name="section"></param>
        public void ExitSection(Section section)
        {
            _jsonWriter.WriteEndArray();
        }
    }
}