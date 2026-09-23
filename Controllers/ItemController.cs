using Microsoft.AspNetCore.Mvc;
using MyShop.DAL;
using MyShop.Models;
using MyShop.ViewModels; //needed so the controller can see the ItemsViewModel class


namespace MyShop.Controllers;

public class ItemController : Controller
{

    //to access the database, we need to add the following private readonly object along with changing the action methods
    private readonly IItemRepository _itemRepository;
    public ItemController(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

//we have changed all the methods to be asynchronous. That means all these methods that communicate with the database can be done while other methods are being executed leading to less wait time and better user experience
    public async Task<IActionResult> Table()
    {   // await keyword means that the method pauses execution asynchronously until the database 
        // finishes fetching the data, freeing up the thread to handle other incoming requests in the meantime.
        var items = await _itemRepository.GetAll();
        var itemsViewModel = new ItemsViewModel(items, "Table");
        return View(itemsViewModel);
    }

    public async Task<IActionResult> Grid()
    {
        var items = await _itemRepository.GetAll();
        var itemsViewModel = new ItemsViewModel(items, "Grid");
        return View(itemsViewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var item = await _itemRepository.GetItemById(id);
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
            await _itemRepository.Create(item);
            return RedirectToAction(nameof(Table));
        }
        return View(item);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var item = await _itemRepository.GetItemById(id);
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
            await _itemRepository.Update(item);
            return RedirectToAction(nameof(Table));
        }
        return View(item);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _itemRepository.GetItemById(id);
        if (item == null)
        {
            return NotFound();
        }
        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _itemRepository.Delete(id);
        return RedirectToAction(nameof(Table));
    }
}