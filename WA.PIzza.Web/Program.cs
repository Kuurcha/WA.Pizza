using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using Wa.Pizza.Infrasctructure.Data.Services;
using Wa.Pizza.Infrasctructure.Services;
using Wa.Pizza.Infrasctructure.Services.Interfaces;
using Wa.Pizza.Infrasctructure.Validators;
using WA.PIzza.Web.Extensions;

AppDomain.CurrentDomain.SetData("DataDirectory", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

var builder =  WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.injectServices();

builder.Services.configureLogger(builder);

builder.Services.configureDBContext(builder.Configuration.GetConnectionString("Default"));

builder.Services.configureWeb();

builder.Services.configureSwagger();

var app = builder.Build();




// Configure the HTTP request pipeline.
app.useHttp(!app.Environment.IsDevelopment());

app.MapRazorPages();

app.useSwaggerWithUI();

app.UseSerilogRequestLogging();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();

app.UseDeveloperExceptionPage();

