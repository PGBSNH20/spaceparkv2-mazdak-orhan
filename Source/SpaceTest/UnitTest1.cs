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
    }
}
