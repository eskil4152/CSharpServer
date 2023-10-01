using System.Drawing.Drawing2D;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("api/people")]
public class Routes : Controller {
    private readonly PersonFunctions personFunctions;

    public Routes(PersonFunctions functions) {
        personFunctions = functions;
    }

    [HttpGet("all")]
    public IActionResult GetAll(){
        var res = personFunctions.GetAllPeople();

        if (res.IsNullOrEmpty()) {
            return NotFound();
        }
        return Ok(res);
    }

    [HttpGet("search/first/{var}")]
    public IActionResult GetPeopleByFirstName(string var){
        var res = personFunctions.GetPersonByFirstName(var);
        if (res.IsNullOrEmpty()) {
            return NotFound();
        }
        return Ok(res);
    }

    [HttpGet("search/last/{var}")]
    public IActionResult GetPeopleByLastName(string var){
        var res = personFunctions.GetPersonByLastName(var);

        if (res.IsNullOrEmpty()) {
            return NotFound();
        }
        return Ok(res);
    }

    [HttpGet("search/full/{firstName}/{lastName}")]
    public IActionResult GetPeopleByFullName(string firstName, string lastName){
        var res = personFunctions.GetPersonByFullName(firstName, lastName);

        if (res.IsNullOrEmpty()) {
            return NotFound();
        }

        return Ok(res);
    }

    [HttpPost("new")]
    public IActionResult AddPerson([FromBody] Person person){
        return Ok();
    }

}