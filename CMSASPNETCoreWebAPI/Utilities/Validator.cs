using CMSASPNETCoreWebAPI.DAL.Models;
using System.Text.RegularExpressions;

namespace CMSASPNETCoreWebAPI.Utilities;

public class Validator
{
    public static bool ValidateMember(Member member)
    {
        var name = "^.{1,70}$";
        var standard = "^.{0,70}$";
        var stack = "^.{0,2000}$";
        var description = "^.{0,2000}$";
        var experiencedSince = "^.{0,4}$";

        if (!Regex.IsMatch(member.Name, name)
            || !Regex.IsMatch(member.Title, standard)
            || !Regex.IsMatch(member.Stack, stack)
            || !Regex.IsMatch(member.ExperiencedSince.ToString(), experiencedSince)
            || !Regex.IsMatch(member.Description, description))
            return false;

        return true;
    }

    public static bool ValidateUser(User user)
    {
        var id = "^.{1,150}$";
        var password = "^.{6,128}$";
        var role = "^(Admin|Editor|Reader)$";
        var standard = "^.{1,70}$";

        if (!Regex.IsMatch(user.Id, id)
            || !Regex.IsMatch(user.Password, password)
            || !Regex.IsMatch(user.Role, role)
            || !Regex.IsMatch(user.FirstName, standard)
            || !Regex.IsMatch(user.LastName, standard))
            return false;

        return true;
    }

    public static bool ValidateUserData(User user)
    {
        var id = "^.{1,150}$";
        var role = "^(Admin|Editor|Reader)$";
        var standard = "^.{1,70}$";

        if (!Regex.IsMatch(user.Id, id)
            || !Regex.IsMatch(user.Role, role)
            || !Regex.IsMatch(user.FirstName, standard)
            || !Regex.IsMatch(user.LastName, standard))
            return false;

        return true;
    }

    public static bool ValidatePromotion(Promotion promotion)
    {
        var standard = "^.{0,70}$";
        var body = "^.{1,200}$";

        if (!Regex.IsMatch(promotion.Header, standard)
            || !Regex.IsMatch(promotion.Body, body)
            || !Regex.IsMatch(promotion.Link, standard)
            || !Regex.IsMatch(promotion.LinkLabel, standard))
            return false;

        return true;
    }
}