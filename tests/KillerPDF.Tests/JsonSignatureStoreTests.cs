// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 <YOUR NAME HERE>

using KillerPDF.Core.Signatures;
using Xunit;

namespace KillerPDF.Tests;

public sealed class JsonSignatureStoreTests : IDisposable
{
    private readonly string _dir;

    public JsonSignatureStoreTests()
    {
        _dir = Path.Combine(Path.GetTempPath(), "KillerPDF.Tests-" + Guid.NewGuid());
        Directory.CreateDirectory(_dir);
    }

    [Fact]
    public async Task LoadAll_OnEmptyStore_ReturnsEmpty()
    {
        var store = new JsonSignatureStore(_dir);
        var all = await store.LoadAllAsync();
        Assert.Empty(all);
    }

    [Fact]
    public async Task Save_ThenLoad_RoundTrips()
    {
        var store = new JsonSignatureStore(_dir);
        var sig = new Signature
        {
            Name = "Test",
            PngBytes = new byte[] { 1, 2, 3 },
        };

        await store.SaveAsync(sig);
        var loaded = await store.LoadAllAsync();

        var single = Assert.Single(loaded);
        Assert.Equal("Test", single.Name);
        Assert.Equal(new byte[] { 1, 2, 3 }, single.PngBytes);
    }

    [Fact]
    public async Task Save_OverwritesByIdInsteadOfDuplicating()
    {
        var store = new JsonSignatureStore(_dir);
        var sig = new Signature { Name = "v1" };
        await store.SaveAsync(sig);

        sig.Name = "v2";
        await store.SaveAsync(sig);

        var loaded = await store.LoadAllAsync();
        var single = Assert.Single(loaded);
        Assert.Equal("v2", single.Name);
    }

    [Fact]
    public async Task Delete_RemovesById()
    {
        var store = new JsonSignatureStore(_dir);
        var sig = new Signature { Name = "Doomed" };
        await store.SaveAsync(sig);

        await store.DeleteAsync(sig.Id);

        Assert.Empty(await store.LoadAllAsync());
    }

    public void Dispose()
    {
        try { Directory.Delete(_dir, recursive: true); } catch { /* ignore */ }
    }
}
