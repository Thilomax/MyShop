//to always have the items and CurrentViewName is not practical. We may want to pass many data into the View.
//Good practice is then to wrap all data needed in a View to a ViewModel
//Sometimes called the Mode-View-ViewModel pattern (MVVM)

using MyShop.Models;

namespace MyShop.ViewModels
{
    public class ItemsViewModel
    {
        public IEnumerable<Item> Items;
        public string? CurrentViewName;

        public ItemsViewModel(IEnumerable<Item> items, string? currentViewName)
        {
            Items = items;
            CurrentViewName = currentViewName;
        }
    }
}

// now we need to add "using MyShop.ViewModels;" to ItemController 