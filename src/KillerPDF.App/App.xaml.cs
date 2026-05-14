// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 smsmsm2000

using System.IO;
using System.Windows;
using KillerPDF.App.Services.Dialogs;
using KillerPDF.App.Services.Installer;
using KillerPDF.App.Services.Printing;
using KillerPDF.App.ViewModels;
using KillerPDF.App.Views;
using KillerPDF.Core.Documents;
using KillerPDF.Core.Editing;
using KillerPDF.Core.Merging;
using KillerPDF.Core.Rendering;
using KillerPDF.Core.Search;
using KillerPDF.Core.Signatures;

namespace KillerPDF.App;

/// <summary>
/// Composition root. Builds the object graph by hand (no DI container) — there
/// are few enough services that explicit wiring is clearer than a framework.
/// </summary>
public partial class App : System.Windows.Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // PORT: upstream's "Install or Run" dialog logic lives in App.xaml.cs.
        // Wire it through IInstallerService here, then either continue
        // launching the main window or hand off to the installer.
        var installer = new InstallerService();
        if (installer.ShouldShowFirstRunDialog())
        {
            installer.RunFirstRunFlow();
            // installer.RunFirstRunFlow() either installs and re-launches, or
            // returns and lets the main window open in portable mode.
        }

        var sigDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "KillerPDF");

        var mainVm = new MainViewModel(
            documents:  new PdfDocumentService(),
            renderer:   new PdfRenderService(),
            editor:     new PdfEditService(),
            merger:     new PdfMergeService(),
            searcher:   new PdfSearchService(),
            signatures: new JsonSignatureStore(sigDir),
            dialogs:    new DialogService(),
            printing:   new PrintService());

        var window = new MainWindow { DataContext = mainVm };
        window.Show();
    }
}
