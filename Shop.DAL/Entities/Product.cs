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
    public string Genre { get; set; }
    public string Description { get; set; }
    public Guid PhotoID { get; set; }
    public Guid? authorID { get; set; }
    public ICollection<Bucket> Buckets { get; set; }
    //public Bucket? bucket { get; set; }

}
