// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 smsmsm2000

namespace KillerPDF.Core.Annotations;

/// <summary>Translucent highlight rectangle.</summary>
public sealed class HighlightAnnotation : Annotation
{
    public uint Color { get; set; } = 0xFFFFFF00;
    public double Opacity { get; set; } = 0.4;
}
