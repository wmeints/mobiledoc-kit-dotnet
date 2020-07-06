# Mobiledoc kit for .NET Core

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

```
var doc = MobileDocSerializer.Deserialize(someDocumentContent);
```

## Developing

TODO: Describe how to change elements in the code.