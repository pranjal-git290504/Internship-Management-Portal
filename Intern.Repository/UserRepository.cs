using Dapper;
using Intern.Models;
using Intern.Models.ViewModels;
using Intern.Repository.Interfaces;
using Intern.Resources;
using Intern.Utility;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace Intern.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppSetting _appSetting;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="appSetting">The application setting options.</param>
        public UserRepository(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }

        /// <summary>
        /// Gets a user by user
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The user with the specified username.</returns>
        public User GetUserByUsername(string username)
        {
            try
            {
                using (SqlConnection con = new(_appSetting.ConnectionString))
                {
                    con.Open();
                    Console.WriteLine($"Attempting to get user with username: {username}");
                    var user = con.QueryFirstOrDefault<User>(UserQueries.GetUserByUsername, new { Username = username });
                    if (user == null)
                    {
                        Console.WriteLine("No user found with the given username");
                    }
                    else
                    {
                        Console.WriteLine($"User found. HasPasswordHash: {!string.IsNullOrEmpty(user.PasswordHash)}");
                    }
                    return user;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetUserByUsername: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw; // Rethrow to let the controller handle it
            }
        }

        public int CreateUser(User user)
        {
            int userId = 0;
            try
            {
                user.PasswordHash = PasswordHelper.HashPassword(user.Password);
                using (SqlConnection con = new (_appSetting.ConnectionString))
                {
                    userId = con.QueryFirstOrDefault<int>(UserQueries.InsertUser, user);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
            }
            return userId;
        }

        public UserViewModel GetUserByUserId(int userId)
        {
            using (SqlConnection con = new(_appSetting.ConnectionString))
            {
                return con.QueryFirstOrDefault<UserViewModel>(UserQueries.GetUserByUserId, new { UserId = userId });
            }
        }

        public User GetUserByEmail(string email)
        {
            using (SqlConnection con = new(_appSetting.ConnectionString))
            {
                return con.QueryFirstOrDefault<User>(UserQueries.GetUserByEmail, new { Email = email });
            }
        }

        public User GetUserByResetToken(string token)
        {
            using (SqlConnection con = new(_appSetting.ConnectionString))
            {
                return con.QueryFirstOrDefault<User>(UserQueries.GetUserByResetToken, new { ResetToken = token });
            }
        }

        public void UpdateUser(User user)
        {
            using (SqlConnection con = new(_appSetting.ConnectionString))
            {
                con.Execute(UserQueries.UpdateUser, user);
            }
        }
    }
}
