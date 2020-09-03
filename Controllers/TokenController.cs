using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using OnlineHealthManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace OnlineHealthManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly OnlineHMContext _context;

        public TokenController(IConfiguration config, OnlineHMContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost]

        public async Task<IActionResult> Post(Admin _adminData)
        {
            if (_adminData != null && _adminData.UserName != null && _adminData.Password != null)
            {
                var user = await GetUser(_adminData.UserName, _adminData.Password);

                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserName",user.UserName)
                        };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, 
                        expires: DateTime.UtcNow.AddDays(1).AddMinutes(5), signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }

            else
            {
                return BadRequest();
            }
        }

        private async Task<Admin> GetUser(string username, string password)
        {
            return await _context.Admin.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }
    }
}



