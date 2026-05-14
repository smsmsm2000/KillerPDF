// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 smsmsm2000

using KillerPDF.Core.Documents;

namespace KillerPDF.Core.Search;

public sealed class PdfSearchService : IPdfSearchService
{
    // PORT: upstream uses PdfPig to extract text and does substring matching.
    // Look for "Search" / "FindText" / "PdfPig" in MainWindow.xaml.cs.
    public IAsyncEnumerable<SearchHit> SearchAsync(PdfDocument document, string query, CancellationToken ct = default)
        => throw new NotImplementedException("Port full-text search (PdfPig) from upstream.");
}
