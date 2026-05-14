// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 <YOUR NAME HERE>

using KillerPDF.Core.Documents;

namespace KillerPDF.Core.Merging;

public sealed class PdfMergeService : IPdfMergeService
{
    public Task MergeAsync(PdfDocument target, PdfDocument other, CancellationToken ct = default)
        => throw new NotImplementedException("Port merge from upstream.");

    public void Reorder(PdfDocument target, IReadOnlyList<int> newOrder)
        => throw new NotImplementedException("Port reorder (drag-and-drop) from upstream.");

    public Task SplitAsync(PdfDocument source, IReadOnlyList<int> pageIndices, string destinationPath, CancellationToken ct = default)
        => throw new NotImplementedException("Port split from upstream.");
}
