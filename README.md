# Sample API Project

[Description](#description) | [Dependencies](#dependencies) | [Getting Started](#getting-started) | [Discussion](#discussion) | [Remaining TODO](#remaining-todo)


### Description

**Product:** .NET API endpoints for basic commerce system. see these two epics/user stories for my best understanding of the proposed system: 
- https://github.com/juliekdang/sample-api-proj-jk/issues/7
- https://github.com/juliekdang/sample-api-proj-jk/issues/6

#### Project Components

* Creating code
* Testing code
* Documenting code
* Designing Endpoints for Developer Interaction

#### Design considerations

* **Synchronous vs Asynchronous:**
I found lots of examples on line for how to handle synchronous controllers for webapi. I chose .Net CORE for its better performance benchmarks, and being very mixed up in the nodejs world these days, was curious to see what Microsoft offered in the world of asynchronous / non-blocking APIs.

* **Hierarchical vs Flat:**
A hierarchical design for this would have looked like this (e.g.):

Description|URI
--|--
All products for one brand|/api/vx.x/brands/{id}/products
All offers for one product|/api/vx.x/brands/{id}/products/{id}/offers

However, I found this design is rigid for entity management (if/how to handle cascading deletes). Searching and filtering is difficult because it requires storage and understanding of the hierarchy. And it doesn't handle many-to-many relationships and other anomalous relationships that may exist in data that captures real-world operations. This [article](https://softwareengineering.stackexchange.com/questions/274998/nested-rest-urls-and-parent-id-which-is-better-design) gave words to some of the abstract frustrations floating around my mind. And Tim Berners-Lee said: _The only thing you can use an identifier for is to refer to an object. When you are not dereferencing, you should not look at the contents of the URI string to gain other information._

Instead, for the design I opted for a series of flat endpoints structured like this: 

Description|URI
--|--
All Brands|/api/vx.x/brands
One Brands|/api/vx.x/brands/{id}
All Products|/api/vx.x/products
One Product|/api/vx.x/products/{id}
All Offers|/api/vx.x/offers
One Offer|/api/vx.x/offers/{id}
All Orders|/api/vx.x/orders
One Order|/api/vx.x/orders/{id}
All Customers|/api/vx.x/customers
One Customer|/api/vx.x/customers/{id}

Foreign keys (parents) can be retried like this:
http://localhost:5002/api/offers?product=100

**For more details on the URI design take a look at the postman tests**

Each object and collection must have a noun-based endpoint that doesn't depend on prior knowledge of hierarchy. The idea is to keep it flat and uniquely identify each data element, perhaps with a GUID. This is not designed. Also not designed is the handling of relationships between the data. Some ideas: tag the parents and children of each element, e.g., in a HATEOUS format, or by using embedded json, sort of like GraphQL. Tags can also assist with searching and navigating. 

### Dependencies
- **Foundation Framework:** .Net Core 2.2
- **Entity Framework:** EF Core 2.2
- **Unit Testing:** xunit 2.4
- **Integration Testing:** Postman 2.1

### Dev Tools
  - C# - ms-vscode.csharp: C# for Visual Studio Code (powered by OmniSharp).
  - SQL Server (mssql) - ms-mssql.mssql: Develop Microsoft SQL Server, Azure SQL Database and SQL Data Warehouse everywhere
  
### Getting Started

1. Open `sample-api-proj-jk` folder in IDE of choice (I use VSCode and haven't tested it in Visual Studio)
2. Run `db-scripts\3_create_and_populate_brands.sql` in either VSCode using MSSQL or in SQL Management Studio scripting window
 > This creates and populates the Brand Table. 
3. In terminal of choice, preferably the integrated VSCode terminal, `cd` into `foolapi` subdirectory
 > Note: this is where the main application code exists and it can exist independently from the rest of the repo
4. Find `foolapi/appsettings.json` and modify the `DefaultConnectionString` to match your settings and uncomment it out
 > of course comment out the above code pointing to the secret connection string
5. Run `dotnet restore`
6. Run `dotnet build`
7. In terminal, `cd` into parallel subdirectory `foolapi-tests`
 > Note: this is where xunit and the few unit tests exist
8. Run `dotnet restore`
9. Run `dotnet build` - this demonstrates the code and all of its dependencies have properly loaded
10. Run `dotnet test` - this demonstrates that the unit tests still function
11. In terminal, `cd` back into parallel subdirectory `foolapi`
12. Run `dotnet run`
 > Note: this will deploy debug instance to an embedded IIS_express to http://localhost:5002/api/(at least if you use VSCode) 
13. Open `postman-tests\foolapi-integration-tests.postman_collection.json` by File > Import > Import File

### Discussion
(not so good things)
* Did not progress in data mocking, and the unit tests, and documentation via swagger. while the unit tests demonstrate the mechanics, they aren't at all meaningful.
* I did focus on the integration tests using postman as a way to understand the Developer Experience of inputs and outputs. There's still a lot work to do, but most of the integration tests work.
* Went down a rabbit hole, focusing on performance, and didn't build as much as I wnated to 

(good things)
* Returned to my microsoft experience and learned some new things (been focusing on nodejs-express stack)
* Would be interesting to see how this performs at scale, compared to nodejs

### Remaining TODO
* Add Swagger
* Add meaningful unit tests with mocking, where relevant
* Add Models and Controllers for Orders and Customers
* Add Security Best Practices as outlined here: https://github.com/juliekdang/sample-api-proj-jk/issues/10
* Add GET requests that assist with traversing the object model
* Add a search engine - fuzzy searching adds a layer of DX and UX because it enables stemming, tokenizing, synonyms... also it's super fast because it's essentiall a searchable cache
* Add error trapping and validation procedures
* Add sorting and paging functions
* Add tests to work out graceful failures (correct http response code, message in the body)
* Add Version number
* Use repository approach as a way to better decouple the data from the controller
* Need a .gitignore for this projects oops
