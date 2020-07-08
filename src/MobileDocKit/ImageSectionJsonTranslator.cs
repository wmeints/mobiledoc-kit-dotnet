using Newtonsoft.Json;

namespace MobileDocRenderer
{
    /// <summary>
    /// Translates an image section to JSON
    /// </summary>
    public class ImageSectionJsonTranslator: ISectionListener
    {
        private readonly JsonWriter _jsonWriter;

        /// <summary>
        /// Initializes a new instance of <see cref="ImageSectionJsonTranslator"/>
        /// </summary>
        /// <param name="jsonWriter">JSON writer to use.</param>
        public ImageSectionJsonTranslator(JsonWriter jsonWriter)
        {
            _jsonWriter = jsonWriter;
        }

        /// <summary>
        /// Gets the type of section that can be processed by the listener.
        /// </summary>
        public int SectionType => SectionTypes.Image;

        /// <summary>
        /// Called before the section is processed.
        /// </summary>
        /// <param name="section"></param>
        public void EnterSection(Section section)
        {
            _jsonWriter.WriteStartArray();
            _jsonWriter.WriteValue(SectionTypes.Image);
            _jsonWriter.WriteValue(((ImageSection)section).Url);
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