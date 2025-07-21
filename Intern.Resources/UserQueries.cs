namespace Intern.Resources
{
    public static class UserQueries
    {
        public const string GetUserByUsername = @"
            SELECT u.Id, u.FirstName, u.LastName, u.Username, u.Email, u.PasswordHash, u.FKRoleId, u.CreatedAt, u.UpdatedAt, u.IsDeleted, u.ResetToken, u.ResetTokenExpiry, r.Name as RoleName
            FROM [User] u
            INNER JOIN [Role] r ON u.FKRoleId = r.Id
            WHERE u.Username = @Username AND u.IsDeleted = 0";

        public const string InsertUser = @"
            INSERT INTO [User] (FirstName, LastName, Username, Email, PasswordHash, FKRoleId, CreatedAt, UpdatedAt, IsDeleted)
            VALUES (@FirstName, @LastName, @Username, @Email, @PasswordHash, @FKRoleId, GETUTCDATE(), GETUTCDATE(), 0);
            SELECT SCOPE_IDENTITY();";

        public const string GetUserByUserId = @"
            SELECT u.FirstName, u.LastName, u.Username, u.Email, r.Name as RoleName
            FROM [User] u
            INNER JOIN [Role] r ON u.FKRoleId = r.Id
            WHERE u.Id = @UserId AND u.IsDeleted = 0";

        public const string GetUserByEmail = @"
            SELECT u.*, r.Name as RoleName
            FROM [User] u
            INNER JOIN [Role] r ON u.FKRoleId = r.Id
            WHERE u.Email = @Email AND u.IsDeleted = 0";

        public const string GetUserByResetToken = @"
            SELECT u.*, r.Name as RoleName
            FROM [User] u
            INNER JOIN [Role] r ON u.FKRoleId = r.Id
            WHERE u.ResetToken = @ResetToken AND u.IsDeleted = 0";

        public const string UpdateUser = @"
            UPDATE [User]
            SET FirstName = @FirstName,
                LastName = @LastName,
                Username = @Username,
                Email = @Email,
                PasswordHash = @PasswordHash,
                ResetToken = @ResetToken,
                ResetTokenExpiry = @ResetTokenExpiry,
                UpdatedAt = GETUTCDATE()
            WHERE Id = @Id";
    }
} 