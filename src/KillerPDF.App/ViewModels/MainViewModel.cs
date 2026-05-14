// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 smsmsm2000

using System.IO;
using System.Windows.Input;
using KillerPDF.App.Commands;
using KillerPDF.App.Services.Dialogs;
using KillerPDF.App.Services.Printing;
using KillerPDF.Core.Documents;
using KillerPDF.Core.Editing;
using KillerPDF.Core.Merging;
using KillerPDF.Core.Rendering;
using KillerPDF.Core.Search;
using KillerPDF.Core.Signatures;

namespace KillerPDF.App.ViewModels;

/// <summary>
/// The root view model bound to MainWindow. Owns the open document and exposes
/// commands for everything in the toolbar. Replaces the bulk of upstream's
/// 3,574-line MainWindow.xaml.cs by delegating to services.
/// </summary>
public sealed class MainViewModel : ViewModelBase
{
    private readonly IPdfDocumentService _documents;
    private readonly IPdfRenderService _renderer;
    private readonly IPdfEditService _editor;
    private readonly IPdfMergeService _merger;
    private readonly IPdfSearchService _searcher;
    private readonly ISignatureStore _signatures;
    private readonly IDialogService _dialogs;
    private readonly IPrintService _printing;

    private DocumentViewModel? _current;
    private string _statusText = "Ready.";

    public MainViewModel(
        IPdfDocumentService documents,
        IPdfRenderService renderer,
        IPdfEditService editor,
        IPdfMergeService merger,
        IPdfSearchService searcher,
        ISignatureStore signatures,
        IDialogService dialogs,
        IPrintService printing)
    {
        _documents = documents;
        _renderer = renderer;
        _editor = editor;
        _merger = merger;
        _searcher = searcher;
        _signatures = signatures;
        _dialogs = dialogs;
        _printing = printing;

        OpenCommand           = new AsyncRelayCommand(OpenAsync);
        SaveCommand           = new AsyncRelayCommand(SaveAsync, () => Current is not null);
        SaveAsCommand         = new AsyncRelayCommand(SaveAsAsync, () => Current is not null);
        SaveFlattenedCommand  = new AsyncRelayCommand(SaveFlattenedAsync, () => Current is not null);
        CloseCommand          = new RelayCommand(Close, () => Current is not null);
        PrintCommand          = new AsyncRelayCommand(PrintAsync, () => Current is not null);
    }

    public DocumentViewModel? Current
    {
        get => _current;
        private set
        {
            if (Set(ref _current, value))
            {
                Raise(nameof(WindowTitle));
                RelayCommand.RaiseRequery();
            }
        }
    }

    public string StatusText
    {
        get => _statusText;
        set => Set(ref _statusText, value);
    }

    public string WindowTitle =>
        Current is null
            ? "KillerPDF"
            : $"{Path.GetFileName(Current.Document.SourcePath)}{Current.TitleSuffix} — KillerPDF";

    public ICommand OpenCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand SaveAsCommand { get; }
    public ICommand SaveFlattenedCommand { get; }
    public ICommand CloseCommand { get; }
    public ICommand PrintCommand { get; }

    private async Task OpenAsync()
    {
        var path = _dialogs.PickOpenFile("PDF files|*.pdf|All files|*.*");
        if (path is null) return;

        try
        {
            StatusText = $"Opening {Path.GetFileName(path)}…";
            var doc = await _documents.OpenAsync(path, _dialogs.PromptForPasswordAsync);
            Current = new DocumentViewModel(doc);
            StatusText = $"Opened {doc.Pages.Count} page(s).";
        }
        catch (Exception ex)
        {
            _dialogs.ShowError("Could not open file", ex.Message);
            StatusText = "Open failed.";
        }
    }

    private async Task SaveAsync()
    {
        if (Current is null) return;
        await _documents.SaveAsync(Current.Document);
        Current.IsDirty = false;
        Raise(nameof(WindowTitle));
        StatusText = "Saved.";
    }

    private async Task SaveAsAsync()
    {
        if (Current is null) return;
        var path = _dialogs.PickSaveFile("PDF files|*.pdf", suggestedName: Path.GetFileName(Current.Document.SourcePath));
        if (path is null) return;
        await _documents.SaveAsAsync(Current.Document, path);
        StatusText = "Saved as " + Path.GetFileName(path);
    }

    private async Task SaveFlattenedAsync()
    {
        if (Current is null) return;
        var path = _dialogs.PickSaveFile("PDF files|*.pdf", suggestedName: "flattened.pdf");
        if (path is null) return;
        await _documents.SaveFlattenedAsync(Current.Document, path, dpi: 150);
        StatusText = "Flattened copy written.";
    }

    private void Close()
    {
        if (Current is null) return;
        if (Current.IsDirty && !_dialogs.ConfirmDiscardChanges())
            return;

        _documents.Close(Current.Document);
        Current = null;
        StatusText = "Closed.";
    }

    private async Task PrintAsync()
    {
        if (Current is null) return;
        await _printing.PrintAsync(Current.Document, _editor);
        StatusText = "Sent to printer.";
    }
}
