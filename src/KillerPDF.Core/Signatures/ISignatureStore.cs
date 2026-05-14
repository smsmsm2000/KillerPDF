// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 smsmsm2000

namespace KillerPDF.Core.Signatures;

/// <summary>Persistent store for reusable signatures.</summary>
public interface ISignatureStore
{
    Task<IReadOnlyList<Signature>> LoadAllAsync(CancellationToken ct = default);
    Task SaveAsync(Signature signature, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
