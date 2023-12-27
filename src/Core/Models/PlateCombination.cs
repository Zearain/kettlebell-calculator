// <copyright file="PlateCombination.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Kettlebell.Calc.Core.Models;

/// <summary>
/// Represents a combination of plates.
/// </summary>
/// <param name="Plates">The plates.</param>
public sealed record PlateCombination(IEnumerable<Plate> Plates)
{
    /// <summary>
    /// Gets the total weight of the plates.
    /// </summary>
    public float TotalWeight => this.Plates.Sum(p => p.Weight);
}