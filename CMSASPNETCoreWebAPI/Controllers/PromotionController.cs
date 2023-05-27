using CMSASPNETCoreWebAPI.SL.Enums;
using CMSASPNETCoreWebAPI.SL;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using CMSASPNETCoreWebAPI.DAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace CMSASPNETCoreWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableCors]

public class PromotionController : Controller
{
    [HttpGet("/promotions")]
    public IResult GetAllPromotions([FromServices] IPromotionService service, [FromHeader(Name = "Client")] string client)
    {
        var request = service.GetAllPromotions(client);

        if (request.ServiceResponse == ServiceResponse.NotFound) return Results.NotFound();

        if (request.ServiceResponse == ServiceResponse.Unauthorized) return Results.Unauthorized();

        return Results.Ok(request.Promotions);
    }

    [HttpGet("/promotions/{promotionId:int}")]
    public IResult GetPromotion([FromServices] IPromotionService service, [FromHeader(Name = "Client")] string client, int promotionId)
    {
        var request = service.GetPromotion(client, promotionId);

        if (request.ServiceResponse == ServiceResponse.NotFound) return Results.NotFound();

        if (request.ServiceResponse == ServiceResponse.Unauthorized) return Results.Unauthorized();

        return Results.Ok(request.Promotion);
    }

    [Authorize("Editor")]
    [HttpPost("/promotions/post-promotion")]
    public IResult PostPromotion([FromServices] IPromotionService service, [FromBody] Promotion promotion)
    {
        var request = service.PostPromotion(promotion);

        if (request.ServiceResponse == ServiceResponse.BadRequest) return Results.BadRequest();

        if (request.Executed == false) return Results.BadRequest();

        return Results.Ok();
    }

    [Authorize("Editor")]
    [HttpPost("/promotions/update-promotion")]
    public IResult UpdatePromotion([FromServices] IPromotionService service, [FromBody] Promotion promotion)
    {
        var request = service.UpdatePromotion(promotion);

        if (request.ServiceResponse == ServiceResponse.BadRequest) return Results.BadRequest();

        if (request.Executed == false) return Results.BadRequest();

        return Results.Ok();
    }

    [Authorize("Editor")]
    [HttpPost("/promotions/delete-promotion/{promotionId}")]
    public IResult DeleteMember([FromServices] IPromotionService service, int promotionId)
    {
        var request = service.DeletePromotion(promotionId);

        if (request.ServiceResponse == ServiceResponse.BadRequest) return Results.BadRequest();

        if (request.Executed == false) return Results.BadRequest();

        return Results.Ok();
    }
}