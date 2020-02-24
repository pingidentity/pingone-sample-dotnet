# OAuth 2/OIDC (OpenID Connect) Authentication ASP.NET Core Sample Guide

This sample shows how to invoke OpenID Connect/OAuth 2 protocol to:
 - **authenticate an existing user**
 - **show user information** 

using PingOne for Customers (Ping14C) [Authentication](https://apidocs.pingidentity.com/pingone/customer/v1/api/auth/p1-a_Authorize/) service.
The default OAuth 2.0 flow illustrated here is an authorization `code` response type. But for a demonstration purposes you can test `token` and `id_token` types with corresponding [appsettings.json](./PingOne.AspNetCore.Samples.Oidc/appsettings.json) file adjustment. 

# Content 
- [Prerequisites](#prerequisites)
- [Setup & Running](#setup--running)
- [Libraries Used](#packages-used)
- [Developer Notes](#developer-notes)

# Prerequisites

- PingOne for Customers Account.  
If you don’t have an existing one, please register it.
- An OpenID Connect Application.  
Instructions for creating one can be found [here](https://apidocs.pingidentity.com/pingone/customer/v1/api/guide/p1_gettingStarted/#Configure-an-application-connection). 
Also, make sure that it is enabled and access grants (`profile address email openid`) by scopes are properly set.
- At least one user in the same environment as the application (not assigned)
- [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) installed

# Setup & Running
1. Clone this source code: `https://github.com/pingidentity/pingone-customers-sample-dotnet.git`
2. Grab the following application configuration information from the admin console: `EnvironmentId`, `ClientId`, `ClientSecret`.
3. Replace their placeholders in [appsettings.json](./PingOne.AspNetCore.Samples.Oidc/appsettings.json) with respective values in `PingOne.Authentication` section
```json
"PingOne": {
    "Authentication": {
      "AuthBaseUrl": "https://auth.pingone.com",
      "EnvironmentId": "<Environment ID>",
      "ClientId": "<Client ID>",
      "Secret": "<Client secret>",
      "ResponseType": "code",
      "RedirectPath": "/callback",
      "PostSignOffRedirectUrl": "", 
      "Scopes": [
        "openid",
        "profile",
        "email",
        "address"
      ]
    }
  }
```
- `AuthBaseUrl`: *Required*. Authorization and authentication endpoint called to request the access token required to authenticate PingOne API requests.
- `EnvironmentId`: *Required*. Your application's Environment ID. You can find this value at your Application's Settings under **Configuration** tab from the admin console( extract `xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx` string that specifies the environment 128-bit universally unique identifier ([UUID](https://tools.ietf.org/html/rfc4122)) right from `https://auth.pingone.com/xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx/as/authorize` *AUTHORIZATION URL* ). Or from the *Settings* main menu (*ENVIRONMENT ID* variable)
- `ClientId`: *Required*. Your application's client UUID. You can also find this value at Application's Settings right under the Application name.
- `Secret`: *Required*. Your application's client secret known only to the application and the authorization server.
- `ResponseType`: *Required*. The type of credentials returned in the response.
- `RedirectPath`: *Required*. The request path within the application's base path to which the PingOne will redirect the user's browser after authorization has been granted by the user. The middleware will process this request when it arrives. *REDIRECT URLS* values corresponds to this data.
- `PostSignOffRedirectUrl`: *Optional*. The URL to which the browser is redirected after a logout has been performed. *SIGNOFF URLS* values corresponds to this data. 
- `Scopes`:  Array of OIDC or PingOne custom scopes, which you want to request authorization for. [PingOne platform scopes](https://apidocs.pingidentity.com/pingone/customer/v1/api/auth/p1-a_AccessServices/#PingOne-platform-scopes-and-endpoint-operations) are configured under "Access" tab in PingOne Admin Console
4. Open console/terminal and navigate to a folder with OIDC Sample project: `cd .\pingone-customers-sample-oidc\PingOne.AspNetCore.Samples.Oidc`
5. Start an application by `dotnet run` command.
6. Open a browser and navigate to `https://localhost:44377`.

## Packages Used
- [Microsoft.AspNetCore.Authentication.OpenIdConnect](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.OpenIdConnect)
- [Microsoft.Extensions.DependencyInjection.Abstractions](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection.Abstractions)

## Developer Notes
- The solution utilizes [AuthenticationBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authentication.authenticationbuilder?view=aspnetcore-3.0&viewFallbackFrom=aspnetcore-3.1) and configures it to use cookie-based authentication and OpenIdConnect authentication middleware. The configuration takes place at the [AddPingOneAuthentication](../pingone-netcore-sdk/PingOne.Core/Configuration/Extensions/AddAuthenticationExtensions.cs#L24) extension method, which can be updated or used as a reference for a more precise configuration.
- `id_token` verification is embedded and performed by OpenIdConnect authentication middleware using the `nonce` parameter. Validation logic is available in [HandleRemoteAuthenticateAsync()](https://github.com/aspnet/AspNetCore/blob/9a3aacb56af7221bfb29d851ee6b7c883650ddf6/src/Security/Authentication/OpenIdConnect/src/OpenIdConnectHandler.cs#L479) method of the middleware.  
- This sample application is configured to use `44377` port by default. To change default port open [launchSettings.json](./PingOne.AspNetCore.Samples.Oidc/Properties/launchSettings.json) and update the port value for `iisSettings.iisExpress.sslPort` and `profiles.PingOne.AspNetCore.Samples.Oidc.applicationUrl` properties. *REDIRECT URLS* and *SIGNOFF URLS* (if used) should also be updated with new port value.
