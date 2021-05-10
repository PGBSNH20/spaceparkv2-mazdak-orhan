using Moq;
using SpaceParkAPI.Data;
using System;
using Xunit;
using SpaceparkAPI.Models;
using System.Collections.Generic;
using Moq.EntityFrameworkCore;
using SpaceparkAPI.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SpaceTest
{
    public class UnitTest1
    {
        /// <summary>
        ///  TESTING PARKINGCONTROLLER
        /// </summary>

        /*********--Testing ParkingController.GetTravellerActiveParking(string traveller)--********/
        [Fact]
        public async Task When_Having_a_Active_Parking_Expect_Ok()
        {
            //Arrange
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new List<Parking>(){
                new Parking(){ Id = 1, Traveller = "luke skywalker", SpacePortId = 1}
            };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "Stephans kakor" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);

            var parkering = new ParkingsController(myContextMoq.Object);

            //Act
            var result = await parkering.GetTravellerActiveParking("luke skywalker");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetActiveParkings_WithoutAnyParkings_NotFound()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new();
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "Stephans kakor" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);


            var parkering = new ParkingsController(myContextMoq.Object);
            var result = await parkering.GetTravellerActiveParking("luke skywalker");

            Assert.IsType<NotFoundObjectResult>(result);
        }

        /*********--Testing ParkingController.GetTravellerHistoricalParkings(string traveller)--********/
        [Fact]
        public void When_Checking_Luke_Skywalker_EndedParking_Expect_OkObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", EndTime = DateTime.Now }, new Parking { Id = 2, Traveller = "darth vader", SpacePortId = 1, StarShip = "TIE Advanced x1", EndTime = DateTime.Now } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "Stephans kakor" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);


            var parkering = new ParkingsController(myContextMoq.Object);
            var result = parkering.GetTravellerHistoricalParkings("luke skywalker");

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void When_Checking_Darth_Vader_NotEndedParking_Expect_OkObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", EndTime = DateTime.Now }, new Parking { Id = 2, Traveller = "darth vader", SpacePortId = 1, StarShip = "TIE Advanced x1" } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "Stephans kakor" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);


            var parkering = new ParkingsController(myContextMoq.Object);
            var result = parkering.GetTravellerHistoricalParkings("Darth vader");

            Assert.IsType<NotFoundObjectResult>(result);
        }

        /*********--Testing ParkingController.AddParking(string traveller, string starship, int spaceportId)--********/
        [Fact]
        public async Task When_Adding_A_StarWars_Character_To_Parking_Expect_OkObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", EndTime = DateTime.Now }};
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "Stephans kakor" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);


            var parkering = new ParkingsController(myContextMoq.Object);
            var result = await parkering.AddParking(traveller: "Darth vader", starship: "TIE Advanced x1", spaceportId: 1);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task When_Adding_A_NonStarWars_Character_To_Parking_Expect_NotFoundObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", EndTime = DateTime.Now }};
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "Stephans kakor" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);


            var parkering = new ParkingsController(myContextMoq.Object);
            var result = await parkering.AddParking(traveller: "Orhan", starship: "TIE Advanced x1", spaceportId: 1);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        /*********--Testing ParkingController.EndParking(string traveller)--********/
        // Med hjälp av detta test så såg vi att det saknades traveller.ToLower() och därför fick vi inte som Expected till en början. 
        [Fact]
        public async Task When_Ending_Parking_Luke_Skywalker_Expect_OkObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", StartTime = DateTime.Now.AddDays(-1) } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "Stephans kakor" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);


            var parkingMoq = new ParkingsController(myContextMoq.Object);
            var result = await parkingMoq.EndParking(traveller: "Luke Skywalker");

            Assert.IsType<OkObjectResult>(result);
        }       
        
        [Fact]
        public async Task When_Ending_Parking_Darth_Vader_With_No_Added_Parking_Expect_NotFoundObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", StartTime = DateTime.Now.AddDays(-1) } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "Stephans kakor" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);


            var parkingMoq = new ParkingsController(myContextMoq.Object);
            var result = await parkingMoq.EndParking(traveller: "Darth Vader");

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task When_Ending_Parking_Luke_Skywalker_Expect_TotalSum_For_OneDay_1440()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", StartTime = DateTime.Now.AddDays(-1)} };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "Stephans kakor" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);


            var parkingMoq = new ParkingsController(myContextMoq.Object);
            var result = await parkingMoq.EndParking(traveller: "Luke Skywalker");

            Assert.Equal(14400,Math.Round((decimal)parking[0].TotalSum,0));
        }

        [Fact]
        public async Task When_Ending_Parking_Luke_Skywalker_Expect_TotalSum_For_OneHour_600()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", StartTime = DateTime.Now.AddHours(-1) } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "Stephans kakor" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);


            var parkingMoq = new ParkingsController(myContextMoq.Object);
            var result = await parkingMoq.EndParking(traveller: "Luke Skywalker");

            Assert.Equal(600, Math.Round((decimal)parking[0].TotalSum, 0));
        }

        /// <summary>
        ///  TESTING SPACEPORTCONTROLLER
        /// </summary>

        /*********--Testing ParkingController.GetParkingHistoryInSpaceport(int spaceportId)--********/

        [Fact]
        public void When_Checking_ParkingHistoryInSpaceport_Luke_Skywalker_Expect_OkObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", StartTime = DateTime.Now.AddDays(-1), EndTime = DateTime.Now } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "Stephans kakor" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);

            var spaceMoq = new SpaceportController(myContextMoq.Object);
            var result = spaceMoq.GetParkingHistoryInSpaceport(spaceportId: 1);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void When_EndTime_Is_Null_Checking_ParkingHistoryInSpaceport_Expect_NotFoundObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", StartTime = DateTime.Now.AddDays(-1), EndTime = null } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "Stephans kakor" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);

            var spaceMoq = new SpaceportController(myContextMoq.Object);
            var result = spaceMoq.GetParkingHistoryInSpaceport(spaceportId: 1);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        /*********--Testing ParkingController.GetActiveParkingsInSpacePort(int spaceportId)--********/
        [Fact]
        public void When_Checking_An_Active_Parking_Expect_OkObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", StartTime = DateTime.Now.AddDays(-1), EndTime = null } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "Stephans kakor" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);

            var spaceMoq = new SpaceportController(myContextMoq.Object);
            var result = spaceMoq.GetActiveParkingsInSpacePort(spaceportId: 1);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void When_There_Is_No_Active_Parking_Expect_NotFoundObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", StartTime = DateTime.Now.AddDays(-1), EndTime = DateTime.Now } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "Stephans kakor" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);

            var spaceMoq = new SpaceportController(myContextMoq.Object);
            var result = spaceMoq.GetActiveParkingsInSpacePort(spaceportId: 1);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        /*********--Testing ParkingController.GetAllSpacePorts()--********/
        [Fact]
        public void When_There_Is_Spaceports_Expect_OkObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", StartTime = DateTime.Now.AddDays(-1), EndTime = DateTime.Now } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "Stephans kakor"}, new SpacePort() { Id = 2, Name = "StarwarsPort" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);

            var spaceMoq = new SpaceportController(myContextMoq.Object);
            var result = spaceMoq.GetAllSpacePorts();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void When_No_Spaceports_Available_Expect_NotFoundObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", StartTime = DateTime.Now.AddDays(-1), EndTime = DateTime.Now } };
            List<SpacePort> spaceports = new() { };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);

            var spaceMoq = new SpaceportController(myContextMoq.Object);
            var result = spaceMoq.GetAllSpacePorts();

            Assert.IsType<NotFoundObjectResult>(result);
        }

        /*********--Testing ParkingController.GetSpacePortByName(string name)--********/
        [Fact]
        public async Task When_Searching_For_Spaceport_SpaceGarage_Expect_OkObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", StartTime = DateTime.Now.AddDays(-1), EndTime = DateTime.Now } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "SpaceGarage" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);

            var spaceMoq = new SpaceportController(myContextMoq.Object);

            var result = await spaceMoq.GetSpacePortByName(name: "SpaceGarage");

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task When_Searching_For_Spaceport_That_Doesnt_Exist_Expect_NotFoundObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", StartTime = DateTime.Now.AddDays(-1), EndTime = DateTime.Now } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "SpaceGarage" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);

            var spaceMoq = new SpaceportController(myContextMoq.Object);

            var result = await spaceMoq.GetSpacePortByName(name: "Starwarsport");

            Assert.IsType<BadRequestObjectResult>(result);
        }

        /*********--Testing ParkingController.AddNewSpacePort(string name)--********/
        [Fact]
        public async Task When_Adding_A_New_Spaceport_Expect_OkObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", StartTime = DateTime.Now.AddDays(-1), EndTime = DateTime.Now } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "SpaceGarage" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);

            var spaceMoq = new SpaceportController(myContextMoq.Object);

            var result = await spaceMoq.AddNewSpacePort(name: "StarwarsPort");

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task When_Adding_An_Existing_Spaceport_Expect_BadRequestObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", StartTime = DateTime.Now.AddDays(-1), EndTime = DateTime.Now } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "SpaceGarage" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);

            var spaceMoq = new SpaceportController(myContextMoq.Object);

            var result = await spaceMoq.AddNewSpacePort(name: "SpaceGarage");

            Assert.IsType<BadRequestObjectResult>(result);
        }

        /*********--Testing ParkingController.UpdateSpacePort(string name, string newName)--********/
        [Fact]
        public async Task When_Updating_An_Existing_Spaceport_With_NewName_Expect_BadRequestObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", StartTime = DateTime.Now.AddDays(-1), EndTime = DateTime.Now } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "SpaceGarage" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);

            var spaceMoq = new SpaceportController(myContextMoq.Object);

            var result = await spaceMoq.UpdateSpacePort(name: "SpaceGarage", newName: "GarageSpace");

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task When_Updating_An_Existing_Spaceport_With_Same_Name_Expect_BadRequestObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing", StartTime = DateTime.Now.AddDays(-1), EndTime = DateTime.Now } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "SpaceGarage" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);

            var spaceMoq = new SpaceportController(myContextMoq.Object);

            var result = await spaceMoq.UpdateSpacePort(name: "SpaceGarage", newName: "SpaceGarage");

            Assert.IsType<BadRequestObjectResult>(result);
        }

        /*********--Testing ParkingController.DeleteSpaceport(int id)--********/
        [Fact]
        public async Task When_Deleting_Existing_Spaceport_Expect_OkObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing" } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "SpaceGarage" }, new SpacePort() { Id = 2, Name = "GarageWars" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);

            var spaceMoq = new SpaceportController(myContextMoq.Object);

            var result = await spaceMoq.DeleteSpaceport(id: 2);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task When_Deleting_NonExisting_Spaceport_Expect_NotFoundObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing" } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "SpaceGarage" }, new SpacePort() { Id = 2, Name = "GarageWars" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);

            var spaceMoq = new SpaceportController(myContextMoq.Object);

            var result = await spaceMoq.DeleteSpaceport(id: 3);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        /*********--Testing ParkingController.DeleteParking(int id)--********/
        [Fact]
        public async Task When_Deleting_Existing_Parking_Expect_OkObjectresult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing" } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "SpaceGarage" }, new SpacePort() { Id = 2, Name = "GarageWars" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);

            var spaceMoq = new SpaceportController(myContextMoq.Object);

            var result = await spaceMoq.DeleteParking(id: 1);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task When_Deleting_NonExisting_Parking_Expect_NotFoundObjectResult()
        {
            DbContextOptions<SpaceParkContext> dummyOptions = new DbContextOptionsBuilder<SpaceParkContext>().Options;
            var myContextMoq = new Mock<SpaceParkContext>(dummyOptions);

            List<Parking> parking = new() { new Parking { Id = 1, Traveller = "luke skywalker", SpacePortId = 1, StarShip = "X-wing" } };
            List<SpacePort> spaceports = new() { new SpacePort() { Id = 1, Name = "SpaceGarage" }, new SpacePort() { Id = 2, Name = "GarageWars" } };

            myContextMoq.Setup(x => x.Parkings).ReturnsDbSet(parking);
            myContextMoq.Setup(x => x.SpacePorts).ReturnsDbSet(spaceports);

            var spaceMoq = new SpaceportController(myContextMoq.Object);

            var result = await spaceMoq.DeleteParking(id: 2);

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
