using CMSASPNETCoreWebAPI.DAL.DTOs;
using CMSASPNETCoreWebAPI.DAL.Models;
using CMSASPNETCoreWebAPI.SL.DTOs;

namespace CMSASPNETCoreWebAPI.SL;

public interface IMemberService
{
    MembersListReturnServiceResponse GetAllMembers(string client);
    MembersMemberReturnServiceResponse GetMember(string client, int memberId);
    MembersMemberImageReturnServiceResponse GetMemberImage(string cl, int memberId);
    MembersBoolReturnServiceResponse PostMember(Member member);
    MembersBoolReturnServiceResponse UpdateMember(Member member);
    MembersBoolReturnServiceResponse PostAttachment(Attachment attachment, int memberId);
    MembersBoolReturnServiceResponse DeleteMember(int memberId);
}