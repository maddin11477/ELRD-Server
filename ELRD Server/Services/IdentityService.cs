using ELRDDataAccessLibrary.DataAccess;
using ELRDDataAccessLibrary.Models;
using ELRDServerAPI.Domain;
using ELRDServerAPI.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ELRDServerAPI.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly ELRDContext _db;
        private readonly ILogger<UserService> _logger;
        private readonly JwtSettings _jwtSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public IdentityService(ILogger<UserService> log, ELRDContext db, JwtSettings jwtsettings, TokenValidationParameters tokenValidationParameters)
        {
            _logger = log;
            _db = db;
            _jwtSettings = jwtsettings;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public async Task<AuthenticationResult> RegisterAsync(string email, string password, string username)
        {
            
            var existingUser = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
            if(existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User with this email address already exists" }
                };
            }

            var newUser = new User
            {
                Email = email,
                Username = username,
                Password = password
            };

            var createdUser = await _db.Users.AddAsync(newUser);
            await _db.SaveChangesAsync();
            if(createdUser == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Could not create user." }
                };
            }


            return await GenerateAuthenticationResultForUserAsync(newUser);
        }


        public async Task<AuthenticationResult> LoginAsync(string email, string password, string username)
        {
            User u; 

            //check whether login with username or email adresse
            if (String.IsNullOrEmpty(email))
            {
                //login with username
                u = await _db.Users.FirstOrDefaultAsync(x => x.Username == username);
            }
            else
            {
                //login with email adresse
                u = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
            }

            //check if user is in db, if not return error
            if (u == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist" }
                };
            }

            //check password, if not return error
            if(u.Password != password)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User/password combination is wrong" }
                };
            }

            //check if user is already active, if not return error
            if (!u.IsActive)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User waiting for activation" }
                };
            }

            //return token
            return await GenerateAuthenticationResultForUserAsync(u);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);
            if(validatedToken == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "Invalid Token" }
                };
            }

            var exiryDateUnix = 
                long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(exiryDateUnix);

            if(expiryDateTimeUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This Token hasn't expired yet" }
                };
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var storedRefreshToken = await _db.RefreshTokens.SingleOrDefaultAsync(x => x.Token.ToString() == refreshToken);

            if(storedRefreshToken == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This refresh token doesn't exist" }
                };
            }

            if(DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This token has expired" }
                };
            }

            if (storedRefreshToken.Invalidated)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This token has been invalidated" }
                };
            }

            if (storedRefreshToken.Used)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This token has been used" }
                };
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "This refresh token does not math this JWT" }
                };
            }

            storedRefreshToken.Used = true;
            _db.RefreshTokens.Update(storedRefreshToken);
            await _db.SaveChangesAsync();

            int userid = Convert.ToInt32(validatedToken.Claims.Single(x => x.Type == "id").Value);

            User u = await _db.Users.FirstOrDefaultAsync(x => x.Id == userid);
            return await GenerateAuthenticationResultForUserAsync(u);        
        }





        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }
                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase);
        }



        /// <summary>
        /// Create Token for given User
        /// </summary>
        /// <param name="newUser">User as an Opject</param>
        /// <returns>Authentication Result</returns>
        private async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(User newUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim("id", newUser.Id.ToString())
                }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = newUser.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await _db.RefreshTokens.AddAsync(refreshToken);
            await _db.SaveChangesAsync();

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token.ToString()
            };
        }


    }
}
