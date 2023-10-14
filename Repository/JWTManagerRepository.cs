using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class JwtManagerRepository : IJWTManagerRepository {

    private readonly IConfiguration configuration;
    public JwtManagerRepository(IConfiguration configuration) {
        this.configuration = configuration;
    }

    public Tokens Authenticate(User user){

        var tokenHandler = new JwtSecurityTokenHandler();
		var tokenKey = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);
        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new Claim[]
          {
             new(ClaimTypes.Name, user.Username),
             new("AuthorizationLevel", user.AuthorizationLevel.ToString())
		  }),
		   Expires = DateTime.UtcNow.AddMinutes(10),
		   SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);
		return new Tokens { Token = tokenHandler.WriteToken(token) };
    }

    public bool Authorize(string token, int requiredAuthorizationLevel) {
        var handler = new JwtSecurityTokenHandler();

        try
        {
            if (handler.ReadToken(token) is JwtSecurityToken jsonToken)
            {
                var claims = jsonToken.Claims;

                var authorityLevelClaim = claims.FirstOrDefault(c => c.Type == "AuthorizationLevel");
                if (authorityLevelClaim != null && int.TryParse(authorityLevelClaim.Value, out int userAuthorityLevel))
                {
                    if (userAuthorityLevel >= requiredAuthorizationLevel)
                    {
                        return true;
                    }
                    else
                    {
                        System.Console.WriteLine("User does not have the required authorization level.");
                        return false;
                    }
                }
                else
                {
                    System.Console.WriteLine("Authorization level claim not found or invalid.");
                    return false;
                }
            }
            else
            {
                System.Console.WriteLine("Invalid token format");
                return false;
            }
        }
        catch
        {
            System.Console.WriteLine("Token error");
            return false;
        }
    }
}