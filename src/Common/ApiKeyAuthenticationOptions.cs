// Copyright (c) Escendit Ltd. All Rights Reserved.
// Licensed under the MIT. See LICENSE.txt file in the solution root for full license information.

namespace Escendit.Orleans.Clients.OpenSearch.Common;

/// <summary>
/// Api Key Authentication Options.
/// </summary>
public class ApiKeyAuthenticationOptions
{
    /// <summary>
    /// Gets or sets the base64 encoded api key.
    /// </summary>
    /// <value>The base64 encoded api key.</value>
    public string? Base64EncodedApiKey { get; set; }
}
