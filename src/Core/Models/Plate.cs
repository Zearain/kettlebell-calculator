// <copyright file="Plate.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

namespace Kettlebell.Calc.Core.Models;

/// <summary>
/// Represents a plate.
/// </summary>
/// <param name="Index">The index.</param>
/// <param name="Weight">The weight.</param>
public sealed record Plate(int Index, float Weight);