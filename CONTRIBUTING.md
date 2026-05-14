# Contributing

Thanks for thinking about contributing to this fork of KillerPDF. A few
ground rules first, then the practical stuff.

## Ground rules

- **GPL-3.0 is non-negotiable.** Any code you contribute will be released
  under GPL-3.0. Don't paste in code from incompatibly-licensed projects.
- **Credit upstream.** This is a fork of [SteveTheKiller/KillerPDF](https://github.com/SteveTheKiller/KillerPDF).
  If your change is a port from upstream, say so in the commit message
  (e.g. `port: search results highlighting (upstream MainWindow.xaml.cs:1234)`).
- **Don't strip headers.** Every source file carries a copyright line for
  Steve and a modifications line for this fork. Both stay.

## Development setup

Requirements: Windows 10/11 + .NET 8 SDK.

```powershell
git clone https://github.com/smsmsm2000/KillerPDF.git
cd KillerPDF
dotnet restore
dotnet build
dotnet test
```

To run the app from source:

```powershell
dotnet run --project src/KillerPDF.App
```

To produce a single-file EXE for testing:

```powershell
dotnet publish src/KillerPDF.App/KillerPDF.App.csproj -c Release
# Output: src/KillerPDF.App/bin/Release/net8.0-windows/win-x64/publish/KillerPDF.exe
```

## Where to put things

| What you're adding | Where it goes |
|---|---|
| New PDF operation | `KillerPDF.Core` (behind an interface) |
| UI state or command wiring | `KillerPDF.App/ViewModels` |
| WPF dialog or window | `KillerPDF.App/Views` |
| File picker, password prompt, message box | `KillerPDF.App/Services/Dialogs` |
| Unit test | `KillerPDF.Tests`, references `Core` only |

Rule of thumb: **`KillerPDF.Core` must never reference WPF.** If you find
yourself wanting `using System.Windows;` in Core, the logic belongs in App.

## Pull requests

1. Branch from `main`.
2. Keep PRs focused — one feature or fix per PR.
3. Add or update tests for anything in `Core` that has logic worth testing.
4. Run `dotnet format` before committing — the PR check enforces it.
5. Update `CHANGELOG.md` under `[Unreleased] > Fork additions` with a
   one-line summary of what changed.
6. If you're porting from upstream, link the upstream file/line range
   in the PR description.

## Commit message style

Prefix with one of: `port:`, `feat:`, `fix:`, `refactor:`, `test:`,
`docs:`, `ci:`, `chore:`. Keep the first line under 72 characters.

```
port: full-text search via PdfPig (upstream MainWindow.xaml.cs:2100-2240)
feat: dark mode toggle
fix: signature placement off by page padding when zoom != 100%
```

## Reporting bugs

Use the issue templates under "New issue" — they ask the right questions
so we don't have to ping-pong.

## Code of conduct

Be civil. The author of upstream wrote a working PDF editor and gave it
away under GPL; that's the spirit we're building on.
