// <copyright file="Program.cs" company="Zearain">
// Copyright (c) Zearain. All rights reserved.
// </copyright>

using Kettlebell.Calc.App;
using Kettlebell.Calc.Core;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddKettlebellCalculator(builder.Configuration);

await builder.Build().RunAsync();