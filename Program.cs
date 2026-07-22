using GradiApi.Extentions;
//To include signalR
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEnvironmentVariables();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddOpenApi();

builder.Services
.AllowCors(builder.Configuration)
.LoadDb(builder.Configuration)
.AddGlobalException()
.AddMappers()
.AddInfrastructure()
.ConfigureMcp()
;

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();
app.MapMcp("/mcp");

app.Run();

