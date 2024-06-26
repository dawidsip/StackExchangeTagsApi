using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TodoApi.Interfaces;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ITodoContext, TodoContext>(opt =>
    opt.UseInMemoryDatabase("TagsList"), ServiceLifetime.Singleton);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Stack Exchange Tags API", Version = "v1" });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddSingleton<HelperService>();

var app = builder.Build();

app.Services.GetRequiredService<HelperService>().PopulateTags(); // Get the tags from the Stack Exchange API on startup

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();