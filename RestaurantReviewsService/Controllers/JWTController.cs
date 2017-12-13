using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantReviews.Models;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Web;
using System.Security.Claims;
using System.Text.Encodings;
using Microsoft.Extensions.Configuration;

namespace RestaurantReviews.Controllers
{
    public class LoginObject
    {
        public string Email {get; set;}
        public string Pass {get; set;}
    }

    [Route("api/[controller]/[action]")]
    public class JWTController : Controller
    {
        private readonly IConfiguration configuration_;

        public JWTController(IConfiguration configuration)
        {
            configuration_ = configuration;
        }

        // method should be called over HTTPS
        [HttpPost]
        public IActionResult GenerateToken([FromBody]LoginObject loginObj)
        {
            if (ModelState.IsValid)
            {
                /* TODO: Validate users here */
                if (loginObj.Email == "demouser1@demo.com" && loginObj.Pass == "demopass")
                {
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, loginObj.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration_["JwtKey"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var expires = DateTime.Now.AddMinutes(Convert.ToDouble(configuration_["JwtExpireMinutes"]));

                    var token = new JwtSecurityToken(
                        configuration_["JwtIssuer"],
                        configuration_["JwtAudience"],
                        claims,
                        expires: expires,
                        signingCredentials: creds
                    );
                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
                else
                {
                    return Unauthorized();
                }
            }

            return BadRequest("Could not create token");
        }
    }
}