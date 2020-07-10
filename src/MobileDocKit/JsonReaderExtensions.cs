using System;
using Newtonsoft.Json;

namespace MobileDocRenderer
{
    /// <summary>
    /// Extension methods for the JSON reader
    /// </summary>
    public static class JsonReaderExtensions
    {
        /// <summary>
        /// Matches a specific token type.
        /// </summary>
        /// <param name="reader">Reader to read from.</param>
        /// <param name="expectedTokenType">The expected token type.</param>
        /// <returns>Returns the matched token.</returns>
        /// <exception cref="Exception">Gets thrown</exception>
        public static object Match(this JsonReader reader, JsonToken expectedTokenType)
        {
            var rawValue = reader.Value;

            if (reader.TokenType != expectedTokenType)
            {
                throw new Exception($"Expected {expectedTokenType}. Received {reader.TokenType}");
            }

            reader.Read();

            return rawValue;
        }
    }
}