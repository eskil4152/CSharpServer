using Microsoft.EntityFrameworkCore;

public class LoginFunctions {
    private readonly ApplicationDbContext dbContext;
    private readonly JwtManagerRepository jwtManagerRepository;

    public LoginFunctions(
        ApplicationDbContext context, 
        JwtManagerRepository jwtManagerRepository) {
        dbContext = context;
        this.jwtManagerRepository = jwtManagerRepository;
    }

    private readonly PasswordHasher passwordHasher = new();
    
    public Tokens? LogInUser(string username, string password) {
        var existingUser = dbContext.users.FirstOrDefault((e) => e.Username == username);

        if (existingUser == null) {
            return null;
        }

        bool passwordCheck = passwordHasher.VerifyPassword(password, existingUser.Password);

        if (!passwordCheck) {
            return null;
        }

        return jwtManagerRepository.Authenticate(existingUser);
    }

    public Tokens? RegisterUser(string username, string password){

        var userCheck = dbContext.users.FirstOrDefault((e) => e.Username == username);

        if (userCheck != null) {
            return null;
        }

        var newUser = new User {
            Username = username,
            Password = passwordHasher.HashPass(password),
        };


        dbContext.users.Add(newUser);
        dbContext.SaveChanges();

        return jwtManagerRepository.Authenticate(newUser);
    }
}