using CMSASPNETCoreWebAPI.DAL.Models;
using CMSASPNETCoreWebAPI.Utilities;

namespace CMSASPNETCoreWebAPI.DAL;

public class PromotionRepository : IPromotionRepository
{
    private readonly ModelDbContext _dBContext;

    public PromotionRepository(ModelDbContext dBContextObject)
    {
        _dBContext = dBContextObject;
    }
    public List<Promotion> GetAllPromotions()
    {
        return _dBContext.Promotions.ToList();
    }
    public Promotion GetPromotion(int promotionId)
    {
        return _dBContext.Promotions.FirstOrDefault(u => u.Id == promotionId)!;
    }
    public bool PostPromotion(Promotion promotion)
    {
        if (_dBContext.Promotions.ToList().Count >= 1) return false;

        if (!Validator.ValidatePromotion(promotion)) return false;

        _dBContext.Promotions.Add(promotion);
        _dBContext.SaveChanges();
        return true;
    }
    public bool UpdatePromotion(Promotion promotion)
    {
        var result = _dBContext.Promotions.FirstOrDefault(c => c.Id == promotion.Id);
        if (result is null) return false;

        result.Header = promotion.Header;
        result.Body = promotion.Body;
        result.Link = promotion.Link;
        result.LinkLabel = promotion.LinkLabel;
        result.Publish = promotion.Publish;

        if (!Validator.ValidatePromotion(promotion)) return false;

        _dBContext.Promotions.Update(result);
        _dBContext.SaveChanges();
        return true;
    }
    public bool DeletePromotion(int promotionId)
    {
        var storePromotion = _dBContext.Promotions.Find(promotionId);
        if (storePromotion != null) _dBContext.Promotions.Remove(storePromotion);
        _dBContext.SaveChanges();
        return true;
    }
}