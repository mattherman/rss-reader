# rss-reader

This is a simple RSS Reader website built with ASP.NET Core. It uses a SQLite database to track feeds and the status of their items. Authentication uses Google login.

## Development

To build the server (requires .NET SDK 6.0 or greater):

```
dotnet build
```

You will also need to create the database by running:

```
./run-database-migrations.sh
```

This will create a `RssReader.db` file in the root directory.

To run the application:

```
dotnet run
```

The application should be available at http://localhost:7093 and https://localhost:5093.

### Secrets

The application requires a Google app client ID and secret pair to be configured in order to use Google authentication. The configuration key for each is `GoogleAuth:ClientId` and `GoogleAuth:ClientSecret` respectively.

These can either be configured via environment variables manually (replacing colons with `__`, ex. `GoogleAuth__ClientId`) or using the [Secrets Manager](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=linux) provided by the dotnet SDK.

To configure the secrets with Secrets Manager, run the following *from the project directory*:
```
dotnet user-secrets init
dotnet user-secrets set "GoogleAuth:ClientId" "<client_id>"
dotnet user-secrets set "GoogleAuth:ClientSecret" "<client_secret>"
```

ASP.NET will automatically load these values into the configuration and make them available via the `GoogleAuth` configuration options class.

The Secrets Manager is only meant to be used for local development in order to avoid checking them into source control. The values are still stored in plain text on your machine. On Linux, they are located in `~/.microsoft/usersecrets/<user_secrets_id>/secrets.json`
