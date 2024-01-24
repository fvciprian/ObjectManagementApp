using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ObjectManagementApp.Controllers;
using ObjectManagementApp.Models;
using ObjectManagementApp.Services.Interfaces;

namespace ObjectManagementAppTest
{
    public class CustomObjectsControllerTests
    {
        private Mock<IObjectService> _objectServiceMock = new();
        private Mock<ILogger<CustomObjectsController>> _loggerMock = new();

        [Fact]
        public void TestIndexReturnsView()
        {
            // Arrange
            var objectsList = new List<CustomObject>
            {
                new() { Id = 1, Name = "Test" }
            };
            _objectServiceMock
                .Setup(x => x.SearchAsync(It.IsAny<string>()))
                .ReturnsAsync(objectsList);

            // Act
            var result = new CustomObjectsController(_objectServiceMock.Object, _loggerMock.Object).Index("test");

            // Assert
            Assert.NotNull(result);
            var viewResult = result.Result as ViewResult;
            var model = viewResult.Model as List<CustomObject>;
            Assert.NotEmpty(model);
            Assert.Equal(1, model.Count);
        }
    }
}
