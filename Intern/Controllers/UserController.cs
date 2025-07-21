using Intern.Models;
using Intern.Models.ViewModels;
using Intern.Repository.Interfaces;
using Intern.Utility;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Intern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppSetting _appSetting;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="appSetting">The app setting.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="emailService">The email service.</param>
        public UserController(IOptions<AppSetting> appSetting, IUserRepository userRepository, IEmailService emailService)
        {
            _appSetting = appSetting.Value;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="request">The login request.</param>
        /// <returns>The login result.</returns>
        [Route("Login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginViewModel request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                {
                    return BadRequest(new { message = "Username and password are required" });
                }

                Console.WriteLine($"Login attempt for username: {request.Username}");
                
                var user = _userRepository.GetUserByUsername(request.Username);
                if (user == null)
                {
                    Console.WriteLine("User not found");
                    return Unauthorized(new { message = "Invalid username or password" });
                }

                if (string.IsNullOrEmpty(user.PasswordHash))
                {
                    Console.WriteLine("Password hash is null or empty");
                    return StatusCode(500, new { message = "User account is not properly configured" });
                }

                var isValidPassword = PasswordHelper.VerifyPassword(request.Password, user.PasswordHash);
                Console.WriteLine($"Password verification result: {isValidPassword}");

                if (!isValidPassword)
                {
                    return Unauthorized(new { message = "Invalid username or password" });
                }

                var token = GenerateJwtToken(user);
                Console.WriteLine("Login successful, token generated");
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Login: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return StatusCode(500, new { message = "An error occurred while processing your request" });
            }
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns>The result of the user creation.</returns>
        [Route("Create")]
        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            ApiResponse<object> apiResponse = new();
            var result = _userRepository.CreateUser(user);
            apiResponse.Success = result > 0;
            apiResponse.Message = result > 0 ? "User Created Successfully" : "Failed to create User";
            return Ok(apiResponse);
        }

        [Route("GetByUserId/{userId}")]
        [HttpGet]
        public IActionResult GetByUserId(int userId)
        {
            ApiResponse<UserViewModel> apiResponse = new();
            var result = _userRepository.GetUserByUserId(userId);
            apiResponse.Data = result;
            apiResponse.Success = true;
            apiResponse.Message = "Successful";
            return Ok(apiResponse);
        }

        [Route("ForgotPassword")]
        [HttpPost]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            var user = _userRepository.GetUserByEmail(model.Email);
            if (user == null)
            {
                return NotFound(new { message = "Email not found" });
            }

            // Generate reset token
            var resetToken = Guid.NewGuid().ToString();
            var resetTokenExpiry = DateTime.UtcNow.AddHours(24);

            // Save reset token to user
            user.ResetToken = resetToken;
            user.ResetTokenExpiry = resetTokenExpiry;
            _userRepository.UpdateUser(user);

            // Send email
            var resetLink = $"http://localhost:4200/reset-password?token={resetToken}";
            var emailBody = $"Please click the following link to reset your password: {resetLink}";
            _emailService.SendEmail(user.Email, "Password Reset Request", emailBody);

            return Ok(new { message = "Password reset instructions have been sent to your email" });
        }

        [Route("ResetPassword")]
        [HttpPost]
        public IActionResult ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            var user = _userRepository.GetUserByResetToken(model.Token);
            if (user == null || user.ResetTokenExpiry < DateTime.UtcNow)
            {
                return BadRequest(new { message = "Invalid or expired reset token" });
            }

            // Update password
            user.PasswordHash = PasswordHelper.HashPassword(model.NewPassword);
            user.ResetToken = null;
            user.ResetTokenExpiry = null;
            _userRepository.UpdateUser(user);

            return Ok(new { message = "Password has been reset successfully" });
        }

        #region PrivateMethods

        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The generated JWT token.</returns>
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("role", user.RoleName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.Id.ToString())
             };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSetting.JWTSetting.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _appSetting.JWTSetting.Issuer,
                audience: _appSetting.JWTSetting.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_appSetting.JWTSetting.ExpiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion PrivateMethods
    }
}
