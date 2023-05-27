namespace CMSASPNETCoreWebAPI.DAL.DTOs;

public record UserPatch
{
    public UserPatch(string id,
        string role,
        string firstName,
        string lastName)
    {
        Id = id;
        Role = role;
        FirstName = firstName;
        LastName = lastName;
    }

    public UserPatch() { }

    public string Id { get; set; }
    public string Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}