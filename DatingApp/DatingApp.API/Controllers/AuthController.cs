using System.Security.Cryptography;
using System.Text;
using DatingApp.API.Data;
using DatingApp.API.Data.Entities;
using DatingApp.API.Services;
using DatingApp.DatingApp.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenServices;
        public AuthController(DataContext context, ITokenService tokenServices)
        {
            this._context = context;
            this._tokenServices = tokenServices;
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
            var token = _tokenServices.CreateToken(newUser.Username);
            return Ok(new UserTokenDto
            {
                Username = newUser.Username,
                Token = token
            });
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] AutherUserDto autherUserDto)
        {
            autherUserDto.Username = autherUserDto.Username.ToLower();
            var currentUser = _context.AppUsers.FirstOrDefault(u => u.Username == autherUserDto.Username);
            if (currentUser == null)
            {
                return Unauthorized("Username isn't valid");
            }
            using var hmac = new HMACSHA512(currentUser.PasswordSalt);
            var passwordBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(autherUserDto.Password));
            for (int i = 0; i < currentUser.PasswordHash.Length; i++)
            {
                if (currentUser.PasswordHash[i] != passwordBytes[i])
                {
                    return Unauthorized("Password invalid");
                }
            }
            var token = _tokenServices.CreateToken(currentUser.Username);
            return Ok(new UserTokenDto
            {
                Username = currentUser.Username,
                Token = token
            });
        }
        //[Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.AppUsers.ToList());
        }
    }
}