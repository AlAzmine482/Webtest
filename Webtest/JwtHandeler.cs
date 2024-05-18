using Webtest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace Webtest
{
    public class JwtHandeler(IConfiguration configuration, UserManager<WorldCarsUser> userManager) 
    {
        //Generate Token
        public async Task<JwtSecurityToken> GetTokenAsync(WorldCarsUser user) =>
        new(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            claims: await GetClaimsAsync(user),
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration["JwtSettings:ExpirationTimeInMinutes"])),
            signingCredentials: GetSigningCredentials());

        //token Encryption and encoding
        private SigningCredentials GetSigningCredentials()
        {
            byte[] key = Encoding.UTF8.GetBytes(configuration["JwtSettings:SecurityKey"]!);
            SymmetricSecurityKey secret = new(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaimsAsync(WorldCarsUser user)
        {
            List<Claim> claims = [new Claim(ClaimTypes.Name, user.UserName!)];
            claims.AddRange(from role in await userManager.GetRolesAsync(user) select new Claim(ClaimTypes.Role, role));
            return claims;
        }

    }
}
