// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 smsmsm2000

using KillerPDF.Core.Documents;
using KillerPDF.Core.Editing;

namespace KillerPDF.App.Services.Printing;

public sealed class PrintService : IPrintService
{
    // PORT: upstream uses System.Drawing.Printing.PrintDocument fed by PDFium
    // page rasters, with the annotation overlay baked in via IPdfEditService.
    public Task PrintAsync(PdfDocument document, IPdfEditService editor, CancellationToken ct = default)
        => throw new NotImplementedException("Port print pipeline from upstream.");
}
