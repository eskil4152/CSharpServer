using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        return Json(res);
    }

    [HttpGet("search/first/{var}")]
    public IActionResult GetPeopleByFirstName(string var){
        var res = personFunctions.GetPersonByFirstName(var);
        return Json(res);
    }

    [HttpGet("search/last/{var}")]
    public IActionResult GetPeopleByLastName(string var){
        var res = personFunctions.GetPersonByLastName(var);
        return Json(res);
    }

    [HttpGet("search/full/{var}")]
    public IActionResult GetPeopleByFullName(string firstName, string lastName){
        var res = personFunctions.GetPersonByFullName(firstName, lastName);
        return Json(res);
    }

    [HttpGet("{id}")]
    public IActionResult GetOnePerson(int id){
        var res = personFunctions.GetOnePerson(id);
        if (res != null) {
            return Json(res);
        } else {
            return NotFound();
        }
    }
}