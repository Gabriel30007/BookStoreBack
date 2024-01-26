using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.DAL.Entities;

public class Bucket
{
    [Required]
    public Guid ID { get; set; }
    [Required]
    public DateTime CreatedOn { get; set; }
    public Guid? userID { get; set; }
    public Guid? productID { get; set; }
    public decimal Price { get; set; }

    public Bucket() { }
    public Bucket(Guid iD, DateTime createdOn, Guid? userID, Guid? productID, decimal price)
    {
        ID = iD;
        CreatedOn = createdOn;
        this.userID = userID;
        this.productID = productID;
        Price = price;
    }
}
