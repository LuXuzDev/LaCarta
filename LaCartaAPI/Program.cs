using FastEndpoints.Swagger;
using FastEndpoints;
using LaCartaAPI.DependecyInyection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);
//.AddPresentationServices();
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();
app.UseAuthentication();

app.UseFastEndpoints();
app.UseSwaggerGen();

app.Run();
