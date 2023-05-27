using CMSASPNETCoreWebAPI.DAL;
using CMSASPNETCoreWebAPI.DAL.DTOs;
using CMSASPNETCoreWebAPI.DAL.Models;
using CMSASPNETCoreWebAPI.SL.DTOs;
using CMSASPNETCoreWebAPI.Utilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CMSASPNETCoreWebAPI.SL;

public class UserService : IUserService
{
    public IUnitOfWork _store;
    private readonly IConfiguration Configuration;

    public UserService(IUnitOfWork store, IConfiguration configuration)
    {
        _store = store;
        Configuration = configuration;
    }

    public UsersListReturnServiceResponse GetAllUsers()
    {
        if (_store.UserRepository.GetAllUsers().Count <= 0) return new UsersListReturnServiceResponse(Enums.ServiceResponse.NotFound);

        return new UsersListReturnServiceResponse(_store.UserRepository.GetAllUsers(), Enums.ServiceResponse.Ok);
    }

    public UsersClientUserReturnServiceResponse GetUser(string userId)
    {
        var user = _store.UserRepository.GetUser(userId);
        if (user == null) return new UsersClientUserReturnServiceResponse(Enums.ServiceResponse.NotFound);
        return new UsersClientUserReturnServiceResponse(user, Enums.ServiceResponse.Ok);
    }

    public UsersBoolReturnServiceResponse PostUser(User user)
    {
        if (_store.UserRepository.GetAllUsers().Count <= 0) return new UsersBoolReturnServiceResponse(Enums.ServiceResponse.BadRequest);

        PasswordIssuer.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        if (!_store.UserRepository.PostUser(user)) return new UsersBoolReturnServiceResponse(false, Enums.ServiceResponse.BadRequest);

        return new UsersBoolReturnServiceResponse(true, Enums.ServiceResponse.Ok);
    }

    public UsersBoolReturnServiceResponse PostAdmin(string client, User user)
    {
        if (client != Configuration["Admin"]) return new UsersBoolReturnServiceResponse(false, Enums.ServiceResponse.Unauthorized);

        PasswordIssuer.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        if (!_store.UserRepository.PostUser(user)) return new UsersBoolReturnServiceResponse(false, Enums.ServiceResponse.BadRequest);

        return new UsersBoolReturnServiceResponse(true, Enums.ServiceResponse.Ok);
    }

    public UsersClientUserReturnServiceResponse LoginUser(string userId, string password)
    {
        if (!_store.UserRepository.LoginUser(userId, password)) return new UsersClientUserReturnServiceResponse(Enums.ServiceResponse.Unauthorized);

        var user = _store.UserRepository.GetUser(userId);
        if (user == null) return new UsersClientUserReturnServiceResponse(Enums.ServiceResponse.BadRequest);

        user.Token = IssueToken(user);

        if (user.Token == null) return new UsersClientUserReturnServiceResponse(Enums.ServiceResponse.BadRequest);

        return new UsersClientUserReturnServiceResponse(user, Enums.ServiceResponse.Ok);
    }

    public UsersBoolReturnServiceResponse UpdateUser(UserPatch userPatch)
    {
        if (!_store.UserRepository.UpdateUser(userPatch)) return new UsersBoolReturnServiceResponse(false, Enums.ServiceResponse.BadRequest);

        return new UsersBoolReturnServiceResponse(true, Enums.ServiceResponse.Ok);
    }

    public UsersBoolReturnServiceResponse UpdateSelf(SelfPatch selfPatch)
    {
        if (!Utilities.TokenHandler.ValidateTokenUserId(selfPatch.Token, selfPatch.Id)) return new UsersBoolReturnServiceResponse(false, Enums.ServiceResponse.Unauthorized);

        UserPatch userPatch = new()
        {
            Id = selfPatch.Id,
            Role = selfPatch.Role,
            FirstName = selfPatch.FirstName,
            LastName = selfPatch.LastName
        };

        if (!_store.UserRepository.UpdateUser(userPatch)) return new UsersBoolReturnServiceResponse(false, Enums.ServiceResponse.BadRequest);

        return new UsersBoolReturnServiceResponse(true, Enums.ServiceResponse.Ok);
    }

    public UsersBoolReturnServiceResponse UpdatePassword(PasswordPatch passwordPatch)
    {
        if (!_store.UserRepository.UpdatePassword(passwordPatch)) return new UsersBoolReturnServiceResponse(false, Enums.ServiceResponse.BadRequest);

        return new UsersBoolReturnServiceResponse(true, Enums.ServiceResponse.Ok);
    }

    public UsersBoolReturnServiceResponse DeleteUser(string userId)
    {
        var user = _store.UserRepository.GetUser(userId);
        if (user == null) return new UsersBoolReturnServiceResponse(Enums.ServiceResponse.NotFound);

        if (!_store.UserRepository.DeleteUser(userId)) return new UsersBoolReturnServiceResponse(false, Enums.ServiceResponse.BadRequest);

        return new UsersBoolReturnServiceResponse(true, Enums.ServiceResponse.Ok);
    }

    private protected string? IssueToken(User user)
    {
        string? signature = Configuration["Signature"];

        if (signature == null) return null;

        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(signature));

        string? issuer = Configuration["Issuer"];
        string? audience = Configuration["Audience"];

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Role, user.Role),
            new(ClaimTypes.GivenName, user.FirstName),
            }),

            Expires = DateTime.Now.AddMinutes(30),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}