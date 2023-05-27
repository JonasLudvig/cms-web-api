using CMSASPNETCoreWebAPI.DAL.DTOs;
using CMSASPNETCoreWebAPI.DAL.Models;
using CMSASPNETCoreWebAPI.SL.Enums;

namespace CMSASPNETCoreWebAPI.SL.DTOs;

public record UsersListReturnServiceResponse
{
    public UsersListReturnServiceResponse(List<User> users, ServiceResponse serviceResponse)
    {
        List<ClientUser> clientUsers = new();

        foreach (User user in users)
        {
            ClientUser clientUser = new()
            {
                Id = user.Id,
                Role = user.Role,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            clientUsers.Add(clientUser);
        }

        ClientUsers = clientUsers;
        ServiceResponse = serviceResponse;
    }

    public UsersListReturnServiceResponse(ServiceResponse serviceResponse)
    {
        ServiceResponse = serviceResponse;
    }

    public List<ClientUser>? ClientUsers { get; set; }
    public ServiceResponse ServiceResponse { get; set; }
}