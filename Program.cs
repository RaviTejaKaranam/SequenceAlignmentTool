var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://sequence-alignment-tool.netlify.app/")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseRouting();
app.MapControllers();

app.Run();
