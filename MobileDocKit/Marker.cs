using System.Collections.Generic;

namespace MobileDocRenderer
{
    /// <summary>
    /// Derive from this class to create a new type of marker.
    /// </summary>
    public abstract class Marker
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Marker"/>
        /// </summary>
        /// <param name="openMarkupIndices">Markup indices that were opened by the marker</param>
        /// <param name="closedMarkups">Number of closed markups</param>
        protected Marker(IEnumerable<int> openMarkupIndices, int closedMarkups)
        {
            OpenMarkupIndices = openMarkupIndices;
            ClosedMarkups = closedMarkups;
        }
        
        /// <summary>
        /// Gets the indices of the markup types that were opened
        /// </summary>
        public IEnumerable<int> OpenMarkupIndices { get; }
        
        /// <summary>
        /// Gets the number of closed markups
        /// </summary>
        public int ClosedMarkups { get; }
    }
}