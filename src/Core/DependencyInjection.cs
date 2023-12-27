// <copyright file="DependencyInjection.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Kettlebell.Calc.Core.Options;
using Kettlebell.Calc.Core.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kettlebell.Calc.Core;

/// <summary>
/// Dependency injection extensions.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the kettlebell calculator service to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The modified service collection.</returns>
    public static IServiceCollection AddKettlebellCalculator(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<KettlebellOptions>()
            .Configure(options => configuration.GetSection(KettlebellOptions.SectionName));
        services.AddSingleton<KettlebellCalculatorService>();

        return services;
    }
}