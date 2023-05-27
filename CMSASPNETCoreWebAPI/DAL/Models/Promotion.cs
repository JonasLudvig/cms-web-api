using System.ComponentModel.DataAnnotations;

namespace CMSASPNETCoreWebAPI.DAL.Models;

public record Promotion
{
    public Promotion(
    string header,
    string body,
    string link,
    string linkLabel,
    bool publish)
    {
        Header = header;
        Body = body;
        Link = link;
        LinkLabel = linkLabel;
        Publish = publish;
    }

    [Key]
    public int Id { get; set; }
    public string? Header { get; set; }
    public string Body { get; set; }
    public string? Link { get; set; }
    public string? LinkLabel { get; set; }
    public bool Publish { get; set; }
}