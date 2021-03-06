### This markdown file will explain how the API works. We will go through fundamental method calls and how to work with the API and what to avoid

1. Once we have a docker container and image up and ready, then we can start working with the API. You can get the Docker image up and running by first, copying this entire repo, thereafter copy the filepath where dockerfile.yml and compose files are located. When you have completed these steps open your cmd and type `cd "filepath"` followed by 'Enter' button to direct your cmd to the filepath where dockerfile is located. When you are inside that directory you should type (in cmd) `docker-compose up`. When you have executed these steps, then your database container is up and running and you're ready to start using the API.

2. Now we are ready to run the program. Run the program in your IDE (Visual studio). Since we have configured setup for Swagger there is no need to manually work with localhost URL because Swagger does that for us. So when you have done these steps, your browser should look something like this:

![image](https://github.com/PGBSNH20/spaceparkv2-mazdak-orhan/blob/Dev/Documentation/Swagger.png)

---

## IMPORTANT NOTES BEFORE API USAGE

- Check this file for authentication passwords (User and Admin): [API Keys](https://github.com/PGBSNH20/spaceparkv2-mazdak-orhan/blob/Dev/Source/SpaceparkAPI/appsettings.json)
- The API will only work correctly if you create a Spaceport under the Spaceport section. Otherwise, if we do not do this as a FIRST STEP when using the API, we will get alot of BadRequests or NotFound responses while trying to use the API.

---

## API USAGE

- Start with POSTING a Spaceport, you will need to enter a name (that's it).

![image](https://github.com/PGBSNH20/spaceparkv2-mazdak-orhan/blob/Dev/Documentation/CreateSpaceport.png)

- When you have successfully created a spaceport, then it should be added into the database which we run inside a docker container.
- Next step is to add a traveller and his/hers spaceship that has to be less than 15m in Length. If you need to get the Id for Spaceport, then there is a GET method inside Spaceport section where you can find the ID of ALL listed Spaceports:

![image](https://github.com/PGBSNH20/spaceparkv2-mazdak-orhan/blob/Dev/Documentation/GetAllSpaceports.png)

- Once we know the ID we can add a new Parking into whichever spaceport we want to. But we have to do it by entering the Spaceport ID which you can find on the image above.

![image](https://github.com/PGBSNH20/spaceparkv2-mazdak-orhan/blob/Dev/Documentation/AddParking.png)

- Now lets try to actually END a parking, where the traveller will be charged and a totalsum will be printed to the user. If you want more details of how the End Parking section works check this file (line 148-162): [ParkingsController.cs](https://github.com/PGBSNH20/spaceparkv2-mazdak-orhan/blob/Dev/Source/SpaceparkAPI/Controllers/ParkingsController.cs). Anyways, It should look something like this when the user ends a parking:

![image](https://github.com/PGBSNH20/spaceparkv2-mazdak-orhan/blob/Dev/Documentation/EndParking.png)

- At this point you are ready to try out the other API calls from both Parkings API section and Spaceport API section. Go ahead and try the API.
- **BE AWARE** If you choose to delete a Spaceport, then all historical and active parkings that are connected to that Spaceport will be deleted aswell.

---

## Other information
-  API calls are made from [Starwars Api](https://swapi.dev/) whenever we try to POST a Parking. We will check following steps: 
-  1. If it is a starwars character we are trying to park with
-  2. If we pass the 1st condition above, then we check if this character owns a starship (which we also get from Starwars API above)
-  3. If we pass both conditions above then we check if the selected starship the user wants to park is less than 15m (as we only allow starships below 15m to park).
To get more indepth details you can check ParkingsController.cs file here: [Controller folder (Repo)](https://github.com/PGBSNH20/spaceparkv2-mazdak-orhan/tree/Dev/Source/SpaceparkAPI/Controllers).

---

