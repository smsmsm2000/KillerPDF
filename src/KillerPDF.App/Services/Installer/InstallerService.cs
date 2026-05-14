// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 <YOUR NAME HERE>

namespace KillerPDF.App.Services.Installer;

public sealed class InstallerService : IInstallerService
{
    // PORT: upstream's installer code lives in App.xaml.cs. It checks whether
    // the running EXE path is under %LOCALAPPDATA%\KillerPDF, and if not,
    // offers Install / Run / Cancel. Install copies the EXE, writes
    // HKCU\Software\Classes\.pdf and the Applications subkey, drops a
    // Start Menu shortcut, and registers under Uninstall.
    public bool ShouldShowFirstRunDialog()
        => false; // safe default for dev builds

    public void RunFirstRunFlow()
        => throw new NotImplementedException("Port InstallOrRun flow from upstream App.xaml.cs.");
}
