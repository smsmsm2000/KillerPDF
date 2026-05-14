// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 <YOUR NAME HERE>

namespace KillerPDF.Core.Annotations;

/// <summary>
/// Base type for everything the user can draw on top of a page: text boxes,
/// freehand strokes, highlights, signatures, and inline text edits.
/// </summary>
public abstract class Annotation
{
    /// <summary>Stable id assigned at creation; used for undo/redo and selection.</summary>
    public Guid Id { get; } = Guid.NewGuid();

    /// <summary>Page-space bounding rectangle in PDF points.</summary>
    public Rect BoundsInPoints { get; set; }
}

/// <summary>Lightweight rect in PDF points — kept here so Core has no WPF deps.</summary>
public readonly record struct Rect(double X, double Y, double Width, double Height);
