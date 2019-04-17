# Sample API Project


### Description
**Product:** .NET API endpoints for basic commerce system

#### Project Components
* Creating code
* Testing code
* Documenting code
* Designing Endpoints for Developer Interaction

#### Design considerations


### Dependencies
- **Foundation Framework:** .Net Core 2.2
- **Entity Framework:** EF Core 2.2
- **Unit Testing:** xunit 2.4
- **Integration Testing:** Postman 2.1

### Dev Tools
  - C# - ms-vscode.csharp: C# for Visual Studio Code (powered by OmniSharp).
  - SQL Server (mssql) - ms-mssql.mssql: Develop Microsoft SQL Server, Azure SQL Database and SQL Data Warehouse everywhere
  
### Getting Started

1. Open `sample-api-proj-jk` folder in IDE of choice (I use VScode and haven't tested it in Visual Studio)
2. In terminal of choice, preferably the integrated VSCode terminal, `cd` into `foolapi` subdirectory
 > Note: this is where the main application code exists and it can run on its own 
3. Run `dotnet restore`
4. Run `dotnet build`
5. In terminal, `cd` into parallel subdirectory `foolapi-tests`
 > Note: this is where xunit and the few unit tests exist
6. Run `dotnet restore`
7. Run `dotnet build` - this demonstrates the code and all of its dependencies have properly loaded
8. Run `dotnet test` - this demonstrates that the unit tests still function
9. Open `postman-tests\foolapi-integration-tests.postman_collection.json` by File > Import > Import File

### Discussion



### Remaining TODO
