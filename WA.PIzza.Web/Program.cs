

using (ApplicationDbContext db = new ApplicationDbContext())
{
    Adress adress1 = new Adress();
    adress1.AdressString = "haha u got bamboozled";

    Adress adress2 = new Adress();
    adress2.AdressString = "haha u got bamboozled";

    db.Adress.Add(adress1);
    db.Adress.Add(adress2);
    db.SaveChanges();

    Basket basket1 = new Basket();
    basket1.LastModified = DateTime.Now;
    
    ApplicationUser applicationUser1 = new ApplicationUser();
    applicationUser1.adressID = adress1.Id;

    basket1.ApplicationUserId = applicationUser1.Id;

    applicationUser1.basket = basket1;
    applicationUser1.Orders = null;

    db.ApplicationUser.Add(applicationUser1);
    db.Basket.Add(basket1);
    //applicationUser1.Orders;


    db.SaveChanges();

}
HostBuilder.CreateHostBuilder(args).Build().Run();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

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

app.Run();


