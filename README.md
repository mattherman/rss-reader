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

## Deployment

### Infrastructure

The following should be added to your Nginx server configuration to setup the reverse proxy:

```
location /rss-reader {
        proxy_pass              http://127.0.0.1:5010;
        proxy_http_version      1.1;
        proxy_set_header        Upgrade $http_upgrade;
        proxy_set_header        Connection keep-alive;
        proxy_set_header        Host $host;
        proxy_cache_bypass      $http_upgrade;
        proxy_set_header        X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header        X-Forwarded-Proto $scheme;
}
```

You should also setup a systemd service so that the application is run automatically. You can do this by copying the `rss-reader.service` file to `/etc/systemd/system`. You will need to replace the `GOOGLEAUTH__CLIENTID` and `GOOGLEAUTH__CLIENTSECRET` environment variables in this file after copying it.

Once the service file is in place run the following to start the service:

```
systemctl enable rss-reader.service
systemctl start rss-reader.service
```

You can check the status of the service:

```
systemctl status rss-reader.service
```

Or view the logs:

```
journalctl -fu rss-reader.service
```

### Deploying Changes

Includes Github Actions that will build the site and archive the result.

To deploy the web application, run the following from the server:

```
wget https://github.com/mattherman/rss-reader/releases/latest/download/server.zip
unzip server.zip -d /var/www/rss-reader
systemctl restart rss-reader.service
```

To perform a database migration, run the following from the server:

```
TODO
```
