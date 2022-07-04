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

AppDomain.CurrentDomain.SetData("DataDirectory",
    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
var builder =
    WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<OrderDataService>();
builder.Services.AddScoped<BasketDataService>();
builder.Services.AddScoped<CatalogDataService>();

builder.Services.AddControllers();

var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var fullPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    options.IncludeXmlComments(fullPath);
});
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

Log.Logger = logger;

Log.Information("Application is starting up...");
builder.Host.UseSerilog(logger);

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSeq();
});

builder.Services.AddControllers().AddFluentValidation(options =>
{
    options.AutomaticValidationEnabled = true;
    options.RegisterValidatorsFromAssemblyContaining<BasketItemValidator>();
});


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

