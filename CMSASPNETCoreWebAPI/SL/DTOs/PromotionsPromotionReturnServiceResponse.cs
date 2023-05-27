using CMSASPNETCoreWebAPI.DAL.Models;
using CMSASPNETCoreWebAPI.SL.Enums;

namespace CMSASPNETCoreWebAPI.SL.DTOs;

public class PromotionsPromotionReturnServiceResponse
{
    public PromotionsPromotionReturnServiceResponse(Promotion promotion, ServiceResponse serviceResponse)
    {
        Promotion = promotion;
        ServiceResponse = serviceResponse;
    }

    public PromotionsPromotionReturnServiceResponse(ServiceResponse serviceResponse)
    {
        ServiceResponse = serviceResponse;
    }

    public Promotion? Promotion { get; set; }
    public ServiceResponse ServiceResponse { get; set; }
}