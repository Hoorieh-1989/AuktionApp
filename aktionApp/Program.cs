using aktionApp.Entities;
using aktionApp.Services;
using aktionApp.Repos;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

//sätter upp CORS policy
builder.Services.AddCors();
var app = builder.Build();


app.UseCors(policy =>
    policy.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);


app.UseRouting();

app.UseCors();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
