using CiscoApplication.Infrastructure;
using CiscoApplication.Application;
using CiscoApplication.Infrastructure.Contexts;
using Microsoft.AspNetCore.Http.Features;
using CiscoApplication.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = null; // NOTE: set upload limit to unlimited, or specify the limit in number of bytes
});


// NOTE: set a very large limit for multipart/form-data encoded forms; this should be added regardless of setting the limit for a controller, action or the whole server
builder.Services.Configure<FormOptions>(options => options.MultipartBodyLengthLimit = long.MaxValue);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureDependencies(builder.Configuration);
builder.Services.AddApplicationDependencies();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseExceptionHandler();


app.Run();
