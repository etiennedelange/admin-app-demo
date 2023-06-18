# Admin Application

# Build and Test
1.	Install .NET 5 SDK (Development environment, if not already installed with Visual Studio)
2.	Install NodeJS 12.18.4 or later
3.	Build the C# code
4.	Build the client using ng build or the provided scripts in package.json
5.	Set up a local site in IIS (Create a new app pool and set .NET CLR Version to 'No Managed Code')

# EntityFramework migration commands
###### Remember to select the correct Default project in the Package Manager Console when running commands

### Notes
- All new models can be added to AdminApp.Data.Core unless the model class will be referenced by a WCF project.

## Generate idempotent script
### EF Core
Script-Migration -i / Script-Migration -Idempotent
### EF6
Update-Database -Script -SourceMigration: $InitialDatabase
## Remove previous migration
### EF Core
entityframeworkcore\remove-migration -context adminappdbcontext
## Targeting EntityFramework version
### EF Core
Append **entityframeworkcore\\** to scripts
### EF6
Append **entityframework6\\** to scripts

## Change Log
[CHANGELOG](/CHANGELOG.md)

## Staging/Release Notes
[RELEASE](/RELEASE.md)

### Required contributions
[TODO LIST](/TODO.md)
