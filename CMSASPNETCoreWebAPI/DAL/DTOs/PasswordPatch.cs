namespace CMSASPNETCoreWebAPI.DAL.DTOs;

public record PasswordPatch
{
    public PasswordPatch(string id,
        string password,
        string requestedPassword)
    {
        Id = id;
        Password = password;
        RequestedPassword = requestedPassword;
    }

    public string Id { get; set; }
    public string Password { get; set; }
    public string RequestedPassword { get; set; }
}