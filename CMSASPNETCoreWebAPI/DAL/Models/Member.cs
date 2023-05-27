using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CMSASPNETCoreWebAPI.DAL.Models;

public record Member
{
    public Member(
        string name,
        string title,
        int experiencedSince,
        string stack,
        string description)
    {
        Name = name;
        Title = title;
        ExperiencedSince = experiencedSince;
        Stack = stack;
        Description = description;
    }

    public Member()
    {
    
    }

    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Title { get; set; }
    public int? ExperiencedSince { get; set; }
    public string? Stack { get; set; }
    public string? Description { get; set; }
    [JsonIgnore]
    public MemberImage? MemberImage { get; set; }
}