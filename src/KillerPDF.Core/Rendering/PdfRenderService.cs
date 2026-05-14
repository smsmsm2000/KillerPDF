// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 <YOUR NAME HERE>

using KillerPDF.Core.Documents;

namespace KillerPDF.Core.Rendering;

public sealed class PdfRenderService : IPdfRenderService
{
    // PORT: upstream renders via Docnet.Core's IDocReader / GetPageReader.
    // Look for "GetImage" calls in MainWindow.xaml.cs.
    public Task<(byte[] pixels, int width, int height)> RenderPageAsync(
        PdfDocument document, int pageIndex, int dpi, CancellationToken ct = default)
        => throw new NotImplementedException("Port Docnet rendering from upstream.");
}
