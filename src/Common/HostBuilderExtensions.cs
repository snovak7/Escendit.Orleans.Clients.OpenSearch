// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Microsoft.Extensions.Hosting;

using System.Diagnostics.CodeAnalysis;
using Configuration;
using OpenSearch.Net;
using Orleans.Runtime;

/// <summary>
/// Host Builder Extensions.
/// </summary>
[DynamicallyAccessedMembers(
    DynamicallyAccessedMemberTypes.All)]
public static class HostBuilderExtensions
{
    /// <summary>
    /// Add Open Search Static Connection Pool.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="uris">The uris.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchStaticConnectionPool(
        this IHostBuilder hostBuilder,
        string name,
        IEnumerable<Uri> uris)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(uris);
        return hostBuilder
            .ConfigureServices((_, services) =>
            {
                services
                    .AddSingletonNamedService<IConnectionPool>(name, (_, _) =>
                        new StaticConnectionPool(uris));
            });
    }

    /// <summary>
    /// Add Open Search Static Connection Pool.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchStaticConnectionPool(
        this IHostBuilder hostBuilder,
        string name,
        string configSectionPath)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        return hostBuilder
            .ConfigureServices((context, services) =>
            {
                var uris = context.Configuration.GetValue<IEnumerable<Uri>>(configSectionPath);
                services
                    .AddSingletonNamedService<IConnectionPool>(name, (_, _) =>
                        new StaticConnectionPool(uris));
            });
    }

    /// <summary>
    /// Add Open Search Connection Settings.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="uri">The uri.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchConnectionSettings(
        this IHostBuilder hostBuilder,
        string name,
        Uri uri)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(uri);
        return hostBuilder
            .ConfigureServices((_, services) =>
            {
                services
                    .AddSingletonNamedService<IConnectionConfigurationValues>(name, (_, _) =>
                        new ConnectionConfiguration(uri));
            });
    }

    /// <summary>
    /// Add Open Search Connection Settings.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="poolName">The connection pool name.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchConnectionSettings(
        this IHostBuilder hostBuilder,
        string name,
        string poolName)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(poolName);
        hostBuilder.ConfigureServices((_, services) =>
        {
            services
                .AddSingletonNamedService<IConnectionConfigurationValues>(name, (serviceProvider, _) =>
                {
                    var connectionPool = serviceProvider.GetRequiredServiceByName<IConnectionPool>(poolName);
                    return new ConnectionConfiguration(connectionPool);
                });
        });
        return hostBuilder;
    }

    /// <summary>
    /// Add Open Search Client Low Level Client.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="settingsName">The connection settings name.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchClientCore(
        this IHostBuilder hostBuilder,
        string name,
        string settingsName)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(settingsName);
        hostBuilder
            .ConfigureServices((_, services) =>
            {
                services
                    .AddSingletonNamedService<IOpenSearchLowLevelClient>(name, (serviceProvider, _) =>
                    {
                        var settings =
                            serviceProvider
                                .GetRequiredServiceByName<IConnectionConfigurationValues>(settingsName);
                        return new OpenSearchLowLevelClient(settings);
                    });
            });
        return hostBuilder;
    }
}
