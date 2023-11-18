using System;

namespace ShopBLL.Models;

public class User
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Roles { get; set; }
    public string? RefreshToken { get; set; }

    public User(){}
    public User(string name, string password)
    {
        this.Name = name;
        this.Password = password;
    }
    public User (Guid Id, string Name, string Email, string password, string roles)
    {
        this.Id = Id;
        this.Name = Name;
        this.Email = Email;
        this.Password = password;
        Roles = roles;  
    }

    public User(Guid Id, string Name, string Email,string refreshToken)
    {
        this.Id = Id;
        this.Name = Name;
        this.Email = Email;
        this.RefreshToken = refreshToken;
    }
}
