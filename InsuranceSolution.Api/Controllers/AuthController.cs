using InsuranceSolution.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace InsuranceSolution.Api.Controllers
{
    // api/auth 
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        public AuthController(UserManager<IdentityUser> userManager, 
                              IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest model)
        {
            // Validation 
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
                return BadRequest("User already exists");

            var user = new IdentityUser
            {
                Email = model.Email,
                PhoneNumber = model.Phone,
                UserName = model.Email,
            };

            await _userManager.CreateAsync(user, model.Password);
            
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest model)
        {
            // Validation 
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest("Username or password is invalid");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);

            // Generate the token 
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id), // Name Identified = Id
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            };

            string keyAsString = _config["Jwt:Key"];
            byte[] keyAsBytes = Encoding.UTF8.GetBytes(keyAsString); 
            var key = new SymmetricSecurityKey(keyAsBytes);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(tokenAsString);
        }

        [HttpGet("email-confirm")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email); // Find the user by email
            if (user == null)
                return BadRequest("User does not exist");
           
            var result = await _userManager.ConfirmEmailAsync(user, token.Replace(" ", "+"));
            if (!result.Succeeded)
                return BadRequest("Token is invalid");

            return Ok();
        }
    }
}
