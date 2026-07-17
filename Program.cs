using GradiApi.Extentions;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services
.AddEnvironmentVariables()
.AllowCors(builder.Configuration)
.LoadDb(builder.Configuration)
.AddGlobalException()
.AddMappers()
.AddInfrastructure()

;

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();

