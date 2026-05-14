# Architecture

The fork's defining choice is a hard split between **UI** and **logic**.

```
┌────────────────────────────────────────────────────────┐
│  KillerPDF.App  (net8.0-windows, WPF)                  │
│  ─────────────────────────────────────────────────     │
│   Views/        XAML + minimal code-behind             │
│   ViewModels/   ObservableObjects, ICommands           │
│   Commands/     RelayCommand, AsyncRelayCommand        │
│   Services/                                            │
│     Dialogs/    IDialogService (OpenFile, password)    │
│     Installer/  IInstallerService (first-run flow)     │
│     Printing/   IPrintService                          │
└─────────────────────┬──────────────────────────────────┘
                      │ references
                      ▼
┌────────────────────────────────────────────────────────┐
│  KillerPDF.Core  (net8.0, NO WPF refs)                 │
│  ─────────────────────────────────────────────────     │
│   Documents/    PdfDocument, IPdfDocumentService       │
│   Rendering/    IPdfRenderService                      │
│   Annotations/  TextAnnotation, FreehandAnnotation, …  │
│   Editing/      IPdfEditService (bake annotations)     │
│   Merging/      IPdfMergeService (merge/split/reorder) │
│   Security/     IPdfSecurityService (passwords)        │
│   Signatures/   ISignatureStore + JsonSignatureStore   │
│   Search/       IPdfSearchService (full-text)          │
└────────────────────────────────────────────────────────┘
                      ▲
                      │ references
┌─────────────────────┴──────────────────────────────────┐
│  KillerPDF.Tests  (net8.0, xUnit)                      │
└────────────────────────────────────────────────────────┘
```

## Why a Core library?

Upstream is a single project where every PDF operation is a method on
`MainWindow`. That works, but it means:

- The PDF logic can't be unit-tested without spinning up WPF.
- The same logic can't be reused from a CLI, a service, or a future
  Avalonia/MAUI port.
- Adding a feature usually means adding ~50 lines to `MainWindow.xaml.cs`
  and hoping you didn't break anything else.

Putting the PDF logic in a separate library that doesn't reference WPF
forces a clean API. Anything in `Core` can be tested with xUnit, and the
test project doesn't need any UI assemblies.

## Why MVVM?

WPF's data-binding works best when the bound object is a plain CLR object
that raises `PropertyChanged`. Once `MainViewModel` owns the state and
exposes `ICommand` properties, the XAML stops needing `Click="…"` handlers
and the code-behind drops to almost nothing. That's the bulk of the size
reduction from upstream's 3,574-line `MainWindow.xaml.cs`.

## Why no DI container?

There are ~10 services. They are constructed in exactly one place
(`App.OnStartup`). A DI container would add a dependency and a layer of
indirection without removing any lines. If the service count grows,
swap to Microsoft.Extensions.DependencyInjection — the constructors are
already shaped for it.

## Threading model

- **UI thread**: WPF dispatcher. ViewModels live here; their property
  setters and command handlers run on the dispatcher.
- **Background**: anything that touches PDFium or does disk I/O.
  `AsyncRelayCommand` `await`s the background task on the UI thread, so
  callbacks land back on the dispatcher automatically.

Don't reach into PDFium handles from the UI thread. Don't update
ViewModels from background threads. The `await` boundary handles both.

## What stays the same as upstream

- The set of features (open, annotate, merge, split, sign, search, print,
  flatten, password support, self-install).
- The dependency on PDFium via Docnet.Core.
- GPL-3.0 licensing.
- The single-EXE distribution model (now via .NET 8's native single-file
  publish instead of Costura).
