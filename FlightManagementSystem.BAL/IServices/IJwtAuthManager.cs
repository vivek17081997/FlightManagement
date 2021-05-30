using FlightMangementSystem.Models.RequestModel.JWTModels;
using FlightMangementSystem.Models.ResponseModel.JWTModels;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlightManagementSystem.BAL.IServices
{
	public interface IJwtAuthManager
	{
        IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary { get; }
        JwtAuthResponseModel GenerateTokens(string userName, Claim[] claims, DateTime now);
        JwtAuthResponseModel Refresh(string refreshToken, string accessToken, DateTime now);
        void RemoveExpiredRefreshTokens(DateTime now);
        void RemoveRefreshTokenByUserName(string userName);
        (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token);
    }
}
