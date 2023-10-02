using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SQLitePCL;

public class LoginFunctions {
    private readonly ApplicationDbContext dbContext;
    private readonly IConfiguration configuration;
    private readonly JwtManagerRepository jwtManagerRepository;

    public LoginFunctions(
        ApplicationDbContext context, 
        IConfiguration configuration,
        JwtManagerRepository jwtManagerRepository) {
        dbContext = context;
        this.configuration = configuration;
        this.jwtManagerRepository = jwtManagerRepository;
    }

    private readonly PasswordHasher passwordHasher = new();
    
    public Tokens? LogInUser(User user) {
        var username = user.username;
        var password = user.password;

        var existingUser = dbContext.users.FirstOrDefault((e) => e.username == username);

        if (existingUser == null) {
            return null;
        }

        bool passwordCheck = passwordHasher.VerifyPassword(password, existingUser.password);

        if (!passwordCheck) {
            return null;
        }

        var token = jwtManagerRepository.Authenticate(existingUser);

        return token;
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