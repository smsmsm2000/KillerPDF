// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 <YOUR NAME HERE>

namespace KillerPDF.Core.Annotations;

/// <summary>
/// Represents an in-place edit of existing text in the PDF (upstream's
/// "Inline text editing with font matching" feature). Different from
/// <see cref="TextAnnotation"/>, which is an overlay.
/// </summary>
public sealed class InlineTextEdit : Annotation
{
    public string OriginalText { get; set; } = string.Empty;
    public string NewText { get; set; } = string.Empty;
    public string MatchedFontFamily { get; set; } = string.Empty;
    public double FontSize { get; set; }
}
