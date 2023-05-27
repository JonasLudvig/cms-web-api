using CMSASPNETCoreWebAPI.DAL.DTOs;
using CMSASPNETCoreWebAPI.DAL.Models;

namespace CMSASPNETCoreWebAPI.DAL;
public interface IUserRepository
{
    List<User> GetAllUsers();
    User GetUser(string userId);
    bool PostUser(User user);
    bool LoginUser(string userId, string password);
    bool UpdateUser(UserPatch userId);
    bool UpdatePassword(PasswordPatch userId);
    bool DeleteUser(string id);
}