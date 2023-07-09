// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Escendit.Orleans.Clients.OpenSearch.Common.Tests;

using global::OpenSearch.Net;
using global::Orleans;
using global::Orleans.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Xunit.Categories;

/// <summary>
/// Host Builder Tests.
/// </summary>
public class HostBuilderTests
{
    /// <summary>
    /// .
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestBasicAuthenticationCredentialsWithOptions()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchBasicAuthenticationOptions("test", options =>
            {
                options.Username = "test";
                options.Password = "test";
            })
            .Build();
        Assert.NotNull(host);

        var settings = host.Services.GetOptionsByName<BasicAuthenticationCredentials>("test");
        Assert.NotNull(settings);
    }

    /// <summary>
    /// Test Basic Authentication Credentials With Options Builder.
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestBasicAuthenticationCredentialsWithOptionsBuilder()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchBasicAuthenticationOptions("test", options =>
                options.BindConfiguration("Path"))
            .Build();
        Assert.NotNull(host);

        var credentials = host.Services.GetOptionsByName<BasicAuthenticationCredentials>("test");
        Assert.NotNull(credentials);
        Assert.IsAssignableFrom<BasicAuthenticationCredentials>(credentials);
    }

    /// <summary>
    /// Test Api Key Authentication Credentials With Options.
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestApiKeyAuthenticationCredentialsWithOptions()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchApiKeyAuthenticationOptions("test", options =>
            {
                options.Base64EncodedApiKey = "abc";
            })
            .Build();
        Assert.NotNull(host);

        var credentials = host.Services.GetOptionsByName<ApiKeyAuthenticationOptions>("test");
        Assert.NotNull(credentials);
        Assert.IsAssignableFrom<ApiKeyAuthenticationOptions>(credentials);
        Assert.Equal("abc", credentials.Base64EncodedApiKey);
    }

    /// <summary>
    /// Test Api Key Authentication Credentials With Options Builder.
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestApiKeyAuthenticationCredentialsWithOptionsBuilder()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchApiKeyAuthenticationOptions("test", options =>
                options.BindConfiguration("Path"))
            .Build();
        Assert.NotNull(host);

        var credentials = host.Services.GetOptionsByName<ApiKeyAuthenticationOptions>("test");
        Assert.NotNull(credentials);
        Assert.IsAssignableFrom<ApiKeyAuthenticationOptions>(credentials);
        Assert.NotNull(credentials.Base64EncodedApiKey);
    }

    /// <summary>
    /// Test Add OpenSearch Cloud Connection Pool.
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestAddOpenSearchCloudConnectionPoolWithBasicAuthenticationOptions()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchCloudConnectionPool("test", "test:MGEwYjkyMzItODY0MS00Y2FiLWI4N2ItZDg3M2Y5MDg2YzUwJG9wZW5zZWFyY2guZXNjZW5kaXQubmV0", options =>
            {
                options.Username = "test";
                options.Password = "test";
            })
            .Build();
        Assert.NotNull(host);

        var connectionPool = host.Services.GetRequiredServiceByName<IConnectionPool>("test");
        Assert.NotNull(connectionPool);
        Assert.IsAssignableFrom<CloudConnectionPool>(connectionPool);
    }

    /// <summary>
    /// Test Add OpenSearch Cloud Connection Pool With ApiKeyAuthenticationOptions.
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestAddOpenSearchCloudConnectionPoolWithApiKeyAuthenticationOptions()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchCloudConnectionPool(
                "test",
                "test:MGEwYjkyMzItODY0MS00Y2FiLWI4N2ItZDg3M2Y5MDg2YzUwJG9wZW5zZWFyY2guZXNjZW5kaXQubmV0",
                options =>
                {
                    options.Base64EncodedApiKey = "dGVzdDp0ZXN0";
                })
            .Build();
        Assert.NotNull(host);

        var connectionPool = host.Services.GetRequiredServiceByName<IConnectionPool>("test");
        Assert.NotNull(connectionPool);
        Assert.IsAssignableFrom<CloudConnectionPool>(connectionPool);
    }

    /// <summary>
    /// Test Add OpenSearch Sniffing Connection Pool with Uris.
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestAddOpenSearchSniffingConnectionPoolWithUris()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchSniffingConnectionPool("test", new List<Uri> { new("http://localhost:9200") })
            .Build();
        Assert.NotNull(host);

        var connectionPool = host.Services.GetRequiredServiceByName<IConnectionPool>("test");
        Assert.NotNull(connectionPool);
        Assert.IsAssignableFrom<SniffingConnectionPool>(connectionPool);
    }

    /// <summary>
    /// Test Add OpenSearch Sniffing Connection Pool With ConfigSectionPath.
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestAddOpenSearchSniffingConnectionPoolWithConfigSectionPath()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchSniffingConnectionPool("test", "Uris")
            .Build();
        Assert.NotNull(host);

        var connectionPool = host.Services.GetRequiredServiceByName<IConnectionPool>("test");
        Assert.NotNull(connectionPool);
        Assert.IsAssignableFrom<SniffingConnectionPool>(connectionPool);
    }

    /// <summary>
    /// Test Add OpenSearch Static Connection Pool With Uris.
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestAddOpenSearchStaticConnectionPoolWithUris()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchStaticConnectionPool("test", new List<Uri> { new("http://localhost:9200") })
            .Build();
        Assert.NotNull(host);

        var connectionPool = host.Services.GetRequiredServiceByName<IConnectionPool>("test");
        Assert.NotNull(connectionPool);
        Assert.IsAssignableFrom<StaticConnectionPool>(connectionPool);
    }

    /// <summary>
    /// Test Add OpenSearch Static Connection Pool With Uris.
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestAddOpenSearchStaticConnectionPoolWithConfigSectionPath()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchStaticConnectionPool("test", "Uris")
            .Build();
        Assert.NotNull(host);

        var connectionPool = host.Services.GetRequiredServiceByName<IConnectionPool>("test");
        Assert.NotNull(connectionPool);
        Assert.IsAssignableFrom<StaticConnectionPool>(connectionPool);
    }

    /// <summary>
    /// Test Add OpenSearch Sticky Connection Pool With Uris.
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestAddOpenSearchStickyConnectionPoolWithUris()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchStickyConnectionPool("test", new List<Uri> { new("http://localhost:9200") })
            .Build();
        Assert.NotNull(host);

        var connectionPool = host.Services.GetRequiredServiceByName<IConnectionPool>("test");
        Assert.NotNull(connectionPool);
        Assert.IsAssignableFrom<StickyConnectionPool>(connectionPool);
    }

    /// <summary>
    /// Test Add OpenSearch Sticky Connection Pool With Uris.
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestAddOpenSearchStickyConnectionPoolWithConfigSectionPath()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchStickyConnectionPool("test", "Uris")
            .Build();
        Assert.NotNull(host);

        var connectionPool = host.Services.GetRequiredServiceByName<IConnectionPool>("test");
        Assert.NotNull(connectionPool);
        Assert.IsAssignableFrom<StickyConnectionPool>(connectionPool);
    }

    /// <summary>
    /// Test Add OpenSearch Sticky Connection Pool With Uris.
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestAddOpenSearchSingleNodeConnectionPoolWithUri()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchSingleNodeConnectionPool("test", new Uri("http://localhost:9200"))
            .Build();
        Assert.NotNull(host);

        var connectionPool = host.Services.GetRequiredServiceByName<IConnectionPool>("test");
        Assert.NotNull(connectionPool);
        Assert.IsAssignableFrom<SingleNodeConnectionPool>(connectionPool);
    }

    /// <summary>
    /// Test Add OpenSearch Sticky Connection Pool With Uris.
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestAddOpenSearchSingleNodeConnectionPoolWithConfigSectionPath()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchSingleNodeConnectionPool("test", "Uri")
            .Build();
        Assert.NotNull(host);

        var connectionPool = host.Services.GetRequiredServiceByName<IConnectionPool>("test");
        Assert.NotNull(connectionPool);
        Assert.IsAssignableFrom<SingleNodeConnectionPool>(connectionPool);
    }

    /// <summary>
    /// Test Add OpenSearch Sticky Connection Pool With Uris.
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestAddOpenSearchStickySniffingConnectionPoolWithUris()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchStickySniffingConnectionPool(
                "test",
                new[]
                {
                    new Uri("http://localhost:9200"),
                },
                _ => 10.3f)
            .Build();
        Assert.NotNull(host);

        var connectionPool = host.Services.GetRequiredServiceByName<IConnectionPool>("test");
        Assert.NotNull(connectionPool);
        Assert.IsAssignableFrom<StickySniffingConnectionPool>(connectionPool);
    }

    /// <summary>
    /// Test Add OpenSearch Sticky Connection Pool With Uris.
    /// </summary>
    [Fact]
    [UnitTest]
    public void TestAddOpenSearchStickySniffingConnectionPoolWithConfigSectionPath()
    {
        var host = CreateHostBuilder()
            .AddOpenSearchStickySniffingConnectionPool(
                "test",
                "Uris",
                _ => 1f)
            .Build();
        Assert.NotNull(host);

        var connectionPool = host.Services.GetRequiredServiceByName<IConnectionPool>("test");
        Assert.NotNull(connectionPool);
        Assert.IsAssignableFrom<StickySniffingConnectionPool>(connectionPool);
    }

    private static IHostBuilder CreateHostBuilder()
    {
        return Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration(builder =>
                builder.AddJsonFile($"{Environment.CurrentDirectory}/../../../appsettings.json"))
            .ConfigureServices(services => services
                .TryAddSingleton(typeof(IKeyedServiceCollection<,>), typeof(KeyedServiceCollection<,>)));
    }
}
