// <copyright file="KettlebellCalculatorServicesTests.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using FluentAssertions;

using Kettlebell.Calc.Core.Options;
using Kettlebell.Calc.Core.Services;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;

namespace Kettlebell.Calc.Core.Tests;

[TestFixture]
public class KettlebellCalculatorServiceTests
{
    private Mock<ILogger<KettlebellCalculatorService>> mockLogger;
    private Mock<IOptions<KettlebellOptions>> mockOptions;

    [SetUp]
    public void SetUp()
    {
        this.mockLogger = new Mock<ILogger<KettlebellCalculatorService>>();
        this.mockOptions = new Mock<IOptions<KettlebellOptions>>();
        this.mockOptions.Setup(o => o.Value).Returns(new KettlebellOptions
        {
            Plates = new[] { 1.0, 2.0, 3.0 },
        });
    }

    [Test]
    public void Constructor_InitializesFieldsCorrectly()
    {
        var service = new KettlebellCalculatorService(this.mockLogger.Object, this.mockOptions.Object);

        service.PlateCombinations.Should().HaveCount(8);
    }

    [Test]
    public void GeneratePlateCombinations_GeneratesCorrectCombinations()
    {
        var service = new KettlebellCalculatorService(this.mockLogger.Object, this.mockOptions.Object);

        // Check if all combinations are generated
        var expectedCombinations = new List<List<double>>
        {
            new List<double> { 1.0, 2.0, 3.0 },
            new List<double> { 1.0, 2.0 },
            new List<double> { 1.0, 3.0 },
            new List<double> { 2.0, 3.0 },
            new List<double> { 1.0 },
            new List<double> { 2.0 },
            new List<double> { 3.0 },
            new List<double> { },
        };

        var actualCombinations = service.PlateCombinations
            .Select(pc => pc.Plates.Select(p => p.Weight).OrderBy(w => w).ToList())
            .OrderBy(pc => pc.Count)
            .ToList();

        actualCombinations.Should().BeEquivalentTo(expectedCombinations);
    }
}