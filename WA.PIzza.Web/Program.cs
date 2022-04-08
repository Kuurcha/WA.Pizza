using Microsoft.EntityFrameworkCore;
using Wa.Pizza.Infrasctructure.Services;
using Wa.Pizza.Infrasctructure.Services.Interfaces;


var builder =
    WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<OrderService>();
builder.Services.AddControllers();

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // подключаем маршрутизацию на контроллеры
});

/*var orderService = app.Services.GetRequiredService<IOrderService>();

Order order1 = new Order();
order1.Status = OrderStatus.Canceled;
order1.CreationDate = DateTime.Now;
order1.Description = "test";
order1.OrderItems = new List<OrderItem>();
orderService.AddOrder(order1);

Console.WriteLine("Test");*/
app.Run();



