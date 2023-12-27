// <copyright file="Program.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Kettlebell.Calc.Core;
using Kettlebell.Calc.Core.Models;
using Kettlebell.Calc.Core.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices((hostContext, services) =>
{
    services.AddKettlebellCalculator(hostContext.Configuration);
});

var app = builder.Build();

using var scope = app.Services.CreateScope();

var calculator = scope.ServiceProvider.GetRequiredService<KettlebellCalculatorService>();

while (RunCalculator(calculator))
{
    Console.WriteLine();
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();

static bool RunCalculator(KettlebellCalculatorService calculator)
{
    Console.Write("Enter the desired weight: ");
    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input) || input.Equals("q", StringComparison.OrdinalIgnoreCase))
    {
        return false;
    }

    if (input.Equals("all", StringComparison.OrdinalIgnoreCase))
    {
        var allResults = calculator.GetAllCombinations().OrderBy(r => r.TotalWeight).ToList();
        Console.WriteLine($"All possible combinations for kettlebell:");

        for (var i = 0; i < allResults.Count; i++)
        {
            var result = allResults[i];
            Console.WriteLine($"Result {i + 1}:");
            Console.WriteLine($"Total weight: {result.TotalWeight} kg");
            Console.WriteLine($"Plates:");
            PrintPlates(result.PlateCombination.Plates);

            Console.WriteLine("---");
        }

        return true;
    }

    if (!float.TryParse(input, out var weight))
    {
        Console.WriteLine("Invalid input.");
        return true;
    }

    var results = calculator.GetCombinationsForWeight(weight);
    if (results.Count == 0)
    {
        Console.WriteLine("No results found.");
        return true;
    }

    if (results.Count == 1 && results[0].TotalWeight == weight)
    {
        var result = results[0];
        Console.WriteLine($"Exact combination for {result.TotalWeight} kg found:");
        PrintPlates(result.PlateCombination.Plates);

        return true;
    }

    Console.WriteLine($"Closest combinations for {weight} kg:");

    for (var i = 0; i < results.Count; i++)
    {
        var result = results[i];
        Console.WriteLine($"Result {i + 1}:");
        Console.WriteLine($"Total weight: {result.TotalWeight} kg");
        Console.WriteLine($"Plates:");
        PrintPlates(result.PlateCombination.Plates);

        Console.WriteLine("---");
    }

    return true;
}

static void PrintPlates(IEnumerable<Plate> plates)
{
    foreach (var plate in plates.OrderBy(p => p.Index))
    {
        Console.WriteLine($"Plate {plate.Index} [{plate.Weight} kg]");
    }
}