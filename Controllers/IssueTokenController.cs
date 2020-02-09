using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtAuthentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace JwtAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueTokenController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public IssueTokenController(IConfiguration configuration, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _issuer = configuration["Issuer"];
            _audience = configuration["Audience"];
            _key = configuration["Key"];
        }
        
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] JwtTokenCredentials model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByNameAsync(model.UserName);
                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (signInResult.Succeeded)
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
                    var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

                    IList<Claim> claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, model.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, model.UserName)
                    };
                    var securityToken = new JwtSecurityToken(
                            _issuer,
                            _audience,
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(30),
                            signingCredentials:credentials
                        );
                    var results = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                        expiration = securityToken.ValidTo
                    };
                    return new JsonResult(results);
                }
                else
                {
                    return BadRequest();
                }
            }


            return BadRequest();
        }
    }
}