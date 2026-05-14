// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 smsmsm2000

namespace KillerPDF.Core.Documents;

/// <summary>
/// Default implementation backed by PDFium (Docnet.Core) for reads and
/// PdfSharpCore for writes. This is a scaffold — port the working logic from
/// upstream MainWindow.xaml.cs.
/// </summary>
public sealed class PdfDocumentService : IPdfDocumentService
{
    // PORT: open / password-prompt loop — upstream MainWindow.xaml.cs, look for the
    // "Password" prompt logic and the temp-decrypted-copy handling.
    public Task<PdfDocument> OpenAsync(string path, Func<Task<string?>> passwordPrompt, CancellationToken ct = default)
        => throw new NotImplementedException("Port from upstream MainWindow.xaml.cs (open + password flow).");

    // PORT: save — upstream's Save/Save-As handlers that apply annotation overlays
    // back into the PDF before writing.
    public Task SaveAsync(PdfDocument document, CancellationToken ct = default)
        => throw new NotImplementedException("Port from upstream Save handler.");

    public Task SaveAsAsync(PdfDocument document, string destinationPath, CancellationToken ct = default)
        => throw new NotImplementedException("Port from upstream Save-As handler.");

    // PORT: flatten — upstream rasterizes pages at 150 DPI via PDFium and writes
    // a new PDF where each page is a single image.
    public Task SaveFlattenedAsync(PdfDocument document, string destinationPath, int dpi = 150, CancellationToken ct = default)
        => throw new NotImplementedException("Port from upstream 'Save Flattened PDF' handler.");

    public void Close(PdfDocument document)
    {
        // PORT: release PDFium handles, delete temp decrypted copy if any.
    }
}
