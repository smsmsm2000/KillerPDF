// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 <YOUR NAME HERE>

namespace KillerPDF.Core.Annotations;

/// <summary>Free-floating text box added by the user.</summary>
public sealed class TextAnnotation : Annotation
{
    public string Text { get; set; } = string.Empty;
    public string FontFamily { get; set; } = "Segoe UI";
    public double FontSize { get; set; } = 12.0;
    /// <summary>ARGB packed color (0xAARRGGBB).</summary>
    public uint Color { get; set; } = 0xFF000000;
}
