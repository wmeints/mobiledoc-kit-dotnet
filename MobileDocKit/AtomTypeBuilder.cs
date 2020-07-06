using Newtonsoft.Json.Linq;

namespace MobileDocRenderer
{
    /// <summary>
    /// Creates instances of <see cref="AtomType"/>.
    /// </summary>
    public class AtomTypeBuilder
    {
        private string _name;
        private string _text;
        private JObject _payload;

        /// <summary>
        /// Specifies the name of the atom. 
        /// </summary>
        /// <param name="name">Name to set.</param>
        /// <returns>Returns the atom type builder instance.</returns>
        public AtomTypeBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        /// <summary>
        /// Sets the text for the atom.
        /// </summary>
        /// <param name="text">The text to set for the atom type.</param>
        /// <returns>Returns the atom type builder instance.</returns>
        public AtomTypeBuilder WithText(string text)
        {
            _text = text;
            return this;
        }

        /// <summary>
        /// Sets the payload for the atom type.
        /// </summary>
        /// <param name="payload">Payload to set.</param>
        /// <returns>Returns the atom type builder instance.</returns>
        public AtomTypeBuilder WithPayload(JObject payload)
        {
            _payload = payload;
            return this;
        }

        /// <summary>
        /// Builds the <see cref="AtomType"/> instance.
        /// </summary>
        /// <returns>Returns the new instance.</returns>
        public AtomType Build()
        {
            return new AtomType(_name, _text, _payload);
        }
    }
}