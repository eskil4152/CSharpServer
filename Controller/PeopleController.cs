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

    [HttpGet("search/first")]
    public IActionResult GetPeopleByFirstName([FromBody] HttpContent content){
        var res = personFunctions.GetPersonByFirstName(content.firstName);

        if (res.Count == 0) {
            return NotFound();
        }
        return Ok(res);
    }

    [HttpGet("search/last")]
    public IActionResult GetPeopleByLastName([FromBody] HttpContent content)
    {
        var res = personFunctions.GetPersonByLastName(content.lastName);

        if (res.Count == 0) {
            return NotFound();
        }
        return Ok(res);
    }

    [HttpGet("search/full")]
    public IActionResult GetPeopleByFullName([FromBody] HttpContent content)
    {
        var res = personFunctions.GetPersonByFullName(content.firstName, content.lastName);

        if (res.Count == 0) {
            return NotFound();
        }

        return Ok(res);
    }

    [HttpGet("search/id")]
    public IActionResult GetPeopleById([FromBody] HttpContent content)
    {
        var res = personFunctions.GetPersonById(content.id);

        if (res.count == 0)
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
}

public record HttpContent(int id, string firstName, string lastName, int age);