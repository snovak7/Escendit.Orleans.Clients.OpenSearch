// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Microsoft.Extensions.Hosting;

using DependencyInjection;
using Escendit.Orleans.Clients.OpenSearch.Common;
using Options;

/// <summary>
/// Host Builder Extensions.
/// </summary>
public static partial class HostBuilderExtensions
{
    /// <summary>
    /// Add OpenSearch Basic Authentication Credentials.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchBasicAuthenticationOptions(
        this IHostBuilder hostBuilder,
        string name,
        Action<BasicAuthenticationOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .AddOpenSearchAuthenticationOptionsInternal(name, configureOptions);
    }

    /// <summary>
    /// Add OpenSearch Basic Authentication Credentials.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchBasicAuthenticationOptions(
        this IHostBuilder hostBuilder,
        string name,
        Action<OptionsBuilder<BasicAuthenticationOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .AddOpenSearchAuthenticationCredentialsInternal(name, configureOptions);
    }

    /// <summary>
    /// Add OpenSearch Authentication Credentials.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchApiKeyAuthenticationOptions(
        this IHostBuilder hostBuilder,
        string name,
        Action<ApiKeyAuthenticationOptions> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .AddOpenSearchAuthenticationOptionsInternal(name, configureOptions);
    }

    /// <summary>
    /// Add OpenSearch Basic Authentication Credentials.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchApiKeyAuthenticationOptions(
        this IHostBuilder hostBuilder,
        string name,
        Action<OptionsBuilder<ApiKeyAuthenticationOptions>> configureOptions)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .AddOpenSearchAuthenticationCredentialsInternal(name, configureOptions);
    }

    /// <summary>
    /// Add OpenSearch Basic Authentication Credentials.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <typeparam name="TAuthenticationOptions">The authentication credentials type.</typeparam>
    /// <returns>The updated host builder.</returns>
    public static IHostBuilder AddOpenSearchAuthenticationCredentials<TAuthenticationOptions>(
        this IHostBuilder hostBuilder,
        string name,
        string configSectionPath)
        where TAuthenticationOptions : class, new()
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        return hostBuilder
            .AddOpenSearchAuthenticationCredentialsInternal<TAuthenticationOptions>(name, configSectionPath);
    }

    /// <summary>
    /// Add OpenSearch Authentication Credentials.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <typeparam name="TAuthenticationCredentials">The authentication credentials type.</typeparam>
    /// <returns>The updated host builder.</returns>
    internal static IHostBuilder AddOpenSearchAuthenticationOptionsInternal<TAuthenticationCredentials>(
        this IHostBuilder hostBuilder,
        string name,
        Action<TAuthenticationCredentials> configureOptions)
        where TAuthenticationCredentials : class, new()
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .ConfigureServices((_, services) =>
            {
                services
                    .AddOptions<TAuthenticationCredentials>(name)
                    .Configure(configureOptions);
            });
    }

    /// <summary>
    /// Add OpenSearch Authentication Credentials.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configureOptions">The configure options.</param>
    /// <typeparam name="TAuthenticationCredentials">The authentication credentials type.</typeparam>
    /// <returns>The updated host builder.</returns>
    internal static IHostBuilder AddOpenSearchAuthenticationCredentialsInternal<TAuthenticationCredentials>(
        this IHostBuilder hostBuilder,
        string name,
        Action<OptionsBuilder<TAuthenticationCredentials>> configureOptions)
        where TAuthenticationCredentials : class, new()
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configureOptions);
        return hostBuilder
            .ConfigureServices((_, services) =>
            {
                configureOptions
                    .Invoke(services
                    .AddOptions<TAuthenticationCredentials>(name));
            });
    }

    /// <summary>
    /// Add OpenSearch Authentication Credentials.
    /// </summary>
    /// <param name="hostBuilder">The initial host builder.</param>
    /// <param name="name">The name.</param>
    /// <param name="configSectionPath">The config section path.</param>
    /// <typeparam name="TAuthenticationCredentials">The authentication credentials type.</typeparam>
    /// <returns>The updated host builder.</returns>
    internal static IHostBuilder AddOpenSearchAuthenticationCredentialsInternal<TAuthenticationCredentials>(
        this IHostBuilder hostBuilder,
        string name,
        string configSectionPath)
        where TAuthenticationCredentials : class, new()
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(configSectionPath);
        return hostBuilder
            .ConfigureServices((_, services) =>
            {
                services
                    .AddOptions<TAuthenticationCredentials>(name)
                    .BindConfiguration(configSectionPath);
            });
    }
}
