using CMSASPNETCoreWebAPI.DAL.DTOs;
using CMSASPNETCoreWebAPI.DAL.Models;

namespace CMSASPNETCoreWebAPI.DAL;

public interface IMemberRepository
{
    List<Member> GetAllMembers();
    Member GetMember(int memberId);
    bool PostMember(Member member);
    bool PostAttachment(Attachment attachment, int memberId);
    bool UpdateMember(Member member);
    bool DeleteMember(int memberId);
}