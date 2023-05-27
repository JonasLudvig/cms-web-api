using CMSASPNETCoreWebAPI.DAL.Models;
using CMSASPNETCoreWebAPI.SL.Enums;

namespace CMSASPNETCoreWebAPI.SL.DTOs;

public class PromotionsListReturnServiceResponse
{
    public PromotionsListReturnServiceResponse(List<Promotion> promotions, ServiceResponse serviceResponse)
    {
        Promotions = promotions;
        ServiceResponse = serviceResponse;
    }

    public PromotionsListReturnServiceResponse(ServiceResponse serviceResponse)
    {
        ServiceResponse = serviceResponse;
    }

    public List<Promotion>? Promotions { get; set; }
    public ServiceResponse ServiceResponse { get; set; }
}