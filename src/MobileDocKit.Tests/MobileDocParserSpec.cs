using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace MobileDocRenderer.Tests
{
    public class MobileDocParserSpec
    {
        private readonly ITestOutputHelper _output;

        public MobileDocParserSpec(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void CanParseEmptyDocument()
        {
            var document = "{ \"version\": \"0.3.0\", \"sections\": [] }";
            var parser = new MobileDocParser(document);

            var result = parser.MobileDoc();

            result.Version.Should().Be("0.3.0");
            result.Sections.Should().BeEmpty();
        }

        [Fact]
        public void CanParseDocumentWithoutMarkups()
        {
            var document = new MobileDocBuilder()
                .WithMarkupSection(section => section
                    .WithTagName("p")
                    .WithMarkupMarker(new int[] { }, 0, "hello world"))
                .Build();

            var parser = new MobileDocParser(MobileDocSerializer.Serialize(document));

            var result = parser.MobileDoc();

            result.Sections.Should().ContainSingle(x => x.SectionType == 1);
        }

        [Fact]
        public void CanParseMarkers()
        {
            var document = new MobileDocBuilder()
                .WithMarkupSection(section => section
                    .WithTagName("pull-quote")
                    .WithMarkupMarker(new int[] { }, 0, "Hello world")
                    .WithAttribute("data-test", "test"))
                .Build();

            var parser = new MobileDocParser(MobileDocSerializer.Serialize(document));
            var result = parser.MobileDoc();

            result.Sections.First().As<MarkupSection>().Markers.First().As<MarkupMarker>().Text.Should()
                .Be("Hello world");
            result.Sections.First().As<MarkupSection>().Markers.First().OpenMarkupIndices.Should().BeEmpty();
            result.Sections.First().As<MarkupSection>().Markers.First().ClosedMarkups.Should().Be(0);
        }

        [Fact]
        public void CanParseMarkupSectionAttributes()
        {
            var document = new MobileDocBuilder()
                .WithMarkupSection(section => section
                    .WithTagName("pull-quote")
                    .WithMarkupMarker(new int[] { }, 0, "Hello world")
                    .WithAttribute("data-test", "test"))
                .Build();

            var parser = new MobileDocParser(MobileDocSerializer.Serialize(document));
            var result = parser.MobileDoc();

            result.Sections.FirstOrDefault()?.As<MarkupSection>().Attributes.Should()
                .Contain(x => x.Name == "data-test" && x.Value == "test");
        }

        [Fact]
        public void CanParseAtoms()
        {
            var document = new MobileDocBuilder()
                .WithAtom(atom => atom
                    .WithName("test")
                    .WithText("Awesomeness")
                    .WithPayload(new JObject(new JProperty("test", "test"))))
                .WithMarkupSection(section => section
                    .WithTagName("pull-quote")
                    .WithAtomMarker(new int[] { }, 0, 0)
                    .WithAttribute("data-test", "test"))
                .Build();

            var parser = new MobileDocParser(MobileDocSerializer.Serialize(document));
            var result = parser.MobileDoc();

            result.Atoms.Should().ContainSingle();
            result.Atoms.First().Name.Should().Be("test");
            result.Atoms.First().Text.Should().Be("Awesomeness");
            result.Atoms.First().Payload["test"].Value<string>().Should().Be("test");
        }

        [Fact]
        public void CanParseMarkups()
        {
            var document = new MobileDocBuilder()
                .WithMarkup(markup => markup.WithTagName("a").WithAttribute("href", "https://google.nl"))
                .WithMarkupSection(section => section
                    .WithTagName("p")
                    .WithMarkupMarker(new int[] {0}, 1, "Test link"))
                .Build();

            var parser = new MobileDocParser(MobileDocSerializer.Serialize(document));
            var result = parser.MobileDoc();

            result.Markups.Should().NotBeEmpty();
            result.Markups.First().Name.Should().Be("a");
            result.Markups.First().Attributes.Should().Contain(
                x => x.Name == "href" && x.Value == "https://google.nl");
        }

        [Fact]
        public void CanParseCards()
        {
            var document = new MobileDocBuilder()
                .WithCard(card => card
                    .WithName("markdown")
                    .WithPayload(JObject.FromObject(new {text = "# Heading 1"})))
                .WithMarkupSection(section => section
                    .WithTagName("p")
                    .WithMarkupMarker(new int[] { }, 0, "Hello world"))
                .Build();

            var parser = new MobileDocParser(MobileDocSerializer.Serialize(document));
            var result = parser.MobileDoc();

            result.Cards.Should().NotBeEmpty();
            result.Cards.First().Name.Should().Be("markdown");
        }

        [Fact]
        public void CanParseCardSections()
        {
            var document = new MobileDocBuilder()
                .WithCard(card => card
                    .WithName("markdown")
                    .WithPayload(JObject.FromObject(new {text = "# Heading 1"})))
                .WithCardSection(section => section.WithCardIndex(0))
                .Build();

            var parser = new MobileDocParser(MobileDocSerializer.Serialize(document));
            var result = parser.MobileDoc();

            result.Cards.Should().NotBeEmpty();
            result.Cards.First().Name.Should().Be("markdown");
        }

        [Fact]
        public void CanParseListSections()
        {
            var document = new MobileDocBuilder()
                .WithListSection(section => section
                    .WithListType("ol")
                    .WithMarkupMarker(new int[] {0}, 1, "Item 1")
                    .WithAtomMarker(new int[] { }, 0, 0)
                    .WithAttribute("class", "test"))
                .Build();

            var parser = new MobileDocParser(MobileDocSerializer.Serialize(document));
            var result = parser.MobileDoc();

            result.Sections.Should().ContainSingle(x => x.SectionType == SectionTypes.List);
            result.Sections.First().As<ListSection>().Markers.Should().NotBeEmpty();
        }

        [Fact]
        public void CanParseImageSections()
        {
            var document = new MobileDocBuilder()
                .WithImageSection(section => section
                    .WithUrl("/images/image.png"))
                .Build();
            
            var parser = new MobileDocParser(MobileDocSerializer.Serialize(document));
            var result = parser.MobileDoc();

            result.Sections.Should().ContainSingle(x => x.SectionType == SectionTypes.Image);
            result.Sections.First().As<ImageSection>().Url.Should().Be("/images/image.png");
        }
    }
}