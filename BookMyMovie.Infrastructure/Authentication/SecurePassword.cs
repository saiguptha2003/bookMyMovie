using System.Security.Cryptography;
using System.Text;

namespace BookMyMovie.Infrastructure.Authentication;

public class SecurePassword
{
    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    // Helper method to verify passwords
    public static bool VerifyPassword(string inputPassword, string storedHash)
    {
        string hashedInput = HashPassword(inputPassword);
        return hashedInput == storedHash;
    }
    
}