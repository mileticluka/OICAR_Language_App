using AdminPanel.Controllers;
using AdminPanel.Models;
using API.Controllers;
using AutoMapper;
using DAL.DTO;
using DAL.Interfaces;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Security.Claims;
using AuthController = AdminPanel.Controllers.AuthController;
using UserController = AdminPanel.Controllers.UserController;

namespace AdminPanelTests
{
    [TestFixture]
    public class UserControllerTests
    {
        private Mock<IUserRepository> mockUserRepository;
        private Mock<IMapper> mockMapper;
        private UserController userController;

        [SetUp]
        public void Setup()
        {
            mockUserRepository = new Mock<IUserRepository>();   
            mockMapper = new Mock<IMapper>();

            userController = new UserController(mockUserRepository.Object, mockMapper.Object);
        }

        [Test]
        public void Index_GetUsers()
        {
            IList<User> listUsers = new List<User>()
            {
                new User(), new User(), new User()
            };

            IList<UserViewModel> list = new List<UserViewModel>()
            {
                new UserViewModel(), new UserViewModel(), new UserViewModel()
            };

            mockUserRepository.Setup(s => s.GetAll()).Returns(listUsers);
            mockMapper.Setup(s => s.Map<IList<UserViewModel>>(listUsers)).Returns(list);

            var result = userController.Index();

            Assert.IsInstanceOf<ViewResult>(result);
            var vr = result as ViewResult;
            Assert.IsInstanceOf<IList<UserViewModel>>(vr.Model);
            var model = vr.Model as IList<UserViewModel>;
            Assert.IsTrue(model.Count == 3);
        }

        [Test]
        public void Details_GetSingleUser()
        {
            var user = new User() { Id = 1 };
            var viewModel = new UserViewModel() { Id = 1 };

            mockUserRepository.Setup(s => s.GetUser(1)).Returns(user);
            mockMapper.Setup(s => s.Map<UserViewModel>(user)).Returns(viewModel);

            var result = userController.Details(1);

            Assert.IsInstanceOf<ViewResult>(result);
            var vr = result as ViewResult;
            Assert.IsInstanceOf<UserViewModel>(vr.Model);
            var model = vr.Model as UserViewModel;
            Assert.IsTrue(model.Id == 1);
        }
        
        [Test]
        public void Delete_DeleteSingleUser()
        {
            var user = new User() { Id = 1 };

            mockUserRepository.Setup(s => s.GetUser(1)).Returns(user);
            
            var result = userController.Delete(1);

            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }


        [TearDown]
        public void TearDown()
        {
            userController.Dispose();
        }
    }


    [TestFixture]
    public class AuthControllerTests
    {
        private Mock<IAdminUserRepository> mockAdminRepository;
        private Mock<IMapper> mockMapper;
        private Mock<IAuthenticationService> mockAuthenticationService;
        private AuthController authController;

        [SetUp]
        public void Setup()
        {
            this.mockAdminRepository = new Mock<IAdminUserRepository>();
            this.mockMapper = new Mock<IMapper>();
            this.mockAuthenticationService = new Mock<IAuthenticationService>();
            this.authController = new AuthController(mockAdminRepository.Object, mockMapper.Object);
        }

        [Test]
        public void Login_WhenCalledWithInvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var loginRequestDto = new LoginDTO { Username = "invalidUser", Password = "invalidPass" };
            var user = new AdminUser { Id = 1, Username = "invalidUser" };

            mockAdminRepository.Setup(s => s.Authenticate(loginRequestDto)).Returns(false);
            mockAdminRepository.Setup(s => s.GetUser("invalidUser")).Returns(user);

            // Act
            var result = authController.Login(loginRequestDto);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsTrue(authController.ModelState.ContainsKey("Username"));
            var modelStateEntry = authController.ModelState["Username"];
            Assert.IsNotNull(modelStateEntry);
            var errors = modelStateEntry.Errors;
            Assert.IsNotEmpty(errors);
            Assert.That(errors[0].ErrorMessage, Is.EqualTo("Invalid username or password"));
        }

        [Test]
        public void Login_WhenCalledWithValidCredentials_ReturnsOkResult()
        {
            // Setup MockContext
            var httpContext = new DefaultHttpContext();
            httpContext.RequestServices = new ServiceCollection()
                .AddSingleton<IAuthenticationService>(mockAuthenticationService.Object)
                .BuildServiceProvider();

            authController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            // Arrange
            var loginRequestDto = new LoginDTO { Username = "validUser", Password = "validPass" };
            var user = new AdminUser { Id = 1, Username = "validUser" };

            mockAdminRepository.Setup(s => s.Authenticate(loginRequestDto)).Returns(true);
            mockAdminRepository.Setup(s => s.GetUser("validUser")).Returns(user);

            mockAuthenticationService.Setup(s => s.SignInAsync(
                           It.IsAny<HttpContext>(),
                           It.IsAny<string>(),
                           It.IsAny<ClaimsPrincipal>(),
                           It.IsAny<AuthenticationProperties>()))
                           .Returns(Task.CompletedTask);

            // Act
            var result = authController.Login(loginRequestDto);

            // Assert
            Assert.IsInstanceOf<RedirectResult>(result);
            Assert.IsFalse(authController.ModelState.ContainsKey("Username"));
        }

        [TearDown]
        public void TearDown()
        {
            authController.Dispose();
        }
    }
}