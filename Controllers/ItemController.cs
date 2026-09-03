using Microsoft.AspNetCore.Mvc;
using MyShop.Models;

namespace MyShop.Controllers;

public class ItemController : Controller
{
    public IActionResult Table() //this is an action method. Action methods correspond 
    //to a user action, like viewing items, displaying details, submitting forms.
    //These methods usually return an IActionResult, which could be a VIEW, a Redirect, JSON, or other.
    //In this example, the Table action creates a list of items, sets a value in ViewBag and returns a view that will render the list of items
    {
        //creating items in mock fashion
        var items = new List<Item>();
        var item1 = new Item();
        item1.ItemId = 1;
        item1.Name = "Pizza";
        item1.Price = 60;

        var item2 = new Item
        {
            //setting values to ViewBag???
            ItemId = 2,
            Name = "Fried Chicken Leg",
            Price = 15
        };

        var item3 = new Item
        {
            ItemId = 3,
            Name = "Thilos special soup",
            Price = 9999
        };

        items.Add(item1);
        items.Add(item2);
        items.Add(item3);


        // a viewbag is a dynamic property used to pass data from a controller to a view.
        ViewBag.CurrentViewName = "List of Shop Items";
        return View(items);
    }
}