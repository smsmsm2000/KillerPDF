// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 smsmsm2000

using KillerPDF.Core.Documents;

namespace KillerPDF.Core.Merging;

/// <summary>Merges multiple PDFs and splits pages out of a document.</summary>
public interface IPdfMergeService
{
    /// <summary>Append <paramref name="other"/>'s pages onto <paramref name="target"/>.</summary>
    Task MergeAsync(PdfDocument target, PdfDocument other, CancellationToken ct = default);

    /// <summary>Reorder pages in-place using the given 0-based page-index permutation.</summary>
    void Reorder(PdfDocument target, IReadOnlyList<int> newOrder);

    /// <summary>Write the given page indices to a new PDF at <paramref name="destinationPath"/>.</summary>
    Task SplitAsync(PdfDocument source, IReadOnlyList<int> pageIndices, string destinationPath, CancellationToken ct = default);
}
