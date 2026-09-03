using Microsoft.AspNetCore.Mvc;

namespace MyShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        //IActionResult is the action method called index. (I for interface. this method sends the view on the home page.)
        public IActionResult Index()
        {
            return View();
            //Since this method is in HomeController, and the name is Index, it will return the view in /Home/Index
        }
    }
}