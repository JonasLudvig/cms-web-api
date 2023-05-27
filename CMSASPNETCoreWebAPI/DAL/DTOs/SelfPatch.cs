namespace CMSASPNETCoreWebAPI.DAL.DTOs;

public record SelfPatch
{
    public SelfPatch(string id,
        string token,
        string role,
        string firstName,
        string lastName)
    {
        Id = id;
        Token = token;
        Role = role;
        FirstName = firstName;
        LastName = lastName;
    }

    public string Id { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}