using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace MobileDocRenderer
{
    
    /// <summary>
    /// Defines an atom type for a mobiledoc
    /// </summary>
    public class AtomType
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AtomType"/>
        /// </summary>
        /// <param name="name">Name of the atom</param>
        /// <param name="text">Text for the atom</param>
        /// <param name="payload">Payload for the atom</param>
        public AtomType(string name, string text, JObject payload)
        {
            Name = name;
            Text = text;
            Payload = payload;
        }

        /// <summary>
        /// Gets the name of the atom
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// Gets the text for the atom
        /// </summary>
        public string Text { get; }
        
        /// <summary>
        /// Gets the payload for the atom
        /// </summary>
        public JObject Payload { get; }
    }
}