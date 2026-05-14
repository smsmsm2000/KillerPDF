// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 <YOUR NAME HERE>

using System.Text.Json;

namespace KillerPDF.Core.Signatures;

/// <summary>
/// Stores signatures as a single JSON file in the given directory.
/// Fresh implementation in the fork — upstream stored signatures inline in
/// MainWindow.xaml.cs; the fork extracts them so they're testable and the
/// store could later be swapped for SQLite, a cloud backend, etc.
/// </summary>
public sealed class JsonSignatureStore : ISignatureStore
{
    private readonly string _filePath;
    private readonly SemaphoreSlim _gate = new(1, 1);
    private static readonly JsonSerializerOptions s_json = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public JsonSignatureStore(string directory)
    {
        if (string.IsNullOrWhiteSpace(directory))
            throw new ArgumentException("Directory required.", nameof(directory));
        Directory.CreateDirectory(directory);
        _filePath = Path.Combine(directory, "signatures.json");
    }

    public async Task<IReadOnlyList<Signature>> LoadAllAsync(CancellationToken ct = default)
    {
        await _gate.WaitAsync(ct);
        try
        {
            if (!File.Exists(_filePath))
                return Array.Empty<Signature>();

            await using var fs = File.OpenRead(_filePath);
            var list = await JsonSerializer.DeserializeAsync<List<Signature>>(fs, s_json, ct);
            return list ?? new List<Signature>();
        }
        finally
        {
            _gate.Release();
        }
    }

    public async Task SaveAsync(Signature signature, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(signature);

        await _gate.WaitAsync(ct);
        try
        {
            var existing = await LoadUnlockedAsync(ct);
            var idx = existing.FindIndex(s => s.Id == signature.Id);
            if (idx >= 0) existing[idx] = signature;
            else existing.Add(signature);

            await WriteUnlockedAsync(existing, ct);
        }
        finally
        {
            _gate.Release();
        }
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        await _gate.WaitAsync(ct);
        try
        {
            var existing = await LoadUnlockedAsync(ct);
            existing.RemoveAll(s => s.Id == id);
            await WriteUnlockedAsync(existing, ct);
        }
        finally
        {
            _gate.Release();
        }
    }

    private async Task<List<Signature>> LoadUnlockedAsync(CancellationToken ct)
    {
        if (!File.Exists(_filePath)) return new List<Signature>();
        await using var fs = File.OpenRead(_filePath);
        return await JsonSerializer.DeserializeAsync<List<Signature>>(fs, s_json, ct)
               ?? new List<Signature>();
    }

    private async Task WriteUnlockedAsync(List<Signature> items, CancellationToken ct)
    {
        var tmp = _filePath + ".tmp";
        await using (var fs = File.Create(tmp))
        {
            await JsonSerializer.SerializeAsync(fs, items, s_json, ct);
        }
        File.Move(tmp, _filePath, overwrite: true);
    }
}
