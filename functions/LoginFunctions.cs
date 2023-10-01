using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

public class LoginFunctions {
    private readonly ApplicationDbContext dbContext;
    public LoginFunctions(ApplicationDbContext context) {
        dbContext = context;
    }

    private readonly PasswordHasher passwordHasher = new();

    public bool LogInUser(User user) {
        var username = user.username;
        var password = user.password;

        var existingUser = dbContext.users.FirstOrDefault((e) => e.username == username);

        if (existingUser == null) {
            return false;
        }

        return passwordHasher.VerifyPassword(password, existingUser.password);
    }

    public int RegisterUser(User user){
        var username = user.username;
        var password = passwordHasher.HashPass(user.password);

        var userCheck = dbContext.users.FirstOrDefault((e) => e.username == username);

        if (userCheck != null) {
            return 409;
        }

        var newUser = new User {
            id = Guid.NewGuid(),
            username = username,
            password = password,
            authoritylevel = 1
        };

        dbContext.users.Add(newUser);
        dbContext.SaveChanges();

        return 200;
    }
}