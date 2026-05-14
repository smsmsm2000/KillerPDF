# Changelog

This file tracks **fork-specific** changes. For the canonical KillerPDF
changelog (upstream history through v1.3.2), see
[the upstream CHANGELOG](https://github.com/SteveTheKiller/KillerPDF/blob/main/CHANGELOG.md).
When you actually create the fork, copy upstream's `CHANGELOG.md` verbatim
into this repo above the divider below — GPL doesn't require it, but it's
good practice and gives readers the full history in one place.

---

## Fork additions

### [Unreleased]

**Modernization**
- Migrated from .NET Framework 4.8 to .NET 8 (Windows). WPF retained.
- Removed `Costura.Fody` + `Fody`. Single-file output now uses .NET 8's
  built-in `PublishSingleFile=true` / `IncludeAllContentForSelfExtract=true`.
- Removed `PolySharp` (no longer needed on .NET 8).
- Removed `Microsoft.NETFramework.ReferenceAssemblies` (no longer needed).
- Bumped `System.Text.Json` to the .NET 8 in-box version.

**Refactoring**
- Split the single project into a solution with three projects:
  `KillerPDF.App` (WPF), `KillerPDF.Core` (pure .NET), `KillerPDF.Tests` (xUnit).
- Decomposed the ~3,574-line `MainWindow.xaml.cs` into `Views/`,
  `ViewModels/`, `Commands/`, and `Services/`. See
  [`docs/REFACTORING_GUIDE.md`](docs/REFACTORING_GUIDE.md) for the migration
  map.
- Introduced `ViewModelBase` and `RelayCommand` for binding plumbing.
- Defined service interfaces (`IPdfDocumentService`, `IPdfRenderService`,
  `IPdfEditService`, `IPdfMergeService`, `IPdfSecurityService`,
  `IPdfSearchService`, `ISignatureStore`, `IInstallerService`,
  `IPrintService`, `IDialogService`) so implementations can be swapped
  and unit-tested.

**Tooling**
- Added `Directory.Build.props` for centralized properties (target
  framework, nullable, lang version, treat-warnings-as-errors on Core).
- Added `.editorconfig` for consistent formatting.
- Added xUnit test project with one smoke test to keep CI honest.
- Added `.github/workflows/ci.yml` — builds and tests on every push and PR.
- Added `.github/workflows/release.yml` — on `v*` tag push, publishes
  single-file EXE + GPL source zip and creates a GitHub Release.

**Removed (from fork tree)**
- `pdf-landing/` — upstream's marketing site, doesn't belong in a code fork.
- `screenshots/` — upstream's screenshots; the new fork should take fresh
  ones if needed once the refactor is complete.

**Preserved verbatim**
- `LICENSE` (GPL-3.0)
- `Resources/kp-icon.ico`
- `app.manifest`
- `build/bundle-source.ps1` (still wired into the publish target for
  GPL source-bundle generation)
