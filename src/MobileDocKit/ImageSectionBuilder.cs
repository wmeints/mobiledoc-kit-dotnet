namespace MobileDocRenderer
{
    /// <summary>
    /// Creates new image section instances.
    /// </summary>
    public class ImageSectionBuilder
    {
        private string _url;

        /// <summary>
        /// Defines the URL for the image.
        /// </summary>
        /// <param name="url">URL where the image is located.</param>
        /// <returns>Returns the image section builder instance.</returns>
        public ImageSectionBuilder WithUrl(string url)
        {
            _url = url;

            return this;
        }
        
        /// <summary>
        /// Builds the image section.
        /// </summary>
        /// <returns></returns>
        public ImageSection Build()
        {
            return new ImageSection(_url);
        }
    }
}