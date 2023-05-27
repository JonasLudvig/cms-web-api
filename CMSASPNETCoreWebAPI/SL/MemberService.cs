using CMSASPNETCoreWebAPI.DAL;
using CMSASPNETCoreWebAPI.DAL.DTOs;
using CMSASPNETCoreWebAPI.DAL.Models;
using CMSASPNETCoreWebAPI.SL.DTOs;

namespace CMSASPNETCoreWebAPI.SL;

public class MemberService : IMemberService
{
    public IUnitOfWork _store;
    private readonly IConfiguration Configuration;

    public MemberService(IUnitOfWork store, IConfiguration configuration)
    {
        _store = store;
        Configuration = configuration;
    }

    public MembersListReturnServiceResponse GetAllMembers(string client)
    {
        if (client != Configuration["Client"]) return new MembersListReturnServiceResponse(Enums.ServiceResponse.Unauthorized);

        if (_store.UserRepository.GetAllUsers().Count <= 0) return new MembersListReturnServiceResponse(Enums.ServiceResponse.NotFound);

        return new MembersListReturnServiceResponse(_store.MemberRepository.GetAllMembers(), Enums.ServiceResponse.Ok);
    }

    public MembersMemberReturnServiceResponse GetMember(string client, int memberId)
    {
        if (client != Configuration["Client"]) return new MembersMemberReturnServiceResponse(Enums.ServiceResponse.Unauthorized);

        if (_store.MemberRepository.GetAllMembers().Count <= 0) return new MembersMemberReturnServiceResponse(Enums.ServiceResponse.NotFound);

        var member = _store.MemberRepository.GetMember(memberId);
        if (member == null) return new MembersMemberReturnServiceResponse(Enums.ServiceResponse.NotFound);

        return new MembersMemberReturnServiceResponse(member, Enums.ServiceResponse.Ok);
    }

    public MembersMemberImageReturnServiceResponse GetMemberImage(string cl, int memberId)
    {
        if (cl != Configuration["Cl"]) return new MembersMemberImageReturnServiceResponse(Enums.ServiceResponse.Unauthorized);

        if (_store.MemberRepository.GetAllMembers().Count <= 0) return new MembersMemberImageReturnServiceResponse(Enums.ServiceResponse.NotFound);

        var member = _store.MemberRepository.GetMember(memberId);
        if (member == null || member.MemberImage == null || member.MemberImage.Data == null) return new MembersMemberImageReturnServiceResponse(Enums.ServiceResponse.NotFound);

        byte[] bytes = Convert.FromBase64String(member.MemberImage.Data);

        return new MembersMemberImageReturnServiceResponse(bytes, Enums.ServiceResponse.Ok);
    }

    public MembersBoolReturnServiceResponse PostMember(Member member)
    {
        if (!_store.MemberRepository.PostMember(member)) return new MembersBoolReturnServiceResponse(false, Enums.ServiceResponse.BadRequest);

        return new MembersBoolReturnServiceResponse(true, Enums.ServiceResponse.Ok);
    }

    public MembersBoolReturnServiceResponse UpdateMember(Member member)
    {
        if (!_store.MemberRepository.UpdateMember(member)) return new MembersBoolReturnServiceResponse(false, Enums.ServiceResponse.BadRequest);

        return new MembersBoolReturnServiceResponse(true, Enums.ServiceResponse.Ok);
    }

    public MembersBoolReturnServiceResponse PostAttachment(Attachment attachment, int memberId)
    {
        if(!_store.MemberRepository.PostAttachment(attachment, memberId)) return new MembersBoolReturnServiceResponse(Enums.ServiceResponse.BadRequest);

        return new MembersBoolReturnServiceResponse(true, Enums.ServiceResponse.Ok);
    }

    public MembersBoolReturnServiceResponse DeleteMember(int memberId)
    {
        if(!_store.MemberRepository.DeleteMember(memberId)) return new MembersBoolReturnServiceResponse(Enums.ServiceResponse.BadRequest);

        return new MembersBoolReturnServiceResponse(true, Enums.ServiceResponse.Ok);
    }
}