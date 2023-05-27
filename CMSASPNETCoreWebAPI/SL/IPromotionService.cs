using CMSASPNETCoreWebAPI.DAL.Models;
using CMSASPNETCoreWebAPI.SL.DTOs;

namespace CMSASPNETCoreWebAPI.SL;

public interface IPromotionService
{
    PromotionsListReturnServiceResponse GetAllPromotions(string client);
    PromotionsPromotionReturnServiceResponse GetPromotion(string client, int promotionId);
    PromotionsBoolReturnServiceResponse PostPromotion(Promotion promotion);
    PromotionsBoolReturnServiceResponse UpdatePromotion(Promotion promotion);
    PromotionsBoolReturnServiceResponse DeletePromotion(int promotionId);
}