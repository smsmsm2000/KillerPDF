// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 <YOUR NAME HERE>

using System.Collections.ObjectModel;
using KillerPDF.Core.Documents;

namespace KillerPDF.App.ViewModels;

/// <summary>Wraps a <see cref="PdfDocument"/> with UI-friendly observable state.</summary>
public sealed class DocumentViewModel : ViewModelBase
{
    private PageViewModel? _currentPage;
    private double _zoom = 1.0;
    private bool _isDirty;

    public DocumentViewModel(PdfDocument document)
    {
        Document = document;
        Pages = new ObservableCollection<PageViewModel>();
        for (var i = 0; i < document.Pages.Count; i++)
            Pages.Add(new PageViewModel(document.Pages[i], displayIndex: i + 1));

        _currentPage = Pages.FirstOrDefault();
    }

    public PdfDocument Document { get; }
    public ObservableCollection<PageViewModel> Pages { get; }

    public PageViewModel? CurrentPage
    {
        get => _currentPage;
        set => Set(ref _currentPage, value);
    }

    public double Zoom
    {
        get => _zoom;
        set { if (Set(ref _zoom, value) && CurrentPage is not null) CurrentPage.RenderedZoom = value; }
    }

    public bool IsDirty
    {
        get => _isDirty;
        set { if (Set(ref _isDirty, value)) Document.IsDirty = value; }
    }

    public string TitleSuffix => IsDirty ? " *" : string.Empty;
}
