// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Microsoft.Extensions.Hosting;

using OpenSearch.Net;
using Orleans.Runtime;

/// <summary>
/// Host Builder Extensions.
/// </summary>
public static partial class HostBuilderExtensions
{
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
}
