using Moq;
using Xunit;
using SimpleWebApp.Controllers;
using SimpleWebApp.core.Interface;
using SimpleWebApp.data.ViewModel;
using SimpleWebApp.data.Model;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApp.core.Model;

namespace SimpleWebApp.Tests
{
    public class HomeControllerTests
    {
        private readonly Mock<ISimpleWebAppService> _mockService;
        private readonly HomeController _controller;

        public HomeControllerTests()
        {
            _mockService = new Mock<ISimpleWebAppService>();
            _controller = new HomeController(_mockService.Object);
        }

        [Fact]
        public async Task Login_Post_ReturnsRedirectToAction_WhenLoginIsSuccessful()
        {
            // Arrange
            var loginModel = new LoginViewModel { UserEmail = "test@example.com", Password = "password" };
            var userDetails = new UserDetails { ID = 1, UserTypeID = 2 };
            var result = new Result<UserDetails> { ErrorMessage = null, Data = userDetails };

            _mockService.Setup(s => s.ValidateUserCredentials(It.IsAny<LoginViewModel>()))
                        .ReturnsAsync(result);

            // Act
            var response = await _controller.Login(loginModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(response);
            Assert.Equal("ClientView", redirectToActionResult.ActionName);
            Assert.Equal("Home", redirectToActionResult.ControllerName);
            Assert.Equal(1, redirectToActionResult.RouteValues["id"]);
        }


        [Fact]
        public async Task Login_Post_ReturnsViewWithModelError_WhenLoginFails()
        {
            // Arrange
            var loginModel = new LoginViewModel { UserEmail = "test@example.com", Password = "wrongpassword" };
            var result = new Result<UserDetails> { ErrorMessage = "Invalid login", Data = null };

            _mockService.Setup(s => s.ValidateUserCredentials(It.IsAny<LoginViewModel>()))
                        .ReturnsAsync(result);

            // Act
            var response = await _controller.Login(loginModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(response);
            Assert.False(_controller.ModelState.IsValid);
            Assert.Equal(loginModel, viewResult.Model);
        }

    }
}

