using System.Collections.Generic;
using Newtonsoft.Json;

namespace MobileDocRenderer
{
    internal static class JsonTranslations
    {
        /// <summary>
        /// Translates a set of markers to JSON and writes them to the JSON writer instance.
        /// </summary>
        /// <remarks>
        /// This method generates an array of markers. The caller should not generate
        /// the wrapper array around the output of this method.
        /// </remarks>
        /// <param name="jsonWriter">JSON writer instance to use for generating the output.</param>
        /// <param name="markers">Markers to translate</param>
        public static void WriteMarkers(JsonWriter jsonWriter, IEnumerable<Marker> markers)
        {
            jsonWriter.WriteStartArray();

            foreach (var marker in markers)
            {
                jsonWriter.WriteStartArray();

                jsonWriter.WriteValue(marker is MarkupMarker ? 0 : 1);

                jsonWriter.WriteStartArray();

                foreach (var index in marker.OpenMarkupIndices)
                {
                    jsonWriter.WriteValue(index);
                }

                jsonWriter.WriteEndArray();

                jsonWriter.WriteValue(marker.ClosedMarkups);

                if (marker is MarkupMarker markupMarker)
                {
                    jsonWriter.WriteValue(markupMarker.Text);
                }

                if (marker is AtomMarker atomMarker)
                {
                    jsonWriter.WriteValue(atomMarker.AtomIndex);
                }

                jsonWriter.WriteEndArray();
            }

            jsonWriter.WriteEndArray();
        }

        /// <summary>
        /// Writes the attributes to the provided JSON writer.
        /// </summary>
        /// <remarks>
        /// This method generates an array of attributes. The caller should not generate
        /// the wrapper array around the output of this method.
        /// </remarks>
        /// <param name="jsonWriter">JSON writer to use.</param>
        /// <param name="attributes">Attributes to translate.</param>
        public static void WriteAttributes(JsonWriter jsonWriter, IEnumerable<Attribute> attributes)
        {
            jsonWriter.WriteStartArray();

            foreach (var attribute in attributes)
            {
                jsonWriter.WriteValue(attribute.Name);
                jsonWriter.WriteValue(attribute.Value);
            }

            jsonWriter.WriteEndArray();
        }
    }
}