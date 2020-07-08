using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace MobileDocRenderer
{
    /// <summary>
    /// Parses list sections.
    /// </summary>
    public class ListSectionParser: SectionParser
    {
        /// <summary>
        /// Gets the section type that is supported by the parser.
        /// </summary>
        public override int SectionType => SectionTypes.List;

        /// <summary>
        /// Parses the JSON element into a specific section
        /// </summary>
        /// <param name="jsonReader">JSON reader content to parse</param>
        /// <returns>Returns the parsed section</returns>
        public override Section Parse(JsonReader jsonReader)
        {
            var tagName = (string)jsonReader.Match(JsonToken.String);
            var markers = ParseMarkers(jsonReader);

            if (jsonReader.TokenType == JsonToken.StartArray)
            {
                var attributes = ParseAttributes(jsonReader);
                return new MarkupSection(tagName, markers, attributes);
            }

            return new ListSection(tagName, markers, Enumerable.Empty<Attribute>());
        }

        private IEnumerable<Attribute> ParseAttributes(JsonReader jsonReader)
        {
            var attributes = new List<Attribute>();

            jsonReader.Match(JsonToken.StartArray);

            while (jsonReader.TokenType != JsonToken.EndArray)
            {
                var attributeName = (string)jsonReader.Match(JsonToken.String);
                var attributeValue = (string)jsonReader.Match(JsonToken.String);

                attributes.Add(new Attribute(attributeName, attributeValue));
            }

            jsonReader.Match(JsonToken.EndArray);

            return attributes;
        }

        private IEnumerable<Marker> ParseMarkers(JsonReader reader)
        {
            var markers = new List<Marker>();

            reader.Match(JsonToken.StartArray);

            while (reader.TokenType != JsonToken.EndArray)
            {
                markers.Add(ParseMarker(reader));
            }

            reader.Match(JsonToken.EndArray);

            return markers;
        }

        private Marker ParseMarker(JsonReader jsonReader)
        {
            Marker result = null;

            jsonReader.Match(JsonToken.StartArray);

            var textTypeIdentifier = (int)(long)jsonReader.Match(JsonToken.Integer);
            var openMarkupIndices = ParseOpenMarkupIndices(jsonReader);
            var closedMarkups = (int)(long)jsonReader.Match(JsonToken.Integer);

            if (textTypeIdentifier == 0)
            {
                var text = (string)jsonReader.Match(JsonToken.String);
                result = new MarkupMarker(openMarkupIndices, closedMarkups, text);
            }
            else
            {
                var atomIndex = (int)(long)jsonReader.Match(JsonToken.Integer);
                result = new AtomMarker(openMarkupIndices, closedMarkups, atomIndex);
            }

            jsonReader.Match(JsonToken.EndArray);

            return result;
        }

        private IEnumerable<int> ParseOpenMarkupIndices(JsonReader jsonReader)
        {
            var indices = new List<int>();

            jsonReader.Match(JsonToken.StartArray);

            while (jsonReader.TokenType != JsonToken.EndArray)
            {
                indices.Add((int)(long)jsonReader.Match(JsonToken.Integer));
            }

            jsonReader.Match(JsonToken.EndArray);

            return indices;
        }
    }
}