### This markdown file will explain how the API works. We will go through each method call and how to work with the API and what to avoid

1. Once we have a docker container and image up and ready, then we can start working with the API. You can get the Docker image up and running by first, copying this entire repo, thereafter copy the filepath where dockerfile.yml and compose files are located. When you have completed these steps open your cmd and type `cd "filepath"` followed by 'Enter' button to direct your cmd to the filepath where dockerfile is located. When you are inside that directory you should type (in cmd) `docker-compose up`. When you have executed theses steps, then your database container is up and running and you're ready to start using the API.

2. Now we are ready to run the program. Run the program in your IDE (Visual studio). Since we have configured setup for Swagger there is no need to manually work with localhost URL because Swagger does that for us. So when you have done these steps, your browser should look something like this:

![image](https://github.com/PGBSNH20/spaceparkv2-mazdak-orhan/blob/Dev/Documentation/Swagger.png)

---

##IMPORTANT NOTES BEFORE API USAGE

- Check this file for authentication passwords (User and Admin): [API Keys](Source/SpaceparkAPI/appsettings.json)
- The API will only work correctly if you create a Spaceport under the Spaceport section. Otherwise, if we do not do this as a FIRST STEP when using the API, we will get alot of BadRequests or NotFound responses while trying to work with the full API.
- Start with POSTING a Spaceport.
![image](Documentation/CreateSpaceport.png)
- When you have successfully created a spaceport, then it should be posted into the database which we run inside a docker container.
- Next step is to add a traveller and his/hers spaceship that is less than 15m long
- At this point you are ready to try out the other API calls from both Parkings API section and Spaceport API section.
---

3. As you can see, we have two different categories: Parkings and Spaceport. Parkings is used for users who wants to park their vehicle at a spaceport. To be able to do that, we will check and make API calls from [Starwars Api](https://swapi.dev/) to check: 
-  1. If it is a starwars character we are trying to park with
-  2. If we pass the 1st condition above, then we check if this character owns a starship (which we also get from StarWars API above)
-  3. If we pass both conditions above then we check if the selected starship the user wants to park is less than 15m (as we only allow starships below 15m to park).
To get more indepth details you can check ParkingsController.cs file here: [Controller folder (Repo)](https://github.com/PGBSNH20/spaceparkv2-mazdak-orhan/tree/Dev/Source/SpaceparkAPI/Controllers).

4. Within the Parking API the traveller should also be able to get his current active parking or Historical parkings by entering his full name into the field. **Important**: When it comes to the Parkings API we always need to provide a ApiKeyUser, for all methods within the Parkings section we will provide user api key which can be found here: [API Keys](Source/SpaceparkAPI/appsettings.json).
