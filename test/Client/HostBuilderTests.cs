// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Escendit.Orleans.Clients.OpenSearch.Client.Tests;

using global::OpenSearch.Client;
using global::Orleans.Runtime;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Xunit.Categories;

/// <summary>
/// Host Builder Tests.
/// </summary>
public class HostBuilderTests
{
    /// <summary>
    /// Test Simplest Client.
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestSimplestClient()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchClient("test", new Uri("http://localhost:9200"))
            .Build();
        Assert.NotNull(host);

        var openSearchClient = host.Services.GetServiceByName<IOpenSearchClient>("test");
        Assert.NotNull(openSearchClient);
    }

    /// <summary>
    /// Test Add Connection Settings.
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestAddConnectionSettings()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchConnectionSettings("test", new Uri("http://localhost:9200"))
            .Build();
        Assert.NotNull(host);

        var settings = host.Services.GetServiceByName<IConnectionSettingsValues>("test");
        Assert.NotNull(settings);
        Assert.IsAssignableFrom<IConnectionSettingsValues>(settings);
        Assert.IsAssignableFrom<ConnectionSettings>(settings);
    }

    /// <summary>
    /// Test Add Connection Settings With Missing Connection Pool.
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestAddConnectionSettingsWithMissingConnectionPool()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchConnectionSettings("test", "test")
            .Build();
        Assert.NotNull(host);
        Assert.Throws<KeyNotFoundException>(() =>
            host.Services.GetServiceByName<IConnectionSettingsValues>("test"));
    }

    /// <summary>
    /// Test Add Connection Settings With Connection Pool.
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestAddConnectionSettingsWithConnectionPool()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchSingleNodeConnectionPool("test", new Uri("http://localhost:9200"))
            .AddOpenSearchConnectionSettings("test", "test")
            .Build();
        Assert.NotNull(host);

        var settings = host.Services.GetServiceByName<IConnectionSettingsValues>("test");
        Assert.NotNull(settings);
        Assert.IsAssignableFrom<IConnectionSettingsValues>(settings);
        Assert.IsAssignableFrom<ConnectionSettings>(settings);
    }

    private static IHostBuilder CreateHostBuilder()
    {
        return Host
            .CreateDefaultBuilder()
            .ConfigureServices(services => services
                .TryAddSingleton(typeof(IKeyedServiceCollection<,>), typeof(KeyedServiceCollection<,>)));
    }
}
