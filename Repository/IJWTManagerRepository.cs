public interface IJWTManagerRepository {
    Tokens Authenticate(User user);
}