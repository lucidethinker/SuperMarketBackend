using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SuperMarketBackend.DTO;
using SuperMarketBackend.Data;
using SuperMarketBackend.Services;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;


namespace SuperMarketBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserServices _userServices;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UsersController(IMapper mapper, IConfiguration configuration)
        {
            _userServices = new UserServices();
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register(UserDTO user)
        {
            var data = _userServices.Register(_mapper.Map<User>(user));
            if (data == null)
                return Conflict(new UserDTO() { UserName = "UserName Or Email Already Used For An Account!!" });            
            return Ok(data);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login(UserDTO user)
        {
            var authenticatedUser = _userServices.Login(_mapper.Map<User>(user));

            if (authenticatedUser == null)
            {
                return BadRequest(new UserDTO() { UserName = "User Not Found,Please Check Credentials!!" });
            }


            var claims = new[]
            {
            new Claim(ClaimTypes.Name, authenticatedUser.UserName),
            new Claim(ClaimTypes.Role, authenticatedUser.UserType.ToString()),
            new Claim(ClaimTypes.Actor, authenticatedUser.UserName)
            };

            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key1 = _configuration["Jwt:key"];
            var key = Encoding.ASCII.GetBytes(key1);
            var expairy = _configuration["Jwt:TokenExpiryInMinutes"];
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(int.Parse(expairy)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { Token = tokenHandler.WriteToken(token),User = authenticatedUser });
        }

        [HttpGet]
        [Authorize]
        [Route("GetUser/{id?}/{userName?}")]
        public async Task<IActionResult> GetUser(int id = 0,string? userName = "")
        {
            var data = _userServices.GetUser(id, userName);
            if (data != null)
                return Ok(data);
            else
                return BadRequest("User Not Found");
        }

        [HttpGet]
        [Authorize]
        [Route("GetAllUsers/{search?}/{orderBydesc?}")]
        public async Task<IActionResult> GetAllUsers(string? search = "",bool orderBydesc = false)
        {
            var data = _userServices.GetAllUsers(search,orderBydesc);
            if (data != null)
                return Ok(data);
            else 
                return BadRequest("Something went wrong");
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var data = _userServices.DeleteUser(id);
            if (data)
                return Ok(data);
            else
                return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Authorize]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserDTO user)
        {
            var data = await _userServices.UpdateUser(_mapper.Map<User>(user));
            if (data != null)
                return Ok(data);
            else
                return BadRequest(new UserDTO() { UserName = "Something Went Wrong"});
        }

        [HttpGet]
        [Authorize]
        [Route("ChangeStatusUser/{id}/{status?}")]
        public async Task<IActionResult> ChangeStatusUser(int id,int? status = null)
        {
            var data = _userServices.ChangeStatusUser(id,status);
            if (data)
                return Ok(data);
            else
                return BadRequest(data);
        }
    }
}
