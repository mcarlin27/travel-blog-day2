using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TravelBlog.Controllers;
using TravelBlog.Models;
using Xunit;
using Moq;
using System.Linq;

namespace TravelBlog.Tests
{
    public class LocationControllerTest : IDisposable
    {
        Mock<ILocationRepository> mock = new Mock<ILocationRepository>();
        private void DbSetup()
        {
            mock.Setup(m => m.Locations).Returns(new Location[]
           {
                new Location {LocationId = 1, Name = "Paris" },
                new Location {LocationId = 2, Name = "London" },
                new Location {LocationId = 3, Name = "New York City" },
           }.AsQueryable());
        }

        EFLocationRepository db = new EFLocationRepository(new TestDbContext());

        [Fact]
        public void Mock_GetViewResultIndex_Test() //Confirms route returns view
        {
            //Arrange
            DbSetup();
            LocationController controller = new LocationController(mock.Object);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public void Mock_ModelListLocationIndex_Test() //confirms model as list of locations
        {
            //Arrange
            DbSetup();
            ViewResult indexView = new LocationController(mock.Object).Index() as ViewResult;
            //Act
            var result = indexView.ViewData.Model;
            //Assert
            Assert.IsType<List<Location>>(result);
        }
        [Fact]
        public void Mock_ConfirmEntry_Test() //Confirms presence of known entry
        {
            // Arrange
            DbSetup();
            LocationController controller = new LocationController(mock.Object);
            Location testLocation = new Location();
            testLocation.Name = "Paris";
            testLocation.LocationId = 1;

            // Act
            ViewResult indexView = controller.Index() as ViewResult;
            var collection = indexView.ViewData.Model as IEnumerable<Location>;

            // Assert
            Assert.Contains<Location>(testLocation, collection);
        }
        [Fact]
        public void Post_MethodAddsLocation_Test()
        {
            //Arrange
            LocationController controller = new LocationController();
            Location testLocation = new Location();
            testLocation.Name = "Paris";
            //Act
            controller.Create(testLocation);
            ViewResult indexView = new LocationController().Index() as ViewResult;
            var collection = indexView.ViewData.Model as IEnumerable<Location>;
            //Assert
            Assert.Contains<Location>(testLocation, collection);
        }
        [Fact]
        public void DB_CreateNewEntry_Test()
        {
            // Arrange
            LocationController controller = new LocationController(db);
            Location testLocation = new Location();
            testLocation.Name = "TestDb Location";

            // Act
            controller.Create(testLocation);
            var collection = (controller.Index() as ViewResult).ViewData.Model as IEnumerable<Location>;

            // Assert
            Assert.Contains<Location>(testLocation, collection);
        }
        public void Dispose()
        {
            db.DeleteAll();
        }
    }
}
