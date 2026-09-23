using Microsoft.AspNetCore.Mvc;
using MyShop.Models;
using Microsoft.EntityFrameworkCore;
using MyShop.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.DAL;

namespace MyShop.Controllers;

public class OrderController : Controller
{
    private readonly ItemDbContext _itemDbContext;

    public OrderController(ItemDbContext itemDbContext)
    {
        _itemDbContext = itemDbContext;
    }

    public async Task<IActionResult> Table()
    {
        List<Order> orders = await _itemDbContext.Orders.ToListAsync();
        return View(orders);
    }

    [HttpGet]
    public async Task<IActionResult> CreateOrderItem() //Displays the create order view in the async way
    {
        var items = await _itemDbContext.Items.ToListAsync(); //retrieves items and orders and converts to list
        var orders = await _itemDbContext.Orders.ToListAsync(); 
        var createOrderItemViewModel = new CreateOrderItemViewModel //creates new CreateOrderItemViewModel, three variables: OrderItem, ItemSelectList, OrderSelectList
        {
            OrderItem = new OrderItem(),

            ItemSelectList = items.Select(item => new SelectListItem //this wraps the information stored in "items" into the ItemSelectList
            {
                Value = item.ItemId.ToString(), // each SlectListItem has a value that is the content of the item and a TEXT that displays the time
                Text = item.ItemId.ToString() + ": " + item.Name
            }).ToList(),
            
            OrderSelectList = orders.Select(order => new SelectListItem //wraps the information of OrderIds, OrderDates and customer names into OrderSelectList
            {
                Value = order.OrderId.ToString(),
                Text = "Order" + order.OrderId.ToString() + ", Date: " + order.OrderDate + ", Customer: "+ order.Customer.Name
            }).ToList(),
        };
        return View(createOrderItemViewModel);
    }

    [HttpPost] //this following method is important. if users input ItemIds or OrderIds that dont exist in the DB, the OrderItem cant be created.
    public async Task<IActionResult> CreateOrderItem(OrderItem orderItem)
    {
        try
        { //therefore we need this try block to find out whether the user inputs exist.
            var newItem = _itemDbContext.Items.Find(orderItem.ItemId);
            var newOrder = _itemDbContext.Orders.Find(orderItem.OrderId);

            if (newItem == null || newOrder == null) //if they dont exist, return this
            {
                return BadRequest("Item or Order not found.");
            }
            //otherwise, create new OrderItem with the inputs and the price calculated (number bought * price)
            var newOrderItem = new OrderItem
            {
                ItemId = orderItem.ItemId,
                Item = newItem,
                Quantity = orderItem.Quantity,
                OrderId = orderItem.OrderId,
                Order = newOrder,
            };
            newOrderItem.OrderItemPrice = orderItem.Quantity * newOrderItem.Item.Price;

            _itemDbContext.OrderItems.Add(newOrderItem);
            await _itemDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Table));
        }
        catch
        {
            return BadRequest("OrderItem creation failed.");
        }
    }    
}