using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using MyShop.ViewModels; //needed so the controller can see the ItemsViewModel class


namespace MyShop.Controllers;

public class ItemController : Controller
{
    public IActionResult Table() //this is an action method. Action methods correspond 
    //to a user action, like viewing items, displaying details, submitting forms.
    //These methods usually return an IActionResult, which could be a VIEW, a Redirect, JSON, or other.
    //In this example, the Table action gets a list of items, packs them into a ViewModel and returns a view that will render the list of items
    {
        //the items are no longer created here - they come from the GetItems() method below,
        //so both Table and Grid can reuse the same list instead of duplicating it
        var items = GetItems();

        // a viewbag is a dynamic property used to pass data from a controller to a view.
        //We no longer use it here - instead both the item list AND the view name are bundled
        //into ONE object: a ViewModel. A ViewModel is just a class made to carry exactly the data one view needs.
        //Advantage over ViewBag: it's strongly typed, so the compiler catches typos and the view gets IntelliSense.
        //ViewBag is dynamic, so mistakes only show up at runtime.
        var itemsViewModel = new ItemsViewModel(items, "Table");
        return View(itemsViewModel);
    }

    //same as Table, but passes "Grid" as the view name - a different way of displaying the exact same data
    public IActionResult Grid()
    {
        var items = GetItems();
        var itemsViewModel = new ItemsViewModel(items, "Grid");
        return View(itemsViewModel);
    }

    public IActionResult Details(int id)
    {
        var items = GetItems();

        //go through the list and give me the first one that matches my condition. If nothing matches, give me the default value instead (null)
        //the lambda just says, does the id of the current one match the id i was given as a paramater when the method was called?
        var item = items.FirstOrDefault(i => i.ItemId == id);
        //if it doesnt find it, it returns notfound
        if (item == null)
            return NotFound();

        //if it does find it, it hands the item were looking for to the view
        return View(item);
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

    //this is NOT an action method - it's a normal helper method.
    //It doesn't return IActionResult and isn't tied to a URL, it just builds and returns the hardcoded (mock) data.
    public List<Item> GetItems()
    {
        //creating items in mock fashion
        var items = new List<Item>();
        var item1 = new Item
        {
            //object initializer syntax - setting the properties of the Item object
            ItemId = 1,
            Name = "Pizza",
            Price = 150,
            Description = "Delicious Italian dish with a thin crust topped with tomato sauce, cheese, and various toppings.",
            ImageUrl = "/images/pizza.jpg"
        };

        var item2 = new Item
        {
            ItemId = 2,
            Name = "Fried Chicken Leg",
            Price = 20,
            Description = "Crispy and succulent chicken leg that is deep-fried to perfection, often served as a popular fast food item.",
            ImageUrl = "/images/fried_chicken.jpg"
        };

        var item3 = new Item
        {
            ItemId = 3,
            Name = "French Fries",
            Price = 50,
            Description = "Crispy, golden-brown potato slices seasoned with salt and often served as a popular side dish or snack.",
            ImageUrl = "/images/suspicious_stew.png"
        };

        var item4 = new Item
        {
            ItemId = 4,
            Name = "Grilled Ribs",
            Price = 250,
            Description = "Tender and flavorful ribs grilled to perfection, usually served with barbecue sauce.",
            ImageUrl = "/images/suspicious_stew.png"
        };

        var item5 = new Item
        {
            ItemId = 5,
            Name = "Tacos",
            Price = 150,
            Description = "Tortillas filled with various ingredients such as seasoned meat, vegetables, and salsa, folded into a delicious handheld meal.",
            ImageUrl = "/images/suspicious_stew.png"
        };

        var item6 = new Item
        {
            ItemId = 6,
            Name = "Fish and Chips",
            Price = 180,
            Description = "Classic British dish featuring battered and deep-fried fish served with thick-cut fried potatoes.",
            ImageUrl = "/images/pizza.jpg"
        };

        var item7 = new Item
        {
            ItemId = 7,
            Name = "Cider",
            Price = 50,
            Description = "Refreshing alcoholic beverage made from fermented apple juice, available in various flavors.",
            ImageUrl = "/images/pizza.jpg"
        };

        var item8 = new Item
        {
            ItemId = 8,
            Name = "Coke",
            Price = 30,
            Description = "Popular carbonated soft drink known for its sweet and refreshing taste.",
            ImageUrl = "/images/pizza.jpg"
        };

        //adding each item to the list and handing it back to whoever called this method
        items.Add(item1);
        items.Add(item2);
        items.Add(item3);
        items.Add(item4);
        items.Add(item5);
        items.Add(item6);
        items.Add(item7);
        items.Add(item8);
        return items;
    }
}