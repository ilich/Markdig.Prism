# Download Button Usage Guide

The Download Button plugin allows users to download the content of a code block as a text file. This guide will show you how to use it.

## Prerequisites

To use the Download Button plugin, you need to include the following JavaScript code in your project:

```javascript
// This function generates a downloadable file from a string of text.
function downloadFile(filename, text, extension) {
    var blob = new Blob([text], { type: "text/plain" });
    var url = URL.createObjectURL(blob);
    var a = document.createElement("a");
    a.href = url;
    a.download = filename + extension;
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(url);
}

// This function adds a click event listener to a download button.
function addDownloadButtonListener(id) {
    var button = document.getElementById(id + "-download");
    var codeBlock = document.getElementById(id);
    var toolbar = codeBlock.parentElement.parentElement.lastElementChild;

    var newDiv = document.createElement("div");
    newDiv.className = "toolbar-item";
    newDiv.appendChild(button);

    toolbar.appendChild(newDiv);

    button.addEventListener("click", function () {
        var text = codeBlock.textContent;
        var extension = button.getAttribute("data-extension") || ".txt";
        downloadFile(id, text, extension);
    });
}

// This function registers a button with the Prism toolbar plugin.
function registerDownloadButton() {
    Prism.plugins.toolbar.registerButton('download-button', function (env) {
        var button = document.createElement('button');
        button.textContent = 'Download';

        var pre = env.element.parentNode;
        var id = pre.getAttribute('id');
        var extension = pre.getAttribute('data-extension') || '.txt';

        button.addEventListener('click', function () {
            var code = env.element.textContent;
            downloadFile(id, code, extension);
        });

        return button;
    });
}

// This function adds download button listeners for each code block.
function addDownloadButtonListeners() {
    var codeBlocks = document.querySelectorAll("pre[data-download-link]");
    for (var i = 0; i < codeBlocks.length; i++) {
        var codeBlock = codeBlocks[i].children[0];
        addDownloadButtonListener(codeBlock.id);
    }
}

// Run everything after the page has loaded.
window.onload = function () {
    registerDownloadButton();
    addDownloadButtonListeners();
};
```

## Usage
Set the `UseDownloadButton` option to `true` when configuring the extension.
```csharp
private static readonly MarkdownPipeline MarkdownPipeline = new MarkdownPipelineBuilder()
	.UseAdvancedExtensions()
	.UsePrism(new PrismExtensionOptions
	{
		UseDownloadButton = true,
	})
	.Build();
```
Once the JavaScript code is included in your project and the `UseDownloadButton` option is `true`, the Download Button plugin will automatically add a download button to each code block in your Markdown content. Users can click this button to download the content of the code block as a text file.
