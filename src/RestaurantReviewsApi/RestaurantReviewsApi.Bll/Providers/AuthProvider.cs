using Microsoft.IdentityModel.Tokens;
using RestaurantReviewsApi.ApiModels.ApiModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

            var token = new JwtSecurityToken(_jwtIssuer,
               _jwtIssuer,
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new AccessTokenApiModel()
            {
                TokenType = "bearer",
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresIn = 120
            };        
        }
    }
}
