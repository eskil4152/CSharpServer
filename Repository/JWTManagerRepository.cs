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

}