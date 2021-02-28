 <img align="left" width="116" height="116" src="https://raw.githubusercontent.com/jasontaylordev/CleanArchitecture/main/.github/icon.png" />
 
 # Modified Clean Architecture Solution Template

This is a modified copy of https://github.com/jasontaylordev/CleanArchitecture solution template for creating a Single Page App (SPA) with Angular and ASP.NET Core following the principles of Clean Architecture. Create a new project based on this template by clicking the above **Use this template** button.

## Technologies

* [ASP.NET Core 5](https://asp.net)
* [Entity Framework Core 5](https://docs.microsoft.com/en-us/ef/core/)
* [ASP.NET Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity)
* [SQLite](https://www.sqlite.org/index.html)
* [Angular 11](https://angular.io/)
* [ngX Starter Kit for Angular](https://github.com/ngx-rocket/starter-kit)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq) & [Respawn](https://github.com/jbogard/Respawn)
* [Bootstrap](https://getbootstrap.com/)
* [Docker](https://www.docker.com/)

## Getting Started

1. Install the latest [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
2. Install the latest [Node.js LTS](https://nodejs.org/en/)
3. Run `dotnet tool install --global dotnet-ef`
4. Clone this project and `cd` to its root directory
5. Navigate to `src/WebUI/ClientApp` and run `npm install`
6. Navigate to `src/WebUI/ClientApp` and run `npm start` to launch the front end (Angular)
7. Navigate to `src/WebUI` and run `dotnet run` to launch the back end (ASP.NET Core Web API)

### Docker Configuration

In order to get Docker working, you will need to add a temporary SSL cert and mount a volume to hold that cert.
You can find [Microsoft Docs](https://docs.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-5.0) that describe the steps required for Windows, macOS, and Linux.

For Windows:
The following will need to be executed from your terminal to create a cert in PowerShell
`dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\clw_aspnetapp.pfx -p Your_password123`
`dotnet dev-certs https --trust`

NOTE: When using CMD/batch, replace `$env:USERPROFILE` with `%USERPROFILE%`

FOR macOS:
`dotnet dev-certs https -ep ${HOME}/.aspnet/https/clw_aspnetapp.pfx -p Your_password123`
`dotnet dev-certs https --trust`

FOR Linux:
`dotnet dev-certs https -ep ${HOME}/.aspnet/https/clw_aspnetapp.pfx -p Your_password123`

In order to build and run the docker containers, execute `docker-compose -f 'docker-compose.yml' up -d --build` from the root of the solution where you find the docker-compose.yml file.  You can also use "Docker Compose" from Visual Studio for Debugging purposes.
Then open http://localhost:5000 on your browser.

### Database Configuration

Default database engine is set as **SQLite**, but it can be easily switched to **SQL Server** or **InMemory**


The template is configured to use a **SQLite** database by default. This ensures that all users will be able to run the solution without needing to set up additional infrastructure (e.g. SQL Server).

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

#### Known issues

`tests\Application.IntegrationTests\` does not work with SQLite database

#### In-memory database

If you would like to use in-memory database, you will need to update `WebUI/appsettings.json` as follows:

```json
  "UseInMemoryDatabase": true
```

#### SQL Server

If you would like to use SQL Server you need to do following steps:

1. In `src/Infrastructure/DependencyInjection.cs` replace 
`options.UseSqlite` with `options.UseSqlServer`

2. Update `WebUI/appsettings.json` as follows:

```json
  "UseInMemoryDatabase": false,
```

3. Verify that the `DefaultConnection` connection string within `WebUI/appsettings.json` `tests\Application.IntegrationTests\appsettings.json` points to a valid SQL Server instance.

```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CleanArchWebDb;Trusted_Connection=True;MultipleActiveResultSets=true;"
  }
```
4. Remove `Migrations` folder: `src/Infrastructure/Persistence/Migrations/`

5. Execute the following command from the repository root:

```powershell
dotnet ef migrations add "InitialCreate" --project src\Infrastructure --startup-project src\WebUI --output-dir Persistence\Migrations
```

6. Update `docker-compose` - uncomment `db` section:

`docker-compose.yml`
```yaml
  db:
    image: "mcr.microsoft.com/mssql/server"
    environment:
      - "SA_PASSWORD=Your_password123"
      - "ACCEPT_EULA=Y"
```
`docker-compose.override.yml`

```yaml
  db:
    ports:
      - "1433:1433"
```

### Database Migrations

To use `dotnet-ef` for your migrations please add the following flags to your command (values assume you are executing from repository root)

* `--project src/Infrastructure` (optional if in this folder)
* `--startup-project src/WebUI`
* `--output-dir Persistence/Migrations`

For example, to add a new migration from the root folder:

 `dotnet ef migrations add "SampleMigration" --project src\Infrastructure --startup-project src\WebUI --output-dir Persistence\Migrations`

## Overview

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### WebUI

This layer is a single page application based on Angular 10 and ASP.NET Core 5. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.


## Modifications

Major modification compared with original [CleanArchitecture](https://github.com/jasontaylordev/CleanArchitecture)

### Backend

* **SQLite** as default database instead of **InMemory** or **SQL Server**

### ClientApp (SPA)

* Updated to [Angular 11](https://angular.io/)
* Used [ngX Starter Kit for Angular](https://github.com/ngx-rocket/starter-kit) as `WebUI\ClientApp` template
* VSCode settings
  - recommended `extensions.json`
  - `launch.json` for debug and tests

### WebUI (Razor Pages)

* Extracted all [ASP.NET Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity) views to have full control over it
* Changed layout to match this from **ngX Starter Kit**
* Added `libman.json`
* Shared styles `WebUI\ClientApp\src\shared.scss` -> `WebUI\wwwroot\css\site.shared.css`

## License

This project is licensed with the [MIT license](LICENSE).
