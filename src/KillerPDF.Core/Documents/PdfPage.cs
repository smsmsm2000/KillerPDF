// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 smsmsm2000

using KillerPDF.Core.Annotations;

namespace KillerPDF.Core.Documents;

/// <summary>
/// A single page within a <see cref="PdfDocument"/>. Holds the page's source
/// index (in the original document) and its annotation overlay. Pixel content
/// is rendered on demand by <see cref="Rendering.IPdfRenderService"/>.
/// </summary>
public sealed class PdfPage
{
    public PdfPage(int sourceIndex, double widthPoints, double heightPoints)
    {
        SourceIndex = sourceIndex;
        WidthPoints = widthPoints;
        HeightPoints = heightPoints;
        Annotations = new List<Annotation>();
    }

    /// <summary>0-based index of the page in the source PDF before any reordering.</summary>
    public int SourceIndex { get; }

    /// <summary>Page width in PDF points (1/72 inch).</summary>
    public double WidthPoints { get; }

    /// <summary>Page height in PDF points (1/72 inch).</summary>
    public double HeightPoints { get; }

    /// <summary>Annotations layered on top of this page (added by the fork or upstream's editor).</summary>
    public List<Annotation> Annotations { get; }
}
