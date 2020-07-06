using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace MobileDocRenderer
{
    public class MobileDocJsonTranslator : IMobileDocListener
    {
        private readonly JsonWriter _jsonWriter;

        public MobileDocJsonTranslator(JsonWriter jsonWriter)
        {
            _jsonWriter = jsonWriter;
        }

        public void ExitMobileDoc(MobileDoc mobileDoc)
        {
            _jsonWriter.WriteEndObject();
        }

        public void EnterMobileDoc(MobileDoc mobileDoc)
        {
            _jsonWriter.WriteStartObject();
            _jsonWriter.WritePropertyName("version");
            _jsonWriter.WriteValue(mobileDoc.Version);
        }

        public void EnterMarkups(IEnumerable<MarkupType> mobileDocMarkups)
        {
            _jsonWriter.WritePropertyName("markups");
            _jsonWriter.WriteStartArray();
        }

        public void ExitMarkups(IEnumerable<MarkupType> mobileDocMarkups)
        {
            _jsonWriter.WriteEndArray();
        }

        public void EnterMarkup(MarkupType markup)
        {
            _jsonWriter.WriteStartArray();
            _jsonWriter.WriteValue(markup.Name);

            if (markup.Attributes.Any())
            {
                _jsonWriter.WriteStartArray();
                
                foreach (var attribute in markup.Attributes)
                {
                    _jsonWriter.WriteValue(attribute.Name);
                    _jsonWriter.WriteValue(attribute.Value);
                }
                
                _jsonWriter.WriteEndArray();
            }
            
            _jsonWriter.WriteEndArray();
        }

        public void ExitMarkup(MarkupType markup)
        {
            
        }

        public void EnterAtoms(IEnumerable<AtomType> mobileDocAtoms)
        {
            _jsonWriter.WritePropertyName("atoms");
            _jsonWriter.WriteStartArray();
        }

        public void ExitAtoms(IEnumerable<AtomType> mobileDocAtoms)
        {
            _jsonWriter.WriteEndArray();
        }

        public void EnterAtom(AtomType atom)
        {
            _jsonWriter.WriteStartArray();
            _jsonWriter.WriteValue(atom.Name);
            _jsonWriter.WriteValue(atom.Text);
            
            atom.Payload.WriteTo(_jsonWriter);
            
            _jsonWriter.WriteEndArray();
        }

        public void ExitAtom(AtomType atom)
        {
            
        }

        public void EnterSections(IEnumerable<Section> mobileDocSections)
        {
            _jsonWriter.WritePropertyName("sections");
            _jsonWriter.WriteStartArray();
        }

        public void ExitSections(IEnumerable<Section> mobileDocSections)
        {
            _jsonWriter.WriteEndArray();
        }
    }
}