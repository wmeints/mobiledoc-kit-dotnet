using System.Linq;
using Newtonsoft.Json;

namespace MobileDocRenderer
{
    /// <summary>
    /// Translates list sections to JSON
    /// </summary>
    public class ListSectionJsonTranslator : ISectionListener
    {
        private readonly JsonWriter _jsonWriter;

        /// <summary>
        /// Initializes a new instance of <see cref="ListSectionJsonTranslator"/>
        /// </summary>
        /// <param name="jsonWriter"></param>
        public ListSectionJsonTranslator(JsonWriter jsonWriter)
        {
            _jsonWriter = jsonWriter;
        }

        /// <summary>
        /// Gets the type of section that can be processed by the listener.
        /// </summary>
        public int SectionType => SectionTypes.List;

        /// <summary>
        /// Called before the section is processed.
        /// </summary>
        /// <param name="section"></param>
        public void EnterSection(Section section)
        {
            var listSection = (ListSection) section;

            _jsonWriter.WriteStartArray();

            _jsonWriter.WriteValue(SectionTypes.List);
            _jsonWriter.WriteValue(listSection.ListType);

            JsonTranslations.WriteMarkers(_jsonWriter, listSection.Markers);

            if (listSection.Attributes.Any())
            {
                JsonTranslations.WriteAttributes(_jsonWriter, listSection.Attributes);
            }
        }

        public void ExitSection(Section section)
        {
            _jsonWriter.WriteEndArray();
        }
    }
}