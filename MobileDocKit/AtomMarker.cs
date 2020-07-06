using System.Collections.Generic;

namespace MobileDocRenderer
{
    /// <summary>
    /// Defines a marker that renders an atom.
    /// </summary>
    public class AtomMarker : Marker
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AtomMarker"/>
        /// </summary>
        /// <param name="openMarkupIndices">Indices of the markups that were opened by the marker</param>
        /// <param name="closedMarkups">Number of markups that were closed by the marker</param>
        /// <param name="atomIndex">Index of the atom to render.</param>
        public AtomMarker(IEnumerable<int> openMarkupIndices, int closedMarkups, int atomIndex) : base(
            openMarkupIndices, closedMarkups)
        {
            AtomIndex = atomIndex;
        }

        /// <summary>
        /// Gets the index of the atom to render.
        /// </summary>
        public int AtomIndex { get; }
    }
}