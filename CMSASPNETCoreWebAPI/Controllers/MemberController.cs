using CMSASPNETCoreWebAPI.DAL.DTOs;
using CMSASPNETCoreWebAPI.DAL.Models;
using CMSASPNETCoreWebAPI.SL;
using CMSASPNETCoreWebAPI.SL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CMSASPNETCoreWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableCors]

public class MemberController : Controller
{
    [HttpGet("/members")]
    public IResult GetAllMembers([FromServices] IMemberService service, [FromHeader(Name = "Client")] string client)
    {
        var request = service.GetAllMembers(client);
        
        if (request.ServiceResponse == ServiceResponse.NotFound) return Results.NotFound();

        if (request.ServiceResponse == ServiceResponse.Unauthorized) return Results.Unauthorized();

        return Results.Ok(request.Members);
    }

    [HttpGet("/members-details")]
    public IResult GetAllMembersDetails([FromServices] IMemberService service, [FromHeader(Name = "Client")] string client)
    {
        var request = service.GetAllMembers(client);

        if (request.ServiceResponse == ServiceResponse.NotFound) return Results.NotFound();

        if (request.ServiceResponse == ServiceResponse.Unauthorized) return Results.Unauthorized();

        if (request.Members == null) return Results.BadRequest();

        return Results.Ok(request.Members);
    }

    [HttpGet("/members-image/{memberId:int}")]
    public IActionResult GetMemberImage([FromServices] IMemberService service, int memberId, [FromQuery] string cl)
    {
        var request = service.GetMemberImage(cl, memberId);

        if (request.ServiceResponse == ServiceResponse.Unauthorized) return Unauthorized();

        if (request.ServiceResponse == ServiceResponse.NotFound) return NotFound();

        if (request.Bytes == null) return NotFound();

        return File(request.Bytes, "image/jpeg");
    }

    [HttpGet("/members/{memberId:int}")]
    public IResult GetMember([FromServices] IMemberService service, [FromHeader(Name = "Client")] string client, int memberId)
    {
        var request = service.GetMember(client, memberId);

        if (request.ServiceResponse == ServiceResponse.NotFound) return Results.NotFound();

        if (request.ServiceResponse == ServiceResponse.Unauthorized) return Results.Unauthorized();

        return Results.Ok(request.Member);
    }

    [Authorize("Editor")]
    [HttpPost("/members/post-member")]
    public IResult PostMember([FromServices] IMemberService service, [FromBody] Member member)
    {
        var request = service.PostMember(member);

        if (request.ServiceResponse == ServiceResponse.BadRequest) return Results.BadRequest();

        if (request.Executed == false) return Results.BadRequest();

        return Results.Ok();
    }

    [Authorize("Editor")]
    [HttpPost("/members/update-member")]
    public IResult UpdateMember([FromServices] IMemberService service, [FromBody] Member member)
    {
        var request = service.UpdateMember(member);

        if (request.ServiceResponse == ServiceResponse.BadRequest) return Results.BadRequest();

        if (request.Executed == false) return Results.BadRequest();

        return Results.Ok();
    }

    [Authorize("Editor")]
    [HttpPost("/members/post-attachment/{memberId}")]
    public IResult PostAttachment([FromServices] IMemberService service, [FromForm] Attachment attachment, int memberId)
    {
        var request = service.PostAttachment(attachment, memberId);

        if (request.ServiceResponse == ServiceResponse.BadRequest) return Results.BadRequest();

        if (request.Executed == false) return Results.BadRequest();

        return Results.Ok();
    }

    [Authorize("Editor")]
    [HttpPost("/users/delete-member/{memberId}")]
    public IResult DeleteMember([FromServices] IMemberService service, int memberId)
    {
        var request = service.DeleteMember(memberId);

        if (request.ServiceResponse == ServiceResponse.BadRequest) return Results.BadRequest();

        if (request.Executed == false) return Results.BadRequest();

        return Results.Ok();
    }
}