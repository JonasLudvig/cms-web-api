using CMSASPNETCoreWebAPI.SL.Enums;

namespace CMSASPNETCoreWebAPI.SL.DTOs;

public class MembersBoolReturnServiceResponse
{
    public MembersBoolReturnServiceResponse(bool executed, ServiceResponse serviceResponse)
    {
        Executed = executed;
        ServiceResponse = serviceResponse;
    }

    public MembersBoolReturnServiceResponse(ServiceResponse serviceResponse)
    {
        ServiceResponse = serviceResponse;
    }

    public bool? Executed { get; set; }
    public ServiceResponse ServiceResponse { get; set; }
}