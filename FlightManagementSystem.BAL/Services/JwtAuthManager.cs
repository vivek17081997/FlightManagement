using FlightManagementSystem.BAL.IServices;
using FlightMangementSystem.Models.RequestModel.JWTModels;
using FlightMangementSystem.Models.ResponseModel.AccountModels;
using FlightMangementSystem.Models.ResponseModel.JWTModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FlightManagementSystem.BAL.Services
{
	public class JwtAuthManager: IJwtAuthManager
	{
        #region variables
        public IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary => _usersRefreshTokens.ToImmutableDictionary();
        private readonly ConcurrentDictionary<string, RefreshToken> _usersRefreshTokens;  
        private readonly JwtTokenConfigModel _jwtTokenConfig;
        private readonly byte[] _secret;
		#endregion


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="jwtTokenConfig"></param>
		public JwtAuthManager(JwtTokenConfigModel jwtTokenConfig)
        {
            _jwtTokenConfig = jwtTokenConfig;
            _usersRefreshTokens = new ConcurrentDictionary<string, RefreshToken>();
            _secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret);
        }


        /// <summary>
        /// Remove Expired Refresh Tokens
        /// </summary>
        /// <param name="now"></param>
        public void RemoveExpiredRefreshTokens(DateTime now)
        {
            var expiredTokens = _usersRefreshTokens.Where(x => x.Value.ExpireAt < now).ToList();
            foreach (var expiredToken in expiredTokens)
            {
                _usersRefreshTokens.TryRemove(expiredToken.Key, out _);
            }
        }

        /// <summary>
        /// Removes the Refresh token by username
        /// </summary>
        /// <param name="userName"></param>
        public void RemoveRefreshTokenByEmail(string email)
        {
            var refreshTokens = _usersRefreshTokens.Where(x => x.Value.Email == email).ToList();
            foreach (var refreshToken in refreshTokens)
            {
                _usersRefreshTokens.TryRemove(refreshToken.Key, out _);
            }
        }

        /// <summary>
        /// Generates Token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="claims"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        public JwtAuthResponseModel GenerateTokens(string email, Claim[] claims, DateTime now)
        {
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);

            //todo pass the issuer and the audiance from the appsetting
            var jwtToken = new JwtSecurityToken(
                _jwtTokenConfig.Issuer,
                shouldAddAudienceClaim ? _jwtTokenConfig.Audience : string.Empty,
                claims,
                expires: now.AddMinutes(_jwtTokenConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var refreshToken = new RefreshToken
            {
                Email = email,
                TokenString = GenerateRefreshTokenString(),
                ExpireAt = now.AddMinutes(_jwtTokenConfig.RefreshTokenExpiration)
            };

            //todo vivekv add or update the token so that it can be accessed for futer use
            //_usersRefreshTokens.AddOrUpdate(refreshToken.TokenString, refreshToken, (_, _) => refreshToken);

            return new JwtAuthResponseModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }


        /// <summary>
        /// Refresh the JWT Token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="accessToken"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        public JwtAuthResponseModel Refresh(string refreshToken, string accessToken, DateTime now)
        {
            var (principal, jwtToken) = DecodeJwtToken(accessToken);

            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw new SecurityTokenException("Invalid token");
            }

            var email = principal.Identity?.Name;

            //todo vivek cheking the token and updating that
            //if (!_usersRefreshTokens.TryGetValue(refreshToken, out var existingRefreshToken))
            if (_usersRefreshTokens.TryGetValue(refreshToken, out var existingRefreshToken))
            {
                throw new SecurityTokenException("Invalid token");
            }

            //if (existingRefreshToken.Email != email || existingRefreshToken.ExpireAt < now)
            //{
            //    throw new SecurityTokenException("Invalid token");
            //}

            return GenerateTokens(email, principal.Claims.ToArray(), now); 
        }


        /// <summary>
        /// Decode the JWT Token and identify the claims and the expiration
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new SecurityTokenException("Invalid token");
            }
            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(token,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _jwtTokenConfig.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(_secret),
                        ValidAudience = _jwtTokenConfig.Audience,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    },
                    out var validatedToken);
            return (principal, validatedToken as JwtSecurityToken);
        }


        /// <summary>
        /// Method for generating the referesh token
        /// </summary>
        /// <returns></returns>
        private static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
