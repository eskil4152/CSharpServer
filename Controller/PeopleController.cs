using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/people")]
public class PeopleController : Controller {
    private readonly PersonFunctions personFunctions;

    public PeopleController(PersonFunctions personFunctions) {
        this.personFunctions = personFunctions;
    }

    [HttpGet("all")]
    public IActionResult GetAll(){
        var res = personFunctions.GetAllPeople();

        if (res.Count == 0) {
            return NotFound();
        }
        
        return Ok(res);
    }

    [HttpGet("search/first/{var}")]
    public IActionResult GetPeopleByFirstName(string var){
        var res = personFunctions.GetPersonByFirstName(var);

        if (res.Count == 0) {
            return NotFound();
        }
        return Ok(res);
    }

    [HttpGet("search/last/{var}")]
    public IActionResult GetPeopleByLastName(string var){
        var res = personFunctions.GetPersonByLastName(var);

        if (res.Count == 0) {
            return NotFound();
        }
        return Ok(res);
    }

    [HttpGet("search/full/{firstName}/{lastName}")]
    public IActionResult GetPeopleByFullName(string firstName, string lastName){
        var res = personFunctions.GetPersonByFullName(firstName, lastName);

        if (res.Count == 0) {
            return NotFound();
        }

        return Ok(res);
    }

    [HttpPost("new")]
    public IActionResult AddPerson([FromBody] Person person){
        var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
        var token = authorizationHeader.Replace("Bearer: ", "");

        var res = personFunctions.AddPerson(person, token);

        if (res == 401) {
            return Unauthorized();
        } else if (res == 200) {
            return Ok();
        } else {
            return StatusCode(500);
        }
    }

}