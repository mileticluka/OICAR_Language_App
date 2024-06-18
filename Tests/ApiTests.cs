using API.Controllers;
using AutoMapper;
using DAL.DTO;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NuGet.ProjectModel;
using NuGet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace ApiTests
{
    public class AuthControllerTests
    {
        private Mock<IUserRepository> mockUserRepository;
        private Mock<IMapper> mockMapper;
        private Mock<IConfiguration> mockConfiguration;

        private AuthController authController;

        [SetUp]
        public void Setup()
        {
            mockUserRepository = new Mock<IUserRepository>();
            mockMapper = new Mock<IMapper>();
            mockConfiguration = new Mock<IConfiguration>();

            authController = new AuthController(mockUserRepository.Object, mockMapper.Object, mockConfiguration.Object);

            mockConfiguration.Setup(s => s["JWT:Key"]).Returns("d785f748fdd26faf72cd8a7b7fd1d07dessssadsafdas1");
            mockConfiguration.Setup(s => s["JWT:Issuer"]).Returns("localhost");
            mockConfiguration.Setup(s => s["JWT:Audience"]).Returns("localhost");
        }

        [Test]
        public void Login_ValidUser()
        {
            var loginDto = new LoginDTO() { Username = "validUser", Password = "password" };
            var user = new User() { Id = 1, Username = "validUser" };

            mockUserRepository.Setup(s => s.GetUser("validUser")).Returns(user);
            mockUserRepository.Setup(s => s.Authenticate(loginDto)).Returns(true);

            var result = authController.Login(loginDto);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var ok = result as OkObjectResult;
            Assert.IsTrue(ok.Value.ToJToken().GetValue<int>("id") == 1);
        }
        
        [Test]
        public void Login_InvalidUser()
        {
            var loginDto = new LoginDTO() { Username = "invalidUser", Password = "password" };
            var user = new User() { Id = 1, Username = "invalidUser" };

            mockUserRepository.Setup(s => s.GetUser("invalidUser")).Returns(user);
            mockUserRepository.Setup(s => s.Authenticate(loginDto)).Returns(false);

            var result = authController.Login(loginDto);
            
            Assert.IsInstanceOf<ObjectResult>(result);
            var sc = result as ObjectResult;
            Assert.IsTrue(sc.StatusCode == 400);
        }

        
        [Test]
        public void Register_AlreadyExists()
        {
            var registerDTO = new RegisterDTO() { Username = "user", Password = "password" };
            var user = new User() { Id = 1, Username = "user" };

            mockUserRepository.Setup(s => s.UserExists("user")).Returns(true);

            var result = authController.Register(registerDTO);
            
            Assert.IsInstanceOf<ObjectResult>(result);
            var sc = result as ObjectResult;
            Assert.IsTrue(sc.StatusCode == 400);
        }
        
        [Test]
        public void Register_EmailInUse()
        {
            var registerDTO = new RegisterDTO() { Username = "user", Email = "user@localhost", Password = "password" };
            var user = new User() { Id = 1, Username = "user" };

            mockUserRepository.Setup(s => s.CanCreate(registerDTO)).Returns(false);

            var result = authController.Register(registerDTO);
            
            Assert.IsInstanceOf<ObjectResult>(result);
            var sc = result as ObjectResult;
            Assert.IsTrue(sc.StatusCode == 400);
        }
        
        [Test]
        public void Register_Success()
        {
            var registerDTO = new RegisterDTO() { Username = "user", Email = "user@localhost", Password = "password" };
            var user = new User() { Id = 1, Username = "user" };

            mockUserRepository.Setup(s => s.UserExists("user")).Returns(false);
            mockUserRepository.Setup(s => s.CanCreate(registerDTO)).Returns(true);

            var result = authController.Register(registerDTO);
            
            Assert.IsInstanceOf<Microsoft.AspNetCore.Mvc.OkResult>(result);
        }
    }
}
