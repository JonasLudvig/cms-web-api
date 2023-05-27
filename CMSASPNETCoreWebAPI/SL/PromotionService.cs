using CMSASPNETCoreWebAPI.DAL;
using CMSASPNETCoreWebAPI.DAL.Models;
using CMSASPNETCoreWebAPI.SL.DTOs;

namespace CMSASPNETCoreWebAPI.SL;

public class PromotionService : IPromotionService
{
    public IUnitOfWork _store;
    private readonly IConfiguration Configuration;

    public PromotionService(IUnitOfWork store, IConfiguration configuration)
    {
        _store = store;
        Configuration = configuration;
    }

    public PromotionsListReturnServiceResponse GetAllPromotions(string client)
    {
        if (client != Configuration["Client"]) return new PromotionsListReturnServiceResponse(Enums.ServiceResponse.Unauthorized);

        if (_store.PromotionRepository.GetAllPromotions().Count <= 0) return new PromotionsListReturnServiceResponse(Enums.ServiceResponse.NotFound);

        return new PromotionsListReturnServiceResponse(_store.PromotionRepository.GetAllPromotions(), Enums.ServiceResponse.Ok);
    }

    public PromotionsPromotionReturnServiceResponse GetPromotion(string client, int promotionId)
    {
        if (client != Configuration["Client"]) return new PromotionsPromotionReturnServiceResponse(Enums.ServiceResponse.Unauthorized);

        if (_store.PromotionRepository.GetAllPromotions().Count <= 0) return new PromotionsPromotionReturnServiceResponse(Enums.ServiceResponse.NotFound);

        var promotion = _store.PromotionRepository.GetPromotion(promotionId);
        if (promotion == null) return new PromotionsPromotionReturnServiceResponse(Enums.ServiceResponse.NotFound);

        return new PromotionsPromotionReturnServiceResponse(promotion, Enums.ServiceResponse.Ok);
    }

    public PromotionsBoolReturnServiceResponse PostPromotion(Promotion promotion)
    {
        if (!_store.PromotionRepository.PostPromotion(promotion)) return new PromotionsBoolReturnServiceResponse(false, Enums.ServiceResponse.BadRequest);

        return new PromotionsBoolReturnServiceResponse(true, Enums.ServiceResponse.Ok);
    }

    public PromotionsBoolReturnServiceResponse UpdatePromotion(Promotion promotion)
    {
        if (!_store.PromotionRepository.UpdatePromotion(promotion)) return new PromotionsBoolReturnServiceResponse(false, Enums.ServiceResponse.BadRequest);

        return new PromotionsBoolReturnServiceResponse(true, Enums.ServiceResponse.Ok);
    }
    public PromotionsBoolReturnServiceResponse DeletePromotion(int promotionId)
    {
        if (!_store.PromotionRepository.DeletePromotion(promotionId)) return new PromotionsBoolReturnServiceResponse(Enums.ServiceResponse.BadRequest);

        return new PromotionsBoolReturnServiceResponse(true, Enums.ServiceResponse.Ok);
    }
}