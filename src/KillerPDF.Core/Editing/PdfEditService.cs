// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 smsmsm2000

using KillerPDF.Core.Documents;

namespace KillerPDF.Core.Editing;

public sealed class PdfEditService : IPdfEditService
{
    // PORT: upstream uses PdfSharpCore to write XObjects/text onto pages.
    public Task<byte[]> ApplyAnnotationsAsync(PdfDocument document, CancellationToken ct = default)
        => throw new NotImplementedException("Port annotation-baking from upstream.");

    // PORT: upstream's font-matching heuristic. Walk InstalledFontCollection,
    // pick the closest family by name similarity + style.
    public string MatchFont(string embeddedFontName)
        => throw new NotImplementedException("Port font-matching heuristic from upstream.");
}
