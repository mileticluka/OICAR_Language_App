using AutoMapper;
using Azure;
using DAL.DTO;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AuthController(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        [HttpGet]
        public string HelloWorld()
        {
            return "Hello world";
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginDTO model)
        {
            var user = userRepository.GetUser(model.Username);
            if (user != null && userRepository.Authenticate(model))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

                var token = new JwtSecurityToken(
                    issuer: configuration["JWT:Issuer"],
                    audience: configuration["JWT:Audience"],
                    expires: DateTime.Now.AddDays(30),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    id = user.Id,
                    username = user.Username,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            ModelState.AddModelError("", "Invalid credentials");
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] RegisterDTO model)
        {
            var userExists = userRepository.UserExists(model.Username);
            if (userExists)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            if (!userRepository.CanCreate(model))
            {
                ModelState.AddModelError("", "Username or Email is already in use.");
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            userRepository.Register(model);
            return Ok();
        }
    }
}
