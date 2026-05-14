# Refactoring guide: porting upstream `MainWindow.xaml.cs` into the fork

Upstream's `MainWindow.xaml.cs` is ~3,574 lines. This document tells you
where each chunk of that file should land in the new structure. Open
upstream's file alongside this doc and work top to bottom.

The migration is **mechanical**, not creative. You aren't rewriting
Steve's logic — you're moving it to a better address.

## Step 0 — Set yourself up

1. Open both repos side by side (upstream + your fork).
2. Open `src/KillerPDF.App/ViewModels/MainViewModel.cs`. Half of upstream's
   `MainWindow.xaml.cs` ends up here.
3. Search upstream for the strings in the "Look for" column below — those
   are the anchors that tell you where each feature lives in the monolith.

## Migration map

Each row is a feature. The "Look for" column gives you a grep target in
upstream's `MainWindow.xaml.cs`. The "Move to" column tells you which new
file the logic belongs in.

| Feature | Look for (in upstream) | Move to |
|---|---|---|
| Opening a PDF | `OpenFileDialog`, `DocLib.Instance` | `KillerPDF.Core/Documents/PdfDocumentService.OpenAsync` |
| Password-protected PDFs | `PdfReaderException`, `"Password"` prompt | `KillerPDF.Core/Security/PdfSecurityService` + `IDialogService.PromptForPasswordAsync` |
| Page rendering | `GetPageReader`, `GetImage`, `IDocReader` | `KillerPDF.Core/Rendering/PdfRenderService.RenderPageAsync` |
| Zoom preset + scroll-wheel sync | `MouseWheel`, `ZoomLevel`, `ComboBox` zoom binding | `DocumentViewModel.Zoom` (already present) + `MainWindow.xaml` zoom combo binding |
| Page reorder (drag-and-drop) | `DragDrop`, `PreviewMouseLeftButtonDown` on the page list | `KillerPDF.Core/Merging/PdfMergeService.Reorder` + a small drag-drop helper in `Views/` |
| Merge PDFs | "Merge" handler in toolbar/menu | `KillerPDF.Core/Merging/PdfMergeService.MergeAsync` |
| Split / extract pages | "Split" / "Extract" handler | `KillerPDF.Core/Merging/PdfMergeService.SplitAsync` |
| Inline text editing | Font matching loop, `InstalledFontCollection` | `KillerPDF.Core/Editing/PdfEditService.MatchFont` + `Annotations/InlineTextEdit` |
| Text boxes | "AddText" / `TextBox` overlay | `Annotations/TextAnnotation` + a tool in `MainViewModel` |
| Freehand drawing | `InkCanvas` / `Stroke` events | `Annotations/FreehandAnnotation` |
| Highlight overlays | "Highlight" handler, opacity slider | `Annotations/HighlightAnnotation` |
| Signature draw window | Custom signature-pad window | `Views/SignaturePadWindow.xaml` (create new) + writes via `ISignatureStore.SaveAsync` |
| Signature import (PNG/JPG/BMP) | `OpenFileDialog` filter with image types | A `SignatureImporter` helper that produces PNG bytes for `ISignatureStore` |
| Placing a saved signature | Click handler that drops a signature at cursor | `Annotations/SignatureAnnotation` |
| Full-text search | `Find` / `Search` button, result list, highlight | `KillerPDF.Core/Search/PdfSearchService.SearchAsync` |
| Drag-select copy | `SelectionStart` / `SelectionEnd`, clipboard write | Stay in `MainWindow.xaml.cs` view-glue; calls `PdfSearchService` for text extraction |
| Dirty tracking + title bar `*` | `IsDirty`, `Title = …` | `DocumentViewModel.IsDirty` + `MainViewModel.WindowTitle` (already present) |
| Close-file confirmation | "unsaved changes" message box | `IDialogService.ConfirmDiscardChanges` (already present) |
| Close file (Ctrl+W) | `Ctrl+W` keybinding, close handler | `MainViewModel.CloseCommand` (already present) |
| Print with flattening | `PrintDocument`, "OnPrintPage" | `KillerPDF.App/Services/Printing/PrintService.PrintAsync` |
| Save Flattened PDF (150 DPI) | "Flatten", 150 DPI rasterization | `PdfDocumentService.SaveFlattenedAsync` |
| Self-installing EXE | "Install or Run" dialog logic in `App.xaml.cs` | `KillerPDF.App/Services/Installer/InstallerService` |
| PDF file handler registration | `HKCU\Software\Classes\.pdf` writes | Inside `InstallerService`, in a `RegisterFileAssociations` method |
| Start Menu shortcut | `IShellLink` / WSH COM, shortcut creation | Inside `InstallerService`, in `CreateStartMenuShortcut` |
| Self-uninstall | Uninstall-key registration + cleanup | Inside `InstallerService`, in `RegisterUninstallEntry` |
| Temp decrypted copy cleanup | `Path.GetTempFileName()` for decrypted copy | `PdfSecurityService.TryDecryptToTempAsync` returns the path; document tracks it; `PdfDocumentService.Close` deletes it |

## Step-by-step process

For each row above:

1. **Find the code in upstream.** Grep for the "Look for" anchor in
   `MainWindow.xaml.cs`. The relevant method body is usually contiguous.
2. **Copy it into the matching service method** (replace the
   `NotImplementedException`).
3. **Adjust signatures.** If the upstream code referenced
   `this.SomeControl`, that's now a binding — pass the data in as a
   parameter or expose it on the view model instead.
4. **Replace UI calls with `IDialogService`.** `OpenFileDialog`,
   `SaveFileDialog`, `MessageBox.Show` — all of those go through
   `IDialogService` so the service stays testable.
5. **Make it async if it does I/O.** Almost all of upstream's handlers
   are synchronous. Wrap PDFium / file work in `Task.Run` at first; once
   it works, look for places where the libraries already expose async
   methods.
6. **Add a test if the logic is non-trivial.** `KillerPDF.Tests` already
   references `Core`; just add a `.cs` file.

## What you should *not* do

- **Don't rewrite Steve's algorithms** unless you genuinely want to
  change behavior. The point of this refactor is moving code to better
  addresses, not redoing the work.
- **Don't add a DI container yet.** Wait until you have a real reason.
- **Don't strip the upstream copyright headers.** Every file you copy
  logic from should keep the "Copyright (C) 2024-2025 Steve 'TheKiller'"
  line — the per-file headers in this fork already include it.
- **Don't change the license.** GPL-3.0, period.

## After the port

Once everything compiles and the test suite passes:

1. Take fresh screenshots and put them in a new `screenshots/` folder.
2. Bump the fork version (`<Version>` in `src/KillerPDF.App/KillerPDF.App.csproj`).
3. Tag a release: `git tag v2.0.0-fork.0 && git push --tags`. The
   release workflow will build a single-file EXE and the GPL source zip,
   and create a GitHub Release.
