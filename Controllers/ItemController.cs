using Microsoft.AspNetCore.Mvc;
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


    
    public IActionResult Table() //this is an action method. Action methods correspond 
    //to a user action, like viewing items, displaying details, submitting forms.
    //These methods usually return an IActionResult, which could be a VIEW, a Redirect, JSON, or other.
    //In this example, the Table action gets a list of items, packs them into a ViewModel and returns a view that will render the list of items
    {
        //this line retreieves ALL item records from the Items table in the database and converts them into a list
        List<Item> items = _itemDbContext.Items.ToList(); // instead of getting the items from the GetItems() method, we now get it from the Database.
        

        //We no longer use ViewBag here - instead both the item list AND the view name are bundled
        //into ONE object: a ViewModel. A ViewModel is just a class made to carry exactly the data one view needs.
        //Advantage over ViewBag: it's strongly typed, so the compiler catches typos and the view gets IntelliSense.
        //ViewBag is dynamic, so mistakes only show up at runtime.
        var itemsViewModel = new ItemsViewModel(items, "Table");
        return View(itemsViewModel);
    }

    //same as Table, but passes "Grid" as the view name - a different way of displaying the exact same data
    public IActionResult Grid()
    {
        List<Item> items = _itemDbContext.Items.ToList();
        var itemsViewModel = new ItemsViewModel(items, "Grid");
        return View(itemsViewModel);
    }

    public IActionResult Details(int id)
    {
        List<Item> items = _itemDbContext.Items.ToList();

        //go through the list and give me the first one that matches my condition. If nothing matches, give me the default value instead (null)
        //the lambda just says, does the id of the current one match the id i was given as a paramater when the method was called?
        var item = items.FirstOrDefault(i => i.ItemId == id);
        //if it doesnt find it, it returns notfound
        if (item == null)
            return NotFound();

        //if it does find it, it hands the item were looking for to the view
        return View(item);
    }

//This is a GET method used to display the form for creating a new item
//this method is envoked when you navigate to the create page.
//[HttpGet] attribute makes this action method handle HTTP GET requests (used to retreive data from a server)
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    //This is a post method. Handles the submission of the form when the usre clicks "Create" button
    //takes an item object as a parameter which is inserted into the database 
    //checks if the model state is valid (that the FORM DATA has passed validation rules)

    [HttpPost] //this attribute makes the method able to handle HTTP POST requests. (submitting data to the server)
    public IActionResult Create(Item item)
    {
        if (ModelState.IsValid)
        {
            _itemDbContext.Items.Add(item); //if its valid, its added to the database using the current session (connection to the database)
            _itemDbContext.SaveChanges();
            return RedirectToAction(nameof(Table));
        }
        return View(item); //then after it has added the data, it redirects the user to the table view to show all items in the table
    }
    //old versions kept for reference - these used ViewBag instead of a ViewModel
    //public IActionResult Table()
    //{
    //    var items = GetItems();
    //    ViewBag.CurrentViewName = "Table";
    //    return View(items);
    //}

    //public IActionResult Grid()
    //{
    //    var items = GetItems();
    //    ViewBag.CurrentViewName = "Grid";
    //    return View(items);
    //}
}