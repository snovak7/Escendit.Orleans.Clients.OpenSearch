// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Microsoft.Extensions.Hosting;

using Configuration;
using Escendit.Orleans.Clients.OpenSearch.Common;
using OpenSearch.Net;
using Orleans;
using Orleans.Runtime;

/// <summary>
/// Host Builder Extensions.
/// </summary>
public static partial class HostBuilderExtensions
{
    /// <summary>
    /// Add OpenSearch Cloud Connection Pool.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="cloudId">The cloud id.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchCloudConnectionPool(
        this IHostBuilder hostBuilder,
        string name,
        string cloudId,
        Action<BasicAuthenticationOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        return hostBuilder
            .AddOpenSearchAuthenticationOptionsInternal(name, configureOptions)
            .ConfigureServices((_, services) =>
            {
                services
                    .AddSingletonNamedService<IConnectionPool>(name, (serviceProvider, providerName) =>
                    {
                        var options = serviceProvider.GetOptionsByName<BasicAuthenticationOptions>(providerName);
                        var credentials = new BasicAuthenticationCredentials(options.Username, options.Password);
                        return new CloudConnectionPool(cloudId, credentials);
                    });
            });
    }

    /// <summary>
    /// Add OpenSearch Cloud Connection Pool.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="cloudId">The cloud id.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchCloudConnectionPool(
        this IHostBuilder hostBuilder,
        string name,
        string cloudId,
        Action<ApiKeyAuthenticationOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        return hostBuilder
            .AddOpenSearchAuthenticationOptionsInternal(name, configureOptions)
            .ConfigureServices((_, services) =>
            {
                services
                    .AddSingletonNamedService<IConnectionPool>(name, (serviceProvider, providerName) =>
                    {
                        var options = serviceProvider.GetOptionsByName<ApiKeyAuthenticationOptions>(providerName);
                        var credentials = new ApiKeyAuthenticationCredentials(options.Base64EncodedApiKey);
                        return new CloudConnectionPool(cloudId, credentials);
                    });
            });
    }

    /// <summary>
    /// Add OpenSearch Static Connection Pool.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="uris">The uris.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchSniffingConnectionPool(
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
                        new SniffingConnectionPool(uris));
            });
    }

    /// <summary>
    /// Add OpenSearch Sniffing Connection Pool.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchSniffingConnectionPool(
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
                var uris = context
                    .Configuration
                    .GetSection(configSectionPath)
                    .GetChildren()
                    .Select(s => s.Get<Uri>());

                services
                    .AddSingletonNamedService<IConnectionPool>(name, (_, _) =>
                        new SniffingConnectionPool(uris));
            });
    }

    /// <summary>
    /// Add OpenSearch Static Connection Pool.
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
    /// Add OpenSearch Static Connection Pool.
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
                var uris = context
                    .Configuration
                    .GetRequiredSection(configSectionPath)
                    .GetChildren()
                    .Select(s => s.Get<Uri>());

                services
                    .AddSingletonNamedService<IConnectionPool>(name, (_, _) =>
                        new StaticConnectionPool(uris));
            });
    }

    /// <summary>
    /// Add OpenSearch Sticky Connection Pool.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="uris">The uris.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchStickyConnectionPool(
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
                        new StickyConnectionPool(uris));
            });
    }

    /// <summary>
    /// Add OpenSearch Static Connection Pool.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchStickyConnectionPool(
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
                var uris = context
                    .Configuration
                    .GetRequiredSection(configSectionPath)
                    .GetChildren()
                    .Select(s => s.Get<Uri>());

                services
                    .AddSingletonNamedService<IConnectionPool>(name, (_, _) =>
                        new StickyConnectionPool(uris));
            });
    }

    /// <summary>
    /// Add OpenSearch Single Node Connection Pool.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="uri">The uri.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchSingleNodeConnectionPool(
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
                    .AddSingletonNamedService<IConnectionPool>(name, (_, _) =>
                        new SingleNodeConnectionPool(uri));
            });
    }

    /// <summary>
    /// Add OpenSearch Static Connection Pool.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchSingleNodeConnectionPool(
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
                var uri = context
                    .Configuration
                    .GetRequiredSection(configSectionPath)
                    .Get<Uri>();

                services
                    .AddSingletonNamedService<IConnectionPool>(name, (_, _) =>
                        new SingleNodeConnectionPool(uri));
            });
    }

    /// <summary>
    /// Add OpenSearch Sticky Sniffing Connection Pool.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="uris">The uris.</param>
    /// <param name="nodeScorer">The node scorer.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchStickySniffingConnectionPool(
        this IHostBuilder hostBuilder,
        string name,
        IEnumerable<Uri> uris,
        Func<Node, float> nodeScorer)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(uris);
        ArgumentNullException.ThrowIfNull(nodeScorer);
        return hostBuilder
            .ConfigureServices((_, services) =>
            {
                services
                    .AddSingletonNamedService<IConnectionPool>(name, (_, _) =>
                        new StickySniffingConnectionPool(uris, nodeScorer));
            });
    }

    /// <summary>
    /// Add OpenSearch Sticky Sniffing Connection Pool.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <param name="nodeScorer">The node scorer.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchStickySniffingConnectionPool(
        this IHostBuilder hostBuilder,
        string name,
        string configSectionPath,
        Func<Node, float> nodeScorer)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        ArgumentNullException.ThrowIfNull(nodeScorer);
        return hostBuilder
            .ConfigureServices((context, services) =>
            {
                var uris = context
                    .Configuration
                    .GetRequiredSection(configSectionPath)
                    .GetChildren()
                    .Select(s => s.Get<Uri>());

                services
                    .AddSingletonNamedService<IConnectionPool>(name, (_, _) =>
                        new StickySniffingConnectionPool(uris, nodeScorer));
            });
    }
}
