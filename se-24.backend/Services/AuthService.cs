using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using se_24.backend.Data;
using se_24.backend.Models;
using se_24.backend.Models.DTOs;

namespace se_24.backend.Services
{
    public interface IAuthService
    {
        Task<User> Register(RegisterDTO registerDto);
        Task<User> Login(LoginDTO loginDto);
    }

    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> Register(RegisterDTO registerDto)
        {
            // Check if username or email already exists
            if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
                throw new Exception("Username already exists");

            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
                throw new Exception("Email already exists");

            // Create new user
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = HashPassword(registerDto.Password),
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> Login(LoginDTO loginDto)
        {
            // Try to find user by email or username
            var user = await _context.Users.FirstOrDefaultAsync(u => 
                u.Email == loginDto.LoginIdentifier || 
                u.Username == loginDto.LoginIdentifier);

            if (user == null)
                throw new Exception("Invalid login credentials");

            // Verify password
            if (!VerifyPassword(loginDto.Password, user.PasswordHash))
                throw new Exception("Invalid login credentials");

            // Update last login
            user.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return user;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
} 