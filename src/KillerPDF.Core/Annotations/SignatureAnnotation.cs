// SPDX-License-Identifier: GPL-3.0-only
// KillerPDF — Copyright (C) 2024-2025 Steve "TheKiller" (original)
// Fork modifications — Copyright (C) 2026 <YOUR NAME HERE>

namespace KillerPDF.Core.Annotations;

/// <summary>Placed signature — references a stored signature by id.</summary>
public sealed class SignatureAnnotation : Annotation
{
    public Guid SignatureId { get; set; }
}
