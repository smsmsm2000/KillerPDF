// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 <YOUR NAME HERE>

namespace KillerPDF.Core.Annotations;

/// <summary>Freehand pen stroke. Points are in page space (PDF points).</summary>
public sealed class FreehandAnnotation : Annotation
{
    public List<(double X, double Y)> Points { get; } = new();
    public uint Color { get; set; } = 0xFF000000;
    public double Thickness { get; set; } = 2.0;
    public double Opacity { get; set; } = 1.0;
}
