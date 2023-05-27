namespace CMSASPNETCoreWebAPI.DAL.DTOs;

public record ClientUser
{
    public ClientUser(string id,
        string role,
        string firstName,
        string lastName,
        string token)
    {
        Id = id;
        Role = role;
        FirstName = firstName;
        LastName = lastName;
        Token = token;
    }

    public ClientUser() { }

    public string Id { get; set; }
    public string Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Token { get; set; }
}