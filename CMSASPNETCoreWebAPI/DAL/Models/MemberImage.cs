using System.ComponentModel.DataAnnotations;

namespace CMSASPNETCoreWebAPI.DAL.Models;

public class MemberImage
{
    public MemberImage(string data)
    {
        Data = data;
    }

    [Key]
    public int Id { get; set; }
    public string? Data { get; set; }
    public int MemberId { get; set; }
}