using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSASPNETCoreWebAPI.DAL.Models;

public record User
{
    public User(string id,
        string password,
        string role,
        string firstName,
        string lastName)
    {
        Id = id;
        Password = password;
        Role = role;
        FirstName = firstName;
        LastName = lastName;
    }

    public User()
    {
    
    }

    [Key]
    public string Id { get; set; }
    [NotMapped]
    public string Password { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Token { get; set; }
}