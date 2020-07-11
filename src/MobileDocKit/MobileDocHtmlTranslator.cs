using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;

namespace MobileDocRenderer
{
    /// <summary>
    /// Translates a mobiledoc instance into HTML text.
    /// </summary>
    public class MobileDocHtmlTranslator: MobileDocListener
    {
        private readonly HtmlDocument _document;
        private readonly IEnumerable<AtomRenderer> _atomRenderers;
        private readonly IEnumerable<CardRenderer> _cardRenderers;
        private readonly TextWriter _outputWriter;
        private readonly HtmlNode _rootElement;
        private readonly Stack<HtmlNode> _nodes;
        
        private HtmlNode _currentNode;
        private IEnumerable<MarkupType> _markupTypes;
        private IEnumerable<AtomType> _atomTypes;
        private IEnumerable<CardType> _cardTypes;


        /// <summary>
        /// Initializes a new instance of <see cref="MobileDocHtmlTranslator"/>
        /// </summary>
        /// <param name="cardRenderers">Rendering components to use for translating cards.</param>
        /// <param name="atomRenderers">Rendering components to use for translating atoms.</param>
        /// <param name="outputWriter">Output writer to use for storing the generated HTML.</param>
        public MobileDocHtmlTranslator(IEnumerable<CardRenderer> cardRenderers, 
            IEnumerable<AtomRenderer> atomRenderers, TextWriter outputWriter)
        {
            _cardRenderers = cardRenderers;
            _atomRenderers = atomRenderers;
            _outputWriter = outputWriter;

            _nodes = new Stack<HtmlNode>();
            _document = new HtmlDocument();

            var bodyElement = _document.CreateElement("body");
            var articleElement = _document.CreateElement("article");

            bodyElement.AppendChild(articleElement);
            
            _document.DocumentNode.AppendChild(bodyElement);
            
            _rootElement = articleElement;
            _currentNode = articleElement;
        }

        /// <summary>
        /// Writes the final document content to the output.
        /// </summary>
        /// <param name="mobileDoc"></param>
        public override void ExitMobileDoc(MobileDoc mobileDoc)
        {
            _rootElement.WriteTo(_outputWriter);
        }

        /// <summary>
        /// Tracks the markup definitions for the document.
        /// </summary>
        /// <param name="mobileDocMarkups"></param>
        public override void EnterMarkups(IEnumerable<MarkupType> mobileDocMarkups)
        {
            _markupTypes = mobileDocMarkups;
        }

        /// <summary>
        /// Tracks the atom definitions for the document.
        /// </summary>
        /// <param name="mobileDocAtoms"></param>
        public override void EnterAtoms(IEnumerable<AtomType> mobileDocAtoms)
        {
            _atomTypes = mobileDocAtoms;
        }

        public override void EnterCards(IEnumerable<CardType> cardTypes)
        {
            _cardTypes = cardTypes;
        }

        /// <summary>
        /// Renders each of the sections in the document.
        /// </summary>
        /// <param name="mobileDocSections">Sections to render.</param>
        public override void EnterSections(IEnumerable<Section> mobileDocSections)
        {
            foreach (var section in mobileDocSections)
            {
                switch (section)
                {
                    case MarkupSection markupSection:
                        RenderMarkupSection(markupSection);
                        break;
                    case ListSection listSection:
                        RenderListSection(listSection);
                        break;
                    case ImageSection imageSection:
                        RenderImageSection(imageSection);
                        break;
                    case CardSection cardSection:
                        RenderCardSection(cardSection);
                        break;
                }
            }
        }
        
        private void RenderListSection(ListSection listSection)
        {
            var listElement = _document.CreateElement(listSection.ListType);
            _rootElement.AppendChild(listElement);
            
            listElement.Attributes.AddRange(listSection.Attributes.Select(
                x=> _document.CreateAttribute(x.Name,x.Value)));

            foreach (var listItem in listSection.ListItems)
            {
                _currentNode = _document.CreateElement("li");
                listElement.AppendChild(_currentNode);
                
                _nodes.Clear();
                _nodes.Push(_currentNode);

                RenderMarkers(listItem);
            }
        }
        
        private void RenderCardSection(CardSection cardSection)
        {
            var cardType = _cardTypes.ElementAt(cardSection.CardIndex);
            var cardRenderer = _cardRenderers.FirstOrDefault(x => x.CanRender(cardType));

            cardRenderer?.Render(_currentNode, cardType);
        }

        private void RenderImageSection(ImageSection imageSection)
        {
            _currentNode = _document.CreateElement("img");
            _currentNode.Attributes.Add(_document.CreateAttribute("src", imageSection.Url));

            _rootElement.AppendChild(_currentNode);
        }

        private void RenderMarkupSection(MarkupSection markupSection)
        {
            _currentNode = _document.CreateElement(markupSection.TagName);
            
            // We reset the marker stack, because there may be leftover markers from the previous section.
            _nodes.Clear();
            _nodes.Push(_currentNode);
            
            _currentNode.Attributes.AddRange(markupSection.Attributes.Select(
                x => _document.CreateAttribute(x.Name, x.Value)));

            _rootElement.AppendChild(_currentNode);
            
            RenderMarkers(markupSection.Markers);
        }

        private void RenderMarkers(IEnumerable<Marker> markers)
        {
            foreach (var marker in markers)
            {
                RenderMarker(marker);
            }
        }

        private void RenderMarker(Marker marker)
        {
            foreach (var openedMarkup in marker.OpenMarkupIndices)
            {
                var markup = _markupTypes.ElementAt(openedMarkup);
                PushMarkupElement(markup);
            }

            switch (marker)
            {
                case MarkupMarker markupMarker:
                    RenderMarkupText(markupMarker);
                    break;
                case AtomMarker atomMarker:
                    RenderAtom(atomMarker);
                    break;
            }

            for (var i = 0; i < marker.ClosedMarkups; i++)
            {
                PopMarkupElement();
            }
        }

        private void RenderMarkupText(MarkupMarker markupMarker)
        {
            _currentNode.AppendChild(_document.CreateTextNode(markupMarker.Text));
        }
        
        private void RenderAtom(AtomMarker atomMarker)
        {
            var atomType = _atomTypes.ElementAt(atomMarker.AtomIndex);
            var atomRenderer = _atomRenderers.FirstOrDefault(x => x.CanRender(atomType));

            if (atomRenderer != null)
            {
                atomRenderer.Render(_currentNode, atomType);
            }
            else
            {
                _currentNode.AppendChild(_document.CreateTextNode(""));
            }
        }

        private void PushMarkupElement(MarkupType markup)
        {
            var markupNode = _document.CreateElement(markup.Name);
            markupNode.Attributes.AddRange(markup.Attributes.Select(
                x=> _document.CreateAttribute(x.Name,x.Value)));

            _currentNode.AppendChild(markupNode);
            
            _nodes.Push(markupNode);
            _currentNode = markupNode;
        }

        private void PopMarkupElement()
        {
            _nodes.Pop();
            _currentNode = _nodes.Peek();
        }
    }
}