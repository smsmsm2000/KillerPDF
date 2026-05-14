// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 smsmsm2000

using KillerPDF.Core.Documents;

namespace KillerPDF.Core.Search;

/// <summary>Full-text search across an open document.</summary>
public interface IPdfSearchService
{
    /// <summary>Find every occurrence of <paramref name="query"/> in <paramref name="document"/>.</summary>
    IAsyncEnumerable<SearchHit> SearchAsync(PdfDocument document, string query, CancellationToken ct = default);
}

/// <summary>A single search result.</summary>
public readonly record struct SearchHit(int PageIndex, int CharOffset, int Length, string Context);
