using BCrypt.Net;

public class PasswordHasher {
    public string HashPass(string password) {
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

        return hashedPassword; 
    }

    public bool VerifyPassword(string enteredPassword, string hashedPassword) {
        return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
    }
}