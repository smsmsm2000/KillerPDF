# KillerPDF (modernized fork)

[![CI](https://github.com/smsmsm2000/KillerPDF/actions/workflows/ci.yml/badge.svg)](https://github.com/smsmsm2000/KillerPDF/actions/workflows/ci.yml)
[![Release](https://github.com/smsmsm2000/KillerPDF/actions/workflows/release.yml/badge.svg)](https://github.com/smsmsm2000/KillerPDF/actions/workflows/release.yml)
[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![License: GPL v3](https://img.shields.io/badge/License-GPL_v3-blue.svg)](LICENSE)
[![Upstream: KillerPDF v1.3.2](https://img.shields.io/badge/upstream-KillerPDF%20v1.3.2-orange)](https://github.com/SteveTheKiller/KillerPDF)

> A modernized, MVVM-refactored fork of [KillerPDF](https://github.com/SteveTheKiller/KillerPDF)
> by Steve "TheKiller". Same feature set, .NET 8, split into a testable
> `Core` library + WPF `App` shell. GPL-3.0.

This fork exists to make KillerPDF easier to **maintain and extend** for
personal use. It does not change what the app does — it changes how the
code is organized so adding a feature doesn't require touching a 3,500-line
file.

If you just want a working PDF editor for Windows, grab the upstream
release at <https://github.com/SteveTheKiller/KillerPDF/releases/latest>.
This fork is for people who want to hack on the source.

## What's different from upstream

| | Upstream v1.3.2 | This fork |
|---|---|---|
| Target framework | .NET Framework 4.8 | .NET 8 (Windows) |
| Projects | 1 | 3 (`App`, `Core`, `Tests`) |
| Largest source file | `MainWindow.xaml.cs` ~3,574 LOC | Decomposed across ViewModels/Services |
| Bundling | Costura.Fody | Native `PublishSingleFile` |
| Tests | None | xUnit scaffold |
| CI | None | GitHub Actions (build + release on tag) |
| Nullable refs | Enabled | Enabled (and enforced on `Core`) |

User-visible features are identical: render via PDFium, merge/split,
inline text editing, freehand + highlight + text-box annotations,
signatures, search, print-with-flatten, password-protected PDFs,
self-installing single-EXE. See upstream's [README](https://github.com/SteveTheKiller/KillerPDF/blob/main/README.md)
and [CHANGELOG](https://github.com/SteveTheKiller/KillerPDF/blob/main/CHANGELOG.md)
for the canonical feature list.

## Requirements

- Windows 10 or 11 (x64)
- .NET 8 SDK to build
- No runtime install needed for the *published* EXE — single-file publish
  bundles everything.

## Build

```powershell
git clone <your fork URL>
cd KillerPDF
dotnet build
dotnet test
```

## Publish (single-file EXE for distribution)

```powershell
dotnet publish src/KillerPDF.App/KillerPDF.App.csproj `
    -c Release `
    -r win-x64 `
    --self-contained true `
    /p:PublishSingleFile=true `
    /p:IncludeAllContentForSelfExtract=true
```

Output lands in `src/KillerPDF.App/bin/Release/net8.0-windows/win-x64/publish/`.

The csproj also runs `build/bundle-source.ps1` after publish to produce a
GPL-compliant `KillerPDF-<version>-src.zip` alongside the binary. **Keep
this** — distributing a GPL binary without offering source is a license
violation.

## Project layout

```
KillerPDF/
├── src/
│   ├── KillerPDF.App/        ← WPF UI shell (Views, ViewModels, Commands)
│   └── KillerPDF.Core/       ← Pure .NET — PDF logic, no WPF refs
├── tests/
│   └── KillerPDF.Tests/      ← xUnit, references Core only
├── docs/
│   ├── ARCHITECTURE.md       ← How the pieces fit together
│   └── REFACTORING_GUIDE.md  ← Where to put each chunk of the old MainWindow
└── .github/workflows/        ← CI + release pipelines
```

See `docs/ARCHITECTURE.md` for the rationale and `docs/REFACTORING_GUIDE.md`
for the line-range map from upstream `MainWindow.xaml.cs` into the new
structure.

## License

GPL-3.0. Same as upstream. See [LICENSE](LICENSE) and [NOTICE](NOTICE).

If you fork *this* fork, you must also release under GPL-3.0 with source
available. No rebrands, no closed-source derivatives. That's not me being
strict — that's the GPL, and it's what made KillerPDF available to fork
in the first place.

## Credits

- **Steve "TheKiller"** — original author of KillerPDF
  ([github.com/SteveTheKiller](https://github.com/SteveTheKiller),
  [killertools.net](https://killertools.net))
- **smsmsm2000** — this fork's refactoring and modernization

See [ATTRIBUTION.md](ATTRIBUTION.md) for the detailed file-by-file changelog.
