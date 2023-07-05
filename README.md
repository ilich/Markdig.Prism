# Markdig.Prism (Forked Version)

This is a forked version of the [Markdig.Prism](https://github.com/WebStoating/Markdig.Prism) library, an extension that adds syntax highlighting to a [Markdig](https://github.com/lunet-io/markdig) pipeline through [Prism.js](https://prismjs.com/) JavaScript library.

This forked version includes additional features such as a download button for code blocks. Please note that some of these features require additional JavaScript to function correctly.

## Usage

1. Add this forked version of `Markdig.Prism` to your project.

```powershell
dotnet add package YourPackageName --version 1.0.0
```

2. Create Markdig pipeline

```csharp
private static readonly MarkdownPipeline MarkdownPipeline = new MarkdownPipelineBuilder()
    .UseAdvancedExtensions()
    .UsePrism()
    .Build();
```

3. Download [Prism](https://prismjs.com/download.html) and add it to your Razor page or layout template. Make sure to include the additional JavaScript required for the new features.

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
	<script src="yourAdditionalScript.js"></script>
</body>
</html>
```

4. Convert Markdown to HTML

```csharp
var html = Markdown.ToHtml(markdown, MarkdownPipeline);
```

## Configuration

You can configure the extension by passing a `PrismExtensionOptions` object to the `UsePrism` method.

```csharp
private static readonly MarkdownPipeline MarkdownPipeline = new MarkdownPipelineBuilder()
	.UseAdvancedExtensions()
	.UsePrism(new PrismExtensionOptions
	{
		UseDownloadButton = true,
		UseLineNumbers = true,
	})
	.Build();
```

It will default to the following options:

```csharp
new PrismExtensionOptions
{
	UseDownloadButton = false,
	UseLineNumbers = false,
}
```
## Supported Plugins

| Supported | Plugin Name | Usage Guide |
| ----------- | --------- | ----------- |
| ❌ | Line Highlight | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/LineHighlightUsage.md) |
| ✅ | Line Numbers |  [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/LineNumbersUsage.md) |
| ❌ | Show Invisibles  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/ShowInvisiblesUsage.md) |
| ❌ | Autolinker | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/AutolinkerUsage.md) |
| ❌ | WebPlatform Docs  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/WebPlatformDocsUsage.md) |
| ❌ | Custom Class  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/CustomClassUsage.md) |
| ❌ | File Highlight  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/FileHighlightUsage.md) |
| ❌ | Show Language  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/ShowLanguageUsage.md) |
| ❌ | JSONP Highlight  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/JSONPHighlightUsage.md) |
| ❌ | Highlight Keywords  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/HighlightKeywordsUsage.md) |
| ❌ | Remove initial line feed  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/RemoveInitialLineFeedUsage.md) |
| ❌ | Inline color  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/InlineColorUsage.md) |
| ❌ | Previewers  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/PreviewersUsage.md) |
| ❌ | Autoloader  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/AutoloaderUsage.md) |
| ❌ | Keep Markup  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/KeepMarkupUsage.md) |
| ❌ | Command Line  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/CommandLineUsage.md) |
| ❌ | Unescaped Markup  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/UnescapedMarkupUsage.md) |
| ❌ | Normalize Whitespace  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/NormalizeWhitespaceUsage.md) |
| ❌ | Data-URI Highlight  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/DataURIHighlightUsage.md) |
| ✅ | Toolbar  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/ToolbarUsage.md) |
| ✅ | Copy to Clipboard Button  | [Guide](./CopyToClipboardButtonUsage.md) |
| ✅ | Download Button  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/DownloadButtonUsage.md) |
| ❌ | Match braces  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/MatchBracesUsage.md) |
| ❌ | Diff Highlight  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/DiffHighlightUsage.md) |
| ❌ | Filter highlightAll  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/FilterHighlightAllUsage.md) |
| ❌ | Treeview  | [Guide](https://github.com/Retrokiller543/Markdig.Prism/blob/main/docs/TreeviewUsage.md) |

## Credits

This library is a fork of [Markdig.Prism](https://github.com/WebStoating/Markdig.Prism) by [Ilya Verbitskiy](https://github.com/ilich). All credit for the original work goes to them.
