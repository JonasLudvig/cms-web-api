using CMSASPNETCoreWebAPI.DAL.Models;
using CMSASPNETCoreWebAPI.SL.Enums;

namespace CMSASPNETCoreWebAPI.SL.DTOs;

public class MembersMemberImageReturnServiceResponse
{
    public MembersMemberImageReturnServiceResponse(byte[] bytes, ServiceResponse serviceResponse)
    {
        Bytes = bytes;
        ServiceResponse = serviceResponse;
    }

    public MembersMemberImageReturnServiceResponse(ServiceResponse serviceResponse)
    {
        ServiceResponse = serviceResponse;
    }

    public byte[]? Bytes  { get; set; }
    public ServiceResponse ServiceResponse { get; set; }
}