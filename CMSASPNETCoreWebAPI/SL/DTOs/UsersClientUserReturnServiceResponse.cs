using CMSASPNETCoreWebAPI.DAL.DTOs;
using CMSASPNETCoreWebAPI.DAL.Models;
using CMSASPNETCoreWebAPI.SL.Enums;

namespace CMSASPNETCoreWebAPI.SL.DTOs;

public class UsersClientUserReturnServiceResponse
{
    public UsersClientUserReturnServiceResponse(User user, ServiceResponse serviceResponse)
    {
        ClientUser clientUser = new()
        {
            Id = user.Id,
            Role = user.Role,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = user.Token
        };

        ClientUser = clientUser;

        ServiceResponse = serviceResponse;
    }

    public UsersClientUserReturnServiceResponse(ServiceResponse serviceResponse)
    {
        ServiceResponse = serviceResponse;
    }

    public ClientUser? ClientUser { get; set; }
    public ServiceResponse ServiceResponse { get; set; }
}