using Intern.Utility;
using Microsoft.Data.SqlClient;

try
{
    var connectionString = "Server=.\\SQLEXPRESS;Database=Intern;Trusted_Connection=True;TrustServerCertificate=True;";
    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();
        Console.WriteLine("Connected to database successfully");

        // First, let's check the current hash
        var selectCommand = new SqlCommand("SELECT PasswordHash FROM [User] WHERE Username = @Username", connection);
        selectCommand.Parameters.AddWithValue("@Username", "admin");
        var currentHash = selectCommand.ExecuteScalar()?.ToString();
        Console.WriteLine($"\nCurrent hash from database: {currentHash}");

        if (!string.IsNullOrEmpty(currentHash))
        {
            // Try to verify the current password
            var isCurrentValid = PasswordHelper.VerifyPassword("admin123", currentHash);
            Console.WriteLine($"Current password verification result: {isCurrentValid}");
        }

        // Generate a new hash
        var password = "admin123";
        var newHash = PasswordHelper.HashPassword(password);
        Console.WriteLine($"\nGenerated new hash: {newHash}");

        // Update the admin user with the new hash
        var updateCommand = new SqlCommand("UPDATE [User] SET PasswordHash = @Hash WHERE Username = @Username", connection);
        updateCommand.Parameters.AddWithValue("@Hash", newHash);
        updateCommand.Parameters.AddWithValue("@Username", "admin");
        var rowsAffected = updateCommand.ExecuteNonQuery();
        Console.WriteLine($"Rows affected by update: {rowsAffected}");

        // Verify that the hash was stored correctly
        selectCommand = new SqlCommand("SELECT PasswordHash FROM [User] WHERE Username = @Username", connection);
        selectCommand.Parameters.AddWithValue("@Username", "admin");
        var storedHash = selectCommand.ExecuteScalar()?.ToString();
        Console.WriteLine($"\nStored hash from database: {storedHash}");

        // Verify the password using the stored hash
        var isValid = PasswordHelper.VerifyPassword(password, storedHash);
        Console.WriteLine($"Password verification result: {isValid}");

        // Double-check by generating a new hash and verifying with it
        var verificationHash = PasswordHelper.HashPassword(password);
        Console.WriteLine($"\nNew verification hash: {verificationHash}");
        var isValidWithNewHash = PasswordHelper.VerifyPassword(password, verificationHash);
        Console.WriteLine($"Password verification with new hash: {isValidWithNewHash}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    Console.WriteLine($"Stack trace: {ex.StackTrace}");
} 