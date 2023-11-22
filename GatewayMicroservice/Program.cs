using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("ocelot.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables();

builder.Services.AddMvc();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddOcelot(builder.Configuration);


//builder.Services.AddCustomJwtAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerForOcelotUI();
app.UseOcelot().Wait();
app.UseHttpsRedirection();

app.UseAuthorization();
await app.UseOcelot();
app.MapControllers();

app.Run();
