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

public class UserController : ControllerBase
{
    [Authorize("User")]
    [HttpGet("/users")]
    public IResult GetAllUsers([FromServices] IUserService service)
    {
        var request = service.GetAllUsers();

        if (request.ServiceResponse == ServiceResponse.NotFound) return Results.NotFound();

        return Results.Ok(request.ClientUsers);
    }

    [Authorize("User")]
    [HttpGet("/users/{usersId}")]
    public IResult GetUser([FromServices] IUserService service, string usersId)
    {
        var request = service.GetUser(usersId);

        if (request.ServiceResponse == ServiceResponse.NotFound) return Results.NotFound();

        return Results.Ok(request.ClientUser);
    }

    [Authorize("Admin")]
    [HttpPost("/users/post-user")]
    public IResult PostUser([FromServices] IUserService service, [FromBody] User user)
    {
        var request = service.PostUser(user);

        if (request.ServiceResponse == ServiceResponse.BadRequest) return Results.BadRequest();

        if (request.Executed == false) return Results.BadRequest();

        return Results.Ok();
    }

    [HttpPost("/users/post-admin")]
    public IResult PostAdmin([FromServices] IUserService service, [FromHeader(Name = "Client")] string client, [FromBody] User user)
    {
        var request = service.PostAdmin(client, user);

        if (request.ServiceResponse == ServiceResponse.Unauthorized) return Results.Unauthorized();
        if (request.ServiceResponse == ServiceResponse.BadRequest) return Results.BadRequest();

        if (request.Executed == false) return Results.BadRequest();

        return Results.Ok();
    }

    [HttpPost("/users/login/{userId}")]
    public IResult LoginUser([FromServices] IUserService service, string userId, [FromBody] string password)
    {
        var request = service.LoginUser(userId, password);

        if (request.ServiceResponse == ServiceResponse.Unauthorized) return Results.Unauthorized();
        else if (request.ServiceResponse == ServiceResponse.BadRequest) return Results.BadRequest();

        return Results.Ok(request.ClientUser);
    }

    [Authorize("Admin")]
    [HttpPost("/users/update-user")]
    public IResult UpdateUser([FromServices] IUserService service, [FromBody] UserPatch userPatch)
    {
        var request = service.UpdateUser(userPatch);

        if (request.ServiceResponse == ServiceResponse.BadRequest) return Results.BadRequest();

        if (request.Executed == false) return Results.BadRequest();

        return Results.Ok();
    }

    [Authorize("User")]
    [HttpPost("/users/update-self")]
    public IResult UpdateSelf([FromServices] IUserService service, [FromBody] SelfPatch selfPatch)
    {
        var request = service.UpdateSelf(selfPatch);
        
        if (request.ServiceResponse == ServiceResponse.BadRequest) return Results.BadRequest();

        if (request.Executed == false) return Results.BadRequest();

        return Results.Ok();
    }

    [Authorize("User")]
    [HttpPost("/users/update-password")]
    public IResult UpdatePassword([FromServices] IUserService service, [FromBody] PasswordPatch passwordPatch)
    {
        var loginRequest = service.LoginUser(passwordPatch.Id, passwordPatch.Password);

        if (loginRequest.ServiceResponse == ServiceResponse.Unauthorized) return Results.Unauthorized();
        else if (loginRequest.ServiceResponse == ServiceResponse.BadRequest) return Results.BadRequest();

        var request = service.UpdatePassword(passwordPatch);

        if (request.ServiceResponse == ServiceResponse.BadRequest) return Results.BadRequest();

        if (request.Executed == false) return Results.BadRequest();

        return Results.Ok();
    }

    [Authorize("Admin")]
    [HttpPost("/users/delete-user/{userId}")]
    public IResult DeleteUser([FromServices] IUserService service, string userId)
    {
        var result = service.DeleteUser(userId);

        if (result.ServiceResponse == ServiceResponse.NotFound) return Results.NotFound();
        else if (result.ServiceResponse == ServiceResponse.BadRequest) return Results.BadRequest();

        if (result.Executed == false) return Results.BadRequest();

        return Results.Ok();
    }
}