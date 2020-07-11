using HtmlAgilityPack;

namespace MobileDocRenderer
{
    /// <summary>
    /// Renders a particular atom type.
    /// </summary>
    public abstract class AtomRenderer
    {
        /// <summary>
        /// Renders the provided atom type and appends the output to the provided parent element.
        /// </summary>
        /// <param name="parentElement">parent element to render in.</param>
        /// <param name="atomType">Atom type to render.</param>
        public abstract void Render(HtmlNode parentElement, AtomType atomType);

        /// <summary>
        /// Checks whether an atom type can be rendered with this renderer.
        /// </summary>
        /// <param name="atomType">Atom type to render.</param>
        /// <returns>Returns <c>true</c> when the atom type can be rendered; Otherwise <c>false</c>.</returns>
        public abstract bool CanRender(AtomType atomType);
    }
}