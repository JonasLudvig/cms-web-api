using CMSASPNETCoreWebAPI.DAL.DTOs;
using CMSASPNETCoreWebAPI.DAL.Models;
using CMSASPNETCoreWebAPI.SL.DTOs;

namespace CMSASPNETCoreWebAPI.SL;

public interface IUserService
{
    UsersListReturnServiceResponse GetAllUsers();
    UsersClientUserReturnServiceResponse GetUser(string userId);
    UsersBoolReturnServiceResponse PostUser(User user);
    UsersBoolReturnServiceResponse PostAdmin(string client, User user);
    UsersClientUserReturnServiceResponse LoginUser(string userId, string password);
    UsersBoolReturnServiceResponse UpdateUser(UserPatch userPatch);
    UsersBoolReturnServiceResponse UpdateSelf(SelfPatch selfPatch);
    UsersBoolReturnServiceResponse UpdatePassword(PasswordPatch passwordPatch);
    UsersBoolReturnServiceResponse DeleteUser(string id);
}