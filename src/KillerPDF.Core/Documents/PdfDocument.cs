// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 <YOUR NAME HERE>
//
// This file is part of the KillerPDF fork. It is licensed under GPL-3.0.
// See the LICENSE and NOTICE files in the repository root.

using System.Collections.ObjectModel;

namespace KillerPDF.Core.Documents;

/// <summary>
/// Represents an open PDF document. Pure data + identity; no rendering, no I/O.
/// Operations on the document are performed via <see cref="IPdfDocumentService"/>
/// and the other service interfaces.
/// </summary>
public sealed class PdfDocument
{
    public PdfDocument(string sourcePath, IReadOnlyList<PdfPage> pages, bool wasEncrypted)
    {
        SourcePath = sourcePath;
        Pages = new ObservableCollection<PdfPage>(pages);
        WasEncrypted = wasEncrypted;
    }

    /// <summary>Absolute path to the source file the document was loaded from.</summary>
    public string SourcePath { get; }

    /// <summary>Ordered list of pages. Mutable to support reorder/merge/split flows.</summary>
    public ObservableCollection<PdfPage> Pages { get; }

    /// <summary>True if the source PDF was password-protected; the working copy is decrypted.</summary>
    public bool WasEncrypted { get; }

    /// <summary>True if there are unsaved edits (annotations, edits, reorders, etc.).</summary>
    public bool IsDirty { get; set; }
}
