// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 smsmsm2000

using KillerPDF.Core.Documents;

namespace KillerPDF.Core.Rendering;

/// <summary>
/// Rasterizes PDF pages to in-memory bitmaps. Decoupled from WPF so it can be
/// unit-tested and reused from other UIs.
/// </summary>
public interface IPdfRenderService
{
    /// <summary>Render a page to a 32-bit BGRA byte buffer at the given DPI.</summary>
    /// <returns>(pixels, width, height) where pixels.Length == width * height * 4.</returns>
    Task<(byte[] pixels, int width, int height)> RenderPageAsync(
        PdfDocument document, int pageIndex, int dpi, CancellationToken ct = default);
}
