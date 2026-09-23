using Microsoft.EntityFrameworkCore;
using MyShop.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
//registering the dbContext service here.
builder.Services.AddDbContext<ItemDbContext>(options =>
{
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:ItemDbContextConnection"]
    );
});

builder.Services.AddScoped<IItemRepository, ItemRepository>();

var app = builder.Build();
//checks whether the application is running in development environment. yes? add dev exception page - an error page with info useful for debugging for developers
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    DBInit.Seed(app); // this calls the seeding method which uses DBInit.cs to initialise the database with the predefined data
}

app.MapStaticAssets(); //this enables the static assets from wwwroot (the images and the javascript and the css)

//this line is equal to the code that is commented out
app.MapDefaultControllerRoute();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();