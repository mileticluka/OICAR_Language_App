using AutoMapper;
using DAL.DTO;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/user")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<UserDTO> GetUser()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            User? user = userRepository.GetUser(int.Parse(userId));

            return Ok(mapper.Map<UserDTO>(user));
        }

        [HttpGet]
        public ActionResult DeleteUser()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            User? user = userRepository.GetUser(int.Parse(userId));

            userRepository.DeleteUser(int.Parse(userId));

            return Ok();
        }
    }
}
