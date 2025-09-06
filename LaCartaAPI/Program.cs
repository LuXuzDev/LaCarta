using FastEndpoints.Swagger;
using FastEndpoints;
using LaCartaAPI.DependecyInyection;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddPresentationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Usa el middleware estándar de Swagger
    app.UseSwaggerUI(); // Habilita la UI de Swagger
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();
app.UseAuthentication();
app.UseFastEndpoints(); // Middleware de FastEndpoints



app.Run();
