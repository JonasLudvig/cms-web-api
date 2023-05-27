using System.IdentityModel.Tokens.Jwt;

namespace CMSASPNETCoreWebAPI.Utilities;

public class TokenHandler
{
    public static bool ValidateTokenUserId(string token, string userIdClaim)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jsonToken = tokenHandler.ReadJwtToken(token);
        var payload = jsonToken.Payload.ToArray();

        var userId = payload[0].Value.ToString();

        if (userId != userIdClaim)
            return false;

        return true;
    }
}