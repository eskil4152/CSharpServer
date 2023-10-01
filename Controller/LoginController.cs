using Microsoft.AspNetCore.Mvc;

[ApiController]
public class LoginController : Controller {

    private readonly LoginFunctions loginFunctions;
    public LoginController (LoginFunctions loginFunctions) {
        this.loginFunctions = loginFunctions;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User user){
        var statusCode = loginFunctions.LogInUser(user);

        if (statusCode == false) {
            return Unauthorized();
        } else if (statusCode == true) {
            return Ok();
        } else {
            return StatusCode(500);
        }
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] User user){
        var statusCode = loginFunctions.RegisterUser(user);

        if (statusCode == 409) {
            return Conflict();
        } else if (statusCode == 200) {
            return Ok();
        } else {
            return StatusCode(500);
        }
    }

}