public class WebApplicationConfig
{
    public static void ConfigureRoutes(WebApplication app)
    {
        var people = app.MapGroup("/people");

        people.MapGet("/all", () => PersonFunctions.GetAllPeople());
        people.MapGet("/{id}", (int id) => PersonFunctions.GetOnePerson(id));
    }
}