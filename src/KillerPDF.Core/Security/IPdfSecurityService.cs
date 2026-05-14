// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 smsmsm2000

namespace KillerPDF.Core.Security;

/// <summary>Handles password-protected PDFs.</summary>
public interface IPdfSecurityService
{
    /// <summary>True if the file at the given path requires a password to open.</summary>
    bool IsEncrypted(string path);

    /// <summary>
    /// Try to decrypt the file with the given password. On success, returns the
    /// path to a decrypted temp copy (caller is responsible for cleanup).
    /// </summary>
    Task<string?> TryDecryptToTempAsync(string path, string password, CancellationToken ct = default);
}
