// <copyright file="KettlebellOptions.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Kettlebell.Calc.Core.Options;

/// <summary>
/// Options for the kettlebell calculator.
/// </summary>
public sealed class KettlebellOptions
{
    /// <summary>
    /// The name of the section in the configuration file.
    /// </summary>
    public const string SectionName = "Kettlebell";

    /// <summary>
    /// Gets the base weight of the kettlebell.
    /// </summary>
    public float BaseWeight { get; init; } = 3.4f;

    /// <summary>
    /// Gets the weight of the plates.
    /// </summary>
    public float[] Plates { get; init; } = new[]
    {
        2.1f,
        2.5f,
        2.7f,
        2.7f,
        2.5f,
        2.1f,
    };
}