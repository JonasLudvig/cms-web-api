using CMSASPNETCoreWebAPI.DAL.DTOs;
using CMSASPNETCoreWebAPI.DAL.Models;
using CMSASPNETCoreWebAPI.Utilities;
using Microsoft.EntityFrameworkCore;

namespace CMSASPNETCoreWebAPI.DAL;

public class MemberRepository : IMemberRepository
{
    private readonly ModelDbContext _dBContext;

    public MemberRepository(ModelDbContext dBContextObject)
    {
        _dBContext = dBContextObject;
    }

    public List<Member> GetAllMembers()
    {
        return _dBContext.Members.ToList();
    }

    public Member GetMember(int memberId)
    {
        return _dBContext.Members.Include(m => m.MemberImage).FirstOrDefault(m => m.Id == memberId);
    }

    public bool PostMember(Member member)
    {
        if (!Validator.ValidateMember(member)) return false;

        _dBContext.Members.Add(member);
        _dBContext.SaveChanges();

        return true;
    }

    public bool PostAttachment(Attachment attachment, int memberId)
    {
        var member = _dBContext.Members.Include(m => m.MemberImage).FirstOrDefault(m => m.Id == memberId);

        if (member == null)
            return false;

        var file = attachment.File;

        string base64 = FileConverter.ScaleAndConvertImage(file, 800, 800); ;
        MemberImage memberImage = new(base64);
        member.MemberImage = memberImage;

        _dBContext.SaveChanges();

        return true;
    }

    public bool UpdateMember(Member member)
    {
        var storeMember = _dBContext.Members.FirstOrDefault(c => c.Id == member.Id);
        if (storeMember is null) return false;

        storeMember.Name = member.Name;
        storeMember.Title = member.Title;
        storeMember.Description = member.Description;
        storeMember.ExperiencedSince = member.ExperiencedSince;
        storeMember.Stack = member.Stack;

        if (!Validator.ValidateMember(member)) return false;

        _dBContext.Members.Update(storeMember);
        _dBContext.SaveChanges();

        return true;
    }

    public bool DeleteMember(int itemId)
    {
        var storeItem = _dBContext.Members.Find(itemId);
        if (storeItem != null) _dBContext.Members.Remove(storeItem);
        _dBContext.SaveChanges();

        return true;
    }
}