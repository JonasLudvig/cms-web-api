using CMSASPNETCoreWebAPI.DAL.Models;
using CMSASPNETCoreWebAPI.SL.Enums;

namespace CMSASPNETCoreWebAPI.SL.DTOs;

public record MembersListReturnServiceResponse
{
    public MembersListReturnServiceResponse(List<Member> members, ServiceResponse serviceResponse)
    {
        Members = members;
        ServiceResponse = serviceResponse;
    }

    public MembersListReturnServiceResponse(ServiceResponse serviceResponse)
    {
        ServiceResponse = serviceResponse;
    }

    public List<Member>? Members { get; set; }
    public ServiceResponse ServiceResponse { get; set; }
}