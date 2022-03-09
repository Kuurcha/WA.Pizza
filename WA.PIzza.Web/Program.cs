
ApplicationContext applicationContext = new ApplicationContext();

using (ApplicationContext db = new ApplicationContext())
{
    Basket basket1  = new Basket();
    basket1.LastModified = DateTime.Now;

    Basket basket2 = new Basket();
    basket2.LastModified = DateTime.Now.AddDays(-1);

    db.Basket.Add(basket1);
    db.Basket.Add(basket2);
    db.SaveChanges();
     
    BasketItem basketItem1 = new BasketItem();
    basketItem1.Quantity = 1;
    basketItem1.CatalogItemName = "TestCatalog";
    basketItem1.basketId = basket1.Id;
    basketItem1.basket = basket1;
    basketItem1.CatalogType = WA.Pizza.Core.CatalogType.CatalogType.Pizza;
    db.BasketItem.Add(basketItem1);
    db.SaveChanges();
    
}
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


