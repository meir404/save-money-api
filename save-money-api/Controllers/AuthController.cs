using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Logic.Models;
using Logic.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace save_money_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public AuthController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(User user)
        {
            var userLogin = await _userRepository.CheckLogin(user);
            if (userLogin == null)
                return BadRequest();
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345!"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Role, "Manager"),
                    new Claim("id", userLogin.Id.ToString()),
                }),
                Expires = DateTime.Now.AddDays(12),
                SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new { token = tokenString });
        }

        [HttpPost, Route("register")]
        public async Task<IActionResult> Register(User user)
        {
            //ToDo check if mail exists 
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(await _userRepository.Save(user));
        }
    }
}