// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 smsmsm2000

using KillerPDF.Core.Annotations;
using KillerPDF.Core.Documents;

namespace KillerPDF.Core.Editing;

/// <summary>
/// Applies annotations and inline edits onto a PDF in memory, producing a new
/// stream of bytes. Pure transformation — does not touch the filesystem.
/// </summary>
public interface IPdfEditService
{
    /// <summary>
    /// Walks the document, merges every page's annotations into the PDF's content
    /// stream, and returns the resulting bytes. Used by Save and Print-with-flatten.
    /// </summary>
    Task<byte[]> ApplyAnnotationsAsync(PdfDocument document, CancellationToken ct = default);

    /// <summary>
    /// Finds the closest matching system font for a given embedded font name,
    /// so inline text edits look continuous with the original. Upstream calls
    /// this its "font matching" feature.
    /// </summary>
    string MatchFont(string embeddedFontName);
}
