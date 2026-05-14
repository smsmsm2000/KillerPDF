// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 smsmsm2000

using System.Windows;

namespace KillerPDF.App.Views;

/// <summary>
/// Thin code-behind. All logic lives in MainViewModel. View-only concerns
/// (focusing a textbox after a command, scrolling a list into view, etc.)
/// can live here, but business logic must not.
///
/// Upstream's 3,574-line MainWindow.xaml.cs is decomposed across
/// ViewModels/, Commands/, and Services/. See docs/REFACTORING_GUIDE.md.
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
}
