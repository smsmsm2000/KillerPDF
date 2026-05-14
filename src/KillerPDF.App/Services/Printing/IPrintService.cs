// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 <YOUR NAME HERE>

using KillerPDF.Core.Documents;
using KillerPDF.Core.Editing;

namespace KillerPDF.App.Services.Printing;

/// <summary>
/// Prints a PDF document. Upstream flattens annotations into the printed
/// output, which means it asks the edit service to bake them first.
/// </summary>
public interface IPrintService
{
    Task PrintAsync(PdfDocument document, IPdfEditService editor, CancellationToken ct = default);
}
