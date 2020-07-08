using System.Linq;
using Newtonsoft.Json;

namespace MobileDocRenderer
{
    public class MarkupSectionJsonTranslator : ISectionListener
    {
        private readonly JsonWriter _jsonWriter;

        public MarkupSectionJsonTranslator(JsonWriter jsonWriter)
        {
            _jsonWriter = jsonWriter;
        }

        public int SectionType => 1;

        public void EnterSection(Section section)
        {
            var markupSection = (MarkupSection) section;

            _jsonWriter.WriteStartArray();

            _jsonWriter.WriteValue(SectionTypes.Markup);
            _jsonWriter.WriteValue(markupSection.TagName);

            JsonTranslations.WriteMarkers(_jsonWriter, markupSection.Markers);

            if (markupSection.Attributes.Any())
            {
                JsonTranslations.WriteAttributes(_jsonWriter, markupSection.Attributes);
            }
        }

        public void ExitSection(Section section)
        {
            _jsonWriter.WriteEndArray();
        }
    }
}