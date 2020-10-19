using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using RestaurantReviewsApi.ApiModels.ApiModels;
using RestaurantReviewsApi.Bll.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantReviewsApi.Bll.Providers
{
    public class AuthProvider : IAuthProvider
    {
        private readonly string _jwtIssuer;
        private readonly string _jwtKey;

        public AuthProvider(string jwtIssuer, string jwtKey)
        {
            _jwtIssuer = jwtIssuer;
            _jwtKey = jwtKey;
        }
        public bool AuthenticateUser(string username)
        {
            //Typically auth would be handled by cognito or a separate service
            //For this demo, we will authenticate any user that gets passed
            return true;
        }

        public AccessTokenApiModel GetAccessTokenApiModel(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>()
            {
                new Claim("UserName", username),
                new Claim("scope", "USER")               
            };

            if (username.Contains("admin", StringComparison.InvariantCultureIgnoreCase))
                claims.Add(new Claim("scope", "ADMIN"));

            var token = new JwtSecurityToken(_jwtIssuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);;

            return new AccessTokenApiModel()
            {
                TokenType = "bearer",
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = 120
            };        
        }

        public UserModel GetUserModel(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return GetUserModel(request.Headers[HeaderNames.Authorization]);
        }

        public UserModel GetUserModel(string token)
        {
            if (token.StartsWith("Bearer ", StringComparison.CurrentCultureIgnoreCase))
                token = token.Substring(7);

            return new UserModel(){
                UserName = ValidateToken(token)?.FindFirst("UserName")?.Value
            };
        }

        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;
            validationParameters.ValidIssuer = _jwtIssuer;
            validationParameters.ValidateAudience = false;
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);
            return principal;
        }
    }
}
