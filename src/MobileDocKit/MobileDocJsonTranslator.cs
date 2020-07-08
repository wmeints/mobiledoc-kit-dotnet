using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace MobileDocRenderer
{
    /// <summary>
    /// Translates a mobiledoc instance to JSON
    /// </summary>
    public class MobileDocJsonTranslator : IMobileDocListener
    {
        private readonly JsonWriter _jsonWriter;

        /// <summary>
        /// Initializes a new instance of <see cref="MobileDocJsonTranslator"/>
        /// </summary>
        /// <param name="jsonWriter">JSON writer to use.</param>
        public MobileDocJsonTranslator(JsonWriter jsonWriter)
        {
            _jsonWriter = jsonWriter;
        }

        /// <summary>
        /// Writes the final closing tag for the document.
        /// </summary>
        /// <param name="mobileDoc"></param>
        public void ExitMobileDoc(MobileDoc mobileDoc)
        {
            _jsonWriter.WriteEndObject();
        }

        /// <summary>
        /// Writes the opening tag and the header for the document.
        /// </summary>
        /// <param name="mobileDoc"></param>
        public void EnterMobileDoc(MobileDoc mobileDoc)
        {
            _jsonWriter.WriteStartObject();
            _jsonWriter.WritePropertyName("version");
            _jsonWriter.WriteValue(mobileDoc.Version);
        }

        /// <summary>
        /// Starts processing markups.
        /// </summary>
        /// <param name="mobileDocMarkups"></param>
        public void EnterMarkups(IEnumerable<MarkupType> mobileDocMarkups)
        {
            _jsonWriter.WritePropertyName("markups");
            _jsonWriter.WriteStartArray();
        }

        /// <summary>
        /// Finishes writing markups.
        /// </summary>
        /// <param name="mobileDocMarkups"></param>
        public void ExitMarkups(IEnumerable<MarkupType> mobileDocMarkups)
        {
            _jsonWriter.WriteEndArray();
        }

        /// <summary>
        /// Starts writing a single markup.
        /// </summary>
        /// <param name="markup"></param>
        public void EnterMarkup(MarkupType markup)
        {
            _jsonWriter.WriteStartArray();
            _jsonWriter.WriteValue(markup.Name);

            if (markup.Attributes.Any())
            {
                JsonTranslations.WriteAttributes(_jsonWriter, markup.Attributes);
            }
        }

        /// <summary>
        /// Finalizes writing a single markup.
        /// </summary>
        /// <param name="markup"></param>
        public void ExitMarkup(MarkupType markup)
        {
            _jsonWriter.WriteEndArray();
        }

        /// <summary>
        /// Starts processing atoms.
        /// </summary>
        /// <param name="mobileDocAtoms"></param>
        public void EnterAtoms(IEnumerable<AtomType> mobileDocAtoms)
        {
            _jsonWriter.WritePropertyName("atoms");
            _jsonWriter.WriteStartArray();
        }

        /// <summary>
        /// Finalizes processing atoms.
        /// </summary>
        /// <param name="mobileDocAtoms"></param>
        public void ExitAtoms(IEnumerable<AtomType> mobileDocAtoms)
        {
            _jsonWriter.WriteEndArray();
        }

        /// <summary>
        /// Writes a single atom.
        /// </summary>
        /// <param name="atom"></param>
        public void EnterAtom(AtomType atom)
        {
            _jsonWriter.WriteStartArray();
            _jsonWriter.WriteValue(atom.Name);
            _jsonWriter.WriteValue(atom.Text);
            
            atom.Payload.WriteTo(_jsonWriter);
        }

        /// <summary>
        /// Finalizes writing an atom.
        /// </summary>
        /// <param name="atom"></param>
        public void ExitAtom(AtomType atom)
        {
            _jsonWriter.WriteEndArray();
        }

        /// <summary>
        /// Starts processing the cards.
        /// </summary>
        /// <param name="cardTypes"></param>
        public void EnterCards(IEnumerable<CardType> cardTypes)
        {
            _jsonWriter.WritePropertyName("cards");
            _jsonWriter.WriteStartArray();
        }

        /// <summary>
        /// Finalizes processing the cards.
        /// </summary>
        /// <param name="cardTypes"></param>
        public void ExitCards(IEnumerable<CardType> cardTypes)
        {
            _jsonWriter.WriteEndArray();
        }

        /// <summary>
        /// Writes a single card.
        /// </summary>
        /// <param name="cardType"></param>
        public void EnterCard(CardType cardType)
        {
            _jsonWriter.WriteStartArray();
            _jsonWriter.WriteValue(cardType.Name);
            
            cardType.Payload.WriteTo(_jsonWriter);
        }

        /// <summary>
        /// Finalizes writing a single card.
        /// </summary>
        /// <param name="cardType"></param>
        public void ExitCard(CardType cardType)
        {
            _jsonWriter.WriteEndArray();   
        }

        /// <summary>
        /// Starts processing sections.
        /// </summary>
        /// <param name="mobileDocSections"></param>
        public void EnterSections(IEnumerable<Section> mobileDocSections)
        {
            _jsonWriter.WritePropertyName("sections");
            _jsonWriter.WriteStartArray();
        }

        /// <summary>
        /// Finalizes processing sections.
        /// </summary>
        /// <param name="mobileDocSections"></param>
        public void ExitSections(IEnumerable<Section> mobileDocSections)
        {
            _jsonWriter.WriteEndArray();
        }
    }
}