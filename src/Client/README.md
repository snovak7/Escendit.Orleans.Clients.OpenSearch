# NuGet Package: Escendit.Orleans.Clients.OpenSearch.Client

Escendit.Orleans.Clients.OpenSearch.Client is a NuGet package that provides the ability to register
`IOpenSearchClient`. This package is suitable for worker allowing you to easily configure and manage
OpenSearch clients within your Orleans-based projects.

## Installation

To install Escendit.Orleans.Clients.OpenSearch.Client, run the following command in the Package Manager Console:

```powershell
Install-Package Escendit.Orleans.Clients.OpenSearch.Client
```

## Usage

There are several ways to register contracts that can be used in an application:

### Host

#### Register `IConnectionSettingsValues` for High Level Client _and can reference optional pool name_

```csharp
Host
    .CreateDefaultBuilder()
    .AddOpenSearchConnectionSettings("name", ...)
```

#### Register `IOpenSearchClient`

```csharp
Host
    .CreateDefaultBuilder()
    .AddOpenSearchClient("name", ...)
```

#### Consume

```csharp
var connectionOptions = serviceProvider.GetRequiredServiceByName<IOpenSearchClient>("name");
```

## Contributing

If you'd like to contribute to Escendit.Orleans.Clients.OpenSearch,
please fork the repository and make changes as you'd like.
Pull requests are warmly welcome.
