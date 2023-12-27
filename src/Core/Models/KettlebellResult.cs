// <copyright file="KettlebellResult.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Kettlebell.Calc.Core.Models;

/// <summary>
/// Represents the result of a kettlebell calculation.
/// </summary>
/// <param name="BaseWeight">The base weight of the kettlebell.</param>
/// <param name="PlateCombination">The plate combination.</param>
public sealed record KettlebellResult(double BaseWeight, PlateCombination PlateCombination)
{
    /// <summary>
    /// Gets the total weight of the kettlebell.
    /// </summary>
    public double TotalWeight => this.BaseWeight + this.PlateCombination.TotalWeight;
}