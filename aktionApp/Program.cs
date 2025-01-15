var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");


app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.Run();









