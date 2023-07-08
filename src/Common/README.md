# NuGet Package: Escendit.Orleans.Clients.OpenSearch.Common


Escendit.Orleans.Clients.OpenSearch.Common is a NuGet package that provides the ability to register
authentication and connection pool options for OpenSearch clients.

This package is suitable for workers allowing you to easily configure and manage OpenSearch clients
connections within your Orleans-based projects.

## Installation

To install Escendit.Orleans.Clients.OpenSearch.Common, run the following command in the Package Manager Console:

```powershell
Install-Package Escendit.Orleans.Clients.OpenSearch.Common
```

## Usage

Escendit.Orleans.Clients.OpenSearch.Common package is not intended to be used as a standalone package.
It is meant to be used in conjunction with either:
- Escendit.Orleans.Clients.OpenSearch.Client
- Escendit.Orleans.Clients.OpenSearch.LowLevelClient.

There are several ways how to register connection options.

### Host

#### Register Authentication Credentials (_not mandatory_)

```csharp
Host
    .CreateDefaultBuilder()
    .AddOpenSearchAuthenticationCredentials("name", ...)
```

#### Consume

```csharp
var credentials = serviceProvider.GetRequiredServiceByName<BasicAuthenticationCredentials>("name");
```

or 

```csharp
var credentials = serviceProvider.GetRequiredServiceByName<ApiKeyAuthenticationCredentials>("name");
```


#### Register Connection Pool (_not mandatory_)

##### Cloud Connection Pool

```csharp
Host
    .CreateDefaultBuilder()
    .AddOpenSearchCloudConnectionPool("name", "cloudId", ...)
```

##### Sniffing Connection Pool

```csharp
Host
    .CreateDefaultBuilder()
    .AddOpenSearchSniffingConnectionPool("name", ...)
```

##### Static Connection Pool

```csharp
Host
    .CreateDefaultBuilder()
    .AddOpenSearchStaticConnectionPool("name", ...)
```

##### Sticky Connection Pool

```csharp
Host
    .CreateDefaultBuilder()
    .AddOpenSearchStickyConnectionPool("name", ...)
```

##### Single Node Connection Pool

```csharp
Host
    .CreateDefaultBuilder()
    .AddOpenSearchSingleNodeConnectionPool("name", ...)
```

##### Sticky Sniffing Connection Pool

```csharp
Host
    .CreateDefaultBuilder()
    .AddOpenSearchStickySniffingConnectionPool("name", ...)
```

#### Consume 
```csharp
var connectionPool = serviceProvider.GetRequiredServiceByName<IConnectionPool>("name");
```

## Contributing

If you'd like to contribute to Escendit.Orleans.Clients.OpenSearch,
please fork the repository and make changes as you'd like.
Pull requests are warmly welcome.
