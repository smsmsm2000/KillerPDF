# Attribution

This fork is built on top of [**KillerPDF**](https://github.com/SteveTheKiller/KillerPDF)
by **Steve "TheKiller"**, released under GPL-3.0.

If you find this fork useful, please also star the upstream repository.
The hard work of writing a working PDF editor from scratch is Steve's.
This fork's contribution is structural: modernization, refactoring, and
tooling around the same core functionality.

## Upstream

| Field | Value |
|---|---|
| Project | KillerPDF |
| Author | Steve "TheKiller" |
| Upstream URL | https://github.com/SteveTheKiller/KillerPDF |
| Upstream version forked | v1.3.2 |
| Upstream license | GPL-3.0 |
| Website | https://killertools.net |

## What this fork changes (high level)

This is a **structural** fork. The user-visible feature set is the same as
upstream v1.3.2; what changed is *how* the code is organized so it's easier
to maintain, test, and reuse:

1. **Target framework**: .NET Framework 4.8 → **.NET 8** (Windows). WPF is
   retained; the app stays Windows-only as upstream intends.
2. **Bundling**: Removed `Costura.Fody` + `Fody` in favor of .NET 8's built-in
   `PublishSingleFile=true` with `IncludeAllContentForSelfExtract=true`.
3. **Project layout**: One project → solution with three projects:
   - `KillerPDF.App` (WPF UI, references Core)
   - `KillerPDF.Core` (pure .NET, no WPF — PDF logic, testable)
   - `KillerPDF.Tests` (xUnit, references Core)
4. **MVVM**: The original ~3,574-line `MainWindow.xaml.cs` is decomposed into
   `ViewModels/`, `Services/`, and `Commands/` with a minimal `ViewModelBase`
   and `RelayCommand`. See `docs/REFACTORING_GUIDE.md` for the migration map.
5. **Tooling**: Adds `.editorconfig`, `Directory.Build.props`, a `.github/`
   CI pipeline (build + test on push, release on tag), and `nullable enable`
   on every project.

## File-by-file mapping (upstream v1.3.2 → fork)

| Upstream file | Status in fork | Notes |
|---|---|---|
| `KillerPDF.sln` | Replaced | Now hosts three projects, not one |
| `KillerPDF.csproj` | Replaced | Split into `KillerPDF.App.csproj` + `KillerPDF.Core.csproj` |
| `App.xaml` / `App.xaml.cs` | **Port required** | Move installer/first-launch logic into `Services/Installer/InstallerService` |
| `MainWindow.xaml` | **Port required** | Keep XAML mostly as-is; bind to `MainViewModel` instead of code-behind |
| `MainWindow.xaml.cs` | **Port required** | The big one. Split per `docs/REFACTORING_GUIDE.md` — should shrink to a few hundred lines of view-glue |
| `EditingTypes.cs` | **Port required** | Move into `KillerPDF.Core/Annotations/` and split into one file per type |
| `AssemblyInfo.cs` | Removed | .NET SDK-style projects generate this |
| `FodyWeavers.xml` / `.xsd` | Removed | Costura/Fody no longer used |
| `Resources/kp-icon.ico` | Preserved | Same icon, same path |
| `app.manifest` | Preserved | Copy as-is |
| `build/bundle-source.ps1` | Preserved | GPL source-bundling target still wired in csproj |
| `release.ps1` | Replaced | Now triggered by `.github/workflows/release.yml` on tag push |
| `LICENSE` | **Preserved verbatim** | GPL-3.0 unchanged |
| `README.md` | Rewritten | Credits upstream prominently, documents fork-specific build |
| `CHANGELOG.md` | Preserved + extended | Original entries kept; fork entries appended below a divider |
| `pdf-landing/` | Removed from fork | Belongs to upstream's marketing site (killertools.net) |
| `screenshots/` | Removed from fork | Upstream's screenshots; take your own if needed |

"Port required" = a stub exists in the fork; you copy the working
implementation from the corresponding upstream file and adapt it into the
new structure. The stubs have `// PORT: ...` comments pointing to the
upstream file and approximate line range.

## License continuity

Per GPL-3.0 §5, every modified source file in this fork carries a notice
indicating it was modified. The combined work is distributed under GPL-3.0,
the same license as upstream. The original copyright line is preserved at
the top of every file that originated upstream.
