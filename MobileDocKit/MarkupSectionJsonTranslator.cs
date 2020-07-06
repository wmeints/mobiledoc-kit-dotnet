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

            WriteMarkers(markupSection);

            if (markupSection.Attributes.Any())
            {
                WriteAttributes(markupSection);
            }
        }

        private void WriteAttributes(MarkupSection markupSection)
        {
            _jsonWriter.WriteStartArray();

            foreach (var attribute in markupSection.Attributes)
            {
                _jsonWriter.WriteValue(attribute.Name);
                _jsonWriter.WriteValue(attribute.Value);
            }

            _jsonWriter.WriteEndArray();
        }

        private void WriteMarkers(MarkupSection markupSection)
        {
            _jsonWriter.WriteStartArray();

            foreach (var marker in markupSection.Markers)
            {
                _jsonWriter.WriteStartArray();

                _jsonWriter.WriteValue(marker is MarkupMarker ? 0 : 1);

                _jsonWriter.WriteStartArray();

                foreach (var index in marker.OpenMarkupIndices)
                {
                    _jsonWriter.WriteValue(index);
                }

                _jsonWriter.WriteEndArray();

                _jsonWriter.WriteValue(marker.ClosedMarkups);

                if (marker is MarkupMarker markupMarker)
                {
                    _jsonWriter.WriteValue(markupMarker.Text);
                }

                if (marker is AtomMarker atomMarker)
                {
                    _jsonWriter.WriteValue(atomMarker.AtomIndex);
                }

                _jsonWriter.WriteEndArray();
            }

            _jsonWriter.WriteEndArray();
        }

        public void ExitSection(Section section)
        {
            _jsonWriter.WriteEndArray();
        }
    }
}