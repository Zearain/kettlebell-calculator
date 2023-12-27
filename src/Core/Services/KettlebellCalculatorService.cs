// <copyright file="KettlebellCalculatorService.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Kettlebell.Calc.Core.Models;
using Kettlebell.Calc.Core.Options;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kettlebell.Calc.Core.Services;

/// <summary>
/// Service for calculating the plates to use on a kettlebell.
/// </summary>
public sealed class KettlebellCalculatorService
{
    private readonly ILogger<KettlebellCalculatorService> logger;
    private readonly KettlebellOptions options;
    private readonly List<Plate> plates;
    private readonly List<PlateCombination> combinations;

    /// <summary>
    /// Initializes a new instance of the <see cref="KettlebellCalculatorService"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="options">The options.</param>
    public KettlebellCalculatorService(ILogger<KettlebellCalculatorService> logger, IOptions<KettlebellOptions> options)
    {
        this.logger = logger;
        this.options = options.Value;

        this.plates = this.options.Plates.Select((p, i) => new Plate(i + 1, p)).ToList();
        this.combinations = GenerateCombinations(this.plates).ToList();
    }

    /// <summary>
    /// Gets the plates.
    /// </summary>
    public IReadOnlyList<Plate> Plates => this.plates.AsReadOnly();

    /// <summary>
    /// Gets the plate combinations.
    /// </summary>
    public IReadOnlyList<PlateCombination> PlateCombinations => this.combinations.AsReadOnly();

    /// <summary>
    /// Gets all possible weights and plate combinations for the kettlebell.
    /// </summary>
    /// <returns>The weights and plate combinations.</returns>
    public IReadOnlyList<KettlebellResult> GetAllCombinations()
    {
        return this.combinations.Select(c => new KettlebellResult(this.options.BaseWeight, c)).ToList().AsReadOnly();
    }

    /// <summary>
    /// Gets the combinations for the given weight.
    /// </summary>
    /// <param name="weight">The weight.</param>
    /// <returns>The combinations.</returns>
    public IReadOnlyList<KettlebellResult> GetCombinationsForWeight(double weight)
    {
        var exactMatch = this.combinations.FirstOrDefault(c => c.TotalWeight + this.options.BaseWeight == weight);
        if (exactMatch is not null)
        {
            return new[] { new KettlebellResult(this.options.BaseWeight, exactMatch) }.AsReadOnly();
        }

        var results = new List<KettlebellResult>();
        var closestLowMatch = this.combinations.Where(c => c.TotalWeight + this.options.BaseWeight < weight).OrderByDescending(c => c.TotalWeight).FirstOrDefault();
        if (closestLowMatch is not null)
        {
            results.Add(new KettlebellResult(this.options.BaseWeight, closestLowMatch));
        }

        var closestHighMatch = this.combinations.Where(c => c.TotalWeight + this.options.BaseWeight > weight).OrderBy(c => c.TotalWeight).FirstOrDefault();
        if (closestHighMatch is not null)
        {
            results.Add(new KettlebellResult(this.options.BaseWeight, closestHighMatch));
        }

        return results.AsReadOnly();
    }

    private static IEnumerable<PlateCombination> GenerateCombinations(IEnumerable<Plate> plates)
    {
        if (!plates.Any())
        {
            yield return new PlateCombination(Enumerable.Empty<Plate>());
        }
        else
        {
            var current = plates.First();
            var remaining = plates.Skip(1);

            foreach (var combination in GenerateCombinations(remaining))
            {
                yield return new PlateCombination(combination.Plates.Append(current));
                yield return combination;
            }
        }
    }
}