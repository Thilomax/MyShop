// using System; means include the system namespace, which gives us basic classes, common values and data types etc.
using System;

//this line defines a namespace MyShop.Models. A namespace is a way to organize code into a hierarchical structure. Theyre like packages in java
//this namespace will contain code for a Models folder.
namespace MyShop.Models
{
    public class Item //declares item class
    {

        // get; set; is just a short way of adding getters and setters
        public int ItemId { get; set; } // variables must start with upper case. C# convention
        
        //must be declared with default value (string.Empty). Can not have a null value
        public string Name { get; set; } = string.Empty;
        
        //decimal is like double
        public decimal Price { get; set; }

        //the ? after string? makes these nullable. So we don't need to fill them in.
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

    }
}