// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 <YOUR NAME HERE>

namespace KillerPDF.App.Services.Installer;

/// <summary>
/// Upstream ships a single EXE that, on first launch, shows an "Install or
/// Run" dialog. If the user installs, the EXE copies itself to %LOCALAPPDATA%,
/// registers as a PDF handler, drops a Start Menu shortcut, and adds an
/// uninstall entry. This interface isolates that flow so it can be tested
/// and disabled in dev builds.
/// </summary>
public interface IInstallerService
{
    /// <summary>True when the running EXE is not already installed.</summary>
    bool ShouldShowFirstRunDialog();

    /// <summary>Show the install/run dialog and execute the chosen branch.</summary>
    void RunFirstRunFlow();
}
