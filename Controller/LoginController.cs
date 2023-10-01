using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("login")]
public class LoginController : Controller {

    [HttpGet]
    public IActionResult Login(){

        return NotFound();
    }

}