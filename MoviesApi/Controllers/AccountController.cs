using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MoviesApi.Dtos;
using MoviesApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Net.WebRequestMethods;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(RegistrationUserDto userDto)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);

            ApplicationUser applicationUser = new()
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
            };
             
            IdentityResult identityResult = await _userManager
                .CreateAsync(applicationUser, userDto.Password);

            if (identityResult.Succeeded)
                return Ok("Account add success");
            return BadRequest(identityResult.Errors.FirstOrDefault());
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            if (ModelState.IsValid)
            {
               ApplicationUser? user = await _userManager.FindByNameAsync(userDto.UserName);
                if (user is not null)
                {
                   bool isFound = await _userManager.CheckPasswordAsync(user, userDto.Password);
                    if (isFound)
                    {
                        var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name,userDto.UserName),
                            new Claim(ClaimTypes.NameIdentifier,user.Id),
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                        };

                        var roles = await _userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role,role));
                        }
                        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                        SigningCredentials credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken token = new JwtSecurityToken(
                            issuer: _configuration["JWT:validIssuer"],
                            audience: _configuration["JWT:ValidAudiance"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddHours(1),
                            signingCredentials: credentials
                            );

                        return Ok(new
                        {
                            token= new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    } 
                }

            }
            return Unauthorized();
        }
    }
}
