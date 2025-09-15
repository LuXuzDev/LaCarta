using FastEndpoints.Swagger;
using FastEndpoints;
using LaCartaAPI.DependecyInyection;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");

// Si tus imágenes están en una carpeta "Images" fuera de wwwroot:
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});

app.UseAuthorization();
app.UseAuthentication();

app.UseFastEndpoints();
app.UseSwaggerGen();

app.Run();
