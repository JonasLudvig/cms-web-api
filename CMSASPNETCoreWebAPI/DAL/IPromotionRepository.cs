using CMSASPNETCoreWebAPI.DAL.Models;

namespace CMSASPNETCoreWebAPI.DAL;

public interface IPromotionRepository
{
    List<Promotion> GetAllPromotions();
    Promotion GetPromotion (int promotionId);
    bool PostPromotion(Promotion promotion);
    bool UpdatePromotion(Promotion promotion);
    bool DeletePromotion(int promotionId);
}