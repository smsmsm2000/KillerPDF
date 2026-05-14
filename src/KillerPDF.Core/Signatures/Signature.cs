// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 smsmsm2000

namespace KillerPDF.Core.Signatures;

/// <summary>
/// A reusable signature the user has drawn or imported. The image is stored
/// as a PNG byte array so it round-trips through JSON cleanly.
/// </summary>
public sealed class Signature
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public byte[] PngBytes { get; set; } = Array.Empty<byte>();
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
