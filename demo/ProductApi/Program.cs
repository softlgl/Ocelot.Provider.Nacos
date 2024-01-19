using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Nacos.AspNetCore.V2;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddNacosAspNet(builder.Configuration);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductApi", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.Request | HttpLoggingFields.RequestQuery;
    logging.RequestBodyLogLimit = 4096;
});


var app = builder.Build();

app.UseHttpLogging();
app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductApi v1");
});

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();