using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MobileDocRenderer
{
    /// <summary>
    /// Renders mobiledoc instances.
    /// </summary>
    public class MobileDocRenderer
    {
        private readonly IEnumerable<AtomRenderer> _atomRenderers;
        private readonly IEnumerable<CardRenderer> _cardRenderers;

        /// <summary>
        /// Initializes a new instance of <see cref="MobileDocRenderer"/>.
        /// </summary>
        /// <param name="cardRenderers">Card renderers to use.</param>
        /// <param name="atomRenderers">Atom renderers to use.</param>
        public MobileDocRenderer(IEnumerable<CardRenderer> cardRenderers, IEnumerable<AtomRenderer> atomRenderers)
        {
            _cardRenderers = cardRenderers;
            _atomRenderers = atomRenderers;
        }

        /// <summary>
        /// Renders the mobiledoc using the rendering settings specified for this renderer.
        /// </summary>
        /// <param name="mobileDoc">Mobiledoc instance to render.</param>
        /// <returns>Returns the rendered mobiledoc instance.</returns>
        public string Render(MobileDoc mobileDoc)
        {
            var outputBuffer = new StringBuilder();
            var outputWriter = new StringWriter(outputBuffer);
            var walker = new MobileDocWalker();

            var listener = new MobileDocHtmlTranslator(_cardRenderers, _atomRenderers, outputWriter);

            walker.Walk(mobileDoc, listener, Enumerable.Empty<ISectionListener>());

            outputWriter.Flush();

            return outputBuffer.ToString();
        }
    }
}