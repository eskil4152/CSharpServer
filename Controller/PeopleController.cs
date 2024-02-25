using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/people")]
public class PeopleController : Controller {
    private readonly PersonFunctions personFunctions;

    public record HttpId(string id);
    public record HttpFirstName(string firstName);
    public record HttpLastName(string lastName);
    public record HttpFullname(string firstName, string lastName);

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

    [HttpPost("search/first")]
    public IActionResult GetPeopleByFirstName([FromBody] HttpFirstName content){
        var res = personFunctions.GetPersonByFirstName(content.firstName);

        if (res.Count == 0) {
            return NotFound();
        }
        return Ok(res);
    }

    [HttpPost("search/last")]
    public IActionResult GetPeopleByLastName([FromBody] HttpLastName content)
    {
        var res = personFunctions.GetPersonByLastName(content.lastName);

        if (res.Count == 0) {
            return NotFound();
        }
        return Ok(res);
    }

    [HttpPost("search/full")]
    public IActionResult GetPeopleByFullName([FromBody] HttpFullname content)
    {
        var res = personFunctions.GetPersonByFullName(content.firstName, content.lastName);

        if (res.Count == 0) {
            return NotFound();
        }

        return Ok(res);
    }

    [HttpPost("search/id")]
    public IActionResult GetPeopleById([FromBody] HttpId content)
    {
        _ = long.TryParse(content.id, out long id);

        var res = personFunctions.GetPersonById(id);

        if (res.Count == 0)
        {
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

    [HttpDelete("delete")]
    public IActionResult DeletePerson([FromBody] HttpId content)
    {
        _ = long.TryParse(content.id, out long id);

        var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
        var token = authorizationHeader.Replace("Bearer: ", "");

        var res = personFunctions.DeletePerson(id, token);

        if (res == 200){
            return NoContent();
        }

        return Unauthorized();
    }
}