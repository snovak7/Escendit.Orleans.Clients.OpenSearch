// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Escendit.Orleans.Clients.OpenSearch.LowLevelClient.Tests;

using global::OpenSearch.Net;
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
            .AddOpenSearchLowLevelClient("test", new Uri("http://localhost:9200"))
            .Build();
        Assert.NotNull(host);

        var openSearchLowLevelClient = host
            .Services
            .GetRequiredServiceByName<IOpenSearchLowLevelClient>("test");
        Assert.NotNull(openSearchLowLevelClient);
        Assert.IsAssignableFrom<IOpenSearchLowLevelClient>(openSearchLowLevelClient);
        Assert.IsAssignableFrom<OpenSearchLowLevelClient>(openSearchLowLevelClient);
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

        var settings = host.Services.GetServiceByName<IConnectionConfigurationValues>("test");
        Assert.NotNull(settings);
        Assert.IsAssignableFrom<IConnectionConfigurationValues>(settings);
        Assert.IsAssignableFrom<ConnectionConfiguration>(settings);
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
            host.Services.GetServiceByName<IConnectionConfigurationValues>("test"));
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

        var settings = host.Services.GetServiceByName<IConnectionConfigurationValues>("test");
        Assert.NotNull(settings);
        Assert.IsAssignableFrom<IConnectionConfigurationValues>(settings);
        Assert.IsAssignableFrom<ConnectionConfiguration>(settings);
    }

    private static IHostBuilder CreateHostBuilder()
    {
        return Host
            .CreateDefaultBuilder()
            .ConfigureServices(services => services
                .TryAddSingleton(typeof(IKeyedServiceCollection<,>), typeof(KeyedServiceCollection<,>)));
    }
}
