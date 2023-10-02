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
			 new(ClaimTypes.Name, user.username),
             new(ClaimTypes.Role, user.authoritylevel.ToString())  
		  }),
		   Expires = DateTime.UtcNow.AddMinutes(10),
		   SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.Aes128CbcHmacSha256)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);
		return new Tokens { Token = tokenHandler.WriteToken(token) };
    }

    public bool CheckTokenAuthorization(string token, int requiredLevel) {
        var handler = new JwtSecurityTokenHandler();
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenAuthorityLevel = 0;

        try {
            if (handler.ReadToken(token) is JwtSecurityToken jsonToken){
                var authoritylevelClaim = jsonToken.Claims.FirstOrDefault((e) => e.Type == "role");

                if (authoritylevelClaim != null && int.TryParse(authoritylevelClaim.Value, out int authoritylevel)) {
                    tokenAuthorityLevel = authoritylevel;
                    System.Console.WriteLine("Users authority level: " + authoritylevel);
                } else {
                    System.Console.WriteLine("Authority level not found or invalid");
                    return false;
                }
            } else {
                System.Console.WriteLine("Invalid token format");
                return false;
            }

            if (tokenAuthorityLevel >= requiredLevel) {
                return true;
            } else {
                return false;
            }
        } catch {
            System.Console.WriteLine("Token error");
            return false;
        }
    }
}