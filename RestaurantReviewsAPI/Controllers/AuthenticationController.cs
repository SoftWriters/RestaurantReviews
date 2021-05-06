using RestaurantReviewsAPI.Models;
using RestaurantReviewsAPI.Models.DataTransferObjects;
using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace RestaurantReviewsAPI.Controllers
{
    [RequireHttps]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(UserManager<ApplicationUser> userManager, AppDbContext dbContext, IConfiguration configuration, ILogger<AuthenticationController> logger)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginDTO payload)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid Model");
                    return BadRequest();
                }

                /* find the user */
                var user = await _userManager.FindByNameAsync(payload.uid);

                /* authenticate */
                if (user != null && await _userManager.CheckPasswordAsync(user, payload.pwd))
                {
                    var tokenValue = await GenerateJwtToken(user);
                    return Ok(tokenValue);
                }
                else
                {
                    _logger.LogWarning("Unauthorized User ({0})", payload.uid);
                    return Unauthorized();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed authenticating Application User.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private async Task<AuthResultDTO> GenerateJwtToken(ApplicationUser user)
        {
            try
            {
                /* Get authorization claims */
                var authClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString())
                };

                /* create key with secret */
                var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

                /* create token */
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    expires: DateTime.UtcNow.AddMinutes(10), 
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                /* create jwt token */
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                /* refresh token */
                var refreshToken = new RefreshToken()
                {
                    JwtId = token.Id,
                    IsRevoked = false,
                    ApplicationUserId = user.Id,
                    DateAdded = DateTime.UtcNow,
                    DateExpired = DateTime.UtcNow.AddMonths(6),
                    Token = Guid.NewGuid().ToString().ToString() + "-" + Guid.NewGuid().ToString().ToString()
                };

                /* add to db */
                await _dbContext.RefreshTokens.AddAsync(refreshToken);
                await _dbContext.SaveChangesAsync();

                /* wrap up tokens and return */
                var response = new AuthResultDTO
                {
                    Token = jwtToken,
                    RefreshToken = refreshToken.Token,
                    ExpiredAt = token.ValidTo
                };

                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed generating JwtToken.");
                throw; // bubble-up
            }
        }
        
    }
}
