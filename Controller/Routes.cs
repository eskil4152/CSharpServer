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