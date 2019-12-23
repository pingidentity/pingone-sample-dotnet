# Client Registration ASP.NET Core Sample Guide

This sample demonstrates how to:
 - **register a new user**
 - **update user password by application**
 - **recover a forgotten password**
 
 using PingOne for Customers (Ping14C) [Management API](https://apidocs.pingidentity.com/pingone/customer/v1/api/man) service.
It uses `client_credentials` grant type to obtain an access token, thereby it bypasses the authentication flow steps and calls the `/{environmentId}/as/token` endpoint directly to acquire the token.

# Content 
- [Prerequisites](#prerequisites)
- [Setup & Running](#setup--running)
- [Libraries Used](#packages-used)
- [Developer Notes](#developer-notes)

# Prerequisites
- PingOne for Customers Account.  
If you don’t have an existing one, please register for a Free Trial Account here: https://developer.pingidentity.com
- A Worker application instance.  
Instructions for 
creating one can be found [here](https://apidocs.pingidentity.com/pingone/customer/v1/api/guide/p1_gettingStarted/#Configure-an-application-connection).
- [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) installed.

# Setup & Running
1. Clone this source code: `https://github.com/pingidentity/pingone-customers-sample-dotnet.git`
2. Grab the following application configuration information from the admin console: `EnvironmentId`, `ClientId`, `ClientSecret`.
3. Paste the values into respecitve placeholders in [appsettings.json](./PingOne.AspNetCore.Samples.Registration/appsettings.json) under `PingOne.Management` section
```json
"PingOne": {
    "Management": {
      "AuthBaseUrl": "https://auth.pingone.com",
      "ApiBaseUrl": "https://api.pingone.com",
      "EnvironmentId": "<Environment ID>",
      "ClientId": "<Client ID>", 
      "Secret": "<Client secret>"
    }
  }
```
- `AuthBaseUrl`: *Required*. Authorization and authentication endpoint called to request the access token required to authenticate PingOne API requests.
- `ApiBaseUrl`: *Required*. Primary endpoint for calling PingOne Management API services.
- `EnvironmentId`: *Required*. Your application's Environment ID. You can find this value at your Application's Settings under **Configuration** tab from the admin console( extract `xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx` string that specifies the environment 128-bit universally unique identifier ([UUID](https://tools.ietf.org/html/rfc4122)) right from `https://auth.pingone.com/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/as/authorize` *AUTHORIZATION URL* ). Or from the *Settings* main menu (*ENVIRONMENT ID* variable)
- `ClientId`: *Required*. Your application's client UUID. You can also find this value at Application's Settings right under the Application name.
- `Secret`: *Required*. Your application's client secret known only to the application and the authorization server.
4. Open console/terminal and go to a folder with Registration Sample project: `cd .\pingone-customers-sample-oidc\PingOne.AspNetCore.Samples.Registration`
5. Start an application by `dotnet run` command.
6. Open a browser and navigate to `http://localhost:44377`. 

## Packages Used
- [Microsoft.AspNet.WebApi.Client](https://www.nuget.org/packages/Microsoft.AspNet.WebApi.Client)
- [Microsoft.Extensions.DependencyInjection.Abstractions](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection.Abstractions)

## Developer Notes
- [ManagementApiClient](../pingone-netcore-sdk/PingOne.Core/Management/Services/ManagementApiClient.cs) uses HttpClient instance,  configured with a delegating handler [PingOneApiAuthorizationHeaderHandler](../pingone-netcore-sdk/PingOne.Core/Management/PingOneApiAuthorizationHeaderHandler.cs) that sets authentication header value for each request. The handler uses server-side in-memory storage to reduce the number of calls to `\token` endpoint.
- This sample application is configured to use `44377` port by default. To change default port open [launchSettings.json](./PingOne.AspNetCore.Samples.Oidc/Properties/launchSettings.json) and update the port value for `iisSettings.iisExpress.sslPort` and `profiles.PingOne.AspNetCore.Samples.Oidc.applicationUrl` properties.
