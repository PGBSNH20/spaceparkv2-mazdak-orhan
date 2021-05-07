***Some tools we used for this project***

- Visual Studio 2019

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

**April 30, 2021**

- We started creating our projects in Visual Studio
- Started setting up docker compose to out projects

---

**May 2, 2021**

- Created some objects such as Person, Starship etc. Also created two tables (Parkings and Spaceports) using EntityFrameworkCore with code first convension. 
- Included two controllers (Parkingscontroller and SpaceportController). Each controller represents functionalities a Spacepark really need. Such as adding, deleting or getting history from that specific spaceport or parking.  
- Started with some API methods such as GetActiveParkings(), GetParkingHistory() and also AddNewSpaceport() to get information on who is using that parking and post for posting information on who accually is using that parking and in which Spaceport.  
- Included SWAPI ([StarwarsAPI](https://swapi.dev/)) to our project so that we can fetch and also check a specific Starwars character so that we later can include the Starwars character and its parking information in our API.
- Also gave a limit of 15 meters as max length for a spaceship. Anything below the value 15 is able to park its spaceship. 


---

**May 3, 2021**

- Created some new GET, POST and PUT methods for spaceports and also included some conditions inside  these API methods in order to make them work properly. 
- Started with DELETE method for deleting parking so that unnessary data can be deleted from the existing data. 
-  We used Linq to join the tables and select the right object in order to either delete, update or select the desired data.
-  When a user include, update or delete a desired data for Spaceport, the result will afterwards be implemented in SQL database and displayed in our API.
-  A put method for EndingParking was created to basically end the parking for a specific Starwars character. 

---

**May 4, 2021**

- Created GetTravellerHistoricalParking(string traveller) API method which basically gives information about different Starwars characters who has ended there parking. 
- Besides that we also created etParkingHistoryInSpaceport(int spaceportId) to see all ended parkings in desired spaceport. 
- To see active parkings two methods was created, one was GetTravellerActiveParking(string traveller) to see if that person has an active parking or not. And the second method was GetActiveParkingsInSpacePort(int spaceportId) to see all the active parkings in desired spaceport.
- We looked through our methods to see if we could make our methods asynchronous, which we did in most of our methods.

---

**May 5, 2021**
- Added authentication to our API, where the users need to provide one userApiKey and the admins need to provide adminApiKey. Added middleware to check if the user have provided any key at all, if yes, then we will go through our attribute authentication which will check if the correct ApiKey has been provided (for Parkings there should be userApiKey provided, and for Spaceports there should be adminApiKey provided). If either Middleware or Attribute check would fail then the API will return a BadRequest.
- Added headers section to Swagger where we can take input (in the form of a string "ApiKey).

---

**May 6, 2021**
- Added Mock (Moq) extension to create "copies" of our database for unit testing.
- Added multiple tests (24 to be exact), where we check each API call method. We make 2 tests for each method where we expect 1. Ok responses from the API and 2. BadRequest/NotFound responses.

---

**May 7, 2021**
- Added 2 more tests where we check that the totalsum for a ended parking is correct.
- Also added a API Documentation for API Usage, you can find the link here: [API Documentation](https://github.com/PGBSNH20/spaceparkv2-mazdak-orhan/blob/Dev/Documentation/ApiDocumentation.md)


