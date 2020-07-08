using Newtonsoft.Json;

namespace MobileDocRenderer
{
    public class ImageSectionParser: SectionParser
    {
        /// <summary>
        /// Gets the section type that is supported by the parser.
        /// </summary>
        public override int SectionType => SectionTypes.Image;

        /// <summary>
        /// Parses the JSON element into a specific section
        /// </summary>
        /// <param name="jsonReader">JSON element content to parse</param>
        /// <returns>Returns the parsed section</returns>
        public override Section Parse(JsonReader jsonReader)
        {
            var url = (string)jsonReader.Match(JsonToken.String);
            
            return new ImageSection(url);
        }
    }
}