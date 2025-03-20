using Application;
using EcommerceScalarBase.Configurations;
using EcommerceScalarBase.Controllers.Base;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;
using static Infrastructure.Common.Extensions.ControllerExtensions;

[assembly: ApiConventionType(typeof(ApiConventions))]
try
{
    var builder = WebApplication.CreateBuilder(args);

    //// Add services to the container
    builder.Services.AddControllers();
    builder.AddConfigurations();
    builder.Services
     .AddControllers(option =>
     {
         option.Conventions.Add(new RouteTokenTransformerConvention(new ToKebabParameterTransformer()));
     })
     .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();
 
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            options.Title = "Ecommerce Clean Architecture Scalar API";
            options.Theme = ScalarTheme.BluePlanet;
            options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
            options.ShowSidebar = true;
            options.WithOpenApiRoutePattern("/swagger/v1/swagger.json");
        });
    }

    app.UseInfrastructure(builder.Configuration);

    app.MapEndpoints();

    app.Run();
}

catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

