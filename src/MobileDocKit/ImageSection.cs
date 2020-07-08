namespace MobileDocRenderer
{
    /// <summary>
    /// Defines a section that contains an image.
    /// </summary>
    public class ImageSection: Section
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ImageSection"/>.
        /// </summary>
        /// <param name="url">URL for the image.</param>
        public ImageSection(string url) : base(SectionTypes.Image)
        {
            Url = url;
        }
        
        /// <summary>
        /// Gets the URL of the image.
        /// </summary>
        public string Url { get; }
    }
}