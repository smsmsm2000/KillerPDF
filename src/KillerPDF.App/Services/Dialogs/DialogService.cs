// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 smsmsm2000

using Microsoft.Win32;
using System.Windows;

namespace KillerPDF.App.Services.Dialogs;

public sealed class DialogService : IDialogService
{
    public string? PickOpenFile(string filter)
    {
        var dlg = new OpenFileDialog { Filter = filter, CheckFileExists = true };
        return dlg.ShowDialog() == true ? dlg.FileName : null;
    }

    public string? PickSaveFile(string filter, string suggestedName)
    {
        var dlg = new SaveFileDialog { Filter = filter, FileName = suggestedName };
        return dlg.ShowDialog() == true ? dlg.FileName : null;
    }

    public Task<string?> PromptForPasswordAsync()
    {
        // PORT: upstream has a custom password prompt window — copy that view in,
        // bind it to a small PasswordPromptViewModel, and return its entered value.
        throw new NotImplementedException("Port PasswordPromptWindow from upstream.");
    }

    public bool ConfirmDiscardChanges()
        => MessageBox.Show(
               "You have unsaved changes. Close anyway?",
               "KillerPDF",
               MessageBoxButton.YesNo,
               MessageBoxImage.Warning) == MessageBoxResult.Yes;

    public void ShowError(string title, string message)
        => MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
}
