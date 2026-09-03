var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();
//checks whether the application is running in development environment. yes? add dev exception page - an error page with info useful for debugging for developers
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//this line is equal to the code that is commented out
app.MapDefaultControllerRoute();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();