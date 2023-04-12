using ApplicationService.Contracts;
using Data.Entitites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Extensions;

namespace ApplicationService.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public string CreateToken(ApplicationUser user, List<IdentityRole<long>> roles)
        {
            var token = user
                .CreateClaims(roles)
                .CreateJwtToken(configuration);
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }
    }
}
