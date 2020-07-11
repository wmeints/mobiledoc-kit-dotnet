using FluentAssertions;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MobileDocRenderer.Tests
{
    public class MobileDocRendererSpec
    {
        [Fact]
        public void CanRenderEmptyDocument()
        {
            var document = new MobileDocBuilder()
                .Build();

            var renderer = new MobileDocRenderer(new CardRenderer[] { }, new AtomRenderer[] { });
            var output = renderer.Render(document);

            output.Should().Be("<article></article>");
        }

        [Fact]
        public void CanRenderSimpleDocument()
        {
            var document = new MobileDocBuilder()
                .WithMarkupSection(section => section
                    .WithMarkupMarker(new int[] { }, 0, "Hello world")
                    .WithTagName("p"))
                .Build();

            var renderer = new MobileDocRenderer(new CardRenderer[] { }, new AtomRenderer[] { });
            var output = renderer.Render(document);

            output.Should().Be("<article><p>Hello world</p></article>");
        }

        [Fact]
        public void CanRenderWithPartiallyClosedMarkup()
        {
            var document = new MobileDocBuilder()
                .WithMarkup(markup => markup
                    .WithTagName("h1"))
                .WithMarkup(markup => markup
                    .WithTagName("b"))
                .WithMarkupSection(section => section
                    .WithTagName("p")
                    .WithMarkupMarker(new int[] {0}, 0, "Hello ")
                    .WithMarkupMarker(new int[] {1}, 2, "world"))
                .Build();

            var renderer = new MobileDocRenderer(new CardRenderer[] { }, new AtomRenderer[] { });
            var output = renderer.Render(document);

            output.Should().Be("<article><p><h1>Hello <b>world</b></h1></p></article>");
        }

        [Fact]
        public void CanRenderWithMarkup()
        {
            var document = new MobileDocBuilder()
                .WithMarkup(markup => markup
                    .WithTagName("b")
                    .WithAttribute("class", "very-bold"))
                .WithMarkupSection(section => section
                    .WithTagName("p")
                    .WithMarkupMarker(new int[] {0}, 1, "Hello world"))
                .Build();

            var renderer = new MobileDocRenderer(new CardRenderer[] { }, new AtomRenderer[] { });
            var output = renderer.Render(document);

            output.Should().Be("<article><p><b class=\"very-bold\">Hello world</b></p></article>");
        }

        [Fact]
        public void CanRenderWithMissingAtomRenderer()
        {
            var document = new MobileDocBuilder()
                .WithMarkup(markup => markup
                    .WithTagName("b")
                    .WithAttribute("class", "very-bold"))
                .WithAtom(atom => atom.WithName("test").WithText("test").WithPayload(new JObject()))
                .WithMarkupSection(section => section
                    .WithTagName("p")
                    .WithMarkupMarker(new int[] {0}, 1, "Hello")
                    .WithAtomMarker(new int[] { }, 0, 0))
                .Build();

            var renderer = new MobileDocRenderer(new CardRenderer[] { }, new AtomRenderer[] { });
            var output = renderer.Render(document);

            output.Should().Be("<article><p><b class=\"very-bold\">Hello</b></p></article>");
        }

        [Fact]
        public void CanRenderWithAtomRenderer()
        {
            var document = new MobileDocBuilder()
                .WithMarkup(markup => markup
                    .WithTagName("b")
                    .WithAttribute("class", "very-bold"))
                .WithAtom(atom => atom.WithName("link").WithText("world").WithPayload(new JObject()))
                .WithMarkupSection(section => section
                    .WithTagName("p")
                    .WithMarkupMarker(new int[] {0}, 1, "Hello")
                    .WithAtomMarker(new int[] { }, 0, 0))
                .Build();

            var renderer = new MobileDocRenderer(new CardRenderer[] { }, new AtomRenderer[]
            {
                new LinkAtomRenderer()
            });

            var output = renderer.Render(document);

            output.Should().Be("<article><p><b class=\"very-bold\">Hello</b><a href=\"~/\">world</a></p></article>");
        }

        [Fact]
        public void CanRenderListSectionWithoutMarkup()
        {
            var document = new MobileDocBuilder()
                .WithListSection(section => section
                    .WithListType("ul")
                    .WithAttribute("class", "test")
                    .WithListItem(item => item
                        .WithMarkupMarker(new int[] { }, 0, "Hello world")))
                .Build();

            var renderer = new MobileDocRenderer(new CardRenderer[] { }, new AtomRenderer[] { });
            var output = renderer.Render(document);

            output.Should().Be("<article><ul class=\"test\"><li>Hello world</li></ul></article>");
        }

        [Fact]
        public void CanRenderListWithMarkup()
        {
            var document = new MobileDocBuilder()
                .WithMarkup(markup => markup.WithTagName("b"))
                .WithListSection(section => section
                    .WithListType("ul")
                    .WithAttribute("class", "test")
                    .WithListItem(item => item
                        .WithMarkupMarker(new int[] {0}, 1, "Hello world")))
                .Build();

            var renderer = new MobileDocRenderer(new CardRenderer[] { }, new AtomRenderer[] { });
            var output = renderer.Render(document);

            output.Should().Be("<article><ul class=\"test\"><li><b>Hello world</b></li></ul></article>");
        }

        [Fact]
        public void CanRenderImageSections()
        {
            var document = new MobileDocBuilder()
                .WithImageSection(section => section
                    .WithUrl("http://www.google.nl/images/test.png"))
                .Build();

            var renderer = new MobileDocRenderer(new CardRenderer[] { }, new AtomRenderer[] { });
            var output = renderer.Render(document);

            output.Should().Be("<article><img src=\"http://www.google.nl/images/test.png\"></article>");
        }

        [Fact]
        public void CanRenderCardSection()
        {
            var document = new MobileDocBuilder()
                .WithCard(card => card.WithName("test").WithPayload(new JObject()))
                .WithCardSection(section => section.WithCardIndex(0))
                .Build();
            
            var renderer = new MobileDocRenderer(new CardRenderer[]
            {
                new CodeCardRenderer()
            }, new AtomRenderer[] { });
            var output = renderer.Render(document);

            output.Should().Be("<article><div class=\"code\"></div></article>");
        }

        private class LinkAtomRenderer : AtomRenderer
        {
            public override void Render(HtmlNode parentElement, AtomType atomType)
            {
                var element = parentElement.OwnerDocument.CreateElement("a");

                element.Attributes.Add(parentElement.OwnerDocument.CreateAttribute("href", "~/"));
                element.AppendChild(parentElement.OwnerDocument.CreateTextNode(atomType.Text));

                parentElement.AppendChild(element);
            }

            public override bool CanRender(AtomType atomType) => atomType.Name == "link";
        }

        private class CodeCardRenderer : CardRenderer
        {
            public override void Render(HtmlNode parentElement, CardType cardType)
            {
                var codeElement = parentElement.OwnerDocument.CreateElement("div");
                codeElement.Attributes.Add(parentElement.OwnerDocument.CreateAttribute("class","code"));

                parentElement.AppendChild(codeElement);
            }

            public override bool CanRender(CardType cardType) => true;
        }
    }
}