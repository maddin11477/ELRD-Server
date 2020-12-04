using ELRDDataAccessLibrary.DataAccess;
using ELRDDataAccessLibrary.Models;
using ELRDServerAPI.Authentication;
using ELRDServerAPI.Contracts.V1;
using ELRDServerAPI.Contracts.V1.Requests;
using ELRDServerAPI.Contracts.V1.Responses;
using ELRDServerAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace ELRDServerAPI.Controllers.V1
{
    //[Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        private readonly IConfiguration _configuration;

        public UserController(ILogger<UserController> log, IUserService userService, IConfiguration configuration)
        {
            _logger = log;
            _userService = userService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Users.Login)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            //SeedData();
            //_logger.LogInformation(String.Format("Username {0} is trying to login.", model.Username));
            //var user = await _db.Users.FirstOrDefaultAsync(x => x.Username == model.Username);
            //if(user != null && user.Password == model.Password)
            //{
            //    var userRoles = new List<string>
            //    {
            //        "admin"
            //    };
            //    var authClaims = new List<Claim>
            //    {
            //        new Claim(ClaimTypes.Name, user.Username),
            //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            //    };
            //    foreach(var role in userRoles)
            //    {
            //        authClaims.Add(new Claim(ClaimTypes.Role, role));
            //    }

            //    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            //    var token = new JwtSecurityToken(
            //        issuer: _configuration["JWT:ValidIssuer"],
            //        audience: _configuration["JWT:ValidAudience"],
            //        expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JWT:ExpirationTime"])),
            //        claims: authClaims,
            //        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            //        );
            //    return Ok(new
            //    {
            //        token = new JwtSecurityTokenHandler().WriteToken(token),
            //        expiration = token.ValidTo
            //    });
            //}
            //else
            //{
            //    return Unauthorized();
            //}
            return NotFound();
        }


        // GET: api/<UserController>
        
        [HttpGet(ApiRoutes.Users.GetAll)]
        public IActionResult GetAll()
        {
            _logger.LogInformation("GET Request");
            return Ok(_userService.GetUsers());
        }

        // GET: UserByID
        [HttpGet(ApiRoutes.Users.Get)]
        public IActionResult Get([FromRoute]int userID)
        {
            _logger.LogInformation(String.Format("GET Request for user ID: {0}", userID));
            User s = _userService.GetUserById(userID);
            if (s == null)
                return NotFound();


            return Ok(s);
        }

        // POST: Create
        [HttpPost(ApiRoutes.Users.Create)]
        public IActionResult Create([FromBody] CreateUserRequest user)
        {
            if (user != null)
            {
                try
                {
                    _logger.LogInformation("Trying to add new user: " + user.ToString());

                    UserResponse response = _userService.AddNewUser(user);

                    //header location information
                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                    var locationUrl = baseUrl + "/" + ApiRoutes.Users.Get.Replace("{userID}", response.Id.ToString());

                    _logger.LogInformation(String.Format("User created, new ID: {0}", response.Id ));

                    return Created(locationUrl, response);
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    return StatusCode(500);
                }
            }
            else
            {
                _logger.LogInformation("User can't be empty");
                return BadRequest();
            }

        }


        private void SeedData()
        {

            //try
            //{

            //    //If no user is in DB, add Admin user
            //    if (_db.Users.Count() == 0)
            //    {
            //        _logger.LogInformation("No data found, update database first...");
            //        _db.Database.Migrate();

            //        _logger.LogInformation("No user in Database found, adding Admin-User with standard credentials.");
            //        _db.Users.Add(new ELRDDataAccessLibrary.Models.User
            //        {
            //            Firstname = "Administrator",
            //            Lastname = "Administrator",
            //            Username = "admin",
            //            Password = "admin"
            //        });
            //        _db.SaveChanges();
            //        _logger.LogInformation("Admin user created.");

            //    }
            //}
            //catch (Exception e)
            //{
            //    _logger.LogError(e.Message);
            //}
        }
    }
}
