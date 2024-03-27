using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.DAL.Entities;

public class Product
{
    [Required]
    public Guid ID { get; set; }
    [Required]
    public DateTime CreatedOn { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }  
    public string Description { get; set; }
    public Guid PhotoID { get; set; }
    public Guid? authorID { get; set; }
    public Guid? genreID { get; set; }
    public ICollection<Bucket> Buckets { get; set; }
    //public Bucket? bucket { get; set; }

    public Product() { }

    public Product(Guid iD, DateTime createdOn, string name, decimal price, string description, Guid photoID, Guid? genreID, Guid? authorID)
    {
        ID = iD;
        CreatedOn = createdOn;
        Name = name;
        Price = price;
        Description = description;
        PhotoID = photoID;
        this.authorID = authorID;
        this.genreID = genreID;
    }
}
