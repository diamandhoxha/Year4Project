using AuthenticationPlugin;
using FitnessApi.Data;
using FitnessApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitnessApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private FitnessDbContext _dbContext;
        private IConfiguration _configuration;
        private readonly AuthService _auth;

        public UsersController(FitnessDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _auth = new AuthService(_configuration);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllUsers(string sort, int? pageNumber, int? pageSize)
        {
            var CurrentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 5;
            var users = from user in _dbContext.Users
                           select new
                           {
                               Id = user.Id,
                               Name = user.Name,
                               Email = user.Email,
                               Weight = user.Weight,
                               Height = user.Height
                           };

            switch (sort)
            {
                case "desc":
                    return Ok(users.Skip((CurrentPageNumber - 1) * currentPageSize).Take(currentPageSize).OrderByDescending(u => u.Name));
                case "asc":
                    return Ok(users.Skip((CurrentPageNumber - 1) * currentPageSize).Take(currentPageSize).OrderBy(u => u.Name));
                default:
                    return Ok(users.Skip((CurrentPageNumber - 1) * currentPageSize).Take(currentPageSize));
            }

        }

        // Get api/user/5
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var users = (from user in _dbContext.Users
                            where user.Id == id
                            select new
                            {
                                Id = user.Id,
                                Name = user.Name,
                                Email = user.Email,
                                Height = user.Height,
                                Weight = user.Weight,
                            }).FirstOrDefault();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult Register([FromBody] User user)
        {
            var userWithSameEmail = _dbContext.Users.Where(u => u.Email == user.Email).SingleOrDefault();
            if (userWithSameEmail != null)
            {
                return BadRequest("User with this email already exists");
            }
            var userObj = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = SecurePasswordHasherHelper.Hash(user.Password),
                Weight = user.Weight,
                Height = user.Height,
                Role = "Users"
            };
            _dbContext.Users.Add(userObj);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            var userEmail = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
            if (userEmail == null)
            {
                return NotFound();
            }
            if (!SecurePasswordHasherHelper.Verify(user.Password, userEmail.Password))
            {
                return Unauthorized();
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,userEmail.Role)
            };
            var token = _auth.GenerateAccessToken(claims);
            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                expires_in = token.ExpiresIn,
                token_type = token.TokenType,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
                user_id = userEmail.Id,
                user_name = userEmail.Name,
                user_email = userEmail.Email,
                user_weight = userEmail.Weight,
                user_height = userEmail.Height
            });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            var u = _dbContext.Users.Find(id);
            if (user == null)
            {
                return NotFound("No record found against this id");
            }
            else
            {

                u.Name = user.Name;
                u.Password = u.Password;
                u.Email = u.Email;
                u.Weight = user.Weight;
                u.Height = user.Height;
                u.Role = u.Role;

                _dbContext.SaveChanges();
                return Ok("Record updated successfully");
            }

        }

        // DELETE api/<usersController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _dbContext.Users.Find(id);
            if (user == null)
            {
                return NotFound("Record not found against this id");
            }
            else
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
                return Ok("Record deleted");
            }

        }

    }
}
