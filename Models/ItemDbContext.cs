// Brings in Entity Framework Core — needed for DbContext, DbSet, etc.
using Microsoft.EntityFrameworkCore;

namespace MyShop.Models;

// This class is our door into the database.
// "DbContext" is a ready-made class from Entity Framework that does all the hard work:
// opening the database, writing the SQL, reading the results back.
// By writing ": DbContext" we get all of that for free, and then we just add our own details.
public class ItemDbContext : DbContext
{
    // The options object carries the configuration (most importantly the connection string).
    // You never build it yourself — ASP.NET creates it and passes it in (dependency injection).
    // ": base(options)" forwards it up to DbContext so EF knows which database to talk to.
    public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
    {
        //Database.EnsureCreated(); // For early prototyping only. Remove when switching to EF Core Migrations.
    }

    // This IS the table. DbSet<Item> = "the collection of Item rows".
    // EF names the table after the property name, so this creates a table called "Items".
    // Every model class you want stored needs its own DbSet line here.
    public DbSet<Item> Items { get; set; }
    public DbSet<Customer> Customers { get; set; } //these add the new classes into the database.
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
    /*With lazy loading enabled, when you access a navigation property like OrderItems of the 
Order class, EF Core will automatically load the related OrderItem entities from the database 
on-demand, without explicitly loading the entire tree of dependent objects connected by 
the navigation properties.
*/

}





/*What this all does:
What DbContext actually does for you

Four jobs, all in one object:

- Holds the connection. It knows which database file to open, opens it, and closes it when it's done. 
    You never write connection code.
- Knows the map. The DbSet<Item> Items line tells it "the Item class corresponds to the Items table, 
    and the properties on it are the columns". That mapping is how it knows what SQL to write.
- Translates both directions. You write _itemDbContext.Items.ToList(); it turns that into SELECT * FROM Items, 
    runs it, and turns each row back into an Item object. You never see the SQL.
- Tracks changes. This is the one that surprises people. When you load an object through the context, 
    the context remembers it and watches it. If you then change item.Name = "something", 
        nothing hits the database yet — the context just notes "this row is dirty". When you call SaveChanges(),    
        it works out every insert/update/delete that's pending and sends them together.
*/