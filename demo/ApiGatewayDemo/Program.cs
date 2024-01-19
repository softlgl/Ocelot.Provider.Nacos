using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Nacos;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelotconfig.json", true, true);
builder.Services.AddOcelot().AddNacosDiscovery();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseOcelot().Wait();

app.Run();
