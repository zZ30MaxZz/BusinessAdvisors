using Microsoft.IdentityModel.Tokens;
using SoftTeK.BusinessAdvisors.Dto.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SoftTeK.BusinessAdvisors.Api.Helpers
{
    public class BuildTokenJwt : IBuildTokenJwt
    {
        private readonly IConfiguration _configuration;

        public BuildTokenJwt(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UserDto BuildJwt(UserDto userDto)
        {
            var claims = new List<Claim>()
            {
                new Claim("organization", "softtek")
            };

            string jwtkey = _configuration["jwtkey"]!;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtkey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(5);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            userDto.Token = new JwtSecurityTokenHandler().WriteToken(token);
            userDto.Expiration = expiration;

            return userDto;
        }
    }
}
