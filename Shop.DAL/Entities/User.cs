using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.DAL.Entities;

public class User
{
    [Required]
    public Guid ID { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    [MaxLength(250)]
    public string Email { get; set; }

    public string? Password { get; set; }

    [Required]
    public string Roles { get; set; }
    
    public string? Refresh_Token { get; set; }

    public ICollection<Bucket> Buckets { get; set; }
    //public Bucket? bucket { get; set; }

    public User() { }

    public User(Guid ID, string Name, string Email, string password, string roles)
    {
        this.ID = ID;
        this.Name = Name;
        this.Email = Email;
        this.Password = password;
        this.Roles = roles;
    }
}
