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
using WA.PIzza.Web.Configuration;
AppDomain.CurrentDomain.SetData("DataDirectory",
    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
var builder =
    WebApplication.CreateBuilder(args);



DBWorkConfiguration.configureDBWork(builder);
SwaggerConfiguration.configureSwagger(builder);
CustomLoggerConfiguration.configureLogger(builder);
DBWorkConfiguration.configureDBWork(builder);
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}   


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();



app.UseAuthorization();



app.MapRazorPages();

app.ConfigureCustomExceptionhandler();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // подключаем маршрутизацию на контроллеры
});

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

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

