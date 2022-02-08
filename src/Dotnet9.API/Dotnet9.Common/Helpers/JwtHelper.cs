﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Dotnet9.Common.Helpers;

public class JwtHelper
{
    public static string IssueJwt(TokenModelJwt tokenModel)
    {
        var iss = Appsettings.App("Audience", "Issuer");
        var aud = Appsettings.App("Audience", "Audience");
        var secret = Appsettings.App("Audience", "Secret");

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, tokenModel.Uid.ToString()),
            new(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
            new(JwtRegisteredClaimNames.Exp,
                $"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),
            new(ClaimTypes.Expiration, DateTime.Now.AddSeconds(1000).ToString()),
            new(JwtRegisteredClaimNames.Iss, iss),
            new(JwtRegisteredClaimNames.Aud, aud)
        };

        claims.AddRange(tokenModel.Role!.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            iss,
            claims: claims,
            signingCredentials: creds);

        var jwtHandler = new JwtSecurityTokenHandler();
        var encodedJwt = jwtHandler.WriteToken(jwt);

        return encodedJwt;
    }

    public static TokenModelJwt SerializeJwt(string jwtStr)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var tokenModelJwt = new TokenModelJwt();

        if (string.IsNullOrWhiteSpace(jwtStr) || !jwtHandler.CanReadToken(jwtStr)) return tokenModelJwt;

        var jwtToken = jwtHandler.ReadJwtToken(jwtStr);

        jwtToken.Payload.TryGetValue(ClaimTypes.Role, out var role);

        tokenModelJwt = new TokenModelJwt
        {
            Uid = Convert.ToInt64(jwtToken.Id),
            Role = role == null ? "" : role.ToString()
        };

        return tokenModelJwt;
    }

    public static TokenModelJwt ParsingJwtToken(HttpContext httpContext)
    {
        const string authorizationKey = "Authorization";
        if (!httpContext.Request.Headers.ContainsKey(authorizationKey))
        {
            return null;
        }

        var tokenHeader = httpContext.Request.Headers[authorizationKey].ToString().Replace("Bearer ", "");
        var tm = SerializeJwt(tokenHeader);

        return tm;
    }
}

public class TokenModelJwt
{
    public long Uid { get; set; }

    public string? Role { get; set; }

    public string? Work { get; set; }
}