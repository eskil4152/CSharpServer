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
    
    public Tokens? LogInUser(User user) {
        var username = user.Username;
        var password = user.Password;

        var existingUser = dbContext.users.FirstOrDefault((e) => e.Username == username);

        if (existingUser == null) {
            return null;
        }

        bool passwordCheck = passwordHasher.VerifyPassword(password, existingUser.Password);

        if (!passwordCheck) {
            return null;
        }

        var token = jwtManagerRepository.Authenticate(existingUser);

        return token;
    }

    public int RegisterUser(User user){
        var username = user.Username;
        var password = passwordHasher.HashPass(user.Password);

        var userCheck = dbContext.users.FirstOrDefault((e) => e.Username == username);

        if (userCheck != null) {
            return 409;
        }

        var newUser = new User {
            Id = Guid.NewGuid(),
            Username = username,
            Password = password,
            Authoritylevel = 1
        };

        dbContext.users.Add(newUser);
        dbContext.SaveChanges();

        return 200;
    }
}