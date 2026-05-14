// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 <YOUR NAME HERE>

namespace KillerPDF.Core.Security;

public sealed class PdfSecurityService : IPdfSecurityService
{
    // PORT: upstream's password-prompt loop and temp decrypted-copy logic.
    public bool IsEncrypted(string path)
        => throw new NotImplementedException("Port encryption detection from upstream.");

    public Task<string?> TryDecryptToTempAsync(string path, string password, CancellationToken ct = default)
        => throw new NotImplementedException("Port decrypt-to-temp from upstream.");
}
