using CMSASPNETCoreWebAPI.SL.Enums;

namespace CMSASPNETCoreWebAPI.SL.DTOs;

public class PromotionsBoolReturnServiceResponse
{
    public PromotionsBoolReturnServiceResponse(bool executed, ServiceResponse serviceResponse)
    {
        Executed = executed;
        ServiceResponse = serviceResponse;
    }

    public PromotionsBoolReturnServiceResponse(ServiceResponse serviceResponse)
    {
        ServiceResponse = serviceResponse;
    }

    public bool? Executed { get; set; }
    public ServiceResponse ServiceResponse { get; set; }
}