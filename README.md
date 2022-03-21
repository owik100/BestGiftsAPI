# BestGiftsAPI
API project created in .NET 5. Used by [Best Gifts Angular app](https://github.com/owik100/best-gifts).<br/>
Swagger included.  <img src="https://avatars.githubusercontent.com/u/7658037?s=200&v=4" width="32" height="32">

With API you can:
 - Search and browse for gift ideas
 - Add new ideas
 - Vote for best and worst ideas
 - Comment on ideas
 - Checking API Server status (Online/Offline)

# Configuration
1. Add connection string in **appsettings.json** to DefaultConnection
```
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BestGiftsLocal;Trusted_Connection=True;"
  }

```
2. Create Migration:    
Open the Package Manager Console from the menu Tools -> NuGet Package Manager -> Package Manager Console in Visual Studio and execute the following command to add a migration.
```
add-migration InitMigration
```
 If you are using dotnet Command Line Interface, execute the following command.
```
dotnet ef migrations add InitMigration
```
3. Creating or Updating the Database:  
Use the following command to create or update the database schema.
 ```
Update-Database
```
Or in dotnet Command Line Interface
```
dotnet ef database update
```
