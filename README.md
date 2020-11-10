# Markdig.Prism
An extension that adds syntax highlighting to a [Markdig](https://github.com/lunet-io/markdig) pipeline through [Prism.js](https://prismjs.com/) JavaScript library.

## Usage

1. Add ```WebStoating.Markdig.Prism``` to your project.

```powershell
dotnet add package WebStoating.Markdig.Prism --version 1.0.0
```

2. Create Markdig pipeline

```csharp
private static readonly MarkdownPipeline MarkdownPipeline = new MarkdownPipelineBuilder()
    .UseAdvancedExtensions()
    .UsePrism()
    .Build();
```

3. Download [Prism](https://prismjs.com/download.html) and add it to your Razor page or layout template.

```html
<!DOCTYPE html>
<html>
<head>
	...
	<link href="themes/prism.css" rel="stylesheet" />
</head>
<body>
	...
	<script src="prism.js"></script>
</body>
</html>
```

4. Convert Markdown to HTML

```csharp
var html = Markdown.ToHtml(markdown, MarkdownPipeline);
```