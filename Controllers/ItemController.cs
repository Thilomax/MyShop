using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Models;
using MyShop.ViewModels; //needed so the controller can see the ItemsViewModel class


namespace MyShop.Controllers;

public class ItemController : Controller
{

    //to access the database, we need to add the following private readonly object along with changing the action methods
    private readonly ItemDbContext _itemDbContext; //declares a private read-only field for storing an instance of ItemDbContext
    // the following method is a constructor that takes an ItemDbContext as a parameter and assigns it to the _itemDbContext field
    //that is dependency injection. DbContext is provided to the controller 

    /*A parameter only exists inside the method it belongs to. When the constructor finishes, itemDbContext (the parameter) is gone. 
    But Table(), Grid() and Details() need it too — and they're separate methods.

A field is a variable that belongs to the whole class, so every method in it can reach it. So the line*/
    public ItemController(ItemDbContext itemDbContext)
    {
        _itemDbContext = itemDbContext;
    }

//we have changed all the methods to be asynchronous. That means all these methods that communicate with the database can be done while other methods are being executed leading to less wait time and better user experience
    public async Task<IActionResult> Table()
    {   // await keyword means that the method pauses execution asynchronously until the database 
        // finishes fetching the data, freeing up the thread to handle other incoming requests in the meantime.
        List<Item> items = await _itemDbContext.Items.ToListAsync();
        var itemsViewModel = new ItemsViewModel(items, "Table");
        return View(itemsViewModel);
    }

    public async Task<IActionResult> Grid()
    {
        List<Item> items = await _itemDbContext.Items.ToListAsync();
        var itemsViewModel = new ItemsViewModel(items, "Grid");
        return View(itemsViewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var item = await _itemDbContext.Items.FirstOrDefaultAsync(i => i.ItemId == id);
        if (item == null)
            return NotFound();
        return View(item);
    }

    [HttpGet]
    public IActionResult Create() //this method doesnt communicate with the database, since it only shows the Create view.
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Item item)
    {
        if (ModelState.IsValid)
        {
            _itemDbContext.Items.Add(item);
            await _itemDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Table));
        }
        return View(item);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var item = await _itemDbContext.Items.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Item item)
    {
        if (ModelState.IsValid)
        {
            _itemDbContext.Items.Update(item);
            await _itemDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Table));
        }
        return View(item);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _itemDbContext.Items.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _itemDbContext.Items.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        _itemDbContext.Items.Remove(item);
        await _itemDbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Table));
    }
}