using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MobileDocRenderer
{
    /// <summary>
    /// Parses mobiledoc content
    /// </summary>
    public class MobileDocParser
    {
        private readonly IEnumerable<SectionParser> _sectionParsers;
        private readonly JsonReader _jsonReader;

        private string _version;
        private List<Section> _sections;
        private List<AtomType> _atoms;
        private List<CardType> _cards;
        private List<MarkupType> _markups;

        /// <summary>
        /// Initializes a new instance of <see cref="MobileDocParser"/>
        /// </summary>
        /// <param name="text">Raw text to parse</param>
        /// <param name="sectionParsers">Parsers to use for processing sections</param>
        public MobileDocParser(string text)
        {
            _sectionParsers = new SectionParser[]
            {
                new MarkupSectionParser(),
                new CardSectionParser(),
                new ListSectionParser() 
            };

            _jsonReader = new JsonTextReader(new StringReader(text));
            _sections = new List<Section>();
            _atoms = new List<AtomType>();
            _cards = new List<CardType>();
            _markups = new List<MarkupType>();
        }

        /// <summary>
        /// Parses a document from the given input.
        /// </summary>
        /// <returns>Returns the parsed document</returns>
        public MobileDoc MobileDoc()
        {
            _jsonReader.Read();
            _jsonReader.Match(JsonToken.StartObject);

            while (_jsonReader.TokenType != JsonToken.EndObject)
            {
                if (_jsonReader.TokenType == JsonToken.PropertyName && (string)_jsonReader.Value == "version")
                {
                    ParseVersion();
                    continue;
                }

                if (_jsonReader.TokenType == JsonToken.PropertyName && (string)_jsonReader.Value == "sections")
                {
                    ParseSections();
                    continue;
                }

                if (_jsonReader.TokenType == JsonToken.PropertyName && (string)_jsonReader.Value == "markups")
                {
                    ParseMarkups();
                    continue;
                }

                if (_jsonReader.TokenType == JsonToken.PropertyName && (string)_jsonReader.Value == "atoms")
                {
                    ParseAtoms();
                    continue;
                }

                if (_jsonReader.TokenType == JsonToken.PropertyName && (string)_jsonReader.Value == "cards")
                {
                    ParseCards();
                    continue;
                }
                
                throw new Exception($"Invalid token type {_jsonReader.TokenType} detected: {_jsonReader.Value}");
            }

            return new MobileDoc(_version, _atoms, _cards, _markups, _sections);
        }

        private void ParseVersion()
        {
            _jsonReader.Match(JsonToken.PropertyName);

            _version = (string)_jsonReader.Value;
            _jsonReader.Match(JsonToken.String);
        }

        private void ParseAtoms()
        {
            _jsonReader.Match(JsonToken.PropertyName);
            _jsonReader.Match(JsonToken.StartArray);

            while (_jsonReader.TokenType != JsonToken.EndArray)
            {
                ParseAtom();
            }

            _jsonReader.Match(JsonToken.EndArray);
        }

        private void ParseAtom()
        {
            _jsonReader.Match(JsonToken.StartArray);

            var atomName = (string)_jsonReader.Match(JsonToken.String);
            var atomText = (string)_jsonReader.Match(JsonToken.String);

            var payload = JObject.Load(_jsonReader);
            _jsonReader.Match(JsonToken.EndObject);

            _jsonReader.Match(JsonToken.EndArray);

            _atoms.Add(new AtomType(atomName, atomText, payload));
        }

        private void ParseCards()
        {
            _jsonReader.Match(JsonToken.PropertyName);
            _jsonReader.Match(JsonToken.StartArray);

            while (_jsonReader.TokenType != JsonToken.EndArray)
            {
                ParseCard();
            }

            _jsonReader.Match(JsonToken.EndArray);
        }

        private void ParseCard()
        {
            _jsonReader.Match(JsonToken.StartArray);

            var cardName = (string)_jsonReader.Match(JsonToken.String);
            
            var payload = JObject.Load(_jsonReader);
            _jsonReader.Match(JsonToken.EndObject);
            
            _jsonReader.Match(JsonToken.EndArray);

            _cards.Add(new CardType(cardName, payload));
        }

        private void ParseMarkups()
        {
            _jsonReader.Match(JsonToken.PropertyName);
            _jsonReader.Match(JsonToken.StartArray);

            while (_jsonReader.TokenType != JsonToken.EndArray)
            {
                ParseMarkup();
            }

            _jsonReader.Match(JsonToken.EndArray);
        }

        private void ParseMarkup()
        {
            _jsonReader.Match(JsonToken.StartArray);

            var tagName = (string)_jsonReader.Match(JsonToken.String);
            var attributes = new List<Attribute>();

            if (_jsonReader.TokenType == JsonToken.StartArray)
            {
                _jsonReader.Match(JsonToken.StartArray);

                while (_jsonReader.TokenType != JsonToken.EndArray)
                {
                    var attributeName = (string)_jsonReader.Match(JsonToken.String);
                    var attributeValue = (string)_jsonReader.Match(JsonToken.String);

                    attributes.Add(new Attribute(attributeName, attributeValue));
                }

                _jsonReader.Match(JsonToken.EndArray);
            }

            _jsonReader.Match(JsonToken.EndArray);

            _markups.Add(new MarkupType(tagName, attributes));
        }

        private void ParseSections()
        {
            _jsonReader.Match(JsonToken.PropertyName);
            _jsonReader.Match(JsonToken.StartArray);

            while (_jsonReader.TokenType != JsonToken.EndArray)
            {
                if (_jsonReader.TokenType == JsonToken.StartArray)
                {
                    ParseSection();
                }
            }

            _jsonReader.Match(JsonToken.EndArray);
        }

        private void ParseSection()
        {
            _jsonReader.Match(JsonToken.StartArray);
            var sectionType = (int)(long)_jsonReader.Match(JsonToken.Integer);

            var sectionContentParser = _sectionParsers.FirstOrDefault(
                x => x.SectionType == sectionType);

            if (sectionContentParser == null)
            {
                throw new Exception($"No section parser defined for section type {sectionType}");
            }

            _sections.Add(sectionContentParser.Parse(_jsonReader));

            _jsonReader.Match(JsonToken.EndArray);
        }
    }
}