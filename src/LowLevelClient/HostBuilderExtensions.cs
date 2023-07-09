// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Microsoft.Extensions.Hosting;

using System.Diagnostics.CodeAnalysis;
using OpenSearch.Net;
using Orleans.Runtime;

/// <summary>
/// Host Builder Extensions.
/// </summary>
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
public static partial class HostBuilderExtensions
{
    /// <summary>
    /// Add Open Search Low Level Client.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="uri">The uri.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchLowLevelClient(
        this IHostBuilder hostBuilder,
        string name,
        Uri uri)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(uri);
        return hostBuilder
            .AddOpenSearchConnectionSettings(name, uri)
            .ConfigureServices((_, services) =>
            {
                services
                    .AddSingletonNamedService<IOpenSearchLowLevelClient>(name, (serviceProvider, providerName) =>
                    {
                        var settings = serviceProvider
                            .GetRequiredServiceByName<IConnectionConfigurationValues>(providerName);
                        return new OpenSearchLowLevelClient(settings);
                    });
            });
    }

    /// <summary>
    /// Add Open Search Client Low Level Client.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="settingsName">The connection settings name.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchLowLevelClient(
        this IHostBuilder hostBuilder,
        string name,
        string settingsName)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(settingsName);
        return hostBuilder
            .ConfigureServices((_, services) =>
            {
                services
                    .AddSingletonNamedService<IOpenSearchLowLevelClient>(name, (serviceProvider, _) =>
                    {
                        var settings = serviceProvider
                                .GetRequiredServiceByName<IConnectionConfigurationValues>(settingsName);
                        return new OpenSearchLowLevelClient(settings);
                    });
            });
    }
}
