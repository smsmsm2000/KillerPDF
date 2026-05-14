// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 <YOUR NAME HERE>

using KillerPDF.Core.Documents;

namespace KillerPDF.App.ViewModels;

/// <summary>View model for a single page in the document.</summary>
public sealed class PageViewModel : ViewModelBase
{
    private bool _isSelected;
    private double _renderedZoom = 1.0;

    public PageViewModel(PdfPage page, int displayIndex)
    {
        Page = page;
        DisplayIndex = displayIndex;
    }

    public PdfPage Page { get; }

    /// <summary>1-based page number as shown in the UI (mutable: pages can be reordered).</summary>
    public int DisplayIndex { get; set; }

    public bool IsSelected
    {
        get => _isSelected;
        set => Set(ref _isSelected, value);
    }

    public double RenderedZoom
    {
        get => _renderedZoom;
        set => Set(ref _renderedZoom, value);
    }
}
