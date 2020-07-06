using System;
using Newtonsoft.Json;

namespace MobileDocRenderer
{
    public static class JsonReaderExtensions
    {
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