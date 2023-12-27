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
    public double BaseWeight { get; init; } = 3.4;

    /// <summary>
    /// Gets the weight of the plates.
    /// </summary>
    public double[] Plates { get; init; } = new[]
    {
        2.1,
        2.5,
        2.7,
        2.7,
        2.5,
        2.1,
    };
}