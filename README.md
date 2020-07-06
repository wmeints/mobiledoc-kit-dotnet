# Mobiledoc kit for .NET Core

[![Build](https://github.com/wmeints/mobiledoc-kit-dotnet/workflows/Build/badge.svg)](https://github.com/wmeints/mobiledoc-kit-dotnet/actions?query=workflow%3ABuild)

Mobiledoc Kit for .NET Core is a C# implementation of the mobiledoc format.
It supports the following features:

* Parsing Mobiledoc content
* Programmatically creating Mobiledoc content
* Rendering Mobiledoc content to HTML

## System requirements

You'll need the following to use Mobiledoc Kit for .NET Core:

* .NET Core 3.1 SDK

## Getting started

To use the library, add the following package reference to your `.csproj` file:

```xml 
<PackageReference Include="MobileDocKit"/>
```

You can now parse Mobiledoc with the following code:

```csharp
var doc = MobileDocSerializer.Deserialize(someDocumentContent);
```

Similarly, you can serialize mobiledoc content using the following code:

```csharp
MobileDocSerializer.Serialize(mobileDocContent);
```

If you're looking to create mobiledoc content programmatically, 
you can build a mobiledoc document using the builder interface:

```csharp
var document = new MobileDocBuilder()
    .WithMarkupSection(section => section
        .WithTagName("p")
        .WithMarker(new int[] { }, 0, "Hello world"))
    .Build();
```

## Learning more about mobiledoc

You can learn more about the mobiledoc format in 
[the official specification](https://github.com/bustle/mobiledoc-kit/blob/master/MOBILEDOC.md). 

## Developing

### Recommended developer tooling

* Windows/Linux/Mac
* .NET Core SDK 3.1
* Visual Studio Code/Visual Studio 2019/Rider 2020.1

### Setting up your development environment

You don't need anything special on your machine, aside from the .NET Core SDK
and a code editor.

### Running tests

You can run tests using the following command:

```shell
dotnet test src/MobileDocKit.Tests/MobileDocKit.Tests.csproj
```

