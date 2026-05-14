// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 <YOUR NAME HERE>

namespace KillerPDF.Core.Documents;

/// <summary>
/// Opens, saves, and closes PDF documents. The single entry point for everything
/// that touches the filesystem on behalf of the editor.
/// </summary>
public interface IPdfDocumentService
{
    /// <summary>Open a PDF from disk. Prompts via <paramref name="passwordPrompt"/> if encrypted.</summary>
    /// <param name="path">Absolute path to the source PDF.</param>
    /// <param name="passwordPrompt">Invoked when the file is password-protected; return null to cancel.</param>
    Task<PdfDocument> OpenAsync(string path, Func<Task<string?>> passwordPrompt, CancellationToken ct = default);

    /// <summary>Save the document back to its source path, applying any pending edits and annotations.</summary>
    Task SaveAsync(PdfDocument document, CancellationToken ct = default);

    /// <summary>Save the document to a different path. Does not flatten annotations.</summary>
    Task SaveAsAsync(PdfDocument document, string destinationPath, CancellationToken ct = default);

    /// <summary>
    /// Rasterize every page at the given DPI and write a fully flat (non-editable) PDF.
    /// Upstream uses 150 DPI via PDFium — preserve that default.
    /// </summary>
    Task SaveFlattenedAsync(PdfDocument document, string destinationPath, int dpi = 150, CancellationToken ct = default);

    /// <summary>Release any unmanaged resources (PDFium handles, temp decrypted copies).</summary>
    void Close(PdfDocument document);
}
