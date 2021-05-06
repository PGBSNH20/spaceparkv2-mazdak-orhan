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
        /*********--Testins ParkingController.GetTravellerActiveParking()--********/
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

        /*********--Testins ParkingController.GetTravellerHistoricalParkings()--********/
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

        /*********--Testins ParkingController.AddParking()--********/
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

    }
}
