using Intern.Models;
using Intern.Models.ViewModels;

namespace Intern.Repository.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Gets a user by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The user with the specified username.</returns>
        User GetUserByUsername(string username);

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns><c>true</c> if the user is created successfully; otherwise, <c>false</c>.</returns>
        int CreateUser(User user);

        UserViewModel GetUserByUserId(int userId);

        User GetUserByEmail(string email);

        User GetUserByResetToken(string token);

        void UpdateUser(User user);
    }
}
