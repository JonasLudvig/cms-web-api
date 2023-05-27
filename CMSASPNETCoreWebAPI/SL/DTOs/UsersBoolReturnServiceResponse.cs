using CMSASPNETCoreWebAPI.DAL.Models;
using CMSASPNETCoreWebAPI.SL.Enums;

namespace CMSASPNETCoreWebAPI.SL.DTOs;

public record UsersBoolReturnServiceResponse
{
    public UsersBoolReturnServiceResponse(bool executed, ServiceResponse serviceResponse)
    {
        Executed = executed;
        ServiceResponse = serviceResponse;
    }

    public UsersBoolReturnServiceResponse(ServiceResponse serviceResponse)
    {
        ServiceResponse = serviceResponse;
    }

    public bool? Executed { get; set; }
    public ServiceResponse ServiceResponse { get; set; }
}