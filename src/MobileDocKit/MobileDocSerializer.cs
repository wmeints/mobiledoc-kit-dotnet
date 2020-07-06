using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace MobileDocRenderer
{
    /// <summary>
    /// Helper class that provides quick access to serialization and deserialization of mobile doc.
    /// </summary>
    public static class MobileDocSerializer
    {
        /// <summary>
        /// Serializes a mobile doc to JSON
        /// </summary>
        /// <param name="document">Document to serialize</param>
        /// <returns>Returns the serialized document.</returns>
        public static string Serialize(MobileDoc document)
        {
            var outputBuilder = new StringBuilder();
            using var jsonWriter = new JsonTextWriter(new StringWriter(outputBuilder));
        
            var documentListener = new MobileDocJsonTranslator(jsonWriter);
            
            var sectionListeners = new ISectionListener[]
            {
                new MarkupSectionJsonTranslator(jsonWriter),
                new CardSectionJsonTranslator(jsonWriter)
            };
            
            var walker = new MobileDocWalker();
            
            walker.Walk(document, documentListener, sectionListeners);
            
            jsonWriter.Flush();
            
            return outputBuilder.ToString();
        }

        /// <summary>
        /// Deserializes JSON to a mobile doc instance.
        /// </summary>
        /// <param name="text">Raw JSON to parse</param>
        /// <returns>Returns the parsed mobile doc instance.</returns>
        public static MobileDoc Deserialize(string text)
        {
            var parser = new MobileDocParser(text);
            return parser.MobileDoc();
        }
    }
}