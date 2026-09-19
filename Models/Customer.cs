namespace MyShop.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    // navigation property
    public virtual List<Order>? Orders { get; set; } // the virtual keyword enables lazy loading. Letting us load related data on demand, rather than loading all related data upfront.
}
