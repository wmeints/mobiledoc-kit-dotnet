using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MobileDocRenderer
{
    /// <summary>
    /// Parses markup sections
    /// </summary>
    public class MarkupSectionParser: SectionParser
    {
        /// <summary>
        /// Gets the section type that is supported by the parser.
        /// </summary>
        public override int SectionType => SectionTypes.Markup;

        /// <summary>
        /// Parses the JSON element into a specific section
        /// </summary>
        /// <param name="reader">JSON reader content to parse</param>
        /// <returns>Returns the parsed section</returns>
        public override Section Parse(JsonReader reader)
        {
            var tagName = (string)reader.Match(JsonToken.String);
            var markers = ParseMarkers(reader);

            if (reader.TokenType == JsonToken.StartArray)
            {
                var attributes = ParseAttributes(reader);
                return new MarkupSection(tagName, markers, attributes);
            }
            
            return new MarkupSection(tagName, markers, Enumerable.Empty<MarkupSectionAttribute>());
        }

        private IEnumerable<MarkupSectionAttribute> ParseAttributes(JsonReader jsonReader)
        {
            var attributes = new List<MarkupSectionAttribute>();
            
            jsonReader.Match(JsonToken.StartArray);

            while (jsonReader.TokenType != JsonToken.EndArray)
            {
                var attributeName = (string)jsonReader.Match(JsonToken.String);
                var attributeValue = (string) jsonReader.Match(JsonToken.String);
                
                attributes.Add(new MarkupSectionAttribute(attributeName, attributeValue));
            }
            
            jsonReader.Match(JsonToken.EndArray);

            return attributes;
        }

        private IEnumerable<Marker> ParseMarkers(JsonReader reader)
        {
            var markers = new List<Marker>();
            
            reader.Match(JsonToken.StartArray);

            while(reader.TokenType != JsonToken.EndArray)
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
                var text = (string) jsonReader.Match(JsonToken.String);
                result = new MarkupMarker(openMarkupIndices,closedMarkups, text);
            }
            else
            {
                var atomIndex = (int) (long) jsonReader.Match(JsonToken.Integer);
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
                indices.Add((int)jsonReader.Match(JsonToken.Integer));
            }
            
            jsonReader.Match(JsonToken.EndArray);
            
            return indices;
        }
    }
}