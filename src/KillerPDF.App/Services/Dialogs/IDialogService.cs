// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 <YOUR NAME HERE>

namespace KillerPDF.App.Services.Dialogs;

/// <summary>Wraps all WPF dialog primitives so ViewModels stay testable.</summary>
public interface IDialogService
{
    string? PickOpenFile(string filter);
    string? PickSaveFile(string filter, string suggestedName);
    Task<string?> PromptForPasswordAsync();
    bool ConfirmDiscardChanges();
    void ShowError(string title, string message);
}
