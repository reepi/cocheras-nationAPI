using Common.DTOs.UserDTOs;
using Data.Entities;
using Data.Repositories.AuthRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;

        public AuthService(IAuthRepository authRepository, IConfiguration config)
        {
            _authRepository = authRepository;
            _config = config;
        }

        private string GenerateToken(User user)
        {
            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));

            SigningCredentials signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("given_name", user.Name));

            var jwtSecurityToken = new JwtSecurityToken(
              _config["Authentication:Issuer"],
              _config["Authentication:Audience"],
              claimsForToken,
              DateTime.UtcNow,
              DateTime.UtcNow.AddMinutes(5),
              signature);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public string? Authenticate(UserForAuthDto userForAuth)
        {
            User? user = _authRepository.Get(userForAuth.Username);
            if (user is null || user.Password != userForAuth.Password)
            {
                return null;
            }

            return GenerateToken(user);
        }

        public string? Register(UserForRegistrationDto userForRegistration)
        {
            User? user = _authRepository.Get(userForRegistration.Username);
            if (user is not null)
            {
                return null;
            }

            user = new User
            {
                Username = userForRegistration.Username,
                Name = userForRegistration.Name,
                Password = userForRegistration.Password
            };

            _authRepository.Add(user);
            return GenerateToken(user);
        }

    }
}
