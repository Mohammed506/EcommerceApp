using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecommerce.DB.Data;
using Ecommerce.Domain.Shared.DTO;
using Ecommerce.Domain.Shared.Entites;
using Ecommerce.Domain.Shared.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace EcommerceApp.Domain.Services
{


    public class AuthService : IAuthService
    {
        private readonly MongoDBContext _context;
        private readonly JwtSettings _jwtSettings;

        public AuthService(MongoDBContext context, IOptions<JwtSettings> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<string> AuthenticateAsync(LoginRequest request)
        {
            var user = await _context.Users.Find(u => u.Username == request.Username).FirstOrDefaultAsync();
            if (user == null || !PasswordUtils.VerifyPassword(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials.");

            return GenerateJwtToken(user);
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _context.Users.Find(user => user.Username == request.Username).FirstOrDefaultAsync();
            if (existingUser != null)
                throw new InvalidOperationException("Username already exists.");

            var user = new User
            {
                Username = request.Username,
                PasswordHash = PasswordUtils.HashPassword(request.Password),
                Email = request.Email
            };

            await _context.Users.InsertOneAsync(user);
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _context.Users.Find(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.Find(user => true).ToListAsync();
        }

        public async Task UpdateUserAsync(string id, User userIn)
        {
            var user = await _context.Users.Find(user => user.Id == id).FirstOrDefaultAsync();
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            await _context.Users.ReplaceOneAsync(user => user.Id == id, userIn);
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _context.Users.Find(user => user.Id == id).FirstOrDefaultAsync();
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            await _context.Users.DeleteOneAsync(user => user.Id == id);
        }

        private string GenerateJwtToken(User user)
        {
            var issuer = _jwtSettings.Issuer;
            var audience = _jwtSettings.Audience;
            var key = _jwtSettings.SecretKey;
            var expiration = TimeSpan.FromMinutes(_jwtSettings.ExpirationInMinutes);

            var tokenHandler = new JwtSecurityTokenHandler();
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.Add(expiration),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
