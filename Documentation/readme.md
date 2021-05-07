***Some tools which we used for this project***

**NuGet packages**
- coverlet.collector
- EntityFrameworkCoreMock.Moq
- Microsoft.AspNetCore.Authentication.Core
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.NET.Test.Sdk
- Microsoft.VisualStudio.Web.CodeGeneration.Design
- Moq.EntityFrameworkCore
- RestSharp
- Swashbuckle.AspNetCore
- xunit
- xunit.runner.visualstudio

**Database**
- Microsoft SQL Server Management Studio

**Other tools**
- Browser, such as Google Chrome to display our API

---

**April 30 2021**

- We started creating our projects in Visual Studio
- Started setting up docker compose to out projects

---

**May 2 2021**

- Created some objects such as Person, Starship etc. Also created two tables (Parkings and Spaceports) using EntityFrameworkCore with code first convension. 
- Started with some API methods such as GetParkings() to see who is using that parking and post for posting information on who accually is using that parking.  
- Included SWAPI ([StarwarsAPI](https://swapi.dev/)) to our project so that we can fetch and also check a specific Starwars character so that we later can include the Starwars character and its parking information in our API.
- Also gave a limit of 15 meters as max length for a spaceship. Anything below the value 15 is able to park its spaceship. 


---

**May 3 2021**

- Created som new GET, POST and PUT methods for spaceports and also included some conditions inside our these API methods in order to make them work properly. 
- Also included a DELETE method, so that we can delete the existing data. 
-  We used Linq to join the tables and select the right object in order to either delete, update or select the desired data.
-  When a user include, update or delete a desired data for Spaceport, the result will afterwards be implemented in SQL database and displayed in our API.
