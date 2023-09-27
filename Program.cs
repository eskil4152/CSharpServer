var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

WebApplicationConfig.ConfigureRoutes(app);

app.Run();