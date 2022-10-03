using System.Security.Cryptography;
using System.Text;
using DatingApp.API.Data;
using DatingApp.API.Data.Entities;
using DatingApp.DatingApp.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly DataContext _context;
        public AuthController(DataContext context)
        {
            this._context = context;
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] AutherUserDto autherUserDto)
        {
            autherUserDto.Username = autherUserDto.Username.ToLower();
            if (_context.AppUsers.Any(u => u.Username == autherUserDto.Username))
            {
                return BadRequest("Username is already registed!");
            }
            using var hmac = new HMACSHA512();
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(autherUserDto.Password));
            var newUser = new User
            {
                Username = autherUserDto.Username,
                PasswordSalt = hmac.Key,
                PasswordHash = passwordHash,
            };
            _context.AppUsers.Add(newUser);
            _context.SaveChanges();
            return Ok(newUser.Username);
        }
        [HttpPost("login")]
        public void Login([FromBody] string value)
        {

        }
    }
}