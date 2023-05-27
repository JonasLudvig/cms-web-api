using CMSASPNETCoreWebAPI.DAL.Models;
using CMSASPNETCoreWebAPI.SL.Enums;

namespace CMSASPNETCoreWebAPI.SL.DTOs;

public class MembersMemberReturnServiceResponse
{
    public MembersMemberReturnServiceResponse(Member member, ServiceResponse serviceResponse)
    {
        Member = member;
        ServiceResponse = serviceResponse;
    }

    public MembersMemberReturnServiceResponse(ServiceResponse serviceResponse)
    {
        ServiceResponse = serviceResponse;
    }

    public Member? Member { get; set; }
    public ServiceResponse ServiceResponse { get; set; }
}