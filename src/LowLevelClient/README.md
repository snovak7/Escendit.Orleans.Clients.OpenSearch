# NuGet Package: Escendit.Orleans.Clients.OpenSearch.LowLevelClient

Escendit.Orleans.Clients.OpenSearch.LowLevelClient is a NuGet package that provides the ability to register
`IOpenSearchLowLevelClient`. This package is suitable for worker allowing you to easily configure and manage
OpenSearch clients within your Orleans-based projects.

## Installation

To install Escendit.Orleans.Clients.OpenSearch.LowLevelClient, run the following command in the Package Manager Console:

```powershell
Install-Package Escendit.Orleans.Clients.OpenSearch.LowLevelClient
```

## Usage

There are several ways to register contracts that can be used in an application:

### Host

#### Register `IConnectionConfigurationValues` for Low Level Client _and can reference optional pool name_

```csharp
Host
    .CreateDefaultBuilder()
    .AddOpenSearchConnectionSettings("name", ...)
```

#### Register `IOpenSearchLowLevelClient`

```csharp
Host
    .CreateDefaultBuilder()
    .AddOpenSearchLowLevelClient("name", ...)
```

#### Consume

```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByName<IOpenSearchLowLevelClient>("name");
```

## Contributing

If you'd like to contribute to Escendit.Orleans.Clients.OpenSearch,
please fork the repository and make changes as you'd like.
Pull requests are warmly welcome.
