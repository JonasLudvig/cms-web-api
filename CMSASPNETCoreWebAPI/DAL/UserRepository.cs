using CMSASPNETCoreWebAPI.DAL.DTOs;
using CMSASPNETCoreWebAPI.DAL.Models;
using CMSASPNETCoreWebAPI.Utilities;

namespace CMSASPNETCoreWebAPI.DAL;

public class UserRepository : IUserRepository
{
    private readonly ModelDbContext _dBContext;

    public UserRepository(ModelDbContext dBContextObject)
    {
        _dBContext = dBContextObject;
    }

    public List<User> GetAllUsers()
    {
        return _dBContext.Users.ToList();
    }

    public User GetUser(string userId)
    {
        return _dBContext.Users.FirstOrDefault(u => u.Id == userId)!;
    }

    public bool PostUser(User user)
    {
        if (!Validator.ValidateUserData(user)) return false;

        _dBContext.Add(user);
        _dBContext.SaveChanges();
        return true;
    }

    public bool LoginUser(string userId, string password)
    {
        var user = GetUser(userId);

        if (user == null)
            return false;

        if (!PasswordIssuer.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            return false;

        _dBContext.SaveChanges();

        return true;
    }

    public bool UpdateUser(UserPatch userPatch)
    {
        var user = GetUser(userPatch.Id);

        if (user == null)
            return false;

        user.Role = userPatch.Role;
        user.FirstName = userPatch.FirstName;
        user.LastName = userPatch.LastName;

        if (!Validator.ValidateUserData(user)) return false;

        _dBContext.Update(user);
        _dBContext.SaveChanges();

        return true;
    }

    public bool UpdatePassword(PasswordPatch passwordPatch)
    {
        var user = GetUser(passwordPatch.Id);

        user.Password = passwordPatch.RequestedPassword;

        if (!Validator.ValidateUser(user)) return false;

        if (user == null)
            return false;

        PasswordIssuer.CreatePasswordHash(passwordPatch.RequestedPassword, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        _dBContext.Update(user);
        _dBContext.SaveChanges();

        return true;
    }

    public bool DeleteUser(string userId)
    {
        var userIsStored = false;
        foreach (var dBUser in _dBContext.Users) if (dBUser.Id == userId) userIsStored = true;

        if (!userIsStored)
            return false;

        var user = GetUser(userId);
        _dBContext.Remove(user);
        _dBContext.SaveChanges();

        return true;
    }
}