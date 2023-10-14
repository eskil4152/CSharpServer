using Microsoft.AspNetCore.Mvc;

[ApiController]
public class LoginController : Controller {

    private readonly LoginFunctions loginFunctions;
    public LoginController (LoginFunctions loginFunctions) {
        this.loginFunctions = loginFunctions;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginContent content){
        var result = loginFunctions.LogInUser(content.username, content.password);

        if (result == null) {
            return Unauthorized();
        } else if (result is Tokens)
        {
            return Ok(result.Token);
        } else
        {
            return StatusCode(500);
        }
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] LoginContent content){
        var result = loginFunctions.RegisterUser(content.username, content.password);

        if (result == null) {
            return Conflict();
        } else if (result is Tokens) {
            return Ok(result.Token);
        } else {
            return StatusCode(500);
        }
    }
}

public record LoginContent (string username, string password);