using BCrypt.Net;
using FileStorageApp.Application.Interfaces;
using FileStorageApp.Application.Users.Login;
using FileStorageApp.Application.Users.Register;
using FileStorageApp.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FileStorageApp.Persistence.Services
{
    public class UserService : IUser
    {
        private readonly IApplicationUserRepository _repository;
        private readonly IConfiguration _configuration;
        public UserService(IApplicationUserRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO)
        {
            var getUser = await _repository.GetUserByEmailAsync(loginDTO.Email!);
            if (getUser == null) return new LoginResponse(false,"User not found");

            bool chekPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, getUser.Password);
            if (chekPassword)
                return new LoginResponse(true, "Login successfuly", GenerateJWTToken(getUser));
            else
                return new LoginResponse(false, "Invalid credentials");
        }

        public async Task<RegistrationResponse> RegisterUserAsync(RegisterUserDTO registrationDTO)
        {
            var getUser = await _repository.GetUserByEmailAsync(registrationDTO.Email!);
            if (getUser != null)
                return new RegistrationResponse(false, "User already exist");

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Name = registrationDTO.Name,
                Email = registrationDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registrationDTO.Password)
            };

            await _repository.AddUserAsync(user);
            return new RegistrationResponse(true, "Registration completed");
        }

        private string GenerateJWTToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Email, user.Email!)
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
