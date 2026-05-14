// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 <YOUR NAME HERE>

using KillerPDF.Core.Annotations;
using Xunit;

namespace KillerPDF.Tests;

public sealed class SmokeTests
{
    [Fact]
    public void Annotation_Id_IsAssigned()
    {
        var a = new TextAnnotation();
        Assert.NotEqual(Guid.Empty, a.Id);
    }

    [Fact]
    public void TextAnnotation_HasSensibleDefaults()
    {
        var a = new TextAnnotation();
        Assert.Equal("Segoe UI", a.FontFamily);
        Assert.Equal(12.0, a.FontSize);
        Assert.Equal(0xFF000000u, a.Color);
    }
}
