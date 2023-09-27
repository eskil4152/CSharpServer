var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/all", () => PersonFunctions.GetPeople());
app.MapGet("/person/{i}", (int i) => PersonFunctions.GetPerson(i));

app.Run();