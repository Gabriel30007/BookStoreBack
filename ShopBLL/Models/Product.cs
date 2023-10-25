using System;

namespace ShopBLL.Models;

public class Product
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string PhotoID { get; set; }

    public Product(Guid id, DateTime createdOn, string name, decimal price, string description, string photoID)
    {
        Id = id;
        CreatedOn = createdOn;
        Name = name;
        Price = price;
        Description = description;
        PhotoID = photoID;
    }
}
